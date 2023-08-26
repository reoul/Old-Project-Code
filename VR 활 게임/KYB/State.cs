using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly StateMachine _stateMachine;
    protected readonly GameObject _gameObject;

    protected State(GameObject gameObject)
    {
        this._gameObject = gameObject;
        _stateMachine = this._gameObject.GetComponent<StateMachine>();
    }
    
    public virtual void StateStart(){}
    public abstract void StateUpdate();
    public virtual void StateExit(){}
}
