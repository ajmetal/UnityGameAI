using UnityEngine;
using System.Collections.Generic;
using System;

//public abstract class Transition<StateMachineType, ActionType, ConditionType> 
//  : ScriptableObject
//  where ActionType : Action<StateMachineType>
//  where ConditionType : Condition<StateMachineType>
//{

//  [SerializeField]
//  protected ConditionType condition;
//  public ConditionType Condition
//  {
//    get { return condition; }
//  }

//  [SerializeField]
//  protected State<StateMachineType, ActionType, ConditionType> trueState;
//  public State<StateMachineType, ActionType, ConditionType> TrueState
//  {
//    get { return trueState; }
//  }

//  [SerializeField]
//  protected State<StateMachineType, ActionType, ConditionType> falseState = null;
//  public State<StateMachineType, ActionType, ConditionType> FalseState
//  {
//    get { return falseState; }
//  }

//}

//[CreateAssetMenu()]
//public class EnemyStateTransition 
//  : Transition<EnemyStateMachine, EnemyAction, EnemyCondition> { }

//[CreateAssetMenu()]
//public class PlayerStateTransition 
//  : Transition<PlayerStateMachine, PlayerAction, PlayerCondition> { }

//public class TransitionList<ConditionType, StateType>
//{
//  [Serializable]
//  public struct Transition
//  {
//    ConditionType condition;
//    StateType state;
//  }

//  [SerializeField]
//  public List<Transition> transitions;
//}

