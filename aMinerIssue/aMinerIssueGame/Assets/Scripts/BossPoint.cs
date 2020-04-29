using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoint : MonoBehaviour
{
    public bool bossStarts;

    // Start is called before the first frame update
    void Start()
    {
        bossStarts = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bossStarts = true;
        }
    }
}
