using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardNavigation
{
    [Inject]
    private Board m_board;

    public bool CheckCellAt(Vector2Int index)
    {
        return m_board.GameBoard.ContainsKey(index);
    }

    public Cell GetCellAt(Vector2Int index)
    {
        m_board.GameBoard.TryGetValue(index, out Cell cell);
        return cell;
    }

    public Cell GetCellAt(Cell source, Vector2Int direction)
    {
        return GetCellAt(source.Index + direction);
    }

    public bool CheckCellAt(Cell source, Vector2Int direction)
    {
        Vector2Int index = source.Index + direction;
        return CheckCellAt(index);
    }

    public bool CheckPassage(Cell source, Cell target)
    {
        Vector2Int initialIndex = source.Index;
        Vector2Int targetIndex = target.Index;
        Vector2Int direction = targetIndex - initialIndex;

        return source.IsPassable(direction) && target.IsPassable(direction * -1);
    }

    public bool CheckPassage(Cell source, Vector2Int direction)
    {
        if(!CheckCellAt(source, direction))
        {
            return false;
        }

        Cell targetCell = GetCellAt(source, direction);
        return CheckPassage(source, targetCell);
    }
}
