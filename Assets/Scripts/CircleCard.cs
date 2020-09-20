using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCard : Card
{
    void Start()
    {
        realStart();
    }

    void FixedUpdate()
    {
        realUpdate();
    }

    void OnMouseDown()
    {
       realOnMouseDown();
    }

    void OnMouseUp()
    {
       realOnMouseUp();
    }

    void OnMouseOver()
    {
        realOnMouseOver();
    }

    public void OnMouseEnter()
    {
        realOnMouseEnter();
    }

    public void OnMouseExit()
    {
       realOnMouseExit();
    }

    public override void Effect()
    {
        Debug.Log("s");
    }

}
