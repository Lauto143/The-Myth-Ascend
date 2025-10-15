using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public UnityEvent onDeath;
    public UnityEvent<int> onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth);        
    }

}
