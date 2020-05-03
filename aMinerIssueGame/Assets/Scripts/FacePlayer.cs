using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public characterMovement theCharacterMovement;
    public bool facingLeft;
    public bool facingRight;
    public float xScale;
    public float yScale;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        facingLeft = false;
        facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        // determines movement
        if (this.GetComponent<Rigidbody2D>().transform.position.x > theCharacterMovement.thePlayer.transform.position.x)
        {
            transform.localScale = new Vector3(-xScale, yScale, 0f);
            facingLeft = true;
            facingRight = false;
        }
        else
        {
            transform.localScale = new Vector3(xScale, yScale, 0f);
            facingRight = true;
            facingLeft = false;
        }
    }
}
