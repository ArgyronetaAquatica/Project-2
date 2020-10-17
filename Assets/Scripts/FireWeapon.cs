using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] Camera cameraController = null;
    [SerializeField] Transform rayOrigin = null;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] LayerMask hitLayers;

    RaycastHit objectHit;

    //visual feedback
    [SerializeField] GameObject visualFeedback = null;
    GameObject newInstance = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //aim
        Vector3 rayDirection = cameraController.transform.forward;
        //debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.cyan, 1f);
        //raycast check
        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
        {
            UnityEngine.Debug.Log(objectHit.transform.name);
            ImpactFlash();

            //apply damage if object is enemy
            if (objectHit.transform.tag == "Enemy")
            {
                EnemyShooter enemyShooter = objectHit.transform.GetComponent<EnemyShooter>();
                if (enemyShooter != null)
                {
                    enemyShooter.TakeDamage(weaponDamage);
                }
            }
            
        }
        else
        {
            UnityEngine.Debug.Log("miss");
        }
    }

    void ImpactFlash()
    {
        newInstance = Instantiate(visualFeedback, objectHit.point, Quaternion.identity);
        Destroy(newInstance, .25f);
    }

}
