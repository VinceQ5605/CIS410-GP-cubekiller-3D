using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float lifeTime = 3f;
    public GameObject explosionPrefab;
    public float minY = -20f;
    public float explosionRadius;
    public float explosionIntensity;
    public List<Material> grenadeMaterials;

    void Start()
    {

    }


    void Update()
    {
        StatusCheck();
    }

    
    /// colors: {black, red, gold, green, blue}
    public void SetValues(int color, float mass, float radius, float intensity)
    {
        GetComponent<MeshRenderer>().material = grenadeMaterials[color]; // sets the grenade to the correct color
        GetComponent<Rigidbody>().mass = mass;
        explosionRadius = radius;
        explosionIntensity = intensity;
    }

    void StatusCheck()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Detonate();
        }

        if (transform.position.y < minY)
        {
            Detonate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Enemy"))
            {
                Detonate();
            }
        }
    }
    void Detonate()
    {
        Explosion explosion = (Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject).GetComponent<Explosion>();
        explosion.SetValues(explosionRadius, explosionIntensity, this.gameObject);
        explosion.Explode();
        //this.gameObject.SetActive(false);
    }
}
