using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnLog
{
    [Inject]
    private Board m_board;

    public List<Dictionary<Entity, Cell>> m_log = new List<Dictionary<Entity, Cell>>();

    public void Log()
    {
        Dictionary<Entity, Cell> entry = new Dictionary<Entity, Cell>();
        foreach(Cell cell in m_board.GameBoard.Values)
        {
            foreach(Entity entity in cell.Entities)
            {
                entry.Add(entity, cell);
            }
        }
        m_log.Add(entry);
    }

    public Dictionary<Entity, Cell> GetLastLog()
    {
        if (m_log.Count <= 0)
        {
            return null;
        }
        return m_log[m_log.Count - 1];
    }

    public void RemoveLastLog()
    {
        m_log.RemoveAt(m_log.Count - 1);
    }

    public void ResetLogs()
    {
        m_log.Clear();
    }
}
