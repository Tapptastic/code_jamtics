using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicAi : MonoBehaviour
{
    public StateMachine<BasicAi> State;

    private float hunger;
    private float thirst;

    internal void GetWater()
    {
        if (thirst < .3f)
        {
            State.Set(new AiStateCollectWater());
        }
    }

    internal void GetFood()
    {
        if (hunger < .3f)
        {
            State.Set(new AiStateGathering());
        }
    }

    internal void Move()
    {
        //Give this a random Direction
        var transform1 = transform;
        
        var direction = transform1.right;

        Debug.DrawLine(transform1.position, (transform1.position + transform1.forward), Color.black);
    }

    private void Start()
    {
        hunger = 1;
        thirst = 1;
        
        this.State = new StateMachine<BasicAi>(this);

        State.Set(new AiStateIdle());
    }

    private void Update()
    {
        State.Update();
    }
}