using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName =  "Enemy/State")]
public class EnemyState
  : State<EnemyStateMachine, EnemyAction>
{

  [Serializable]
  public struct EnemyTransition
  {
    public EnemyCondition Condition;
    public EnemyState TrueState;
    public EnemyState FalseState;
  }

  [SerializeField]
  protected List<EnemyTransition> transitions;
  public List<EnemyTransition> Transitions
  {
    get { return transitions; }
  }

}