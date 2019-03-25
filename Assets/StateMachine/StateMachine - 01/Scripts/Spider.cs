using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Spider : Unit
{

  private NavMeshAgent agent;
  private Animator animator;

  protected override void Awake()
  {
    base.Awake();
    selectionIcon.SetActive(false);
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
  }

  override public void SelectUnit()
  {
    Debug.Log(gameObject.name + "selected");
    selectionIcon.SetActive(true);
  }

  override public void DeselectUnit()
  {
    Debug.Log(gameObject.name + "deselected");
    selectionIcon.SetActive(false);
  }

  public override void Move(Vector3 destination)
  {
    throw new System.NotImplementedException();
  }

  public override void Attack(Unit target)
  {
    throw new System.NotImplementedException();
  }

}
