using UnityEngine;

public class CustomDebug
{
    public static void DrawDebugCircle(Vector3 center, float radius, Color color, int segments)
    {
        // Check for valid input
        if (radius <= 0f || segments <= 0) return;

        float angleStep = 360f / segments;
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float nextAngle = (i + 1) * angleStep;

            // Calculate start and end points using trigonometry
            lineStart.x = center.x + Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            lineStart.y = center.y + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            lineStart.z = center.z; // Set Z to a consistent value for 2D view

            lineEnd.x = center.x + Mathf.Cos(Mathf.Deg2Rad * nextAngle) * radius;
            lineEnd.y = center.y + Mathf.Sin(Mathf.Deg2Rad * nextAngle) * radius;
            lineEnd.z = center.z;

            // Draw a line between the points
            Debug.DrawLine(lineStart, lineEnd, color);
        }
    }
}
