using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall1 : MonoBehaviour
{
    public BossPoint theBossPoint;

    // Start is called before the first frame update
    void Start()
    {
        theBossPoint = FindObjectOfType<BossPoint>();

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(theBossPoint.bossStarts)
        {
            Debug.Log("close");
            this.gameObject.SetActive(true);
        }
    }
}
