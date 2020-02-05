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

    public bool shotLeft;
    public bool shotRight;

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
        canPickUpObject = false;
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
        }
        else if (Input.GetKey(left)||Input.GetKey(leftAlt))
        {
            thePlayerRB.velocity = new Vector3(-moveSpeed, thePlayerRB.velocity.y, 0f);
            transform.localScale = new Vector3(-2f, 2f, 2f);
            facingRight = false;
            facingLeft = true;
        }
        else
        {
            thePlayerRB.velocity = new Vector3(0f, thePlayerRB.velocity.y, 0f);
        }

        // jumping
        if (((Input.GetKeyDown(jump))||Input.GetKeyDown(jumpAlt)) && isGrounded)
        {
            thePlayerRB.velocity = new Vector3(thePlayerRB.velocity.x, jumpSpeed, 0f);
        }

        // attacking
        if (Input.GetKeyDown(attack) && canShoot)
        {
            BasicAttack();
            StartCoroutine(ShootDelay());
            canShoot = false;
            //StartCoroutine(ShootDelay());
        }

        // check if player is on ground constantly
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void BasicAttack()
    {
        if (facingRight)
        {
            var attackInst = Instantiate(attackRB, new Vector3(thePlayerRB.position.x, thePlayerRB.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            attackInst.velocity = new Vector2(attackSpeed, 0);
            Physics2D.IgnoreCollision(attackInst.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            shotRight = true;
            shotLeft = false;
        }
        else if (facingLeft)
        {
            var attackInst = Instantiate(attackRB, new Vector3(thePlayerRB.position.x, thePlayerRB.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
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
            canPickUpObject = false;
        }
        else if (collision.tag == "Pickaxe" && canPickUpObject == false)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
    }
}
