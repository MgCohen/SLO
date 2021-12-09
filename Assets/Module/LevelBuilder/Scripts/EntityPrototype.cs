using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EntityType
{
    None,
    Hero,
    Monster,
    Exit,
}

public class EntityPrototype : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject m_hero;
    [SerializeField]
    private GameObject m_monster;
    [SerializeField]
    private GameObject m_exit;

    public EntityType Entity
    {
        get; private set;
    }

    private void Start()
    {
        SetNone();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void Toggle()
    {
        switch (Entity)
        {
            case EntityType.Exit:
                SetNone();
                break;
            case EntityType.Hero:
                SetExit();
                break;
            case EntityType.Monster:
                SetHero();
                break;
            case EntityType.None:
                SetMonster();
                break;
            default:
                break;
        }
    }

    private void SetNone()
    {
        Entity = EntityType.None;
        m_hero.SetActive(false);
        m_monster.SetActive(false);
        m_exit.SetActive(false);
    }

    private void SetHero()
    {
        Entity = EntityType.Hero;
        m_hero.SetActive(true);
        m_monster.SetActive(false);
        m_exit.SetActive(false);
    }

    private void SetMonster()
    {
        Entity = EntityType.Monster;
        m_hero.SetActive(false);
        m_monster.SetActive(true);
        m_exit.SetActive(false);
    }

    private void SetExit()
    {
        Entity = EntityType.Exit;
        m_hero.SetActive(false);
        m_monster.SetActive(false);
        m_exit.SetActive(true);
    }
}
