using UnityEngine;
using Zenject;

public class PersistenceInstaller : MonoInstaller
{
    [SerializeField]
    private LocalBoardStorage m_localStorage;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BoardStorage>().AsSingle();
        Container.Bind<LocalBoardStorage>().FromInstance(m_localStorage).AsSingle();
    }
}