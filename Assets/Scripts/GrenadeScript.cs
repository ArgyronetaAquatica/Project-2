using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

    [SerializeField] LayerMask hitLayers;
    [SerializeField] int grenadeDamage = 50;
    [SerializeField] float hitRadius = 10f;
    [SerializeField] GameObject explosion = null;
    [SerializeField] AudioClip explosionSound = null;
    
    Vector3 positionHolder;

    //on trigger enter
    void OnTriggerEnter()
    {
        positionHolder = this.gameObject.transform.position;
        AudioHelper.PlayClip2D(explosionSound, 1f);
        GameObject explosionInstance = Instantiate(explosion, positionHolder, Quaternion.identity);
        Destroy(explosionInstance, 3.5f);
        Collider[] hitColliders = Physics.OverlapSphere(positionHolder, hitRadius, hitLayers);
        foreach (var hitCollider in hitColliders)
        {
            EnemyShooter shooter = hitCollider.gameObject.GetComponent<EnemyShooter>();
            if (shooter != null)
            {
                shooter.TakeDamage(grenadeDamage);
            }
        }
        //do visuals and sound
        //destroy
        Destroy(this.gameObject, 4f);
    }
    //spawn boom
    //create sphere overlap thingy
    //check if enemies are in sphere
    //damage enemies
    //destroy

}
