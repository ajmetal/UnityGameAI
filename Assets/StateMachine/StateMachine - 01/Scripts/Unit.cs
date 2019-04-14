using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
abstract public class Unit : MonoBehaviour
{

  [SerializeField]
  protected GameObject objective;
  [SerializeField]
  protected GameObject selectionIcon;
  [SerializeField]
  protected UIManager uiManager;
  [SerializeField]
  protected GameObject model;
  [SerializeField]
  protected float flashTime = 0.2f;

  protected NavMeshAgent agent;
  protected Animator animator;

  private static int unitCount = 0;

  public enum Alliance
  {
    PLAYER,
    COMPUTER
  }

  [SerializeField]
  protected Alliance alliance = Alliance.COMPUTER;
  public Alliance GetAlliance
  {
    get { return alliance; }
  }

  protected Health health;

  //the unit this unit is attacking
  protected Unit currentTarget;
  public Unit CurrentTarget
  {
    get { return currentTarget; }
  }

  protected int unitID;
  public int UnitID
  {
    get { return unitID; }
  }

  public virtual void SelectUnit()
  {
    selectionIcon.SetActive(true);
  }

  public virtual void DeselectUnit()
  {
    selectionIcon.SetActive(false);
    objective.SetActive(false);
  }

  protected virtual void Awake()
  {
    unitID = unitCount++;
    health = GetComponent<Health>();
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
  }

  public virtual void Move(Vector3 destination)
  {
    if (agent == null) return;
    agent.SetDestination(destination);
    objective.SetActive(true);
    objective.transform.position = destination;
    animator.SetBool("attacking", false);
  }

  public virtual void Attack(Unit target)
  {
    agent.SetDestination(transform.position);
    transform.LookAt(target.transform.position);
    animator.SetBool("attacking", true);
    currentTarget = target.GetComponent<Unit>();
  }

  public virtual void TakeDamage(int damage)
  {
    health.TakeDamage(damage);
    StartCoroutine(HitFlash());
  }

  protected IEnumerator HitFlash()
  {
    List<Renderer> renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
    Renderer renderer = GetComponent<Renderer>();
    if (renderer != null)
    {
      renderers.Add(renderer);
    }
    for (int i = 0; i < renderers.Count; ++i)
    {
      Material mat = renderers[i].material;
      mat.SetColor("_EmissionColor", Color.white);
    }

    yield return new WaitForSeconds(flashTime);

    for (int i = 0; i < renderers.Count; ++i)
    {
      Material mat = renderers[i].material;
      mat.SetColor("_EmissionColor", Color.black);
    }
  }

  public bool NavMeshAgentStopped()
  {
    return (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance);
  }

}

