using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using FSM;

public class ChasePlayerState : State
{
  private EnemyUnit unit;
  private NavMeshAgent agent;
  private FieldOfView fov;
  private Gun gun;

  private float enterStateTime = 0;

  public ChasePlayerState(GameObject obj)
  {

    stateID = StateID.CHASE_STATE;
    AddTransition(Transition.RESET, StateID.PATROL_STATE);
    AddTransition(Transition.ATTACK_ENEMY, StateID.ATTACK_STATE);

    unit = obj.GetComponent<EnemyUnit>();
    agent = obj.GetComponent<NavMeshAgent>();
    fov = obj.GetComponent<FieldOfView>();
    gun = obj.GetComponent<Gun>();

  }

  public override void Act()
  {
    if (unit.CurrentTarget == null) return;

    if(fov.InLineOfSight(unit.CurrentTarget.transform.position))
    {
      enterStateTime = Time.time;
    }

    agent.SetDestination(unit.CurrentTarget.transform.position);

  }

  public override Transition Decide()
  {
    if(unit.CurrentTarget == null || Time.time - enterStateTime >= unit.timeToReset)
    {
      return Transition.RESET;
    }

    if (gun.InRange(unit.CurrentTarget.transform.position) && fov.InLineOfSight(unit.CurrentTarget.transform.position))
    {
      return Transition.ATTACK_ENEMY;
    }

    return Transition.NULL_TRANSITION;
  }

  public override void OnEnterState()
  {
    enterStateTime = Time.time;
    unit.CurrentTarget = fov.LastDetectedUnit;
  }

  public override void OnLeaveState()
  {
    unit.Move(unit.transform.position);
  }

}