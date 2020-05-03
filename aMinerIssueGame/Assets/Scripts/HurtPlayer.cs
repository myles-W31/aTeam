using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private manager theLevelManager;
    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelManager.HurtPlayer(damageToGive);
        }
    }
}
