using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endtPosition;
    [SerializeField] private float platformSpeed;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = Vector2.Lerp(startPosition,endtPosition, platformSpeed);
        transform.localPosition = Vector2.Lerp(startPosition, endtPosition, Mathf.PingPong(Time.time * platformSpeed, 1));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.position.y < collision.transform.position.y)
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
