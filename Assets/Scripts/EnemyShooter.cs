using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShooter : MonoBehaviour
{

    //health
    int health = 100;

    //shooting
    [SerializeField] Transform target = null;
    [SerializeField] float rateOfFire = 3f;
    [SerializeField] Transform bulletOrigin = null;
    [SerializeField] float hitRange = 20f;
    [SerializeField] AudioClip bulletSound = null;
    [SerializeField] AudioClip damageSound = null;
    float countDown = 0;
    public GameObject bullet;
    private GameObject newInstance = null;
    float playerDistance;

    void Update()
    {
        countDown += Time.deltaTime;
        playerDistance = Vector3.Distance(target.position, this.transform.position);

        if (playerDistance <= hitRange && countDown >= rateOfFire)
        {
            transform.LookAt(target);
            FireWeapon();
        }
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioHelper.PlayClip2D(damageSound, 0.8f);
        UnityEngine.Debug.Log("enemy health is " + health.ToString());
        if (health <= 0)
        {
            health = 0;
            UnityEngine.Debug.Log("enemy dead");
        }
    }

    void FireWeapon()
    {
        countDown = 0;
        newInstance = Instantiate(bullet, bulletOrigin.position, transform.rotation);
        newInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
        AudioHelper.PlayClip2D(bulletSound, 0.8f);
    }

}
