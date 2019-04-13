using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(FieldOfView))]
public class EnemyStateMachine : StateMachine<EnemyState>
{
  public List<Vector3> pathPoints;

  public int currentPoint;
  public Transform lastPathPosition;
  public bool returning = false;

  private NavMeshAgent agent;
  public NavMeshAgent Agent { get { return agent; } }

  private Unit unit;
  public Unit ParentUnit { get { return unit; } }

  private FieldOfView fov;
  public FieldOfView FOV { get { return fov; } }

  protected void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    unit = GetComponent<Unit>();
    fov = GetComponent<FieldOfView>();
  }

  protected void Start()
  {
    if (pathPoints != null && pathPoints.Count > 1)
    {
      transform.position = pathPoints[1];
    }
  }

  protected override void Update()
  {
    currentState.OnUpdate(this);

    for (int i = 0; i < currentState.Transitions.Count; ++i)
    {
      if (currentState.Transitions[i].Condition.Check(this))
      {
        if (currentState.Transitions[i].TrueState as EnemyState != null)
        {
          ChangeState(currentState.Transitions[i].TrueState as EnemyState);
        }
      }
      else if (currentState.Transitions[i].FalseState as EnemyState != null)
      {
        ChangeState(currentState.Transitions[i].FalseState as EnemyState);
      }
    }
  }

  protected virtual void ChangeState(EnemyState to)
  {
    currentState.OnLeaveState(this);
    currentState = to;
    to.OnEnterState(this);
  }

  protected override void OnDrawGizmos()
  {
    base.OnDrawGizmos();
    for(int i = 0; i < pathPoints.Count; ++i)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(pathPoints[i], Vector3.one);
      Handles.Label(pathPoints[i] + Vector3.up, i.ToString());
    }

  }

}
