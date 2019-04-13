using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/ContinuePatrol")]
public class NavigateAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    if (fsm.ParentUnit.NavMeshAgentStopped())
    {
      fsm.currentPoint = (fsm.currentPoint + 1) % fsm.pathPoints.Count;
      fsm.ParentUnit.MoveTo(fsm.pathPoints[fsm.currentPoint]);
    }
  }
}

[CreateAssetMenu(menuName = "Enemy/Actions/ResumePatrol")]
public class ResumePatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.Agent.isStopped = false;
  }
}


[CreateAssetMenu(menuName = "Enemy/Actions/StopPatrol")]
public class StopPatrolAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.Agent.isStopped = true;
  }

}

