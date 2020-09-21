using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State {Base, DraggingCard, PlayingCard, SelectingTargetSummoning, ReadyToPlayCard, Simulating};

    public static State currentState;

    void Start()
    {
        currentState = State.Base;
    }
}
