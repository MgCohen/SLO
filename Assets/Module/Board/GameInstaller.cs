using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private Cell m_cellPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.Bind<Board>().AsSingle();
        Container.Bind<BoardNavigation>().AsSingle();
        Container.DeclareSignal<OnBoardEndSignal>().OptionalSubscriber();
        Container.DeclareSignal<OnBoardResetSignal>().OptionalSubscriber();

        Container.BindFactory<Vector2Int, List<int>, Vector2, Cell, Cell.Factory>().FromComponentInNewPrefab(m_cellPrefab);
    }
}