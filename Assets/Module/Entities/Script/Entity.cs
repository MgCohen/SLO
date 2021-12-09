using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public abstract class Entity : MonoBehaviour
{
    [Inject]
    protected TurnController m_turns;
    [Inject]
    protected BoardNavigation m_navigation;


    public Cell CurrentCell
    {
        get; private set;
    }

    public Vector2Int Index => CurrentCell.Index;

    public void ChangeCell(Cell newCell)
    {
        CurrentCell?.RemoveEntity(this);
        newCell.AddEntity(this);
        CurrentCell = newCell;
    }

    protected bool CheckMove(Vector2Int direction)
    {
        return m_navigation.CheckPassage(CurrentCell, direction);
    }

    protected void TryMove(Vector2Int direction)
    {
        if (CheckMove(direction))
        {
            Move(direction);
        }
    }

    protected void Move(Vector2Int direction)
    {
        BeforeMove();
        Cell targetCell = m_navigation.GetCellAt(CurrentCell, direction);
        ChangeCell(targetCell);
        transform.DOMove(targetCell.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(AfterMove);
    }

    public abstract void BeforeMove();

    public abstract void AfterMove();

    public abstract void StartTurn();

    public abstract void EndTurn();
}
