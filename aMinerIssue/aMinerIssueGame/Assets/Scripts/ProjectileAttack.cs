﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public enemyMovement theEnemyMovement;
    public characterMovement theCharacterMovement;
    public Animator myAnim;
    public bool isOnGroundRight;
    public bool isOnGroundLeft;

    // Start is called before the first frame update
    void Start()
    {
        theEnemyMovement = FindObjectOfType<enemyMovement>();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Destroy(gameObject);
            theEnemyMovement.HurtEnemyMethod(collision.GetComponent<enemyMovement>(), 0.5f);
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
