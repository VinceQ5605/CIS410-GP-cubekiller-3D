using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoximonEnemyController : MonoBehaviour
{
    public GameObject baseObj;
    public float fireCoolDown;
    public float range; // how close a target needs to be to trigger an attack (square of the horizontal distance)
    public GameObject player;

    private EnemyController enemyController;
    private Animator animator;
    private float fireCDRemaining;
    private int enemyType;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        animator.SetBool("Jump", true);
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
        //Debug.Log(direction.ToString());
    }

    private void Update()
    {
        fireCDRemaining -= Time.deltaTime;
        if (enemyType == 0)
        {
            Enemy1Update();
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
            return playerDelta;
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
        if (fireCDRemaining < 0  &&  target != Vector3.zero)
        {
            fireCDRemaining = fireCoolDown;
            Fire1();
        }
    }

    public void Fire1()
    {
        Debug.Log("Firing");
    }
}
