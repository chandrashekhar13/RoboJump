using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public Animator MyAnimator { get; private set; }

    [SerializeField]
    protected Transform BulletPos;

    [SerializeField]
    protected float movementspeed;

    protected bool facingRight;

    [SerializeField]
    private GameObject BulletPrefab;



    [SerializeField]
    protected Stat healthStat;


    [SerializeField]
    private EdgeCollider2D meleeColider;

    [SerializeField]
    private List<string> damageSources;

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public abstract bool IsDead { get;  }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public bool Shoot { get; set; }
    public EdgeCollider2D MeleeColider
    {
        get
        {
            return meleeColider;
        }
            
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("CharStart");
        facingRight = true;
        MyAnimator = GetComponent<Animator>();

        healthStat.Intialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void  ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowBullet(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(BulletPrefab, BulletPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(BulletPrefab, BulletPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }

    public void MeleeAttack()
    {
        MeleeColider.enabled = true; 
    }
   

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
