#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion    

public abstract class PushdownState  {
    public PushdownStateMachine StateMachine {
        get {
            return _stateMachine;
        }
        set {
            _stateMachine = value;
        }
    }
    private PushdownStateMachine _stateMachine;

    public virtual string GetStateName() {
        return "Base Pushdown State";
    }
}