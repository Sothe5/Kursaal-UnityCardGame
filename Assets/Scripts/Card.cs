using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isPlayer1Owner;

    public bool isDragging;
    public bool isAvailableToBePlayed;
    public bool shouldBeScaledDown;
    public bool hasCursorExitedCardTrigger;

    private int orderInLayerScale1;
    private int orderInLayerScale2;
    private int orderInLayerScale3;
    private int orderInLayerScale4;



    public void realStart()
    {
        this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
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
         bool ok = true;
        foreach(Card a in this.transform.root.GetComponentsInChildren<Hand>()[0].cards)
        {
            if(a.isDragging)
            {
                ok = false;
            }
        }
    
        foreach(Card a in this.transform.root.GetComponentsInChildren<Hand>()[1].cards)
        {
            if(a.isDragging)
            {
                ok = false;
            }
        }

        if(!(isPlayer1Owner ^ TurnManager.isPlayer1Turn) && ok)
        {
            isDragging = true;
            this.gameObject.GetComponentInParent<Hand>().cardBeingDragged = this;
            isAvailableToBePlayed = false;
            cardScaleDown(false);
        }
    }

    public void realOnMouseUp()
    {
        if(!(isPlayer1Owner ^ TurnManager.isPlayer1Turn) && isDragging && this.gameObject.GetComponentInParent<Hand>().cardBeingDragged == this)
        {
            isAvailableToBePlayed = true;

            if(!this.GetComponentsInChildren<SpriteRenderer>()[0].enabled)
            {
                this.gameObject.GetComponentInParent<Hand>().SetTheNewPositionsOfCards();
                isAvailableToBePlayed = false;
            }

            if(!hasCursorExitedCardTrigger)
            {
               cardScaleUp();
            }
            isDragging = false;
        }
    }

    public void realOnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && isDragging)
        {
            isDragging = false;
            isAvailableToBePlayed = false;
            this.gameObject.GetComponentInParent<Hand>().SetTheNewPositionsOfCards();      
        }
    }

    public void realOnMouseEnter()
    {
        hasCursorExitedCardTrigger = false;
                 bool ok = true;
        foreach(Card a in this.transform.root.GetComponentsInChildren<Hand>()[0].cards)
        {
            if(a.isDragging)
            {
                ok = false;
            }
        }
    
        foreach(Card a in this.transform.root.GetComponentsInChildren<Hand>()[1].cards)
        {
            if(a.isDragging)
            {
                ok = false;
            }
        }
        if(ok)
        {
            cardScaleUp();
        }
    }

    public void realOnMouseExit()
    {
        hasCursorExitedCardTrigger = true;
        if(!isDragging && shouldBeScaledDown)
        {
            cardScaleDown(true);
        }
    }

    private void cardScaleUp()
    {
        shouldBeScaledDown = true;
        transform.localScale *= 1.3f;
        transform.position += isPlayer1Owner ? new Vector3(0,1,0) : new Vector3(0,-1,0);
        
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
        transform.localScale /= 1.3f;
        transform.position -= isPlayer1Owner ? new Vector3(0,1,0) : new Vector3(0,-1,0);

        if(shouldKeepOrderInlayer)
        {
            GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder = orderInLayerScale1;
            GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = orderInLayerScale2;
            GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = orderInLayerScale3;
            GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = orderInLayerScale4;
        }
    }
    

    public virtual void Effect()
    {
        
    }

}
