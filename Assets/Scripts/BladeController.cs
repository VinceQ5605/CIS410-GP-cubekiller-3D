using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    private GameObject baseObj;
    private GameObject player;
    private BoximonEnemyController boximon;


    public void SetValues(GameObject baseObj, GameObject player, BoximonEnemyController boximon)
    {
        this.baseObj = baseObj;
        this.player = player;
        this.boximon = boximon;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj == baseObj)
        {
            boximon.DamageBase();
        }
        if (obj == player)
        {
            boximon.DamagePlayer();
        }
    }
}
