using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CellPrototype : MonoBehaviour
{
    [SerializeField]
    private WallPrototype[] m_walls = new WallPrototype[4];

    [SerializeField]
    private EntityPrototype m_entity;

    public EntityType Entity => m_entity.Entity;

    [Inject]
    public Vector2Int CellIndex
    {
        get; private set;
    }

    public List<int> CheckWalls()
    {
        List<int> walledPositions = new List<int>();
        for (int i = 0; i < m_walls.Length; i++)
        {
            if (m_walls[i].IsWalled)
            {
                walledPositions.Add(i);
            }
        }
        return walledPositions;
    }

    public class Factory : PlaceholderFactory<Vector2Int, CellPrototype>
    {

    }
}

[System.Serializable]
public class CellData
{
    public CellData(CellPrototype prototype)
    {
        m_index = prototype.CellIndex;
        m_walls = prototype.CheckWalls();
        m_entity = prototype.Entity;
    }

    public Vector2Int Index => m_index;

    [SerializeField]
    private Vector2Int m_index;

    public List<int> Walls => m_walls;

    [SerializeField]
    private List<int> m_walls;

    public EntityType Entity => m_entity;

    [SerializeField]
    private EntityType m_entity;
}
