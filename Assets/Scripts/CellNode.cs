using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNode : MonoBehaviour
{
    public void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void OnMouseExit()
    {
       GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseDown()
    {
       GetComponentInParent<GridBoard>().SetLocation(transform.position);
       // statemachine readytoplay
    }
}
