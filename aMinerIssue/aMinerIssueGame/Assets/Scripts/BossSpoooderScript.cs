using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpoooderScript : MonoBehaviour
{
    public GameObject theEnemy;
    public float enemyHealth;
    private float maxEnemyHealth;
    //bool moveRight;
    public bool canTurnRight;
    public bool canTurnLeft;

    public bool movingRight;
    public bool movingLeft;
    public bool facingRight;
    public bool facingLeft;

    public Rigidbody2D enemyRigid;
    public float moveSpeed;

    public manager theManager;
    public characterMovement theCharacterMovement;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 1;
        enemyHealth = maxEnemyHealth;
        //moveRight = false;
        movingLeft = true;
        movingRight = false;
        facingRight = false;
        facingLeft = true;
        enemyRigid = GetComponent<Rigidbody2D>();
        theManager = FindObjectOfType<manager>();
        theCharacterMovement = FindObjectOfType<characterMovement>();
        canTurnRight = false;
        canTurnLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(theEnemy.gameObject);
            Instantiate(theManager.enemyExplosion, theEnemy.transform.position, theEnemy.transform.rotation);
            //theManager.AddCoins(100);
        }

        if (movingRight)
        {
            facingRight = true;
            facingLeft = false;
            canTurnLeft = true;
            canTurnRight = false;
            enemyRigid.velocity = new Vector3(5f, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(movingLeft)
        {
            facingRight = false;
            facingLeft = true;
            canTurnLeft = false;
            canTurnRight = true;
            enemyRigid.velocity = new Vector3(-5f, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(1f,  1f, 1f);
        }
    }

    public void HurtEnemyMethod(enemyMovement objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("hey");
            /*if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }*/
            if(movingLeft && canTurnRight)
            {
                movingRight = true;
                movingLeft = false;
                Debug.Log("right");
                //enemyRigid.velocity = new Vector3(5f, enemyRigid.velocity.y, 0f);
                //transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (movingRight && canTurnLeft)
            {
                movingRight = false;
                movingLeft = true;
                //canTurn = false;
                Debug.Log("left");
            }
        }
    }
}
