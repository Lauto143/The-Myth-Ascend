using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireForce = 8f;
    public float fireCooldown = 1.5f;
    public float shootRange = 6f;

    [Header("Vida")]
    public float maxHealth = 3f;
    private float currentHealth;

    [Header("Movimiento (opcional)")]
    public float moveSpeed = 2f;
    public float patrolDistance = 2f;
    public float waitTime = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 startPoint;
    private bool movingRight = true;
    private float waitTimer;
    private float shootTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        shootTimer = 0f;
        waitTimer = 0f;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        shootTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.position);

        // Patrulla si el jugador no está en rango
        if (distance > shootRange)
            Patrol();
        else
            rb.linearVelocity = Vector2.zero;

        // Disparo si está en rango
        if (distance <= shootRange && shootTimer >= fireCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Patrol()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distanceFromStart = transform.position.x - startPoint.x;

        if (movingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, 0f);
            if (distanceFromStart >= patrolDistance)
            {
                movingRight = false;
                waitTimer = waitTime;
                FlipSprite();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, 0f);
            if (distanceFromStart <= -patrolDistance)
            {
                movingRight = true;
                waitTimer = waitTime;
                FlipSprite();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        if (player == null) return;

        Vector2 direction = (player.position - shootPoint.position).normalized;
        rbBullet.linearVelocity = direction * fireForce;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(bullet, 3f);
        Debug.Log(name + " disparó una bala");
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // --- SISTEMA DE VIDA ---
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
