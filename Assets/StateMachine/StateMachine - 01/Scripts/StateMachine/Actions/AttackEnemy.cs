using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class AttackEnemyAction : PlayerAction
{
  public override void Act(PlayerStateMachine fsm)
  {
    Unit enemy = FindObjectOfType<EnemyUnit>();
    fsm.GetComponent<Unit>().Attack(enemy);
  }

}
