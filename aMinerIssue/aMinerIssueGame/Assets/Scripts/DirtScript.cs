using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScript : MonoBehaviour
{
    public GameObject dirtExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(dirtExplosion, this.transform.position, this.transform.rotation);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DiggingPickaxe")
        {
            Destroy(gameObject);
            Instantiate(dirtExplosion, this.transform.position, this.transform.rotation);
        }
    }
}
