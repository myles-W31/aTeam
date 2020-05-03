using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject demonExplosion;
    public GameObject playerExplosion;
    public GameObject spiderBossExplosion;

    public enemyMovement theEnemyMovement;
    public characterMovement theCharacterMovement;
    public MovePickaxe theMovePickaxe;
    public BossWall1 theBossWall1;
    public BossSpoooderScript theBossSpoooderScript;
    public BossPoint theBossPoint;

    public bool respawning;
    public bool respawnCoActive;
    public float waitToRespawn;

    public GameObject dirt;
    public Text pointText;

    public int mainScore;

    LevelLoader LevelLoader;
    public string mainMenu;

    public float attackTime;
    public bool attackCountdown;
    public Scene currentScene;

    public AudioSource bossMusic;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        theEnemyMovement = FindObjectOfType<enemyMovement>();
        LevelLoader = FindObjectOfType<LevelLoader>();
        theMovePickaxe = FindObjectOfType<MovePickaxe>();
        theBossWall1 = FindObjectOfType<BossWall1>();
        theBossSpoooderScript = FindObjectOfType<BossSpoooderScript>();
        theBossPoint = FindObjectOfType<BossPoint>();

        attackCountdown = false;
        attackTime = 0;
        currentScene = SceneManager.GetActiveScene();

        //SpriteRenderer dirtdim = dirt.GetComponent<SpriteRenderer>();
        for (int i = 0; i<10;i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Vector3 pos = dirt.transform.position;
                //pos.x = dirtdim.size.x * i;
                //pos.y = dirtdim.size.y * j;
                Instantiate(dirt,pos, Quaternion.identity);
            }
        }
        //mainScore = PlayerPrefs.GetInt("Player Score");
    }

    // Update is called once per frame
    void Update()
    {        
        if(attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        else if(attackTime <= 0 && attackCountdown)
        {
            attackTime = 0;
            Debug.Log("pick up");
            //Destroy(FindObjectOfType<ProjectileAttack>());
            theCharacterMovement.canShoot = true;
            theCharacterMovement.canDig = true;
            theCharacterMovement.canPickUpObject = false;
            attackCountdown = false;
        }
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
            //pointText.text = "Score: " + mainScore;
        }

        if(currentScene.name == "BossLevel1" && theBossSpoooderScript.enemyHealth > 0)
        {
            //bossMusic.Play();
        }
        else if (currentScene.name == "BossLevel1" && theBossSpoooderScript.enemyHealth <= 0)
        {
            //bossMusic.Stop();
        }
        //PlayerPrefs.SetInt("Player Score", mainScore);
    }

    public void AddCoins(int pointsToAdd)
    {
        mainScore += pointsToAdd;

        //pointText.text = "Score: " + mainScore;
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

        if (currentScene.name == "BossLevel1")
        {
            Debug.Log("hey");
            theBossPoint.bossStarts = false;
            theBossSpoooderScript.seen = false;
            theBossSpoooderScript.theEnemy.gameObject.transform.position = theBossSpoooderScript.bossSpawn;
            theBossSpoooderScript.theEnemy.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            theBossSpoooderScript.enemyRigid.velocity = Vector3.zero;
            theBossWall1.theBossWall.SetActive(false);
        }

        theCharacterMovement.gameObject.SetActive(true);
        //SceneManager.LoadScene(mainMenu);
    }

    public void HurtPlayer(int damageToTake)
    {
            theCharacterMovement.playerHealth -= damageToTake;
    }
}
