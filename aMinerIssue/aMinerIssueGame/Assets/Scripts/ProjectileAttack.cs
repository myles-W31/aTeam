using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public enemyMovement theEnemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        theEnemyMovement = FindObjectOfType<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
            theEnemyMovement.HurtEnemyMethod(collision.GetComponent<enemyMovement>(), 0.5f);
        }
    }
}
