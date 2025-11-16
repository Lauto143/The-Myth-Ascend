using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public UnityEvent onDeath;
    public UnityEvent<int> onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= Mathf.RoundToInt(amount);

        if (currentHealth < 0)
            currentHealth = 0;

        onHealthChanged?.Invoke(currentHealth);

        Debug.Log("Player recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
        onDeath?.Invoke();
        // Podés agregar respawn o animación acá.
    }
}
