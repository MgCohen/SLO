using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{

    [SerializeField]
    private Hero m_heroPrefab;
    [SerializeField]
    private Monster m_monsterPrefab;

    public override void InstallBindings()
    {
        Container.Bind<TurnController>().AsSingle();
        Container.Bind<TurnLog>().AsSingle();

        Container.BindFactory<Cell, Hero, Hero.Factory>().FromComponentInNewPrefab(m_heroPrefab);
        Container.BindFactory<Cell, Monster, Monster.Factory>().FromComponentInNewPrefab(m_monsterPrefab);
    }
}