#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ExampleState : PushdownState, IOnStateEnter, IOnStateExit, IOnStateTick {
    public Vector3 destination;

    public ExampleState() { }

    public override string GetStateName() { return "Example State"; }

    public void OnEnter() { }

    public void OnExit() { }

    public void OnTick() { }
}