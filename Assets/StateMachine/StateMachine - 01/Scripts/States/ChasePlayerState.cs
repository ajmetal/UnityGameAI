using UnityEngine;
using UnityEngine.AI;
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
    stateID = StateID.CHASE;

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

  public override StateID Decide()
  {
    if (unit.CurrentTarget == null || Time.time - enterStateTime >= unit.timeToReset)
    {
      return StateID.PATROL;
    }

    if (gun.InRange(unit.CurrentTarget.transform.position) && fov.InLineOfSight(unit.CurrentTarget.transform.position))
    {
      return StateID.ATTACK;
    }

    return StateID.NULL;
  }

  public override void OnEnterState()
  {
    enterStateTime = Time.time;
    if (unit.CurrentTarget == null)
    {
      unit.CurrentTarget = fov.LastDetectedUnit;
    }
  }

  public override void OnLeaveState()
  {
    unit.Move(unit.transform.position);
  }

}