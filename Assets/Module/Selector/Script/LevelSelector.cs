using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{
    [Inject]
    private BoardStorage m_storage;
    [Inject]
    private LevelView.Factory m_viewFactory;
    [Inject]
    private LevelSelection m_selection;

    [SerializeField]
    private Transform viewHolder;

    private LevelView m_selectedView;



    public bool HasBoardSelected => m_selectedView != null;

    private void Start()
    {
        foreach(BoardData data in m_storage.Boards)
        {
            LevelView view = m_viewFactory.Create(viewHolder,m_storage.Boards.IndexOf(data));
        }
    }

    public void Pick(LevelView view)
    {
        if (m_selectedView)
        {
            m_selectedView.Unpick();
        }
        m_selection.SetSelectedIndex(view.LevelIndex);
        m_selectedView = view;
    }

    public void Unpick(LevelView view)
    {
        if(m_selectedView == view)
        {
            m_selectedView = null;
            m_selection.SetSelectedIndex(-1);
        }
    }

    public void StartLevel()
    {
        if(m_selection.SelectedBoardIndex == -1)
        {
            return;
        }

        DOTween.KillAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

}
