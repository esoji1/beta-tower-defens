using UnityEngine;

public class OpenCloseWindows : MonoBehaviour
{
    [SerializeField] private GameObject _windows;

    public void OpenWindow() => _windows.SetActive(true);
    public void CloseWindow() => _windows.SetActive(false);
}