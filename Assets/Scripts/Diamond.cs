using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    //OnTriggerEnter2D to collect it
    //check for the player
    //add the value of diamond to the player
    public int diamonds = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddGem(diamonds);

                Destroy(this.gameObject);
            }
        }
    }

}
