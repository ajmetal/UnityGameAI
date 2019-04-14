using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/KeepPatrolling")]
public class KeepPatrollingAction : EnemyAction
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




