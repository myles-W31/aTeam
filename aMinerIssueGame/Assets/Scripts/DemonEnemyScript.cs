using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemyScript : MonoBehaviour
{
    public GameObject theEnemy;
    public float enemyHealth;
    private float maxEnemyHealth;

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
        maxEnemyHealth = 2;
        enemyHealth = maxEnemyHealth;

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
            Instantiate(theManager.demonExplosion, theEnemy.transform.position, theEnemy.transform.rotation);
        }

        distance = Mathf.Abs(enemyRigid.gameObject.transform.position.x - theCharacterMovement.thePlayer.transform.position.x);    
        
        if(distance <= 8)
        {
            float step = moveSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, theCharacterMovement.thePlayer.transform.position, step);
        }
    }

    public void HurtEnemyMethod(enemyMovement objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "InvisibleGround")
        {
            
        }
    }
}