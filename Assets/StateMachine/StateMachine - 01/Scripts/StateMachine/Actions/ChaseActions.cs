using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Enemy/Actions/ChaseAction")]
public class ChasePlayer : EnemyAction
{
  public float chaseTime = 3f;

  public override void Act(EnemyStateMachine fsm)
  {
    fsm.ParentUnit.MoveTo(fsm.ParentUnit.CurrentTarget.transform.position);
    fsm.aggrod = true;
  }

  private IEnumerator Chase(EnemyStateMachine fsm)
  {
    yield return new WaitForSeconds(chaseTime);
    fsm.aggrod = false;
  }
}
