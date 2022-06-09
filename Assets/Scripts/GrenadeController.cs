using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public PowerUps powerUps;
    public GameObject explosionPrefab;
    public List<Material> grenadeMaterials;
    public float defaultSpeed = 10f;
    public float defaultRadius = 3f;
    public float defaultIntensity = 50f;
    public float defaultLifeTime = 3f;
    public float defaultDamage = 100;
    public int defaultBouncesAvailable = 0;

    private int color;
    private float grenadeSpeed;
    private float explosionRadius;
    private float explosionIntensity;
    private float lifeTime = 3f; // time before explosion is triggered even without a collision
    private float damage;
    private int bouncesAvailable;
    private float minY = 0f;
    private bool fieryGround = false;
    private bool gravityWell = false;
    private bool miniFire = false;


    void Start()
    {
        grenadeSpeed = defaultSpeed;
        explosionRadius = defaultRadius;
        explosionIntensity = defaultIntensity;
        lifeTime = defaultLifeTime;
        damage = defaultDamage;
        bouncesAvailable = defaultBouncesAvailable;
    }


    void Update()
    {
        StatusCheck();
    }

    
    /// colors: {black, gold, red, blue, purple, green}
    public float SetType(int color, bool isMiniFire = false)
    {
        GetComponent<MeshRenderer>().material = grenadeMaterials[color]; // sets the grenade to the correct color
        this.color = color;
        

        if (color == 0) // Black (basic)
        {
            return defaultSpeed;
        }
        else if (color == 1) // Gold (large explosion, high damage)
        {
            explosionRadius = 2 * explosionRadius;
            damage = 2 * damage;
            return .7f * defaultSpeed;
        }
        else if (color == 2) // Red (create a fiery ground effect which damages enemies over time)
        {
            fieryGround = true;
            if (isMiniFire)
            {
                explosionRadius = .5f * explosionRadius;
                explosionIntensity = .5f * explosionIntensity;
                damage = .2f * damage;
                bouncesAvailable = 0;
                this.transform.localScale = .5f * this.transform.localScale;
            }
            return .7f * defaultSpeed;
        }
        else if (color == 3) // Blue (heavy, high knockback)
        {
            GetComponent<Rigidbody>().mass = 2.5f;
            explosionRadius = 2 * explosionRadius;
            return 14f * defaultSpeed;
        }
        else if (color == 4) // Purple (creates a gravity well sucking the enemies and player in, stronger near center)
        {
            gravityWell = true;
            explosionRadius = 4 * explosionRadius;          // these values will serve a different purpose for gravity wells, 
            explosionIntensity = .4f * explosionIntensity;  // but may still be affected by certain powerUps
            damage = 0;
            return .7f * defaultSpeed;
        }
        else if (color == 5) // Green (???)
        {

            return .7f * defaultSpeed;
        }

        return .7f * defaultSpeed;
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
        bouncesAvailable--;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Enemy") || (bouncesAvailable < 0))
            {
                Detonate();
            }
        }
    }

    void Detonate()
    {
        Explosion explosion = (Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject).GetComponent<Explosion>();
        explosion.SetValues(explosionRadius, explosionIntensity, damage, this.gameObject, miniFire);
        explosion.Explode();
        //this.gameObject.SetActive(false);
    }
}
