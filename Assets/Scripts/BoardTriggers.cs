using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTriggers : MonoBehaviour
{
    private TurnManager turnManager;
    public bool idPlayer1;

    public Vector3 location;
    void OnTriggerStay2D(Collider2D other)
    {
         Card card = other.gameObject.GetComponent<Card>();       
        if(StateMachine.currentState == StateMachine.State.PlayingCard && card.isPlayer1Owner == idPlayer1)
        {
            if(card.type == Card.Type.RequiredLocation)
            {
                StateMachine.currentState = StateMachine.State.SelectingTargetSummoning;
            }
            else if(card.type == Card.Type.NothingRequired)
            {
                StateMachine.currentState = StateMachine.State.ReadyToPlayCard;
            }   
        }
        else if(StateMachine.currentState == StateMachine.State.ReadyToPlayCard && card.isPlayer1Owner == idPlayer1)    // CHANGE
        {
            if(card.isPlayer1Owner)
            {
                
                turnManager.Player1PlayACard(card, location);    
            }
            else
            {
                turnManager.Player2PlayACard(card, location);
            }
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
    }


}
