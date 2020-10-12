#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class PushdownStateMachine {
    public PushdownState InitialState;

    private List<PushdownState> _pushdownStack = new List<PushdownState>();

    public void Start() {
        if (InitialState != null) {
            PushState(InitialState);
        }
    }

    public void Tick() {
        PushdownState currentState = PeekState();
        if (currentState != null) {
            //Debug.LogFormat("PushdownStateMachine.Tick: currentState = {0}", currentState.GetStateName());
            OnTick(currentState);
        } else {
            Debug.LogFormat("CurrentState = null!");
        }
    }

    public void PushState(PushdownState state) {
        AddState(state);
        PushdownState previousState = PeekState();
        if (previousState != null) {
            OnExit(previousState);
        }

        PushdownState nextState = state;
        if (nextState != null) {
            _pushdownStack.Add(nextState);
            OnEnter(nextState);
        }
    }

    public void QueueState(PushdownState inputState) {
        if (inputState == null) {
            Debug.LogFormat("Tried to queue a null state!");
            return;
        }

        _pushdownStack.Insert(1, inputState);
        inputState.StateMachine = this;


        OnAwake(inputState);
    }

    public void PopState() {
        if (_pushdownStack.Count == 0) {
            return;
        }

        PushdownState previousState = _pushdownStack[_pushdownStack.Count - 1];
        _pushdownStack.RemoveAt(_pushdownStack.Count - 1);
        if (previousState != null) {
            OnExit(previousState);
        }

        PushdownState nextState = PeekState();
        if (nextState != null) {
            OnEnter(nextState);
        }
    }

    public PushdownState PeekState() {
        if (_pushdownStack.Count > 0) {
            return _pushdownStack[_pushdownStack.Count - 1];
        }

        return null;
    }

    public void ClearStates() {
        while (_pushdownStack.Count > 1) {
            PushdownState tempState = _pushdownStack[_pushdownStack.Count - 1];
            _pushdownStack.RemoveAt(_pushdownStack.Count - 1);
            OnExit(tempState);
        }
    }

    public void AddState(PushdownState inputState) {
        if (inputState == null) {
            return;
        }
        inputState.StateMachine = this;
        OnAwake(inputState);
    }

    public int GetStackCount() {
        return _pushdownStack.Count;
    }
    
    private void OnAwake(PushdownState inputState) {
        IOnStateAwake inputInterface = inputState as IOnStateAwake;
        if (inputInterface != null) {
            inputInterface.OnAwake();
        }
    }

    private void OnEnter(PushdownState inputState) {
        IOnStateEnter inputInterface = inputState as IOnStateEnter;
        if (inputInterface != null) {
            inputInterface.OnEnter();
        }
    }

    private void OnExit(PushdownState inputState) {
        IOnStateExit inputInterface = inputState as IOnStateExit;
        if (inputInterface != null) {
            inputInterface.OnExit();
        }
    }


    private void OnTick(PushdownState inputState) {
        IOnStateTick inputInterface = inputState as IOnStateTick;
        if (inputInterface != null) {
            inputInterface.OnTick();
        }
    }
}