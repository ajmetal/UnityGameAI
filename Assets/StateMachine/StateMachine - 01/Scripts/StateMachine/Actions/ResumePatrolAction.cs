using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class ResumePatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    if (fsm.patrolling == true) return;
    fsm.patrolling = true;

    fsm.Agent.SetDestination(fsm.lastPathPosition.position);
  }
}
