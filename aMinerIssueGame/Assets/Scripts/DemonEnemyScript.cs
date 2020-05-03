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
    public bool enraged;
    public GameObject fireball;
    public float fireballSpeed;
    public bool shot;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 1;
        enemyHealth = maxEnemyHealth;

        facingRight = false;
        facingLeft = true;
        seen = false;
        enraged = false;
        shot = true;

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
        
        if(distance <= 12 && distance >= 4)
        {
            float step = moveSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, theCharacterMovement.thePlayer.transform.position, step);
            Debug.Log("chase");
            enraged = true;
        }
        else if (distance < 4)
        {
            enraged = true;
        }
        else
        {
            enraged = false;
        }

        if(enraged)
        {
            ShootFireball();
        }
    }

    public void HurtEnemyMethod(DemonEnemyScript objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "InvisibleGround")
        {
            
        }
    }

    public void ShootFireball()
    {
        if(shot)
        {
            var cloneFireball = Instantiate(fireball, this.transform.position, this.transform.rotation);
            cloneFireball.GetComponent<Rigidbody2D>().velocity = (theCharacterMovement.thePlayer.transform.position - transform.position).normalized * fireballSpeed;
            shot = false;
            StartCoroutine(ExecuteAfterTime());
        }
    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(1f);
        ShootFireball();
        shot = true;
    }
}