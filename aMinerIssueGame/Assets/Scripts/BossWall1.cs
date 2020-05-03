using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall1 : MonoBehaviour
{
    public BossPoint theBossPoint;
    public GameObject theBossWall;

    // Start is called before the first frame update
    void Start()
    {
        theBossPoint = FindObjectOfType<BossPoint>();

        theBossWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(theBossPoint.bossStarts == true)
        {
            theBossWall.SetActive(true);
        }
    }
}
