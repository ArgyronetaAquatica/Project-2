using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        UnityEngine.Debug.Log("enemy health is " + health.ToString());
    }

}
