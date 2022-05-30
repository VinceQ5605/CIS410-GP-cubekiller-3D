using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float explosionRadius;
    private float explosionIntensity;
    private float explosionDamage;
    private GameObject grenade;
    private bool isFieryGround;
    private bool isMiniFire;
    private bool isGravityWell;

    public AudioSource explosionLeftClick;
    public AudioSource explosionRightClick;


    public void SetValues(float radius, float intensity, float damage, GameObject grenade, bool isMiniFire)
    {
        explosionRadius = radius;
        explosionIntensity = intensity;
        explosionDamage = damage;
        this.grenade = grenade;
        this.isMiniFire = isMiniFire;
    }

    public void SetFieryGround()
    {
        isFieryGround = true;
    }

    public void SetGravityWell()
    {
        isGravityWell = true;
    }

    public void Explode()
    {
        
        if (isFieryGround)
        {
            // spawn mini fiery grenades in 6 directions
            if (!isMiniFire) 
            {
                float piDividedBy6 = Mathf.PI / 6f;
                for (int i = 0; i < 6; i++)
                {
                    float angle = i * piDividedBy6;
                    Vector3 direction = new Vector3(Mathf.Cos(angle), 1f, Mathf.Cos(angle));
                    GameObject grenadeCopy = Instantiate(grenade, transform.position, transform.rotation) as GameObject;
                    grenadeCopy.GetComponent<GrenadeController>().SetType(0, true);
                    grenadeCopy.GetComponent<Rigidbody>().AddForce(3 * direction, ForceMode.Impulse);
                }
            }
            StartCoroutine(FieryGround());
            
        }
        else if (isGravityWell)
        {
            StartCoroutine(GravityWell());
            
        }
        else
        {
            StartCoroutine(ExplosionVisual());
            
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            GameObject gameObject = hitCollider.gameObject;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            Vector3 direction = gameObject.transform.position - transform.position;

            direction.Normalize();
            EnemyController enemyController = gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                gameObject.GetComponent<EnemyController>().Hit(explosionIntensity * direction, explosionDamage);
            }
        }
    }

    private IEnumerator FieryGround()
    {
        

        // to do: spawn fire visual effect on ground
        yield return null;
    }

    private IEnumerator GravityWell()
    {


        yield return null;
    }

    private IEnumerator ExplosionVisual()
    {
        Transform transform = grenade.transform;
        Vector3 position = transform.position;
        grenade.GetComponent<Rigidbody>().isKinematic = false;
        grenade.GetComponent<Rigidbody>().useGravity = false;
        grenade.GetComponent<SphereCollider>().isTrigger = true;
        
        transform.localScale = new Vector3(.1f, .1f, .1f);

        for (int i = 1; i < explosionIntensity + 1; i += 4)
        {
            transform.localScale = i*(new Vector3(.1f, .1f, .1f));
            transform.position = position;
            yield return null;
        }
        grenade.SetActive(false);
        this.gameObject.SetActive(false);
        ExplosionAudioPlay();
    }

    public void ExplosionAudioPlay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            explosionLeftClick.Play();
        }
        if (Input.GetMouseButtonDown(1))
        {
            explosionRightClick.Play();
        }
    }

}
