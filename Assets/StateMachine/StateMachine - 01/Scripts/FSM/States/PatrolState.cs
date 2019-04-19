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
    //AddTransition(Transition.ATTACK_ENEMY, StateID.ATTACK_STATE);
    
    unit = obj.GetComponent<Unit>();
    agent = obj.GetComponent<NavMeshAgent>();
    fov = obj.GetComponent<FieldOfView>();

    pathPoints = obj.GetComponent<EnemyStateController>().pathPoints;
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
    //agent.isStopped = false;
    unit.Move(pathPoints[currentPoint]);
  }

  public override void OnLeaveState()
  {
    unit.Move(unit.transform.position);
    //agent.isStopped = true;
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