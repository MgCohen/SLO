using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BoardBuilder
{

    [Inject]
    private CellPrototype.Factory m_cellFactory;

    private List<CellPrototype> m_cells = new List<CellPrototype>();

    public List<CellPrototype> Cells => m_cells;

    public void BuildBoard(Vector2Int boardSize)
    {
        m_cellFactory.Create(boardSize);

        Vector2Int offset = boardSize / 2;

        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                Vector2Int index = new Vector2Int(i, j);
                Vector2 position = index - offset;
                CellPrototype cell = m_cellFactory.Create(index);
                cell.transform.position = position;
                m_cells.Add(cell);
            }
        }
    }

    public void ClearBoard()
    {

    }


    public void SaveBoard()
    {

    }

    public void LoadBoard()
    {

    }
}



