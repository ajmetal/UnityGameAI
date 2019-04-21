using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using FSM;

public class PatrolState : State
{

  private Unit unit;
  private NavMeshAgent agent;
  private FieldOfView fov;

  private List<Vector3> pathPoints;
  private int currentPoint = 0;

  public PatrolState(GameObject obj)
  {
    stateID = StateID.PATROL;
    
    unit = obj.GetComponent<Unit>();
    agent = obj.GetComponent<NavMeshAgent>();
    fov = obj.GetComponent<FieldOfView>();

    pathPoints = obj.GetComponent<EnemyStateMachine>().pathPoints;
  }

  public override void Act()
  {
    if (unit.NavMeshAgentStopped())
    {
      currentPoint = (currentPoint + 1) % pathPoints.Count;
      unit.Move(pathPoints[currentPoint]);
    }
  }

  public override void OnEnterState()
  {
    unit.Move(pathPoints[currentPoint]);
  }

  public override void OnLeaveState()
  {
    unit.Move(unit.transform.position);
  }

  public override StateID Decide()
  {
    if(fov.EnemyDetected)
    {
      return StateID.CHASE;
    }

    return StateID.NULL;
  }
}