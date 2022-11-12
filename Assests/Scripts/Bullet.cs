using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        myRigidbody.velocity = direction * BulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

   
}
