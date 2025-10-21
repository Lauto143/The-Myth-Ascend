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
        onHealthChanged.Invoke(currentHealth);
        
    }

    void Update()
    {
        Debug.Log("La vida es " + (currentHealth));
    }


}
