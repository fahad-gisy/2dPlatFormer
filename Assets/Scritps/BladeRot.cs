using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRot : MonoBehaviour
{
    [SerializeField] private float bladeRotationSpeed;
    [SerializeField] private int bladeDamage;
    private HealthSystem healthSystem;
    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,1 * bladeRotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystem.ApplyDamage(bladeDamage);
        }
    }
}
