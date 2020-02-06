using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class manager : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject playerExplosion;

    public enemyMovement theEnemyMovement;
    public characterMovement theCharacterMovement;
    public MovePickaxe theMovePickaxe;

    public bool respawning;
    public bool respawnCoActive;
    public float waitToRespawn;

    public GameObject dirt;
    public Text pointText;

    public int mainScore;

    LevelLoader LevelLoader;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        theEnemyMovement = FindObjectOfType<enemyMovement>();
        LevelLoader = FindObjectOfType<LevelLoader>();
        theMovePickaxe = FindObjectOfType<MovePickaxe>();

        SpriteRenderer dirtdim = dirt.GetComponent<SpriteRenderer>();
        for (int i = 0; i<10;i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Vector3 pos = dirt.transform.position;
                pos.x = dirtdim.size.x * i;
                pos.y = dirtdim.size.y * j;
                Instantiate(dirt,pos, Quaternion.identity);
            }
        }
        //mainScore = PlayerPrefs.GetInt("Player Score");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainScore);
        
        /*if(theEnemyMovement.theEnemy != null)
        {
            if (theEnemyMovement.enemyHealth <= 0)
            {
                Instantiate(enemyExplosion, theEnemyMovement.theEnemy.transform.position, theEnemyMovement.theEnemy.transform.rotation);
            }
        }*/

        if (theCharacterMovement.playerHealth <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
            theCharacterMovement.isBeingHeld = false;
            //theCharacterMovement.score = 0;
            mainScore = 0;
            pointText.text = "Score: " + mainScore;
        }
        //PlayerPrefs.SetInt("Player Score", mainScore);
    }

    public void AddCoins(int pointsToAdd)
    {
        mainScore += pointsToAdd;

        pointText.text = "Score: " + mainScore;
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
        //SceneManager.LoadScene(mainMenu);
    }

    public void HurtPlayer(int damageToTake)
    {
            theCharacterMovement.playerHealth -= damageToTake;
    }
}
