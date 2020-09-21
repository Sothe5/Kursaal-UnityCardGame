using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum Type {RequiredLocation, NothingRequired};
    public bool isPlayer1Owner;
    public bool isDragging;
    public bool shouldBeScaledDown;
    public bool hasCursorExitedCardTrigger;

    private int orderInLayerScale1;
    private int orderInLayerScale2;
    private int orderInLayerScale3;
    private int orderInLayerScale4;

    public Vector3 basePositionCard;
    public Vector3 basePositionCardKeepZ;
    public Vector3 scaledPositionCard;

    public Type type;

    public void realStart()
    {
        this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        basePositionCard = transform.position;
        basePositionCardKeepZ = transform.position + new Vector3(0,0,-0.1f);
        scaledPositionCard = isPlayer1Owner ? transform.position + new Vector3(0,1,-0.1f) : transform.position + new Vector3(0,-1,-0.1f);
    }

    public void realUpdate()
    {
        if(isDragging)
        {            
            Vector3 distance = transform.position - Camera.main.transform.position;
            
            float realDist = Vector3.Dot(distance, Camera.main.transform.forward);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, realDist));
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    public void realOnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && !(isPlayer1Owner ^ TurnManager.isPlayer1Turn) && StateMachine.currentState == StateMachine.State.Base)
        {
            this.gameObject.GetComponentInParent<Hand>().cardBeingDragged = this;
            StateMachine.currentState = StateMachine.State.DraggingCard;
            isDragging = true;
            cardScaleDown(false);
            //Debug.Log("mouse down");
        }
    }

    public void realOnMouseUp()
    {
        if(Input.GetMouseButtonUp(0) && !(isPlayer1Owner ^ TurnManager.isPlayer1Turn) && StateMachine.currentState == StateMachine.State.DraggingCard)
        {
            this.gameObject.GetComponentInParent<Hand>().cardBeingDragged.isDragging = false;
            if(!this.GetComponentsInChildren<SpriteRenderer>()[0].enabled)
            {
                this.gameObject.GetComponentInParent<Hand>().SetTheNewPositionsOfCards();
                StateMachine.currentState = StateMachine.State.Base;  

                if(!hasCursorExitedCardTrigger)
                {
                this.gameObject.GetComponentInParent<Hand>().cardBeingDragged.cardScaleUp();
                }     
            }
            else
            {
                StateMachine.currentState = StateMachine.State.PlayingCard;
            }
            //Debug.Log("mouse up");
        }
    }

    public void realOnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && StateMachine.currentState == StateMachine.State.DraggingCard)
        {
            this.gameObject.GetComponentInParent<Hand>().cardBeingDragged.isDragging = false;
            StateMachine.currentState = StateMachine.State.Base;
            this.gameObject.GetComponentInParent<Hand>().SetTheNewPositionsOfCards();

            if(!hasCursorExitedCardTrigger)
            {
                cardScaleUp();
            }
            //Debug.Log("rightClick");
        }
    }

    public void realOnMouseEnter()
    {
        hasCursorExitedCardTrigger = false;
        if(StateMachine.currentState == StateMachine.State.Base)
        {
            //Debug.Log("MouseEnter");
            cardScaleUp();
        }
    }

    public void realOnMouseExit()
    {   
        hasCursorExitedCardTrigger = true;
        if(StateMachine.currentState == StateMachine.State.Base && shouldBeScaledDown)
        {
            //Debug.Log("MouseExited");
            cardScaleDown(true);
        }
    }

    private void cardScaleUp()
    {
        shouldBeScaledDown = true;
        transform.position = scaledPositionCard;
        
        orderInLayerScale1 = GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder;
        orderInLayerScale2 = GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder;
        orderInLayerScale3 = GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder;
        orderInLayerScale4 = GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder;
        
        GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder = 1000;
        GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = 1001;
        GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = 1002;
        GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = 1003;   
    }

    private void cardScaleDown(bool shouldKeepOrderInlayer)
    {
        shouldBeScaledDown = false;
        transform.position = basePositionCard;

        if(shouldKeepOrderInlayer)
        {
            transform.position = basePositionCardKeepZ;
            GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder = orderInLayerScale1;
            GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = orderInLayerScale2;
            GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = orderInLayerScale3;
            GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = orderInLayerScale4;
        }
    }
    

    public virtual void Effect(Vector3 location)
    {
        
    }

}
