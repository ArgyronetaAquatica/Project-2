using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyShooter : MonoBehaviour
{

    [SerializeField] int points = 10;
    
    //health
    int health = 100;
    bool isAlive = true;
    [SerializeField] Slider healthSlider = null;

    //shooting
    [SerializeField] Transform target = null;
    [SerializeField] float rateOfFire = 3f;
    [SerializeField] Transform bulletOrigin = null;
    [SerializeField] float hitRange = 20f;
    [SerializeField] AudioClip bulletSound = null;
    [SerializeField] AudioClip damageSound = null;
    [SerializeField] ParticleSystem muzzleFlash = null;
    float countDown = 0;
    public GameObject bullet;
    private GameObject newInstance = null;
    float playerDistance;

    //kill behavior
    [Header("Death Feedback")]
    //audiovisual feedback
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] ParticleSystem deathEffect = null;
    //visuals to disable
    [SerializeField] GameObject weaponToDisable = null;
    MeshRenderer visualsToDisable;
    Collider colliderToDisable;

    void Start()
    {
        visualsToDisable = this.gameObject.GetComponent<MeshRenderer>();
        colliderToDisable = this.gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        countDown += Time.deltaTime;
        playerDistance = Vector3.Distance(target.position, this.transform.position);

        if (playerDistance <= hitRange && countDown >= rateOfFire && isAlive)
        {
            transform.LookAt(target);
            FireWeapon();
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;

            healthSlider.value = health;

            //add visual feedback
            AudioHelper.PlayClip2D(damageSound, 1f);
            
            UnityEngine.Debug.Log("enemy health is " + health.ToString());
            if (health <= 0)
            {
                health = 0;
                UnityEngine.Debug.Log("enemy dead");
                Kill();
            }
        }
    }

    void FireWeapon()
    {
        countDown = 0;
        muzzleFlash.Play();
        newInstance = Instantiate(bullet, bulletOrigin.position, transform.rotation);
        newInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
        AudioHelper.PlayClip2D(bulletSound, 1f);
    }

    void Kill()
    {
        isAlive = false;
        target.gameObject.GetComponent<PlayerController>().levelController.IncreaseScore(points);
        //run feedback
        AudioHelper.PlayClip2D(deathSound, 1f);
        deathEffect.Play();
        //disable visuals and collider
        weaponToDisable.SetActive(false);
        visualsToDisable.enabled = false;
        colliderToDisable.enabled = false;
        healthSlider.gameObject.SetActive(false);
        //update hud
        //destroy object
        Destroy(this.gameObject, 2f);
    }

}
