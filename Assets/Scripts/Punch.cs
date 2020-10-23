using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    [SerializeField] GameObject armToActivate = null;
    [SerializeField] int punchDamage = 40;
    [SerializeField] Camera cameraController = null;
    [SerializeField] Transform rayOrigin = null;
    [SerializeField] LayerMask hitLayers;
    float punchTimer = 0f;
    float timeLimit = 0.5f;
    bool isPunching = false;
    int shootDistance = 2;

    RaycastHit objectHit;

    void Update()
    {
        punchTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            punchTimer = 0;
            isPunching = true;
            armToActivate.SetActive(true);
            HitObject();
        }
        if (punchTimer > timeLimit && isPunching == true)
        {
            isPunching = false;
            armToActivate.SetActive(false);
        }
    }

    void HitObject()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 0.5f);
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
        {
            EnemyShooter enemyShooter = objectHit.transform.GetComponent<EnemyShooter>();
            if (enemyShooter != null)
            {
                enemyShooter.TakeDamage(punchDamage);
            }
        }
    }

}
