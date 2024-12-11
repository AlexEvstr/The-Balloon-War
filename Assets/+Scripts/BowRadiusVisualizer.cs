using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BowRadiusVisualizer : MonoBehaviour
{
    public float radius = 10f; // Радиус круга.
    public int segments = 100; // Количество сегментов для круга.

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // Устанавливаем количество точек.
        lineRenderer.loop = true; // Замыкаем круг.
        DrawCircle();
    }

    private void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f) + transform.position);

            angle += 360f / segments;
        }
    }

    public void UpdateRadius(float newRadius)
    {
        radius = newRadius;
        DrawCircle(); // Перерисовываем круг при изменении радиуса.
    }
}
