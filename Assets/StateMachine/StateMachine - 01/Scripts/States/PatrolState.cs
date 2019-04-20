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
    stateID = StateID.PATROL_STATE;

    AddTransition(Transition.ENEMY_DETECTED, StateID.CHASE_STATE);
    
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

  public override Transition Decide()
  {
    if(fov.EnemyDetected)
    {
      return Transition.ENEMY_DETECTED;
    }

    return Transition.NULL_TRANSITION;
  }
}