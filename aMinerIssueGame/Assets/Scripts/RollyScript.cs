using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyScript : MonoBehaviour
{
    public GameObject theEnemy;
    public float enemyHealth;
    private float maxEnemyHealth;
    public bool canTurnRight;
    public bool canTurnLeft;

    public bool movingRight;
    public bool movingLeft;
    public bool facingRight;
    public bool facingLeft;
    public bool seen;
    public float distance;

    public Rigidbody2D enemyRigid;
    public float moveSpeed;

    public manager theManager;
    public characterMovement theCharacterMovement;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 1;
        enemyHealth = maxEnemyHealth;

        canTurnRight = false;
        canTurnLeft = false;
        movingLeft = true;
        movingRight = false;
        facingRight = false;
        facingLeft = true;
        seen = false;

        enemyRigid = GetComponent<Rigidbody2D>();
        theManager = FindObjectOfType<manager>();
        theCharacterMovement = FindObjectOfType<characterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(theEnemy.gameObject);
            Instantiate(theManager.enemyExplosion, theEnemy.transform.position, theEnemy.transform.rotation);
        }

        distance = Mathf.Abs(enemyRigid.gameObject.transform.position.x - theCharacterMovement.thePlayer.transform.position.x);

        if (movingRight)
        {
            facingRight = true;
            facingLeft = false;

            canTurnLeft = true;
            canTurnRight = false;

            enemyRigid.velocity = new Vector3(moveSpeed, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(0.2f, 0.2f, 0f);
        }
        else if (movingLeft)
        {
            facingRight = false;
            facingLeft = true;

            canTurnLeft = false;
            canTurnRight = true;

            enemyRigid.velocity = new Vector3(-moveSpeed, enemyRigid.velocity.y, 0f);
            transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
        }
    }

    public void HurtEnemyMethod(enemyMovement objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other. == "Ground" || other.tag == "InvisibleGround")
        if(other.GetComponent<Rigidbody2D>())
        {
            if (movingLeft && canTurnRight)
            {
                movingRight = true;
                movingLeft = false;
            }
            else if (movingRight && canTurnLeft)
            {
                movingRight = false;
                movingLeft = true;
            }
        }
    }
}
