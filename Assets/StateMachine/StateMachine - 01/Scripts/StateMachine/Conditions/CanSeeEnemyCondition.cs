using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class CanSeeEnemyCondition : Condition<PlayerStateMachine>
{
  public override bool Check(PlayerStateMachine fsm)
  {
    EnemyUnit[] units = FindObjectsOfType<EnemyUnit>();
    if (units.Length == 0) return false;

    float range = fsm.GetComponent<Gun>().attackRange;
    for (int i = 0; i < units.Length; ++i)
    {
      if(Vector3.Distance(units[i].transform.position, fsm.transform.position) <= range)
      {
        return true;
      }
    }

    return false;
  }
}
