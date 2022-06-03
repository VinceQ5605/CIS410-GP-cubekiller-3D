using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private GameObject baseObj;
    private GameObject player;
    private BoximonEnemyController boximon;
    private bool isBlowingUp = false;


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
            if (!isBlowingUp)
                StartCoroutine(BlowUp());
            boximon.DamageBase();
        }
        if (obj == player)
        {
            if (!isBlowingUp)
                StartCoroutine(BlowUp());
            boximon.DamagePlayer();
        }
        if (obj.CompareTag("Ground"))
        {
            if (!isBlowingUp)
                StartCoroutine(BlowUp());
        }
    }

    IEnumerator BlowUp()
    {
        isBlowingUp = true;
        Vector3 scale = new Vector3(1f, 1f, 1f);
        for (int i = 0; i < 100; i++)
        {
            transform.localScale = (1 + .01f * i) * scale;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
