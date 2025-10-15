using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
public class MapTransition : MonoBehaviour
{
    private PolygonCollider2D mapBondry;
    CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        mapBondry = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBondry;
            confiner.InvalidateBoundingShapeCache();
        }
    }
}
