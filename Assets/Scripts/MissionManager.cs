using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public void LoadMission(int missionNumber)
    {
        Debug.Log($"Вы запускаете миссию #{missionNumber}");
    }
}