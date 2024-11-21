using UnityEngine;
using Zenject;

public class ClickOnSpawnPointInstaller : MonoInstaller
{
    [SerializeField] private GameObject _buttonTower;
    [SerializeField] private GameObject _buttonTowerPumping;
    [SerializeField] private Player _player;        
    [SerializeField] private RemoveTower _removeTower;
    [SerializeField] private RadiusSpawnPoint _spawnRadius;

    public override void InstallBindings()
    {
        Container.Bind<MenuTowerVeiw>().AsSingle().WithArguments(_buttonTower, _buttonTowerPumping);
        BindClickOnSpawnPoint();
    }

    public void BindClickOnSpawnPoint()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle();
        Container.Bind<RemoveTower>().FromInstance(_removeTower).AsSingle();
        Container.Bind<RadiusSpawnPoint>().FromInstance(_spawnRadius).AsSingle();

        Container.BindInterfacesAndSelfTo<ClickOnSpawnPoint>().FromComponentInHierarchy().AsSingle();
    }
}