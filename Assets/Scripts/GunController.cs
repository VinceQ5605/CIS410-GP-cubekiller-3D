using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    float X;
    float Y;
    Quaternion fromRotation;
    Quaternion toRotation;
    public AudioSource shootingAudio;
    public AudioSource shootingAudio2;

    bool m_HasAudioPlayed;
    bool m_HasAudioPlayed2;

    public Camera cam;
    public GameObject grenade;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public int selected;
    public int defaultDamage = 100;

    private float basicGrenadeCDRemaining;
    private float basicGrenadeCD = 1.2f;
    private float[] specialGrenadeCD;
    private float[] specialGrenadeCDRemaining;

    void Start()
    {
        selected = 0;
        specialGrenadeCD = new float[] { 5f, 5f, 5f, 5f, 5f };
        specialGrenadeCDRemaining = new float[] { 0f, 0f, 0f, 0f, 0f };
        //m_HasAudioPlayed = false;
        //m_HasAudioPlayed2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        basicGrenadeCDRemaining -= Time.deltaTime;
        for (int i = 0; i < 5; i++)
        {
            specialGrenadeCDRemaining[i] -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetAxis("Sprint") != 1f && basicGrenadeCDRemaining <= 0)
        {
            FireGrenadeBasic();
            shootingAudio.Play();
            //m_HasAudioPlayed = true;
        }
        if (Input.GetMouseButtonDown(1) && Input.GetAxis("Sprint") != 1f && specialGrenadeCDRemaining[selected] <= 0)
        {
            FireGrenadeSpecial();
            shootingAudio2.Play();
            //m_HasAudioPlayed2 = true;
        }
    }

    void FireGrenadeBasic()
    {
        Vector3 direction = cam.transform.forward;
        GameObject grenadeCopy = Instantiate(grenade, shotPos.position, shotPos.rotation) as GameObject;
        float speed = grenadeCopy.GetComponent<GrenadeController>().SetType(0);
        grenadeCopy.GetComponent<Rigidbody>().velocity = cam.velocity; // start the grenade with the velocity of the player before adding force from the launch
        grenadeCopy.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
        basicGrenadeCDRemaining = basicGrenadeCD;
    }

    void FireGrenadeSpecial() 
    {
        Vector3 direction = cam.transform.forward;
        GameObject grenadeCopy = Instantiate(grenade, shotPos.position, shotPos.rotation) as GameObject;
        float speed = grenadeCopy.GetComponent<GrenadeController>().SetType(selected + 1);
        grenadeCopy.GetComponent<Rigidbody>().velocity = cam.velocity; // start the grenade with the velocity of the player before adding force from the launch
        grenadeCopy.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
        specialGrenadeCDRemaining[selected] = specialGrenadeCD[selected];

    }

    public float GetCD(int index)
        // returns the remaining CD at as a fraction of the full CD for the specified grenade
    {
        if (specialGrenadeCDRemaining[index] <= 0)
            return 0f;
        return specialGrenadeCDRemaining[index] / specialGrenadeCD[index];
    }
}
