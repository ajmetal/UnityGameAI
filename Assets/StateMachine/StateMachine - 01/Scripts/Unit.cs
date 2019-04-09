using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

  //abstract methods
  public abstract void SelectUnit();
  public abstract void DeselectUnit();
  public abstract void Move(Vector3 destination);
  public abstract void Attack(Unit target);

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

  protected virtual void Awake()
  {
    unitID = unitCount++;
    health = GetComponent<Health>();
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

}

