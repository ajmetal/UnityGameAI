using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer), typeof(SpriteRenderer))]
public class Objective : MonoBehaviour
{

  [SerializeField]
  private Vector3 offset;
  [SerializeField]
  private Vector3 rotation;

  private LineRenderer line;
  private SpriteRenderer sprite;
  private NavMeshAgent agent;

  private void Awake()
  {
    line = GetComponent<LineRenderer>();
    sprite = GetComponent<SpriteRenderer>();
    agent = transform.parent.GetComponent<NavMeshAgent>();
  }

  private void LateUpdate()
  {
    transform.position = agent.destination + offset;
    transform.rotation = Quaternion.Euler(rotation);

    line.SetPosition(0, transform.parent.position);
    line.SetPosition(1, transform.position);
  }

}
