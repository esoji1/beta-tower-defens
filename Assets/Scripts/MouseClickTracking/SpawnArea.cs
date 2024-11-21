using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private bool _isAvailable = true;

    public bool IsAvailable => _isAvailable;

    public bool CanSpawn() => _isAvailable;

    public void Occupy() => _isAvailable = false;
    public void NotOccupy() => _isAvailable = true;
}