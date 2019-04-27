using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using FSM;

public class AttackState : State
{
  private Unit unit;
  private Gun gun;
  private FieldOfView fov;

  public AttackState(StateMachine fsm)
  {
    stateID = StateID.ATTACK;

    unit = fsm.GetComponent<Unit>();
    gun = fsm.GetComponent<Gun>();
    fov = fsm.GetComponent<FieldOfView>();
  }

  public override void Act()
  {
    unit.transform.LookAt(unit.CurrentTarget.transform.position);
  }

  public override StateID Decide()
  {
    if(unit.CurrentTarget == null || 
      !fov.InLineOfSight(unit.CurrentTarget.transform.position) ||
      !gun.InRange(unit.CurrentTarget.transform.position))
    {
      return StateID.CHASE;
    }

    return StateID.NULL;
  }

  public override void OnEnterState()
  {
    unit.Attack(unit.CurrentTarget);
  }

  public override void OnLeaveState()
  {
    unit.StopAttack();
  }

}
