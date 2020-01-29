using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public GameObject theEnemy;
    public float enemyHealth;
    private float maxEnemyHealth;
    public GameObject pointA;
    public GameObject pointB;
    bool moveRight;

    public Rigidbody2D enemyRigid;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 1;
        enemyHealth = maxEnemyHealth;
        moveRight = false;
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(theEnemy.gameObject);
        }

        //if moveRight is false, move to point A
        if (moveRight == false)
        {
            enemyRigid.velocity = new Vector3(-moveSpeed, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }
        //check Location
        turnRight(transform.position, pointA.transform.position, pointB.transform.position);
        //if moveRight is true, move to point B
        if (moveRight == true)
        {
            enemyRigid.velocity = new Vector3(moveSpeed, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(-.5f, .5f, .5f);
        }
    }

    public void HurtEnemyMethod(enemyMovement objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    public void turnRight(Vector3 enemyLocation, Vector3 pointALocation, Vector3 pointBLocation)
    {
        //check point A first
        float check = Vector3.Distance(pointALocation, enemyLocation);
        if (check <= 2)
        {
            moveRight = true;
        }
        check = Vector3.Distance(pointBLocation, enemyLocation);
        if (check <= 2)
        {
            moveRight = false;
        }
    }
}
