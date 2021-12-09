using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Local Board Storage")]
public class LocalBoardStorage : ScriptableObject
{
    [SerializeField]
    public List<BoardData> m_boards = new List<BoardData>();
    public List<BoardData> Boards => m_boards;

    public void SaveBoard(List<CellPrototype> cells, Vector2 offset, string tooltip)
    {
        List<CellData> cellData = new List<CellData>();
        foreach (CellPrototype prototype in cells)
        {
            cellData.Add(new CellData(prototype));
        }
        m_boards.Add(new BoardData(cellData, offset, tooltip));
    }
}
