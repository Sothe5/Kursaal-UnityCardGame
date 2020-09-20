using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTriggers : MonoBehaviour
{
    private TurnManager turnManager;
    public bool idPlayer1;

    void OnTriggerStay2D(Collider2D other)
    {
        Card card = other.gameObject.GetComponent<Card>();
        if(card.isPlayer1Owner == idPlayer1 && card.isAvailableToBePlayed)
        {
            if(card.isPlayer1Owner)
            {
                turnManager.Player1PlayACard(card);    
            }
            else
            {
                turnManager.Player2PlayACard(card);
            }
            card.isAvailableToBePlayed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Card card = other.gameObject.GetComponent<Card>();
        if(card.isPlayer1Owner == idPlayer1 && card.isDragging)
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
