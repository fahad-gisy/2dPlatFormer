using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameObject collisionParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.right * ShootForce;
        Destroy(gameObject,bulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Slime"))
        {
            SoundManager.instance.PlayShootImpactSound();
           collision.gameObject.GetComponent<Enemy>().slimeHleath -= 10; ;
            Destroy(gameObject);
            collisionParticles = Instantiate(collisionParticles,transform.position,transform.rotation);
            Destroy(collisionParticles,1);
        }
        else if (collision.gameObject.CompareTag("Untagged"))
        {
            SoundManager.instance.PlayShootImpactSound();
            Destroy(gameObject);
            collisionParticles = Instantiate(collisionParticles, transform.position, transform.rotation);
            Destroy(collisionParticles, 1);
        }
       
    }

}
 
