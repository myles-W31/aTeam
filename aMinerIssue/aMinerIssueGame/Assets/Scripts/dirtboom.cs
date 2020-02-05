using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtboom : MonoBehaviour
{
    public float lifeTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
