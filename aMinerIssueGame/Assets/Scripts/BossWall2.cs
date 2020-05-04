using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWall2 : MonoBehaviour
{
    public BossSpoooderScript theBossSpoooderScript;
    public BossRollyScript theBossRollyScript;
    public GameObject theBossWall;
    public Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        theBossSpoooderScript = FindObjectOfType<BossSpoooderScript>();
        theBossRollyScript = FindObjectOfType<BossRollyScript>();

        theBossWall.SetActive(true);
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name == "BossLevel1")
        {
            if (theBossSpoooderScript.enemyHealth <= 0)
            {
                theBossWall.SetActive(false);
            }
        }
        else if (currentScene.name == "BossLevel2")
        {
            if (theBossRollyScript.enemyHealth <= 0)
            {
                theBossWall.SetActive(false);
            }
        }
    }
}
