using UnityEngine;

namespace Assets.Scripts.EmenyWaveSpawner
{
    public interface IMovable
    {
        Transform Transform { get; }
        float Speed { get; }
    }
}