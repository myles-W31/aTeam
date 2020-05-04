using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject demonExplosion;
    public GameObject rollyExplosion;
    public GameObject playerExplosion;
    public GameObject spiderBossExplosion;

    public enemyMovement theEnemyMovement;
    public characterMovement theCharacterMovement;
    public MovePickaxe theMovePickaxe;
    public BossWall1 theBossWall1;
    public BossSpoooderScript theBossSpoooderScript;
    public BossRollyScript theBossRollyScript;
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

    public bool isPaused = false;
    public GameObject pauseUI;

    public SpriteRenderer theBossSprite;
    public SpriteRenderer theRollyBossSprite;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        theEnemyMovement = FindObjectOfType<enemyMovement>();
        LevelLoader = FindObjectOfType<LevelLoader>();
        theMovePickaxe = FindObjectOfType<MovePickaxe>();
        theBossWall1 = FindObjectOfType<BossWall1>();
        theBossSpoooderScript = FindObjectOfType<BossSpoooderScript>();
        theBossRollyScript = FindObjectOfType<BossRollyScript>();
        theBossPoint = FindObjectOfType<BossPoint>();

        attackCountdown = false;
        attackTime = 0;
        currentScene = SceneManager.GetActiveScene();


        isPaused = false;

        /*
        //SpriteRenderer dirtdim = dirt.GetComponent<SpriteRenderer>();
        for (int i = 0; i<10;i++)
        {
            for(int j = 0; j < 10; j++)
            {
                //Vector3 pos = dirt.transform.position;
                //pos.x = dirtdim.size.x * i;
                //pos.y = dirtdim.size.y * j;
                //Instantiate(dirt,pos, Quaternion.identity);
            }
        }
        */

        mainScore = PlayerPrefs.GetInt("Player Score");
        mainScore = 0;
        pointText.text = "Score: " + mainScore;
    }

    // Update is called once per frame
    void Update()
    {
        // Transistion from game to pause menu
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (attackTime > 0)
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
            pointText.text = "Score: " + mainScore;
        }

        if(currentScene.name == "BossLevel1" && theBossSpoooderScript.enemyHealth > 0)
        {
            //bossMusic.Play();
        }
        else if (currentScene.name == "BossLevel1" && theBossSpoooderScript.enemyHealth <= 0)
        {
            //bossMusic.Stop();
        }

        PlayerPrefs.SetInt("Player Score", mainScore);
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

        if (currentScene.name == "BossLevel1")
        {
            theBossPoint.bossStarts = false;
            theBossSpoooderScript.seen = false;
            theBossSpoooderScript.theEnemy.gameObject.transform.position = theBossSpoooderScript.bossSpawn;
            theBossSpoooderScript.theEnemy.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            theBossSpoooderScript.enemyRigid.velocity = Vector3.zero;
            theBossWall1.theBossWall.SetActive(false);

            theBossSpoooderScript.enemyHealth = 8;
            theCharacterMovement.canShoot = true;
            theCharacterMovement.canDig = true;
            theCharacterMovement.canPickUpObject = false;
        }
        if (currentScene.name == "BossLevel2")
        {
            theBossPoint.bossStarts = false;
            theBossRollyScript.seen = false;
            theBossRollyScript.theEnemy.gameObject.transform.position = theBossRollyScript.bossSpawn;
            theBossRollyScript.theEnemy.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            theBossRollyScript.enemyRigid.velocity = Vector3.zero;
            theBossWall1.theBossWall.SetActive(false);

            theBossRollyScript.enemyHealth = 8;
            theCharacterMovement.canShoot = true;
            theCharacterMovement.canDig = true;
            theCharacterMovement.canPickUpObject = false;
        }

        theCharacterMovement.gameObject.SetActive(true);
    }

    public void HurtPlayer(int damageToTake)
    {
            theCharacterMovement.playerHealth -= damageToTake;
    }

    public void Pause()
    {
        isPaused = true;

        Time.timeScale = 0.0f;
        //music.volume = 0.1f;

        pauseUI.SetActive(true);
    }

    public void Unpause()
    {
        isPaused = false;

        Time.timeScale = 1.0f;
        //music.volume = 0.1f;

        pauseUI.SetActive(false);
    }

    public void Exit()
    {
        Unpause();
        mainScore = 0;
        SceneManager.LoadScene(mainMenu);
    }

    public void FlashRed(SpriteRenderer objectToHurt)
    {
        objectToHurt.color = Color.red;

        StartCoroutine(ExecuteAfterTime(objectToHurt));
    }

    public void ResetColor(SpriteRenderer objectToHurt)
    {
        if (objectToHurt != null)
        {
            objectToHurt.color = theBossSprite.color;
        }
    }

    IEnumerator ExecuteAfterTime(SpriteRenderer objectToAffect)
    {
        yield return new WaitForSeconds(0.25f);
        ResetColor(objectToAffect);
    }
}
