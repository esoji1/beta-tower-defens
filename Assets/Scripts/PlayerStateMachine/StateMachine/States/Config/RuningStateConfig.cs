using System;
using UnityEngine;

[Serializable]
public class RuningStateConfig
{
    [field: SerializeField, Range(1, 30)] public float Speed { get; private set; }
}
