using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();

        //assign Health Property to our enemy health
        Health = base.health;

        filpAttack = true;
    }

    public void Damage(int damage)
    {
        if (isDead)
            return;
        
        //subtract 1 from health
        Health -= damage;

        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        //if health is less than 1
        //destroy the object

        if (Health < 1)
        {
            isDead = true;
            Diamond diamond = Instantiate(diamondPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity).GetComponent<Diamond>();
            diamond.diamonds = gems;
            anim.SetTrigger("Death");
        }
        
    }
}
