using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LevelView : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    public int LevelIndex
    {
        get; private set;
    }

    [Inject]
    private LevelSelector m_selector;

    [SerializeField]
    private TextMeshProUGUI m_text;

    private bool m_picked = false;

    private void Start()
    {
        m_text.text = LevelIndex.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_picked)
        {
            Unpick();
            return;
        }

        Pick();
    }

    public void Pick()
    {
        m_picked = true;
        m_selector.Pick(this);
        transform.DOScale(1.2f, 0.7f).SetEase(Ease.OutBack);
    }

    public void Unpick()
    {
        m_picked = false;
        m_selector.Unpick(this);
        transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
    }

    public class Factory : PlaceholderFactory<Transform, int, LevelView>
    {
        [Inject]
        private LevelView view;
        [Inject]
        private DiContainer Container;

        public override LevelView Create(Transform param1, int param2)
        {
           return Container.InstantiatePrefabForComponent<LevelView>(view, param1, new List<object>() { param2 });
        }
    }
}
