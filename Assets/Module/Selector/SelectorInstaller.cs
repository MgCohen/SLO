using UnityEngine;
using Zenject;

public class SelectorInstaller : MonoInstaller
{
    [SerializeField]
    private LevelSelector m_selector;
    [SerializeField]
    private LevelView m_viewPrefab;

    

    public override void InstallBindings()
    {
            Container.Bind<LevelSelector>().FromInstance(m_selector).AsSingle();
            Container.Bind<LevelView>().FromInstance(m_viewPrefab).AsSingle();

            Container.BindFactory<Transform, int, LevelView, LevelView.Factory>().FromComponentInNewPrefab(m_viewPrefab);

    }
}