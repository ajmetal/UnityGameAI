using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/ResumePatrol")]
public class ResumePatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.Agent.isStopped = false;
    fsm.ParentUnit.MoveTo(fsm.pathPoints[fsm.currentPoint]);
  }

}