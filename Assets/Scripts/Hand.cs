using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> cards;

    public Card cardBeingDragged; 

    void Start()
    {
        cards = new List<Card>();

    }

    void Update()
    {
        
    }

    public void AddCard(Card card)
    {         
        cards.Add(card);
        SetTheNewPositionsOfCards();
    }

    public void PlayCard(Card card, Vector3 location)
    {
        card.Effect(location);
        if(!card.isPermanent)
        {
            cards.Remove(card);
            Destroy(card.gameObject);
        }
        SetTheNewPositionsOfCards();
    }

    public void SetTheNewPositionsOfCards()
    {
        float offset = cards.Count % 2 == 0 ? 0.75f : 0;
        int orderInLayer = 2;
        for(int i = 0; i < cards.Count; i++)
        {
            if(i < Mathf.Ceil((cards.Count-1) / 2.0f))  // card is at the left
            {
                cards[i].transform.position = this.transform.position - new Vector3(((cards.Count / 2) - i) * 1.3f,0,0) + new Vector3(offset,0,-0.2f);
            }
            else if(i > Mathf.Ceil((cards.Count-1) / 2.0f)) // right
            {
                cards[i].transform.position = this.transform.position + new Vector3((i -(cards.Count / 2)) * 1.3f,0,0) + new Vector3(offset,0,-0.2f);
            }
            else    // in the middle
            {
                cards[i].transform.position = this.transform.position + new Vector3(offset,0,-0.2f);
            }

            cards[i].basePositionCard = cards[i].transform.position;
            cards[i].basePositionCardKeepZ = cards[i].transform.position + new Vector3(0,0,-0.1f);
            cards[i].scaledPositionCard = cards[i].isPlayer1Owner ? cards[i].transform.position + new Vector3(0,1,-0.1f) : cards[i].transform.position + new Vector3(0,-1,-0.1f);
            
            cards[i].GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder = orderInLayer;
            orderInLayer++;
            cards[i].GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = orderInLayer;
            orderInLayer++;
            cards[i].GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = orderInLayer;
            orderInLayer++;
            cards[i].GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = orderInLayer;
            orderInLayer++;
        }
    }

    public bool isMaximumHandSize()
    {
        return cards.Count >= 10;
    }

}
