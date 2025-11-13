using UnityEngine;

public class ConsumableItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null)
        {
            PlayerManager.Instance.AddConsumableItem();
            Destroy(gameObject);

        }
    }
}
