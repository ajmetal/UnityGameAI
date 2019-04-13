using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyUnit : Unit
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

    if (animator.GetBool("attacking") && currentTarget == null)
    {
      animator.SetBool("attacking", false);
    }
  }

  public override void Attack(Unit target)
  {
    if (Vector3.Distance(target.transform.position, transform.position) <= gun.attackRange)
    {
      base.Attack(target);
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
