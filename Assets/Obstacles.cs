using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private int obstacleDamage;
    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        if (healthSystem != null)
        {
            Debug.Log("Health not null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystem.ApplyDamage(obstacleDamage);
        }
    }
}
