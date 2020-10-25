using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound = null;

    void OnTriggerEnter(Collider other)
    {
        FireWeapon playerWeapon = other.gameObject.GetComponent<FireWeapon>();
        if (playerWeapon != null)
        {
            playerWeapon.UpdateGrenadeSupply(1);
            //feedback sound
            AudioHelper.PlayClip2D(pickupSound, 1f);
            Destroy(this.gameObject, .25f);
        }
    }

}
