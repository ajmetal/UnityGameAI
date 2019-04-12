using UnityEngine;
using System.Collections.Generic;

public abstract class State<StateMachineType, ActionType> : ScriptableObject
  where ActionType : Action<StateMachineType>
{

  [SerializeField]
  protected List<Action<StateMachineType>> onUpdateActions;

  [SerializeField]
  protected List<Action<StateMachineType>> onEnterActions;

  [SerializeField]
  protected List<Action<StateMachineType>> onLeaveActions;

  [SerializeField]
  protected List<Transition<StateMachineType, ActionType>> transitions;
  public List<Transition<StateMachineType, ActionType>> Transitions
  {
    get { return transitions; }
  }

  public virtual void OnUpdate(StateMachineType fsm)
  {
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

}

[CreateAssetMenu()]
public class PlayerState : State<PlayerStateMachine, PlayerAction> { }

[CreateAssetMenu()]
public class EnemyState : State<EnemyStateMachine, EnemyAction> { }