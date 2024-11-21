using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MageTowerRangeVisualizer : BaseTowerRangeVisualizer
{
    [Header("Settings")] 
    [SerializeField] private BaseTowerConfig _config;
    [SerializeField] private Color _radiusColor;
    [SerializeField] private Material _material;
    [SerializeField] private float _lineWidth;
    [SerializeField] private int _numberOfPoints;
    [SerializeField] private Transform _transformTower;
    
    protected override float Radius => _config.Radius;
    protected override Color RadiusColor => _radiusColor;
    protected override Material Material => _material;
    protected override float LineWidth => _lineWidth;
    protected override int NumberOfPoints => _numberOfPoints;
    protected override Transform TransformTower => _transformTower;
}