using UnityEngine;

public abstract class BaseTowerConfig : ScriptableObject
{
    public abstract float Radius { get; }
    public abstract float TimeBetweenShots { get; }
    public abstract GameObject ProjectilePrefab { get; }
}