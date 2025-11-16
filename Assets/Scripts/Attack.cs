using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    // melee
    public GameObject Melee; // hitbox cuerpo a cuerpo
    public float attackDuration = 0.3f;

    // ranged
    public Transform Aim;          // punto de disparo
    public GameObject Bullet;      // prefab del proyectil
    public float fireForce = 10f;  // fuerza del disparo
    public float shootCooldown = 0.25f;

    private float shootTimer = 0f;
    private bool isAttack = false;
    private bool isShoot = false;
    private PlayerInput PI;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 lastDirection = Vector2.right; // dirección hacia donde mira

    void Start()
    {
        PI = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootTimer = shootCooldown; // permite disparar al inicio
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        HandleMeleeAttack();
        HandleRangedAttack();
    }

    void HandleMeleeAttack()
    {
        Vector2 moveDir = PI.actions["Move"].ReadValue<Vector2>();
        if (moveDir != Vector2.zero)
            lastDirection = moveDir;

        if (PI.actions["Attack"].triggered && !isAttack)
            StartCoroutine(PerformMeleeAttack());
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttack = true;
        Melee.SetActive(true);
        animator.SetBool("IsAttack", true);

        yield return new WaitForSeconds(attackDuration);

        Melee.SetActive(false);
        animator.SetBool("IsAttack", false);
        isAttack = false;
    }

    void HandleRangedAttack()
    {
        if (PI.actions["RangedAttack"].triggered && !isShoot) // 🔹 evita disparar si ya está disparando
        {
            AudioManager.instance.PlaySFX(7);
            StartCoroutine(PerformRangedAttack());
        }
    }

    IEnumerator PerformRangedAttack()
    {
        isShoot = true; // 🔹 activamos el flag
        animator.SetBool("IsShoot", true); // 🔹 para animación (opcional)

        OnShoot();

        yield return new WaitForSeconds(shootCooldown); // tiempo entre disparos

        animator.SetBool("IsShoot", false);
        isShoot = false; // 🔹 lo desactivamos
    }

void OnShoot()
{
    GameObject intBullet = Instantiate(Bullet, Aim.position, Aim.rotation);
    Rigidbody2D rb = intBullet.GetComponent<Rigidbody2D>();

    // Dispara en la última dirección en la que se movió el jugador
    Vector2 direction = lastDirection.normalized;
    rb.AddForce(direction * fireForce, ForceMode2D.Impulse);

    Destroy(intBullet, 2f);
}

}
