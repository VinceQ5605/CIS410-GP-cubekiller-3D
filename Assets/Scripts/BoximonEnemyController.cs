using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoximonEnemyController : MonoBehaviour
{
    public GameObject baseObj;
    public float fireCoolDown;
    public float range; // how close a target needs to be to trigger an attack (square of the horizontal distance)
    public GameObject player;
    [Range(0, 1)]
    public int enemyType;
    public GameObject fireball; // for type 2 enemies
    public float fireballSpeed = 30f;

    private EnemyController enemyController;
    private Animator animator;
    private float fireCDRemaining;

    private GeneralManager generalManager;

    void Start()
    {
        this.enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        animator.SetBool("Jump", true); // I don't think this works
        if (enemyType == 0)
        {
            Transform model = transform.GetChild(0).transform;
            Transform blade = model.GetChild(0);
            Transform blade1 = model.GetChild(0);
            blade1.gameObject.GetComponent<BladeController>().SetValues(baseObj, player, this);
        }
        generalManager = baseObj.transform.GetChild(0).gameObject.GetComponent<GeneralManager>();

    }

    public void SetValues(GameObject player, GameObject baseObj, GameObject fireball)
    {
        this.player = player;
        this.baseObj = baseObj;
        this.fireball = fireball;
        enemyController = GetComponent<EnemyController>();

        enemyController.SetDestination(baseObj);

        // align model with grid as close to the direction of travel as possible
        Vector3 direction = baseObj.transform.position - transform.position;
        //direction = new Vector3(direction.x, 0f, direction.z);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            if (direction.x > 0)
            {
                direction = new Vector3(1f, 0f, 0f);
            }
            else
            {
                direction = new Vector3(-1f, 0f, 0f);
            }
        }
        else
        {
            if (direction.z > 0)
            {
                direction = new Vector3(0f, 0f, 1f);
            }
            else
            {
                direction = new Vector3(0f, 0f, -1f);
            }
        }
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void Update()
    {
        fireCDRemaining -= Time.deltaTime;
        if (enemyType == 0)
        {
            Enemy1Update();
        }
        else if (enemyType == 1)
        {
            Enemy2Update();
        }
    }

    private Vector3 SomethingToFireAt()
    {
        Vector3 playerDelta = player.transform.position - transform.position;
        if (playerDelta.x * playerDelta.x + playerDelta.z * playerDelta.z < range)
        {
            return playerDelta;
        }

        Vector3 baseDelta = baseObj.transform.position - transform.position;
        if (baseDelta.x * baseDelta.x + baseDelta.z * baseDelta.z < range)
        {
            return baseDelta;
        }

        return Vector3.zero;
    }

    private void Enemy1Update()
    {
        if (enemyController.atDestination)
        {
            //enemyController.stopMoving = true;
        }
        Vector3 target = SomethingToFireAt();
        if (fireCDRemaining < 0 && target != Vector3.zero)
        {
            fireCDRemaining = fireCoolDown;
            Fire1();
        }
    }

    private void Enemy2Update()
    {
        if (enemyController.atDestination)
        {
            enemyController.stopMoving = true;
        }
        else
        {
            enemyController.stopMoving = false;
        }
        Vector3 target = SomethingToFireAt();
        if (fireCDRemaining < 0 && target != Vector3.zero)
        {
            fireCDRemaining = fireCoolDown;
            Fire2(target);
        }
    }

    public void Fire1()
    {
        StartCoroutine(BladeSpin());
    }

    IEnumerator BladeSpin()
    {
        Transform model = transform.GetChild(0).transform;
        GameObject blade = model.GetChild(0).gameObject;
        blade.SetActive(true);
        Quaternion initialRotation = model.transform.rotation;
        float speed = 1500f;
        for (float i = 0; i < 360; i += speed * Time.deltaTime)
        {
            model.Rotate(-1 * speed * Time.deltaTime * Vector3.up);
            yield return null;
        }
        model.transform.rotation = initialRotation;
        blade.SetActive(false);
    }

    public void Fire2(Vector3 target)
    {
        target = target.normalized;
        GameObject fBall = Instantiate(fireball, transform.position + target + .5f * Vector3.up, Quaternion.LookRotation(target));
        FireballController fireballController = fBall.GetComponent<FireballController>();
        fireballController.SetValues(baseObj, player, this);
        fBall.GetComponent<Rigidbody>().velocity = fireballSpeed * (target + .3f * Vector3.up);
        fBall.SetActive(true);
    }

    public void DamageBase()
    {
        generalManager.DamageBase();                                                       
    }

    public void DamagePlayer()
    {
        generalManager.DamagePlayer();                                                               
    }
}
