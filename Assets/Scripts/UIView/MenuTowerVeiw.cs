using UnityEngine;

public class MenuTowerVeiw
{
    private GameObject _buttonTower;
    private GameObject _buttonTowerPumping;

    public MenuTowerVeiw(GameObject buttonTower, GameObject buttonTowerPumping)
    {
        _buttonTower = buttonTower;
        _buttonTowerPumping = buttonTowerPumping;
    }

    public void OffTowerBuildingMenu()
    {
        _buttonTower.SetActive(false);
    }

    public void ShowTowerBuildingMenu(Vector3 targetPosition)
    {
        _buttonTower.SetActive(true);
        _buttonTower.transform.position = targetPosition;
    }

    public void OffTowerPumpingMenu()
    {
        _buttonTowerPumping.SetActive(false);
    }

    public void ShowTowerPumpingMenu(Vector3 targetPosition)
    {
        _buttonTowerPumping.SetActive(true);
        _buttonTowerPumping.transform.position = targetPosition;
    }
}