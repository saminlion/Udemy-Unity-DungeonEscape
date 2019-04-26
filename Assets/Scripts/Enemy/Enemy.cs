using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;

    protected bool filpAttack;

    protected bool isDead;

    //variable to store the player
    protected GameObject player;

    public GameObject diamondPrefab;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentTarget = pointA.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        //if Idle Animation is playing
        //Do Nothing (return)
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        if (!isDead)
        {
            Movement();
        }
    }

    public virtual void Movement()
    {
        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);

        if (currentTarget == pointA.position)
        {
            sprite.flipX = false;
        }

        else if (currentTarget == pointB.position)
        {
            sprite.flipX = true;
        }

        //if current pos == point A
        //move to point B
        if (distA <= 0.0f)
        {
            transform.position = pointA.position;
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }

        //else if current pos == Point B
        //Move to Point A
        else if (distB <= 0.0f)
        {
            transform.position = pointB.position;
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (!isHit)
        {
            Vector3 newVector = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            newVector.y = transform.position.y;

            transform.position = newVector;
        }

        else if (isHit && anim.GetBool("InCombat"))
        {
            Vector3 direction = player.transform.localPosition - transform.localPosition;

            if (direction.x > 0)
            {
                //face right
                sprite.flipX = filpAttack;
            }

            else if (direction.x < 0)
            {
                //face left
                sprite.flipX = !filpAttack;
            }
        }

        //check for distance btw player and enemy 
        //if greater than 2 unit isHit = false
        //Animator parameter InCombat = false
        float distanceMP = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distanceMP > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
    }
}

