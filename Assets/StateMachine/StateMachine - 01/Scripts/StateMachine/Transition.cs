using UnityEngine;
using System.Collections;

public abstract class Transition<StateMachineType, ActionType> : ScriptableObject
  where ActionType : Action<StateMachineType>
{

  [SerializeField]
  protected Condition<StateMachineType> condition;
  public Condition<StateMachineType> Condition
  {
    get { return condition; }
  }

  [SerializeField]
  protected State<StateMachineType, ActionType> trueState;
  public State<StateMachineType, ActionType> TrueState
  {
    get { return trueState; }
  }

  [SerializeField]
  protected State<StateMachineType, ActionType> falseState = null;
  public State<StateMachineType, ActionType> FalseState
  {
    get { return falseState; }
  }

}

[CreateAssetMenu()]
public class EnemyStateTransition : Transition<EnemyStateMachine, EnemyAction> { }
[CreateAssetMenu()]
public class PlayerStateTransition : Transition<PlayerStateMachine, PlayerAction> { }
