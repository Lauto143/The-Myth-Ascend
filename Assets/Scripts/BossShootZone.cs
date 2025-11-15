using UnityEngine;

public class BossShootZone : MonoBehaviour
{
    private Boss boss;
    
    [Header("Radio de detección (solo visual)")]
    public float shootRadius = 4f;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            boss.inShootZone = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            boss.inShootZone = false;
    }

    // --- GIZMOS PARA VER EL RADIO DE DISPARO ---
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}
