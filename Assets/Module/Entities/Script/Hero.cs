using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Hero : Entity
{
    [Inject]
    private TurnLog m_log;
    [Inject]
    private SignalBus m_signals;

    private bool m_ready = false;

    [Inject]
    private void Init(Cell startingCell)
    {
        m_turns.RegisterOnTeam("Hero", this);
        ChangeCell(startingCell);
        transform.position = startingCell.transform.position;
        m_signals.Subscribe<OnBoardEndSignal>(PauseInput);
    }

    private void PauseInput()
    {
        m_ready = false;
    }

    private void Update()
    {
        if (m_ready)
        {
            if (Input.GetKey(KeyCode.A))
            {
                TryMove(Vector2Int.left);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                TryMove(Vector2Int.down);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                TryMove(Vector2Int.right);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                TryMove(Vector2Int.up);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                EndTurn();
            }

        }

    }

    public override void StartTurn()
    {
        m_ready = true;

    }

    public override void EndTurn()
    {
        m_ready = false;
        m_turns.PassTurn();
    }

    public override void BeforeMove()
    {
        m_log.Log();
        m_ready = false;
    }

    public override void AfterMove()
    {
        if (CheckEscape())
        {
            m_signals.Fire(new OnBoardEndSignal(false));
            return;
        }
        EndTurn();
    }

    private bool CheckEscape()
    {
        return CurrentCell.IsEscape;
    }

    public class Factory : PlaceholderFactory<Cell, Hero>
    {

    }
}
