using UnityEngine;

public class BossMeleeZone : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            boss.inMeleeZone = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            boss.inMeleeZone = false;
    }
}
