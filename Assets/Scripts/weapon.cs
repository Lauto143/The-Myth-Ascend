using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class weapon : MonoBehaviour
{

    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Call the parameterless TakeDamage if Enemy defines it
            enemy.TakeDamage(damage);
        }
    }

}
