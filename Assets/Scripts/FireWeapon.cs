using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [Header("My Weapons")]
    [SerializeField] GameObject primaryWeapon = null;
    [SerializeField] GameObject secondaryWeapon = null;
    [SerializeField] GameObject grenadeToSpawn = null;
    [SerializeField] Transform grenadeOrigin = null;
    
    [SerializeField] Camera cameraController = null;
    [SerializeField] Transform rayOrigin = null;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] int primaryWeaponDamage = 20;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] Level01Controller levelController = null;

    //feedback
    public ParticleSystem muzzleFlash;
    public ParticleSystem grenadeLauncherFlash;
    [SerializeField] AudioClip fireSound = null;

    RaycastHit objectHit;

    bool grenadeLauncherEquipped = false;

    //visual feedback
    [SerializeField] GameObject visualFeedback = null;
    GameObject newInstance = null;

    void Update()
    {
        if (!levelController.menuToggle)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                toggleWeaponType();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (grenadeLauncherEquipped)
                {
                    LaunchGrenade();
                }
                else
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        //aim
        Vector3 rayDirection = cameraController.transform.forward;
        //feedback
        muzzleFlash.Play();
        AudioHelper.PlayClip2D(fireSound, 1f);
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
                    enemyShooter.TakeDamage(primaryWeaponDamage);
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

    void toggleWeaponType()
    {
        grenadeLauncherEquipped = !grenadeLauncherEquipped;
        if (grenadeLauncherEquipped)
        {
            primaryWeapon.SetActive(false);
            secondaryWeapon.SetActive(true);
        } else
        {
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
        }
    }

    void LaunchGrenade()
    {
        grenadeLauncherFlash.Play();
        GameObject newInstance = Instantiate(grenadeToSpawn, grenadeOrigin.position, transform.rotation);
        newInstance.GetComponent<Rigidbody>().AddForce(cameraController.transform.forward * 100f);
        AudioHelper.PlayClip2D(fireSound, 1f);
        //TODO
        //limit reload time
        //limit amount of ammo
    }

}
