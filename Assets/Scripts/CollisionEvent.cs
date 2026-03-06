using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    public string tagToCheck;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != tagToCheck) { return; }

        OnTriggerEnter?.Invoke();
    }
}
