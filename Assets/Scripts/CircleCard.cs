using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCard : Card
{
    void Start()
    {
        realStart();
        type = Type.RequiredLocation;
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

    public override void Effect(Vector3 location)
    {
        Debug.Log("s");
    }

}
