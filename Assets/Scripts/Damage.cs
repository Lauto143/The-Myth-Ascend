using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Daño que inflige este objeto")]
    public int damageAmount = 10;

    [Header("Usar Trigger en lugar de Colisión")]
    public bool useTrigger = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useTrigger)
            ApplyDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useTrigger)
            ApplyDamage(other.gameObject);
    }

    void ApplyDamage(GameObject target)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.currentHealth -= damageAmount;
            health.onHealthChanged.Invoke(health.currentHealth);

            if (health.currentHealth <= 0)
                health.onDeath.Invoke();
        }
    }
}
