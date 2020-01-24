using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public GameObject theEnemy;
    public float enemyHealth;
    private float maxEnemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHealth = 1;
        enemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(theEnemy.gameObject);
        }
    }

    public void HurtEnemyMethod(enemyMovement objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }
}
