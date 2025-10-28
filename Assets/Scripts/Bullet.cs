using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración")]
    public float damage = 1f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // destruir el proyectil al impactar
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject); // también se destruye al chocar con el suelo
        }
    }
}
