using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class Soldier : Unit
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
    animator.SetFloat("attackSpeed", attackSpeed);

  }

  public void Update()
  {

    if (animator.GetBool("running") && agent.velocity == Vector3.zero)
    {
      animator.SetBool("running", false);
      objective.SetActive(false);
    }
    else if (animator.GetBool("running") == false && agent.velocity != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
      animator.SetBool("running", true);
    }

    if(animator.GetBool("attacking") && currentTarget == null)
    {
      animator.SetBool("attacking", false);
    }

  }

  override public void SelectUnit()
  {
    selectionIcon.SetActive(true);
  }

  override public void DeselectUnit()
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

    if (Vector3.Distance(target.transform.position, transform.position) <= attackRange)
    {
      agent.SetDestination(transform.position);
      transform.LookAt(target.transform.position);
      animator.SetBool("attacking", true);
      currentTarget = target.GetComponent<Unit>();

    }
  }

}
