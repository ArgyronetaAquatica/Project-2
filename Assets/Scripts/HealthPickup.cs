using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] int healthAmount = 25;

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.health < 100)
            {
                player.health += healthAmount;
                if (player.health > 100)
                {
                    player.health = 100;
                }
                //feedback sound
                Destroy(this.gameObject);
            }
        }
    }

}
