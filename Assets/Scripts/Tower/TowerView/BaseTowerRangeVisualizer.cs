using UnityEngine;

public abstract class BaseTowerRangeVisualizer : MonoBehaviour
{
    protected abstract float Radius { get; }
    protected abstract Color RadiusColor { get; }
    protected abstract Material Material { get; }
    protected abstract float LineWidth { get; }
    protected abstract int NumberOfPoints { get; }
    protected abstract Transform TransformTower { get; }

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = Material;
        lineRenderer.startWidth = LineWidth;
        lineRenderer.endWidth = LineWidth;
        lineRenderer.positionCount = NumberOfPoints + 1;

        Material.color = RadiusColor;
    }

    private void Update()
    {
        DrawRadius();
    }

    private void DrawRadius()
    {
        float step = 360f / NumberOfPoints;

        for (int i = 0; i < NumberOfPoints; i++)
        {
            float angle = step * i * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * Radius;
            float y = Mathf.Sin(angle) * Radius;
            Vector3 point = new Vector3(x, y, 0f);
            lineRenderer.SetPosition(i, TransformTower.position + point);
        }

        lineRenderer.SetPosition(NumberOfPoints, lineRenderer.GetPosition(0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = RadiusColor;
        Gizmos.DrawWireSphere(TransformTower.position, Radius);
    }
}