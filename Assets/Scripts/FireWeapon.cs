using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] Camera cameraController = null;
    [SerializeField] Transform rayOrigin = null;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] GameObject visualFeedback = null;
    [SerializeField] LayerMask hitLayers;

    RaycastHit objectHit;

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
            visualFeedback.transform.position = objectHit.point;

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

}
