using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection
{
    public int SelectedBoardIndex => m_selectedBoardIndex;
    private int m_selectedBoardIndex = -1;

    public void SetNextIndex()
    {
        m_selectedBoardIndex++;
    }

    public void SetSelectedIndex(int index)
    {
        m_selectedBoardIndex = index;
        Debug.Log(m_selectedBoardIndex);
    }

}
