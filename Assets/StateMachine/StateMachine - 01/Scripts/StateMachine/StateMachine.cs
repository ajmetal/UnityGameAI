using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public abstract class StateMachine<StateType> : MonoBehaviour
 // where StateType : State<StateMachine<StateType>, Action<StateMachine<StateType>>>
{
  [SerializeField]
  protected StateType currentState;

  [SerializeField]
  protected List<StateType> states;

  protected abstract void Update();

  //protected virtual void ChangeState(StateType to)
  //{
  //  currentState.OnLeaveState(this);
  //  currentState = to;
  //  to.OnEnterState(this);
  //}

  protected virtual void OnDrawGizmos()
  {
    Handles.Label(transform.position + Vector3.up * 2, currentState.ToString());
  }
}

