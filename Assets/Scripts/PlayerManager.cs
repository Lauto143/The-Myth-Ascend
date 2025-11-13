using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int potions;

    public TextMeshProUGUI potionsValueText;
    public TextMeshProUGUI livesValueText;
    private Health health;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        potionsValueText.text = potions.ToString();
    }

    public void AddConsumableItem()
    {
        potions += 1;
        potionsValueText.text = potions.ToString();
        Debug.Log("Se agrego una pocion lista para usar");
    }
}
