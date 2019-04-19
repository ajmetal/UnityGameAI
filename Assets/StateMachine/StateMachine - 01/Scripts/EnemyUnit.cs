using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyUnit : Unit
{

  private Gun gun;
  public float timeToReset = 3f;

  protected override void Awake()
  {
    base.Awake();
    gun = GetComponent<Gun>();
  }

  protected void Update()
  {
    animator.SetFloat("speed", agent.velocity.magnitude);

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

}
