using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class GameController : MonoBehaviour
{
    [Inject]
    private Board m_setter;
    [Inject]
    private TurnLog m_log;
    [Inject]
    private TurnController m_turns;
    [Inject]
    private SignalBus m_signals;

    [SerializeField]
    private GameObject m_loseControllers;
    [SerializeField]
    private GameObject m_winControllers;
    [SerializeField]
    private TextMeshProUGUI m_tooltip;

    // Start is called before the first frame update
    void Start()
    {
        m_setter.SpawnBoard();
        m_turns.StartTurns("Hero");
        m_signals.Subscribe<OnBoardEndSignal>(EndGame);
        m_signals.Subscribe<OnBoardResetSignal>(ResetControls);
        Camera.main.orthographicSize = m_setter.BoardSize.y;
        SetTooltip();
    }

    private void ResetControls()
    {
        SetTooltip();
        m_winControllers.SetActive(false);
        m_loseControllers.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Undo();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextBoard();
        }
    }

    private void EndGame(OnBoardEndSignal signal)
    {
        if (signal.WasPlayerKilled)
        {
            m_loseControllers.SetActive(true);
            return;
        }

        m_winControllers.SetActive(true);
    }


    public void NextBoard()
    {
        m_signals.Fire<OnBoardResetSignal>();
        m_setter.GetNextBoard();
        Camera.main.orthographicSize = m_setter.BoardSize.y;
        m_turns.StartTurns("Hero");
        m_log.ResetLogs();
    }

    public void Retry()
    {
        m_signals.Fire<OnBoardResetSignal>();
        DOTween.KillAll();
        m_setter.SpawnBoard();
        m_turns.StartTurns("Hero");
        m_log.ResetLogs();
    }

    public void Undo()
    {
        var log = m_log.GetLastLog();
        if (log == null)
        {
            return;
        }

        m_log.RemoveLastLog();
        DOTween.KillAll();
        m_turns.SetForcedReset(true);
        foreach(var entry in log)
        {
            entry.Key.EndTurn();
            entry.Key.ChangeCell(entry.Value);
            entry.Key.transform.position = entry.Value.transform.position;
        }
        m_turns.SetForcedReset(false);
        m_turns.StartTurns("Hero");
    }

    public void Wait()
    {
        m_setter.Hero.EndTurn();
    }

    private void SetTooltip()
    {
        m_tooltip.text = m_setter.GetBoardTooltip();
    }
}
