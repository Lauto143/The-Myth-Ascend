using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignorar enemigos (incluye al jefe y otros)
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
            return;

        // Si toca al Player → daño + destruir bala
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Bala impactó al Player");

            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // Si toca paredes, piso o colisión del mapa
        if (collision.CompareTag("Collider"))
        {
            // Debug.Log("Bala impactó un Collider");
            Destroy(gameObject);
        }
    }
}
