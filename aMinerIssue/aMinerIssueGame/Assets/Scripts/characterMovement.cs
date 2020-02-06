using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    // player speed variables
    BoxCollider2D myCollider;
    public float normalSpeed;
    public float moveSpeed;

    // settable player key controls
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode leftAlt;
    public KeyCode rightAlt;
    public KeyCode jumpAlt;
    public KeyCode attack;

    public Rigidbody2D thePlayerRB;
    public GameObject thePlayer;
    public Rigidbody2D attackRB;

    public float jumpSpeed;
    public float normalJumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    public bool facingRight;
    public bool facingLeft;

    // attack variables
    private float attackSpeed;
    public float normalAttackSpeed;

    public float playerHealth;
    public float maxPlayerHealth;
    public Vector3 respawnPosition;

    public bool canShoot;
    public bool canPickUpObject;
    public bool canDig;

    public bool shotLeft;
    public bool shotRight;

    public GameObject diggingProjectile;
    public bool isBeingHeld;
    public bool isWalking;

    public Animator myAnim;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        //myCollider = GetComponent<BoxCollider2D>();
        thePlayerRB = GetComponent<Rigidbody2D>();

        moveSpeed = normalSpeed;
        jumpSpeed = normalJumpSpeed;
        attackSpeed = normalAttackSpeed;

        respawnPosition = transform.position;

        facingRight = true;
        facingLeft = false;

        shotLeft = false;
        shotRight = false;

        maxPlayerHealth = 1;
        playerHealth = maxPlayerHealth;

        canShoot = true;
        canDig = true;
        canPickUpObject = false;

        isBeingHeld = false;
        isWalking = false;

        diggingProjectile.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // walking right, left, or standing still
        if (Input.GetKey(right)||Input.GetKey(rightAlt))
        {
            thePlayerRB.velocity = new Vector3(moveSpeed, thePlayerRB.velocity.y, 0f);
            transform.localScale = new Vector3(2f, 2f, 2f);
            facingRight = true;
            facingLeft = false;
            if(isGrounded)
            {
                isWalking = true;
            }
            if(!isGrounded)
            {
                isWalking = false;
            }
        }
        else if (Input.GetKey(left)||Input.GetKey(leftAlt))
        {
            thePlayerRB.velocity = new Vector3(-moveSpeed, thePlayerRB.velocity.y, 0f);
            transform.localScale = new Vector3(-2f, 2f, 2f);
            facingRight = false;
            facingLeft = true;
            if (isGrounded)
            {
                isWalking = true;
            }
            if (!isGrounded)
            {
                isWalking = false;
            }
        }
        else
        {
            thePlayerRB.velocity = new Vector3(0f, thePlayerRB.velocity.y, 0f);
            isWalking = false;
        }

        // jumping
        if (((Input.GetKeyDown(jump))||Input.GetKeyDown(jumpAlt)) && isGrounded)
        {
            thePlayerRB.velocity = new Vector3(thePlayerRB.velocity.x, jumpSpeed, 0f);
            isWalking = false;
        }

        // attacking
        if (Input.GetKeyDown(attack) && canShoot)
        {
            BasicAttack();
            StartCoroutine(ShootDelay());
            canShoot = false;
            canDig = false;
            StartCoroutine(ShootDelay());
        }

        if (canDig)
        {
            if (Input.GetMouseButton(0))
            {
                isBeingHeld = true;
                canShoot = false;
                canDig = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isBeingHeld = false;
                canShoot = true;
            }

            if (isBeingHeld)
            {
                //Instantiate(diggingProjectile, this.transform.position, this.transform.rotation);
                diggingProjectile.SetActive(true);
            }
            else
            {
                diggingProjectile.SetActive(false);
            }
        }

        // check if player is on ground constantly
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        myAnim.SetBool("canShoot", canShoot);
        myAnim.SetBool("isWalking", isWalking);
        myAnim.SetBool("isGrounded", isGrounded);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(5f);

        /*Debug.Log("pick up");
        Destroy(diggingProjectile);
        canShoot = true;
        canDig = true;
        canPickUpObject = false;*/
    }

    private void BasicAttack()
    {
        if (facingRight)
        {
            var attackInst = Instantiate(attackRB, new Vector3(thePlayerRB.position.x + 0.5f, thePlayerRB.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            attackInst.velocity = new Vector2(attackSpeed, 0);
            Physics2D.IgnoreCollision(attackInst.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            shotRight = true;
            shotLeft = false;
        }
        else if (facingLeft)
        {
            var attackInst = Instantiate(attackRB, new Vector3(thePlayerRB.position.x - 0.5f, thePlayerRB.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            attackInst.velocity = new Vector2(-attackSpeed, 0);
            Physics2D.IgnoreCollision(attackInst.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            shotRight = false;
            shotLeft = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickaxe" && canPickUpObject == true)
        {
            Debug.Log("pick up");
            Destroy(collision.gameObject);
            canShoot = true;
            canDig = true;
            canPickUpObject = false;
        }
        else if (collision.tag == "Pickaxe" && canPickUpObject == false)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
    }
}
