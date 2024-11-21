using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void StartGameButton() => SceneManager.LoadScene(_sceneName);
}   
