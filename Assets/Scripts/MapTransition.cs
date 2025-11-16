using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [Header("Configuración de zona")]
    public PolygonCollider2D zoneBounds; // Los límites de esta zona
    public Transform teleportDestination; // Punto donde aparecerá el jugador

    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();

        // Si no se asignó el collider manualmente, usa el del objeto
        if (zoneBounds == null)
            zoneBounds = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 🔹 Cambiar límites de cámara
            confiner.BoundingShape2D = zoneBounds;
            confiner.InvalidateBoundingShapeCache();

            // 🔹 Teletransportar al jugador (si se definió un destino)
            if (teleportDestination != null)
                collision.transform.position = teleportDestination.position;
        }
    }
}
