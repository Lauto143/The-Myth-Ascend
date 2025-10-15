using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    private PlayerInput PI;
    private Vector2 direction;
    private Animator animator;

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

       if (PI.actions["Attack"].triggered)
        {
            Debug.Log("Jump action triggered");
        }
    
    }

   void UpdateAnimation()
    {

        bool isWalking = direction != Vector2.zero;
        animator.SetBool("IsWalking", isWalking);
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);

    }

}
