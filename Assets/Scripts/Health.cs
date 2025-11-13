using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 1;

    public UnityEvent onDeath;
    public UnityEvent<int> onHealthChanged;

    void Start()
    {
        // currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth);
        PlayerManager.Instance.livesValueText.text = currentHealth.ToString();
    }

    void Update()
    {
        /*Debug.Log("La vida es " + (currentHealth));*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UsePotion();
            Debug.Log("La vida es " + (currentHealth));
        }
        
    }

    private void UsePotion()
    {
        if (PlayerManager.Instance.potions > 0)
        {
            currentHealth += 1;
            PlayerManager.Instance.potions -= 1;
            Debug.Log("Pocion usada");
            PlayerManager.Instance.potionsValueText.text = PlayerManager.Instance.potions.ToString();
            PlayerManager.Instance.livesValueText.text = currentHealth.ToString();

        }
        else
        {
            Debug.Log("Sin pociones");
        }
    }


}
