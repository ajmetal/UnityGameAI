using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class State<StateMachineType, ActionType> 
  : ScriptableObject
  where ActionType : Action<StateMachineType>
{ 

  [SerializeField]
  protected List<ActionType> onUpdateActions;

  [SerializeField]
  protected List<ActionType> onEnterActions;

  [SerializeField]
  protected List<ActionType> onLeaveActions;

  public virtual void OnUpdate(StateMachineType fsm)
  {
    Type StateType = this.GetType();
    for (int i = 0; i < onUpdateActions.Count; ++i)
    {
      onUpdateActions[i].Act(fsm);
    }
  }

  public virtual void OnEnterState(StateMachineType fsm)
  {
    for (int i = 0; i < onEnterActions.Count; ++i)
    {
      onEnterActions[i].Act(fsm);
    }
  }

  public virtual void OnLeaveState(StateMachineType fsm)
  {
    for (int i = 0; i < onLeaveActions.Count; ++i)
    {
      onLeaveActions[i].Act(fsm);
    }
  }

  public override string ToString()
  {
    return name;
  }

}

