using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageable
{
    public int Health { get; set; }
        
    public GameObject _acidPrefab;

    public override void Init()
    {
        base.Init();

        Health = base.health;

        filpAttack = false;
    }

    public override void Update()
    {
      
    }

    public void Damage(int damage)
    {
        if (isDead)
            return;

        Health -= damage;

        if (Health <= 0)
        {
            isDead = true;
            Diamond diamond = Instantiate(diamondPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity).GetComponent<Diamond>();
            diamond.diamonds = gems;
            anim.SetTrigger("Death");
        }
    }

    public override void Movement()
    {
        //Sit still
    }

    public void Attack()
    {
        //Instantiate the acid effect
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}
