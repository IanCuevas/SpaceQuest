using System;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float range;
    public float moveSpeed;
    private Rigidbody2D rb;
    public event Action OnArrive;
    public event Action<Vector2> OnVelocityChange;
    Vector3 newPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeMovement(Vector3 newPosition_)
    {
        StopAllCoroutines();
        newPosition = newPosition_;
        StartCoroutine(MoveToPosition());
    }

    public void SetNewPosition(Vector3 newPosition_) { newPosition = newPosition_; }

    public void StopMovement()
    {
        StopAllCoroutines();
        rb.linearVelocity = Vector3.zero;
    }
    private IEnumerator MoveToPosition()
    {
        bool inRange = false;

        while (!inRange)
        {
            Vector2 moveDir = newPosition - transform.position;
            moveDir.Normalize();
            rb.linearVelocity = moveDir * moveSpeed;
            OnVelocityChange?.Invoke(rb.linearVelocity);
            inRange = (Vector2.Distance(transform.position, newPosition) < range);

            if (inRange)
            {
                rb.linearVelocity = Vector3.zero;
            }
            yield return null;
        }
        OnArrive?.Invoke();

        yield return null;
    }
}
