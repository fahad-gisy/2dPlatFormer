using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHazard : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endtPosition;
    [SerializeField] private float time;
    [SerializeField] private float platformSpeed;
    [SerializeField] private bool IsPlayerDetected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerDetected)
        {
            transform.localPosition = Vector2.Lerp(startPosition, endtPosition, Mathf.PingPong(Time.time * platformSpeed, time));
        }   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsPlayerDetected = true;
        time = 1;
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
