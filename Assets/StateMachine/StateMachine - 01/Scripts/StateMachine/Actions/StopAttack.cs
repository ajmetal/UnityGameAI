using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Enemy/Actions/StopAttack")]
public class StopAttack : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    fsm.ParentUnit.StopAttack();
  }

}
