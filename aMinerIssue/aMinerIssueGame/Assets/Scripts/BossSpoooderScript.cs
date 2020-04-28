using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpoooderScript : MonoBehaviour
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
            Instantiate(theManager.spiderBossExplosion, theEnemy.transform.position, theEnemy.transform.rotation);
        }

        float distance = Mathf.Abs(enemyRigid.gameObject.transform.position.x - theCharacterMovement.thePlayer.transform.position.x);

        if (distance <= 24 || seen)
        {
            seen = true; 

            if (movingRight)
            {
                facingRight = true;
                facingLeft = false;

                canTurnLeft = true;
                canTurnRight = false;

                enemyRigid.velocity = new Vector3(5f, enemyRigid.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (movingLeft)
            {
                facingRight = false;
                facingLeft = true;

                canTurnLeft = false;
                canTurnRight = true;

                enemyRigid.velocity = new Vector3(-5f, enemyRigid.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void HurtEnemyMethod(BossSpoooderScript objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "InvisibleGround")
        {
            if(movingLeft && canTurnRight)
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
