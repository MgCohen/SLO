using UnityEngine;
using Zenject;

public class BoardBuilderInstaller : MonoInstaller
{
    [SerializeField]
    private CellPrototype m_cellPrototype;



    public override void InstallBindings()
    {
        //Container.Bind<CellPrototype>().FromComponentInNewPrefab(m_cellPrototype).AsSingle();
        //Container.Bind<WallPrototype>().FromComponentInNewPrefab(m_wallPrototype).AsSingle();

        Container.Bind<BoardBuilder>().AsSingle();

        Container.BindFactory<Vector2Int, CellPrototype, CellPrototype.Factory>().FromComponentInNewPrefab(m_cellPrototype);
    }
}