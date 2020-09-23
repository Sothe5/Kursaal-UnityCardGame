using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionCard : Card
{
     void Start()
    {
        realStart();
        type = Type.RequiredLocation;
        isPermanent = true;
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
       //model = Instantiate(cubeModel, new Vector3(location.x, location.y, location.z -0.3f), FindObjectOfType<GridBoard>().transform.rotation, FindObjectOfType<GridBoard>().transform) as GameObject;
    }

}
