using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //variable to determine if the damage function can be called
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        
        if (hit != null)
        {
            //if can attack
            if (_canDamage)
            {
                Player player = this.gameObject.GetComponentInParent<Player>();

                if (player != null)
                {
                    if (player.hasFlameSword)
                        hit.Damage(2);

                    else
                        hit.Damage(1);
                }
                else
                {
                    hit.Damage(1);
                }

                _canDamage = false;
                //set that variable to false
                StartCoroutine(ResetAttack());
            }
        }
    }

    //coroutine to reset variable after 0.5f
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
