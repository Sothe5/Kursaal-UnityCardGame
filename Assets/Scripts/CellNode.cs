using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNode : MonoBehaviour
{
    public void OnMouseEnter()
    {
        if(StateMachine.currentState == StateMachine.State.SelectingTargetSummoning)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void OnMouseExit()
    {
       if(StateMachine.currentState == StateMachine.State.SelectingTargetSummoning)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnMouseDown()
    {
        if(StateMachine.currentState == StateMachine.State.SelectingTargetSummoning)
        {
            GetComponentInParent<GridBoard>().SetLocation(transform.position);
            GetComponent<SpriteRenderer>().enabled = false;
            StateMachine.currentState = StateMachine.State.ReadyToPlayCard;
        }
    }
}
