﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    
    public override void Init()
    {
        base.Init();

        Health = base.health;

        filpAttack = false;
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