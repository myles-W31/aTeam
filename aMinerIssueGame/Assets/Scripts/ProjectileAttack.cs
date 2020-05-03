using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public enemyMovement theEnemyMovement;
    public DemonEnemyScript theDemonEnemyScript;
    public BossSpoooderScript theBossSpoooderScript;
    public manager theManager;

    public characterMovement theCharacterMovement;
    public Animator myAnim;
    public bool isOnGroundRight;
    public bool isOnGroundLeft;
    public PickaxePointScript pickaxePoint1;

    // Start is called before the first frame update
    void Start()
    {
        theEnemyMovement = FindObjectOfType<enemyMovement>();
        theDemonEnemyScript = FindObjectOfType<DemonEnemyScript>();
        theBossSpoooderScript = FindObjectOfType<BossSpoooderScript>();
        pickaxePoint1 = FindObjectOfType<PickaxePointScript>();
        theManager = FindObjectOfType<manager>();

        theCharacterMovement = FindObjectOfType<characterMovement>();
        isOnGroundLeft = false;
        isOnGroundRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, 0.5f);
        myAnim.SetBool("isOnGroundRight", isOnGroundRight);
        myAnim.SetBool("isOnGroundLeft", isOnGroundLeft);

        if(theCharacterMovement.canShoot)
        {
            Debug.Log("destroy");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Destroy(gameObject);
            if(collision.gameObject.GetComponent<enemyMovement>())
            {
                theEnemyMovement.HurtEnemyMethod(collision.GetComponent<enemyMovement>(), 0.5f);
            }
            else if (collision.gameObject.GetComponent<DemonEnemyScript>())
            {
                theDemonEnemyScript.HurtEnemyMethod(collision.GetComponent<DemonEnemyScript>(), 0.5f);
            }
            else if (collision.gameObject.GetComponent<BossSpoooderScript>())
            {
                theBossSpoooderScript.HurtEnemyMethod(collision.GetComponent<BossSpoooderScript>(), 0.5f);
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.gameObject.transform.position = pickaxePoint1.gameObject.transform.position;
                theManager.FlashRed(collision.GetComponent<SpriteRenderer>());
            }
        }
        if (collision.tag == "Ground")
        {
            //Destroy(gameObject);
            theCharacterMovement.canPickUpObject = true;

            if(theCharacterMovement.shotRight)
            {
                isOnGroundRight = true;
            }
            else if (theCharacterMovement.shotLeft)
            {
                isOnGroundLeft = true;
            }
        }
    }
}
