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

        maxPlayerHealth = 1;
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerHealth);
        /*
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;
        }

        transform.position = pos;
        */

        // walking right, left, or standing still
        if (Input.GetKey(right)||Input.GetKey(rightAlt))
        {
            thePlayerRB.velocity = new Vector3(moveSpeed, thePlayerRB.velocity.y, 0f);
            transform.localScale = new Vector3(.25f, .25f, .25f);
            facingRight = true;
            facingLeft = false;
        }
        else if (Input.GetKey(left)||Input.GetKey(leftAlt))
        {
            thePlayerRB.velocity = new Vector3(-moveSpeed, thePlayerRB.velocity.y, 0f);
            transform.localScale = new Vector3(-.25f, .25f, .25f);
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
        if (Input.GetKeyDown(attack))
        {
            BasicAttack();

            StartCoroutine(ShootDelay());
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
        }
        else if (facingLeft)
        {
            var attackInst = Instantiate(attackRB, new Vector3(thePlayerRB.position.x, thePlayerRB.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            attackInst.velocity = new Vector2(-attackSpeed, 0);
            Physics2D.IgnoreCollision(attackInst.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
