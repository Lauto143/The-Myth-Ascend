using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimiento y detección")]
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float stopDistance = 0.5f;

    [Header("Patrullaje")]
    public float patrolDistance = 3f;   // Qué tan lejos se mueve desde su punto inicial
    public float waitTime = 2f;         // Tiempo de espera al llegar al borde del patrullaje

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 startPoint;
    private bool movingRight = true;
    private float waitTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        waitTimer = 0f;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del radio de detección → perseguir
        if (distance < detectionRadius)
        {
            ChasePlayer(distance);
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer(float distance)
    {
        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Patrol()
    {
        // Si está esperando, no se mueve
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distanceFromStart = transform.position.x - startPoint.x;

        // Moverse hacia la derecha
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, 0f);
            if (distanceFromStart >= patrolDistance)
            {
                movingRight = false;
                waitTimer = waitTime;
                FlipSprite();
            }
        }
        // Moverse hacia la izquierda
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0f);
            if (distanceFromStart <= -patrolDistance)
            {
                movingRight = true;
                waitTimer = waitTime;
                FlipSprite();
            }
        }
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.left * patrolDistance,
                        transform.position + Vector3.right * patrolDistance);
    }
}
