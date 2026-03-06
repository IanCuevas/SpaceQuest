using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerMovement : MonoBehaviour
{
    public InputAction moveInput;
    private Vector2 movementDirection = Vector2.zero;
    public float moveSpeed = 1.0f;
    public event Action<Vector2> OnMove;
    private Rigidbody2D rb;
    void Awake()
    {
        moveInput.Enable();
        moveInput.performed += GetMoveVector;
        moveInput.canceled += GetMoveVector;
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetMoveVector(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        OnMove?.Invoke(movementDirection);

    }

    public void Update()
    {
        // transform.position += new Vector3(movementDirection.x, movementDirection.y, 0) * moveSpeed * Time.deltaTime;
        rb.linearVelocity = movementDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
