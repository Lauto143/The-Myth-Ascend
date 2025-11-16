using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento y detección")]
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float stopDistance = 0.5f;

    [Header("Vida")]
    public float maxHealth = 3f;
    private float currentHealth;

    [Header("Ataque")]
    public float damage = 1f;

    [Header("Patrullaje")]
    public float patrolDistance = 3f;
    public float waitTime = 2f;

    [Header("Referencias")]
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private Vector2 startPoint;
    private bool movingRight = true;
    private float waitTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        startPoint = transform.position;
        waitTimer = 0f;
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Si el jugador está cerca → perseguir
        if (distance < detectionRadius)
        {
            ChasePlayer(distance);
        }
        else
        {
            Patrol();
        }

        // Actualiza animaciones
        anim.SetBool("IsWalking", rb.linearVelocity.magnitude > 0.1f);
    }

    void ChasePlayer(float distance)
    {
        // Mirar hacia donde está el jugador
        sr.flipX = player.position.x < transform.position.x;

        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
            anim.SetBool("IsAttacking", false);
        }
        else
        {
            // Está en rango de ataque → no moverse y atacar
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsAttacking", true);
        }
    }

    void Patrol()
    {
        anim.SetBool("IsAttacking", false);

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distanceFromStart = transform.position.x - startPoint.x;

        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, 0f);
            sr.flipX = false;

            if (distanceFromStart >= patrolDistance)
            {
                movingRight = false;
                waitTimer = waitTime;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0f);
            sr.flipX = true;

            if (distanceFromStart <= -patrolDistance)
            {
                movingRight = true;
                waitTimer = waitTime;
            }
        }
    }

    // --- COLISIÓN PARA ATAQUE ---
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            anim.SetBool("IsAttacking", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    // --- VIDA ---
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
