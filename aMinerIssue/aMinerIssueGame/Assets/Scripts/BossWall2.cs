using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall2 : MonoBehaviour
{
    public BossSpoooderScript theBossSpoooderScript;
    public GameObject theBossWall;

    // Start is called before the first frame update
    void Start()
    {
        theBossSpoooderScript = FindObjectOfType<BossSpoooderScript>();

        theBossWall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (theBossSpoooderScript.enemyHealth <= 0)
        {
            theBossWall.SetActive(false);
        }
    }
}
