using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Conditions/CanSeePlayer")]
public class CanSeePlayerCondition : EnemyCondition
{
  public override bool Check(EnemyStateMachine fsm)
  {
    return fsm.FOV.EnemyDetected;
  }
}
