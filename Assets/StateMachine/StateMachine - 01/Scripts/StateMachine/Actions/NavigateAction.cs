using UnityEngine;
using UnityEditor;
using System.Collections;

[CreateAssetMenu()]
public class NavigateAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    if (fsm.ParentUnit.NavMeshAgentStopped())
    {
      fsm.currentPoint = (fsm.currentPoint + 1) % fsm.pathPoints.Count;
      fsm.Agent.SetDestination(fsm.pathPoints[fsm.currentPoint]);
    }
  }

}
