using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailDown : MonoBehaviour
{
    [SerializeField] private Transform reSpawnPoint;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = reSpawnPoint.position;
        }
    }
}
