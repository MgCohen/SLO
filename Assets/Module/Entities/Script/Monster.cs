using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Monster : Entity
{
    [Inject]
    private Board m_board;
    [Inject]
    private SignalBus m_signals;
    public Hero Target => m_board.Hero;

    private int m_movesLeft = 0;

    [Inject]
    private void Init(Cell startingCell)
    {
        m_turns.RegisterOnTeam("Monster", this);
        ChangeCell(startingCell);
        transform.position = startingCell.transform.position;
    }

    public override void EndTurn()
    {
        m_turns.PassTurn();
    }

    public override void StartTurn()
    {
        m_movesLeft = 2;
        EvaluateMove();
    }


    public void EvaluateMove()
    {
        Vector2Int targetIndex = Target.Index;
        Vector2Int currentIndex = Index;
        Vector2Int distance = targetIndex - currentIndex;
        if (distance.x != 0)
        {
            Debug.Log(distance);
            Vector2Int normalizedDistance = new Vector2Int(distance.x / Mathf.Abs(distance.x), 0);
            if (CheckMove(normalizedDistance))
            {
                Move(normalizedDistance);
                return;
            }
        }
        if (distance.y != 0)
        {
            Vector2Int normalizeDistance = new Vector2Int(0, distance.y / Mathf.Abs(distance.y));
            if (CheckMove(normalizeDistance))
            {
                Move(normalizeDistance);
                return;
            }
        }

        EndTurn();
    }

    public void CheckKill()
    {
        if (CurrentCell.Entities.Contains(Target))
        {
            Target.gameObject.SetActive(false);
            m_signals.Fire(new OnBoardEndSignal(true));
        }
    }

    public override void BeforeMove()
    {
    }

    public override void AfterMove()
    {
        m_movesLeft--;
        CheckKill();
        if(m_movesLeft <= 0)
        {
            EndTurn();
            return;
        }
        EvaluateMove();
    }

    public class Factory: PlaceholderFactory<Cell, Monster>
    {

    }
}
