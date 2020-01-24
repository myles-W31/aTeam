using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject playerExplosion;

    public enemyMovement theEnemyMovement;
    public characterMovement theCharacterMovement;

    private bool respawning;
    public bool respawnCoActive;
    public float waitToRespawn;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        theEnemyMovement = FindObjectOfType<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theEnemyMovement.theEnemy != null)
        {
            if (theEnemyMovement.enemyHealth <= 0)
            {
                Instantiate(enemyExplosion, theEnemyMovement.theEnemy.transform.position, theEnemyMovement.theEnemy.transform.rotation);
            }
        }

        if (theCharacterMovement.playerHealth <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;

            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        respawnCoActive = true;

        Instantiate(playerExplosion, theCharacterMovement.transform.position, theCharacterMovement.transform.rotation);

        theCharacterMovement.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        respawnCoActive = false;

        theCharacterMovement.playerHealth = theCharacterMovement.maxPlayerHealth;
        respawning = false;

        theCharacterMovement.transform.position = theCharacterMovement.respawnPosition;
        theCharacterMovement.gameObject.SetActive(true);
    }

    public void HurtPlayer(int damageToTake)
    {
            theCharacterMovement.playerHealth -= damageToTake;
    }
}
