using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        FireWeapon playerWeapon = other.gameObject.GetComponent<FireWeapon>();
        if (playerWeapon != null)
        {
            playerWeapon.UpdateGrenadeSupply(1);
            //feedback sound
            Destroy(this.gameObject, .25f);
        }
    }

}
