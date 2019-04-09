using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

  [SerializeField]
  private List<Transform> pathPoints;

  private NavMeshAgent agent;

  private bool patrolling = true;
  private bool returning = false;
  private int nextPoint;

  private enum LoopType
  {
    PINGPONG,
    RESTART
  }

  [SerializeField]
  private LoopType loopType = LoopType.PINGPONG;

  private Transform lastPathPosition;

  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    if (pathPoints == null)
    {
      pathPoints = new List<Transform>();
      pathPoints.Add(transform);
    }
    nextPoint = 0;
  }

  private void Start()
  {
    agent.SetDestination(pathPoints[nextPoint].position);
  }

  private void Update()
  {
    if (patrolling && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
    {
      nextPoint = returning ? --nextPoint : ++nextPoint;
      if(nextPoint == pathPoints.Count)
      {
        if (loopType == LoopType.PINGPONG)
        {
          returning = true;
          --nextPoint;
        }
        else
        {
          nextPoint = 0;
        }
      }
      else if(nextPoint < 0)
      {
        returning = false;
        ++nextPoint;
      }
      agent.SetDestination(pathPoints[nextPoint].position);
    }
  }

  public void ResumePatrol()
  {
    if (patrolling == true) return;
    patrolling = true;

    agent.SetDestination(lastPathPosition.position);
  }

  public void StopPatrol()
  {
    patrolling = false;
    lastPathPosition = transform;
    agent.SetDestination(transform.position);
  }


}
