using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
     Hand player1Hand;
     Hand player2Hand;
    public static bool isPlayer1Turn;
     bool aPlayerPassed;

     bool turnRegister;


    public GameObject cubeCardPrefab;
    public GameObject circleCardPrefab;

    public Button passTurnButton1;
    public Button passTurnButton2;
    public GameObject passTurn1;
    public GameObject passTurn2;


     SpriteRenderer hand1Sprite;
     SpriteRenderer hand2Sprite;

    void Start()
    {
        aPlayerPassed = false;
        passTurn1.SetActive(false);
        passTurn2.SetActive(false);
        player1Hand = GameObject.Find("Player1Hand").GetComponent<Hand>();
        player2Hand = GameObject.Find("Player2Hand").GetComponent<Hand>();
        hand1Sprite = player1Hand.GetComponent<SpriteRenderer>();
        hand2Sprite = player2Hand.GetComponent<SpriteRenderer>();

        turnRegister = Random.Range(0,1) == 0;
        isPlayer1Turn = turnRegister;
        UpdatePriority();

      /*    Esto se tiene que ejecutar despues de que todos esten inicializados
        if(isPlayer1Turn)
        {
            StartTurnPlayer1();
        }
        else
        {
            StartTurnPlayer2();
        }
        */
    }


    public void Player1PlayACard(Card card)
    {
        aPlayerPassed = false;
        passTurn1.SetActive(false);
        passTurn2.SetActive(false);
        player1Hand.PlayCard(card);
        isPlayer1Turn = !isPlayer1Turn;
        UpdatePriority();
    }

    public void Player1PassPriority()
    {
        isPlayer1Turn = !isPlayer1Turn;
        passTurn1.SetActive(true);
        if(aPlayerPassed)
        {
            EndTurn();
        }else
        {
            aPlayerPassed = true;
            UpdatePriority();
        } 
    }

    public void Player2PlayACard(Card card)
    {
        aPlayerPassed = false;
        passTurn1.SetActive(false);
        passTurn2.SetActive(false);
        player2Hand.PlayCard(card);
        isPlayer1Turn = !isPlayer1Turn;
        UpdatePriority();
    }

    public void Player2PassPriority()
    {
        isPlayer1Turn = !isPlayer1Turn;
        passTurn2.SetActive(true);
        if(aPlayerPassed)
        {
            EndTurn();
        }else
        {
            aPlayerPassed = true;
            UpdatePriority();
        }
    }

    public void EndTurn()
    {
        StartCoroutine(EndRealTurn());
    }

     void UpdatePriority()
    {
        if(isPlayer1Turn)
        {
            passTurnButton1.enabled = true;
            passTurnButton1.image.color = new Vector4(1f,0f,0f,1);
            passTurnButton2.enabled = false;
            passTurnButton2.image.color = new Vector4(0.3f,0.3f,0.3f,1);
            hand1Sprite.enabled = true;
            hand2Sprite.enabled = false;
            hand1Sprite.sharedMaterial.SetFloat("Boolean_57610B3C",1);
        }
        else
        {
            passTurnButton1.enabled = false;
            passTurnButton1.image.color = new Vector4(0.3f,0.3f,0.3f,1);
            passTurnButton2.enabled = true;
            passTurnButton2.image.color = new Vector4(0f,0f,1f,1);
            hand1Sprite.enabled = false;
            hand2Sprite.enabled = true;
            hand1Sprite.sharedMaterial.SetFloat("Boolean_57610B3C",0);
        }
    }

    void StartTurnPlayer1()
    {
        //Get card from the deck and use it in the addCard method.

        //TODO
        if(!player1Hand.isMaximumHandSize())
        {
            Card card = (Instantiate(cubeCardPrefab, player1Hand.transform.position, player1Hand.transform.rotation, player1Hand.transform) as GameObject).GetComponent<CubeCard>();
            card.isPlayer1Owner = true;
            player1Hand.AddCard(card);
        }
    }

    void StartTurnPlayer2()
    {
         //Get card from the deck and use it in the addCard method.
        if(!player2Hand.isMaximumHandSize())
        {
            Card card = (Instantiate(circleCardPrefab, player2Hand.transform.position, player2Hand.transform.rotation, player2Hand.transform) as GameObject).GetComponent<CircleCard>();
            card.isPlayer1Owner = false;
            player2Hand.AddCard(card);
        }
    }

    IEnumerator EndRealTurn()
    {
        passTurnButton1.interactable = false;
        passTurnButton2.interactable = false;
        yield return new WaitForSeconds(1);
        passTurnButton1.interactable = true;
        passTurnButton2.interactable = true;

        turnRegister = !turnRegister;
        isPlayer1Turn = turnRegister;
        aPlayerPassed = false;
        passTurn1.SetActive(false);
        passTurn2.SetActive(false);
        StartTurnPlayer1();
        StartTurnPlayer2();

        UpdatePriority();
    }

}
