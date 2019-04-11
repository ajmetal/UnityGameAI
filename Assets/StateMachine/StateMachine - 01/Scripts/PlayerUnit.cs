using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : Unit
{

  private Gun gun;

  protected override void Awake()
  {
    base.Awake();
    selectionIcon.SetActive(false);
    objective.SetActive(false);
    gun = GetComponent<Gun>();
  }

  protected void Update()
  {
    animator.SetFloat("speed", agent.velocity.magnitude);

    if (NavMeshAgentStopped())
    {
      objective.SetActive(false);
    }

    if(animator.GetBool("attacking") && currentTarget == null)
    {
      animator.SetBool("attacking", false);
    }

    if(currentTarget != null && Vector3.Distance(currentTarget.transform.position, transform.position) <= gun.attackRange)
    {
      transform.LookAt(currentTarget.transform.position);
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

  public override void Attack(Unit target)
  {
    if (Vector3.Distance(target.transform.position, transform.position) <= gun.attackRange)
    {
      base.Attack(target);
    }
  }



}
