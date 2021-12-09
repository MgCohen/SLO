using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ViewPop : MonoBehaviour
{
    private LevelView m_view;

    void Start()
    {
        m_view = GetComponent<LevelView>();
        Pop();
    }

    private void Pop()
    {
        int index = m_view.LevelIndex;
        float delay = 0.5f + (index * 0.3f);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetDelay(delay).SetEase(Ease.OutBack).OnComplete(Shake);
    }

    private void Shake()
    {
        transform.DOShakeRotation(1f, 10).SetLoops(-1);
    }




}
