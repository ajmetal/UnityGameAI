using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{

  [SerializeField]
  private List<Transform> pathPoints;

  private NavMeshAgent agent;
  private Unit unit;

  private int currentPoint = 0;

  private Transform lastPathPosition;

  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    unit = GetComponent<Unit>();
    if(pathPoints == null)
    {
      pathPoints = new List<Transform>();
      pathPoints.Add(transform);
    }
  }

  private void Start()
  {
    agent.SetDestination(pathPoints[currentPoint].position);
  }

  private void Update()
  {
    if (unit.NavMeshAgentStopped())
    {
      currentPoint = (currentPoint + 1) % pathPoints.Count;
      unit.Move(pathPoints[currentPoint].position);
    }
  }

  public void ResumePatrol()
  {
    agent.isStopped = false;
    unit.Move(pathPoints[currentPoint].position);
  }

  public void StopPatrol()
  {
    agent.isStopped = true;
  }


}
