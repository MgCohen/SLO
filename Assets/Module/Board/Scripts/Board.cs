using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Board
{
    [Inject]
    private BoardStorage m_storage;
    [Inject]
    private LevelSelection m_selection;
    [Inject]
    private Cell.Factory m_cellFactory;
    [Inject]
    private Hero.Factory m_heroFactory;
    [Inject]
    private Monster.Factory m_monsterFactory;

    private Dictionary<Vector2Int, Cell> m_board = new Dictionary<Vector2Int, Cell>();

    private Vector2 boardSize;

    public Hero Hero
    {
        get; private set;
    }

    private string m_tooltip;

    public Dictionary<Vector2Int, Cell> GameBoard => m_board;

    public Vector2 BoardSize => boardSize;

    public BoardData GetBoard()
    {
        return m_storage.Boards[m_selection.SelectedBoardIndex];
    }

    public void GetNextBoard()
    {
        m_selection.SetNextIndex();
        if(m_selection.SelectedBoardIndex >= m_storage.Boards.Count)
        {
            return;
        }
        SpawnBoard();
    }

    public string GetBoardTooltip()
    {
        return m_tooltip;
    }

    public void SpawnBoard()
    {
        ClearBoard();
        BoardData boardData = GetBoard();
        m_tooltip = boardData.Tooltip;
        boardSize = boardData.Offset * 2;
        foreach (CellData cellData in boardData.Cells)
        {
            Cell cell = m_cellFactory.Create(cellData.Index, cellData.Walls, cellData.Index - boardData.Offset);
            if (cellData.Entity != EntityType.None)
            {
                SpawnEntity(cellData.Entity, cell);
            }
            m_board.Add(cellData.Index, cell);
        }
    }

    private Entity SpawnEntity(EntityType type, Cell cell)
    {
        switch (type)
        {
            case EntityType.None:
                break;
            case EntityType.Hero:
                Hero = m_heroFactory.Create(cell);
                return Hero;
            case EntityType.Monster:
                return m_monsterFactory.Create(cell);
            case EntityType.Exit:
                cell.SetEscape();
                break;
            default:
                break;
        }
        return null;
    }

    public void ClearBoard()
    {
        foreach(Cell cell in m_board.Values)
        {
            foreach(Entity entity in cell.Entities)
            {
                GameObject.Destroy(entity.gameObject);
            }
            GameObject.Destroy(cell.gameObject);
        }

        m_board.Clear();
    }

}
