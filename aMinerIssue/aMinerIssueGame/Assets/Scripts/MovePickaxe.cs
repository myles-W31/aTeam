using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePickaxe : MonoBehaviour
{
    public bool isBeingHeld;
    // Start is called before the first frame update
    void Start()
    {
        isBeingHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isBeingHeld = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
        }

        if (isBeingHeld)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (pos);
        }
    }
    /*void OnMouseDown()
    {
        isBeingHeld = true;
    }*/
}
