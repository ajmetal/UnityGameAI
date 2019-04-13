using UnityEngine;

[CreateAssetMenu(menuName ="Enemy/Actions/AttackPlayer")]
public class AttackPlayerAction : EnemyAction
{
  public override void Act(EnemyStateMachine fsm)
  {
    Unit unit = FindObjectOfType<PlayerUnit>();
    fsm.ParentUnit.Attack(unit);
  }
}
