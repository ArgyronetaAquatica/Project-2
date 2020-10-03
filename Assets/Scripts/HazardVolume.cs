using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{

    [SerializeField] PlayerController _objectToDamage = null;

    void OnTriggerEnter()
    {
        _objectToDamage.health -= 25;
    }

}
