using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePickaxe : MonoBehaviour
{
    public bool isBeingHeld;
    public characterMovement theCharacterMovement;
    public manager theManager;

    // Start is called before the first frame update
    void Start()
    {
        theCharacterMovement = FindObjectOfType<characterMovement>();
        theManager = FindObjectOfType<manager>();
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

        Vector3 v = this.gameObject.transform.position - theCharacterMovement.gameObject.transform.position;
        v = Vector3.ClampMagnitude(v, 1.5f);
        this.gameObject.transform.position = theCharacterMovement.gameObject.transform.position + v;
    }
}
