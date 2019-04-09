using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyUnit : Unit
{

  private NavMeshAgent agent;
  private Animator animator;
  private Gun gun;

  protected override void Awake()
  {
    base.Awake();
    selectionIcon.SetActive(false);
    objective.SetActive(false);
    agent = GetComponent<NavMeshAgent>();
    gun = GetComponent<Gun>();
    animator = GetComponent<Animator>();

  }

  protected void Update()
  {

    animator.SetFloat("speed", agent.velocity.magnitude);

    if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
    {
      objective.SetActive(false);
    }

    if (animator.GetBool("attacking") && currentTarget == null)
    {
      animator.SetBool("attacking", false);
    }

  }

  public override void SelectUnit()
  {
    selectionIcon.SetActive(true);
  }

  public override void DeselectUnit()
  {
    selectionIcon.SetActive(false);
    objective.SetActive(false);
  }

  public override void Move(Vector3 destination)
  {
    agent.SetDestination(destination);
    objective.SetActive(true);
    objective.transform.position = destination;
    animator.SetBool("attacking", false);
  }

  public override void Attack(Unit target)
  {
    if (Vector3.Distance(target.transform.position, transform.position) <= gun.attackRange)
    {
      agent.SetDestination(transform.position);
      transform.LookAt(target.transform.position);
      animator.SetBool("attacking", true);
      currentTarget = target.GetComponent<Unit>();

    }
  }

  public void AlertOn()
  {
    Debug.Log("Detected Player");
  }

  public void AlertOff()
  {
    Debug.Log("Can't see player anymore");
  }



}
