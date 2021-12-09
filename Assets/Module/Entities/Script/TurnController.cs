using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnController
{
    private Dictionary<string, List<Entity>> m_teams = new Dictionary<string, List<Entity>>();

    private Queue<string> m_turns = new Queue<string>();

    private int m_activeEntities = 0;
    private bool m_forcedReset = false;

    [Inject]
    private SignalBus m_signals;

    [Inject]
    private void Init()
    {
        m_signals.Subscribe<OnBoardResetSignal>(ResetTurns);
    }

    private void ResetTurns()
    {
        m_teams.Clear();
        m_turns.Clear();
    }

    public void StartTurns(string startingTeam)
    {
        string team = CycleTurns(startingTeam);
        List<Entity> entities = m_teams[team];
        m_activeEntities = entities.Count;

        if (m_activeEntities <= 0)
        {
            NextTurn();
            return;
        }

        foreach (Entity entity in entities)
        {
            entity.StartTurn();
        }
    }

    public string CycleTurns(string wantedTeam)
    {
        string team = m_turns.Dequeue();
        m_turns.Enqueue(team);
        while (team != wantedTeam)
        {
            team = m_turns.Dequeue();
            m_turns.Enqueue(team);
        }

        return team;
    }

    public void NextTurn()
    {
        string newTurn = m_turns.Dequeue();
        m_turns.Enqueue(newTurn);
        List<Entity> team = m_teams[newTurn];
        m_activeEntities = team.Count;
        
        if (m_activeEntities <= 0)
        {
            NextTurn();
            return;
        }

        foreach (Entity entity in team)
        {
            entity.StartTurn();
        }
    }

    public void PassTurn()
    {
        m_activeEntities--;
        if(m_activeEntities <= 0 && !m_forcedReset)
        {
            NextTurn();
        }
    }

    public void SetForcedReset(bool status)
    {
        m_forcedReset = status;
    }

    public void RegisterOnTeam(string team, Entity entity)
    {
        if (!m_teams.ContainsKey(team))
        {
            m_teams.Add(team, new List<Entity>());
            m_turns.Enqueue(team);
        }

        m_teams[team].Add(entity);
    }
}
