using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Enemy/Conditions/CheckTimer")]
public class EnemyTimerCondition : EnemyCondition
{
  [SerializeField]
  protected EnemyTimerAction timer;

  public override bool Check(EnemyStateMachine fsm)
  {
    return timer.CheckTimer(fsm);
  }
}
