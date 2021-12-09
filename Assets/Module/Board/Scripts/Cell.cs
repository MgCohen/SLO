using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_walls = new GameObject[4];
    [SerializeField]
    private GameObject m_escapeSign;
    private List<Vector2Int> m_directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public Vector2Int Index
    {
        get; private set;
    }

    public bool IsEscape
    {
        get; private set;
    }

    public List<Entity> Entities => m_entities;
    private List<Entity> m_entities = new List<Entity>();

    [Inject]
    private void Init(Vector2Int index, List<int> walls, Vector2 position)
    {
        Index = index;
        transform.position = position;
        foreach (int wallId in walls)
        {
            Vector2Int direction = m_directions[wallId];
            GetWall(direction).SetActive(true);
        }
    }

    public GameObject GetWall(Vector2Int direction)
    {
        int index = m_directions.IndexOf(direction);
        if (index > -1)
        {
            return m_walls[index];
        }
        return null;
    }

    public bool IsPassable(Vector2Int direction)
    {
        return !GetWall(direction).activeInHierarchy;
    }

    public void AddEntity(Entity entity)
    {
        m_entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        m_entities.Remove(entity);
    }

    public void SetEscape()
    {
        IsEscape = true;
        m_escapeSign.SetActive(true);
    }

    public class Factory : PlaceholderFactory<Vector2Int, List<int>, Vector2, Cell>
    {

    }
}
