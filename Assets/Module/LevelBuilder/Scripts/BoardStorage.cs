using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardStorage : BasePersistable<BoardStorage>
{
    [SerializeField]
    private List<BoardData> m_boards = new List<BoardData>();
    public List<BoardData> Boards => LoadBoards();

    [Inject]
    private LocalBoardStorage m_localStorage;

    public void SaveBoard(List<CellPrototype> cells, Vector2 offset, string tooltip)
    {
        List<CellData> cellData = new List<CellData>();
        foreach (CellPrototype prototype in cells)
        {
            cellData.Add(new CellData(prototype));
        }
        m_boards.Add(new BoardData(cellData, offset, tooltip));
    }

    public List<BoardData> LoadBoards()
    {
        List<BoardData> boards = new List<BoardData>();
        boards.AddRange(m_boards);
        boards.AddRange(m_localStorage.Boards);
        return boards;
    }

    public override void OnLoad()
    {

    }

    public override void BeforeSave()
    {

    }
}

[System.Serializable]
public class BoardData
{
    public BoardData(List<CellData> cells, Vector2 offset, string tooltip)
    {
        m_cells = cells;
        m_tooltip = tooltip;
        m_offset = offset;
    }

    public List<CellData> Cells => m_cells;

    [SerializeField]
    private List<CellData> m_cells;

    public Vector2 Offset => m_offset;

    [SerializeField]
    private Vector2 m_offset;

    public string Tooltip => m_tooltip;

    [SerializeField]
    private string m_tooltip;
}
