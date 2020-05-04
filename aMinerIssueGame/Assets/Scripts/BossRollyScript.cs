using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollyScript : MonoBehaviour
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
    public Vector2 bossSpawn;

    public manager theManager;
    public characterMovement theCharacterMovement;
    public BossPoint theBossPoint;
    public GameObject bossPoint1;
    public GameObject bossPoint2;
    public GameObject bossPoint3;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 3;
        enemyHealth = maxEnemyHealth;
        bossSpawn = this.transform.position;

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
        theBossPoint = FindObjectOfType<BossPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(theEnemy.gameObject);
            Instantiate(theManager.spiderBossExplosion, this.transform.position, theEnemy.transform.rotation);
        }

        float distance = Mathf.Abs(enemyRigid.gameObject.transform.position.x - theCharacterMovement.thePlayer.transform.position.x);

        if (theBossPoint.bossStarts || seen)
        {
            seen = true;

            if (movingRight)
            {
                facingRight = true;
                facingLeft = false;

                canTurnLeft = true;
                canTurnRight = false;

                enemyRigid.velocity = new Vector3(5f, enemyRigid.velocity.y, 0f);
                transform.localScale = new Vector3(.2f, .2f, 1f);
            }
            else if (movingLeft)
            {
                facingRight = false;
                facingLeft = true;

                canTurnLeft = false;
                canTurnRight = true;

                enemyRigid.velocity = new Vector3(-5f, enemyRigid.velocity.y, 0f);
                transform.localScale = new Vector3(-.2f, .2f, 1f);
            }
        }
    }

    public void HurtEnemyMethod(BossRollyScript objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "InvisibleGround")
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
