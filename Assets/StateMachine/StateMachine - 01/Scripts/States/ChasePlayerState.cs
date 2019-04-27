using UnityEngine;
using UnityEngine.AI;
using FSM;

public class ChaseState : State
{
  private Unit unit;
  private FieldOfView fov;
  private Gun gun;

  private float enterStateTime = 0;
  private float timeToReset = 0;

  public ChaseState(StateMachine fsm)
  {
    stateID = StateID.CHASE;

    timeToReset = fsm.timeToReset;
    unit = fsm.GetComponent<Unit>();
    fov = fsm.GetComponent<FieldOfView>();
    gun = fsm.GetComponent<Gun>();
  }

  public override void Act()
  {
    if (unit.CurrentTarget == null) return;

    if(fov.InLineOfSight(unit.CurrentTarget.transform.position))
    {
      enterStateTime = Time.time;
    }

    unit.Move(unit.CurrentTarget.transform.position);
  }

  public override StateID Decide()
  {
    if (unit.CurrentTarget == null || Time.time - enterStateTime >= timeToReset)
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