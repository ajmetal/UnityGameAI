using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class StopPatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.patrolling = false;
    fsm.lastPathPosition = fsm.ParentUnit.transform;
    fsm.Agent.SetDestination(fsm.ParentUnit.transform.position);
  }
}
