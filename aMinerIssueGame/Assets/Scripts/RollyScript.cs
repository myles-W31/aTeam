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

    public Animator myAnim;

    public bool movingRight;
    public bool movingLeft;
    public bool facingRight;
    public bool facingLeft;
    public bool seen;
    public float distance;
    public float vert;

    public Rigidbody2D enemyRigid;
    private float moveSpeed;
    public float normalMoveSpeed;
    public float fasterMoveSpeed;

    public manager theManager;
    public characterMovement theCharacterMovement;

    public bool isWalking;
    public bool isRolling;

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

        isWalking = true;
        isRolling = false;

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
            Instantiate(theManager.rollyExplosion, this.transform.position, this.transform.rotation);
        }

        distance = Mathf.Abs(enemyRigid.gameObject.transform.position.x - theCharacterMovement.thePlayer.transform.position.x);
        vert = Mathf.Abs(enemyRigid.gameObject.transform.position.y - theCharacterMovement.thePlayer.transform.position.y);

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

        if (vert <= 1.9)
        {
            moveSpeed = fasterMoveSpeed;
            isWalking = false;
            isRolling = true;
        }
        else
        {
            moveSpeed = normalMoveSpeed;
            isWalking = true;
            isRolling = false;
        }

        if(isWalking)
        {

        }
        else if (isRolling)
        {

        }

        myAnim.SetBool("isRolling", isRolling);
        myAnim.SetBool("isWalking", isWalking);
    }

    public void HurtEnemyMethod(RollyScript objectToHurt, float damageToTake)
    {
        if(!isRolling)
        {
            objectToHurt.enemyHealth -= damageToTake;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other. == "Ground" || other.tag == "InvisibleGround")
        if(other.tag == "Ground" || other.tag == "InvisibleGround")
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
