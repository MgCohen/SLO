using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;


public class BuilderController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField m_XInput;
    [SerializeField]
    private TMP_InputField m_YInput;
    [SerializeField]
    private TMP_InputField m_tooltipInput;

    [Inject]
    private BoardBuilder m_boardBuilder;
    [Inject]
    private BoardStorage m_boardStorage;
    [Inject]
    private LocalBoardStorage m_localStorage;

    private Vector2Int boardSize;

    public void NewBoard()
    {
        int.TryParse(m_XInput.text, out int x);
        int.TryParse(m_YInput.text, out int y);
        boardSize = new Vector2Int(x, y);
        m_boardBuilder.BuildBoard(boardSize);
        Camera.main.orthographicSize = y;
    }

    public void SaveBoard()
    {
        Vector2 offset = new Vector2(boardSize.x / 2f, boardSize.y / 2f);
        m_boardStorage.SaveBoard(m_boardBuilder.Cells, offset, m_tooltipInput.text);
    }

    public void LocalSaveBoard()
    {
        Vector2 offset = new Vector2(boardSize.x / 2f, boardSize.y / 2f);
        m_localStorage.SaveBoard(m_boardBuilder.Cells, offset, m_tooltipInput.text);
    }

    //TODO
    //Remove Board
    //override board


}
