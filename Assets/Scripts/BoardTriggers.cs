using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTriggers : MonoBehaviour
{
    private TurnManager turnManager;
    public bool idPlayer1;

    public Vector3 waitingPosition;
    public Vector3 location;
    void OnTriggerStay2D(Collider2D other)
    {
         Card card = other.gameObject.GetComponent<Card>();       
        if(StateMachine.currentState == StateMachine.State.PlayingCard && card.isPlayer1Owner == idPlayer1)
        {
            if(card.type == Card.Type.RequiredLocation)
            {
                StateMachine.currentState = StateMachine.State.SelectingTargetSummoning;
                card.transform.position = waitingPosition;
            }
            else if(card.type == Card.Type.NothingRequired)
            {
                StateMachine.currentState = StateMachine.State.ReadyToPlayCard;
            }   
        }
        else if(StateMachine.currentState == StateMachine.State.ReadyToPlayCard && card.isPlayer1Owner == idPlayer1) 
        {
            if(card.isPlayer1Owner)
            {
                turnManager.Player1PlayACard(card, GetComponentInParent<GridBoard>().GetLocation());    
            }
            else
            {
                turnManager.Player2PlayACard(card, GetComponentInParent<GridBoard>().GetLocation());
            }
        }
        else if(Input.GetMouseButtonDown(1) && StateMachine.currentState == StateMachine.State.SelectingTargetSummoning)
        {
            FindObjectsOfType<Hand>()[0].SetTheNewPositionsOfCards();
            FindObjectsOfType<Hand>()[1].SetTheNewPositionsOfCards();
            for(int i = 0; i < GetComponentInParent<GridBoard>().gridHeight; i++)
            {
                for(int j = 0; j < GetComponentInParent<GridBoard>().gridWidth; j++)
                {
                    GetComponentInParent<GridBoard>().grid[i][j].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            StateMachine.currentState = StateMachine.State.Base;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Card card = other.gameObject.GetComponent<Card>();
        if(card.isPlayer1Owner == idPlayer1 && StateMachine.currentState == StateMachine.State.DraggingCard)
        {
            card.GetComponentsInChildren<SpriteRenderer>()[0].enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Card card = other.gameObject.GetComponent<Card>();
        if(card.isPlayer1Owner == idPlayer1)
        {
            card.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        }
    }

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        waitingPosition = new Vector3(7.672f, 2f,0);
    }


}
