using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WallPrototype : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject m_wall;
    [SerializeField]
    private GameObject m_control;


    public bool IsWalled
    {
        get; private set;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsWalled = !IsWalled;
        m_wall.SetActive(IsWalled);
        m_control.SetActive(!IsWalled);
    }
}
