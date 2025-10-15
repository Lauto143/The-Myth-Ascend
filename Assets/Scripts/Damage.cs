using UnityEngine;
using UnityEngine.Events;


public class Damage : MonoBehaviour
{
public int damageAmount = 10;

 void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.currentHealth -= damageAmount;
            health.onHealthChanged.Invoke(health.currentHealth);
            if (health.currentHealth <= 0)
            {
                health.onDeath.Invoke();
            }
        }
    }
}
