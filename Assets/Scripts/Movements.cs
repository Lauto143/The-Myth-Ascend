using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    private PlayerInput PI;
    private Vector2 direction;
    private Vector2 lastDirection; // 👈 Guarda la última dirección de movimiento
    private Animator animator;
    public Transform Aim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PI = GetComponent<PlayerInput>();
        Debug.Log("Rigidbody2D component found: " + (rb != null));
    }

    void Update()
    {
        MovePlayer();
        UpdateAnimation();
    }

    void MovePlayer()
    {
        direction = PI.actions["Move"].ReadValue<Vector2>();

        rb.linearVelocity = direction * speed;

        // 👇 Si hay movimiento, guardamos la dirección actual como la última dirección
        if (direction != Vector2.zero)
        {
            lastDirection = direction;
        }
    }

    void UpdateAnimation()
    {
        bool IsWalking = direction != Vector2.zero;
        animator.SetBool("IsWalking", IsWalking);

        // 👇 Si se está moviendo, usamos la dirección actual
        // 👇 Si no, mantenemos la última dirección
        Vector2 facingDirection = (direction != Vector2.zero) ? direction : lastDirection;

        animator.SetFloat("InputX", facingDirection.x);
        animator.SetFloat("InputY", facingDirection.y);

        Aim.right = facingDirection;

        if (facingDirection != Vector2.zero)
        {
            Aim.up = facingDirection;
        }
    }
}
