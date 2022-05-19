using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    float X;
    float Y;
    Quaternion fromRotation;
    Quaternion toRotation;
    public Camera cam;

    public GameObject grenade;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public int selected;
    public int defaultDamage = 100;
    

    void Start()
    {
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireGrenadeBasic();
        }
        if (Input.GetMouseButtonDown(1))
        {
            FireGrenadeSpecial();
        }
    }

    void FireGrenadeBasic()
    {
        Vector3 direction = cam.transform.forward;
        GameObject grenadeCopy = Instantiate(grenade, shotPos.position, shotPos.rotation) as GameObject;
        float speed = grenadeCopy.GetComponent<GrenadeController>().SetType(0);
        grenadeCopy.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
    }

    void FireGrenadeSpecial() 
    {
        Vector3 direction = cam.transform.forward;
        GameObject grenadeCopy = Instantiate(grenade, shotPos.position, shotPos.rotation) as GameObject;
        float speed = grenadeCopy.GetComponent<GrenadeController>().SetType(selected + 1);
        grenadeCopy.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
    }
}
