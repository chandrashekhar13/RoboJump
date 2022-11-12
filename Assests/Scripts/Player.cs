using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public delegate void DeadEventHandler();

public class Player : Character
{
    private static Player instance;

    public event DeadEventHandler Dead;

    public GameObject ImageUI;

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }


    


    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundrRadious;

    [SerializeField]
    private LayerMask valueofGround;

    [SerializeField]
    private bool jumpcontrol;

    [SerializeField]
    private float jumpForce;

    public Rigidbody2D MyRigidbody { get; set; }


    private bool immortal = false;

    private SpriteRenderer spriteRenderer;

    public AudioClip damageSound;

    public AudioClip jumpSound;

    public AudioClip deathSound;

    AudioSource playerAs;

    [SerializeField]
    private float immortaltime;
    

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public override bool IsDead
    {
        get
        {
            if(healthStat.CurrentVal <=0)
            {
                OnDead();
            }
            
            return healthStat.CurrentVal<= 0;
        }
    }

    //public bool IsFalling
    //{
    //    get
    //    {
    //        return MyRigidbody.velocity.y < 0;
    //    }
    //}

    private Vector2 startpos;

    //public Vector3 respawnPoint;
    //private Transform childTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        Debug.Log("PlayerStart");

        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        startpos = transform.position;

        MyRigidbody = GetComponent<Rigidbody2D>();
        //respawnPoint = transform.position;
        playerAs = GetComponent<AudioSource>();
        ImageUI.SetActive(false);
        healthStat.Intialize();
    }

    void Update()
    {
        if(!TakingDamage)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!TakingDamage)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            Movement(horizontal);

            Flip(horizontal);

            LayersHandling();


        }

    }

    public void OnDead()
    {
        if(Dead!=null)
        {
            Dead();
        }
    }

    private void Movement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if (!Shoot && !Slide && (OnGround || jumpcontrol))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementspeed, MyRigidbody.velocity.y);
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

   

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            MyAnimator.SetTrigger("shoot");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
            playerAs.clip = jumpSound;
            playerAs.Play();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            MyAnimator.SetTrigger("slide");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            MyAnimator.SetTrigger("attack");
        }
    }
    
    private void Flip(float horizontal)
    {
        if(horizontal>0 && !facingRight || horizontal<0 && facingRight)
        {
            ChangeDirection();
        }
    }

    
    
    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y<=0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundrRadious, valueofGround);
            
                for(int i=0; i<colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void LayersHandling()
    {
        if(!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowBullet(int value)
    {
       
    
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowBullet(value);
        }
    
}
    private IEnumerator IndicateImmortal()
    {
        while(immortal)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!IsDead)
        {
            if (col.gameObject.tag == "Saw")
            {
                healthStat.CurrentVal -= 1.00f;

            }
            if (col.gameObject.tag == "Spike")
            {
                healthStat.CurrentVal -= 1.00f;

            }
            if (col.gameObject.tag == "Acid")
            {
                healthStat.CurrentVal -= 1.00f;

            }
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
            MyAnimator.SetTrigger("die");
            ImageUI.SetActive(true);
        }
        if(col.gameObject.tag == "Switch")
        {
            ImageUI.SetActive(true);

        }

        if (col.gameObject.tag == "Health")
        {
            healthStat.CurrentVal=100;
            Destroy(col.gameObject);

        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 10;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                playerAs.clip = damageSound;
                playerAs.Play();
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortaltime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                
                ImageUI.SetActive(true);

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Coin")
        {
            GameManager.Instance.CollectedCoins += 10;
            Destroy(other.gameObject);
        }
    }

    public override void Death()
    {
        //playerAs.clip = jumpSound;
        //playerAs.Play();
        //MyRigidbody.velocity = Vector2.zero;
        //MyAnimator.SetTrigger("idle");

        //enemyHealthStat.CurrentVal = enemyHealthStat.MaxVal;
        //transform.position = startpos;
        
    }

    //public  override void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "FallDetector")
    //    {
    //        transform.position = respawnPoint;
    //    }
    //    if (other.tag == "Switch")
    //    {
    //        respawnPoint = other.transform.position;
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.gameObject.tag=="Player")
    //    {
    //        other.gameObject.layer = 10;
    //        other.transform.SetParent(childTransform);
    //    }

    //}

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    other.transform.SetParent(null);

    //}
}
