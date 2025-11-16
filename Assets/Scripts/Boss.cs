using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;

    [Header("Movimiento y detección por distancia")]
    public float speed = 2f;

    public float chaseRadius = 6f;     // Radio para perseguir
    public float shootRadius = 4f;     // Radio para disparar
    public float meleeRadius = 1.3f;   // Radio para ataque cuerpo a cuerpo

    private Rigidbody2D rb;

    [Header("Ataque cuerpo a cuerpo")]
    public float meleeCooldown = 1f;
    private float meleeTimer = 0;

    [Header("Ataque a distancia")]
    public float shootCooldown = 1.5f;
    private float shootTimer = 0;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletForce = 8f;

    private Animator anim;

    public bool inShootZone = false;
    public bool inMeleeZone = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        meleeTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // ⭐ PRIORIDAD 1 - Melee
        if (distance <= meleeRadius)
        {
            rb.linearVelocity = Vector2.zero;
            TryMeleeAttack();
            return;
        }

        // ⭐ PRIORIDAD 2 - Disparo
        if (distance <= shootRadius)
        {
            rb.linearVelocity = Vector2.zero;
            TryShootAttack();
            return;
        }

        // ⭐ PRIORIDAD 3 - Persecución
        if (distance <= chaseRadius)
        {
            ChasePlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsMoving", false);
        }
    }

    void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        anim.SetBool("IsMoving", true);
        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
    }

    void TryMeleeAttack()
    {
        if (meleeTimer >= meleeCooldown)
        {
            meleeTimer = 0;
            anim.SetTrigger("Ataque");
        }
    }

    void TryShootAttack()
    {
        if (shootTimer >= shootCooldown)
        {
            shootTimer = 0;
            anim.SetTrigger("Disparo");
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = (player.position - shootPoint.position).normalized;
        b.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletForce;

        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0, 0, ang);

        Destroy(b, 3f);
    }

    // ⭐ GIZMOS DE DETECCIÓN ⭐
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
    }
}
