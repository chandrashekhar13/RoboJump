using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
   

    [SerializeField]
    private BoxCollider2D platformColider;

    [SerializeField]
    private BoxCollider2D trigerPlatform;
   
    // Start is called before the first frame update
    void Start()
    {
        
        Physics2D.IgnoreCollision(platformColider,  trigerPlatform, true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" || other.gameObject.name=="Enemy")
        {
            Physics2D.IgnoreCollision(platformColider, other, true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" ||  other.gameObject.name == "Enemy")
        {
            Physics2D.IgnoreCollision(platformColider, other, false);
        }
    }
}
