using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonsTowerSpawningInstaller : MonoInstaller
{
    [SerializeField] private GameObject _prefabMeleeTower;
    [SerializeField] private GameObject _prefabMageTower;
    [SerializeField] private GameObject _prefabDistatnceTower;
    [SerializeField] private Button _buttonMiddleTower;
    [SerializeField] private Button _buttonDistantTower;
    [SerializeField] private Button _buttonMeleeTower;

    public override void InstallBindings()
    {
        BindButtonsTowerSpawning();

        Container.Bind<Button>().WithId("MiddleTowerButton").FromInstance(_buttonMiddleTower).AsTransient();
        Container.Bind<Button>().WithId("DistantTowerButton").FromInstance(_buttonDistantTower).AsTransient();
        Container.Bind<Button>().WithId("MeleeTowerButton").FromInstance(_buttonMeleeTower).AsTransient();

        Container.Bind<ButtonsTowerSpawning>().FromComponentInHierarchy().AsSingle();
    }

    public void BindButtonsTowerSpawning()
    {
        Container.Bind<MeleeTowerPrefab>().FromInstance(new MeleeTowerPrefab(_prefabMeleeTower)).AsSingle();
        Container.Bind<MageTowerPrefab>().FromInstance(new MageTowerPrefab(_prefabMageTower)).AsSingle();
        Container.Bind<DistanceTowerPrefab>().FromInstance(new DistanceTowerPrefab(_prefabDistatnceTower)).AsSingle();
    }
}