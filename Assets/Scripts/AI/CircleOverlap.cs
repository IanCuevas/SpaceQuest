using System;
using UnityEngine;

public class CircleOverlap : MonoBehaviour
{
    public float radius;
    public string tagToCheck;
    public Color color;

    public event Action<Vector2> OnOverlap;
    private void Update()
    {
        CustomDebug.DrawDebugCircle(transform.position, radius, color, 50);
    }

    public bool CircleOverlapCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag(tagToCheck))
            {
                OnOverlap?.Invoke(hit.gameObject.transform.position);
                return true;
            }
        }
        return false;
    }
}
