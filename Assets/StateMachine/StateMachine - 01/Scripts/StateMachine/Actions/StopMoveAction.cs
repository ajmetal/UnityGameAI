using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Enemy/Actions/StopMove")]
public class StopMoveAction : EnemyAction
{

  public override void Act(EnemyStateMachine fsm)
  {
    fsm.Agent.SetDestination(fsm.transform.position);
  }

}
