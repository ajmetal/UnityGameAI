using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Enemy/Actions/StartTimer")]
public class EnemyTimerAction : EnemyAction
{
  private Dictionary<EnemyStateMachine, float> runningTimers;
  [SerializeField]
  protected float duration = 0f;

  private void OnEnable()
  {
    Debug.Log("EnemyTimer Enabled");
    runningTimers = new Dictionary<EnemyStateMachine, float>();
  }

  public override void Act(EnemyStateMachine fsm)
  {
    runningTimers[fsm] = Time.time;
  }

  public bool CheckTimer(EnemyStateMachine fsm)
  {
    if (runningTimers.ContainsKey(fsm))
    {
      if (runningTimers[fsm] <= Time.time - duration)
      {
        runningTimers.Remove(fsm);
        return true;
      }
    }
    return false;
  }
}
