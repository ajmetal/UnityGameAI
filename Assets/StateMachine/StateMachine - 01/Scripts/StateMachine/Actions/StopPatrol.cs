using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/StopPatrol")]
public class StopPatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.Agent.isStopped = true;
  }

}
