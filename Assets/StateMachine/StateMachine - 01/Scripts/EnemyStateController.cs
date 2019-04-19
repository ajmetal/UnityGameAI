using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using FSM;

public class EnemyStateController : MonoBehaviour
{
  StateMachine stateMachine;

  public List<Vector3> pathPoints;

  void Awake()
  {
    stateMachine = new StateMachine();
    stateMachine.AddState(new PatrolState(gameObject));
    stateMachine.AddState(new AttackState(gameObject));
    stateMachine.AddState(new ChasePlayerState(gameObject));
  }

  private void Update()
  {
    stateMachine.OnUpdate();
  }

  private void OnDrawGizmosSelected()
  {
    for (int i = 0; i < pathPoints.Count; ++i)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(pathPoints[i], Vector3.one);
      Handles.Label(pathPoints[i] + Vector3.up, i.ToString());
    }
    if (stateMachine != null)
    {
      Handles.Label(transform.position + Vector3.up, stateMachine.CurrentState.ToString());
    }
  }

}
