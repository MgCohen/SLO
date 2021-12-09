using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellPop : MonoBehaviour
{
    private Cell m_cell;

    private void Start()
    {
        m_cell = GetComponent<Cell>();
        Pop();
    }

    public void Pop()
    {
        Vector2Int index = m_cell.Index;
        int indexScale = Mathf.Max(index.x, index.y);
        float delay = (indexScale * 0.1f) + 0.5f;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).SetDelay(delay);
    }
}
