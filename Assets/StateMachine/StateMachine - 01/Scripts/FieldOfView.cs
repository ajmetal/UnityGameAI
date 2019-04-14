using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Unit))]
public class FieldOfView : MonoBehaviour
{
  //Cached components
  [SerializeField]
  private Light flashLight;
  private Unit thisUnit;

  [SerializeField]
  private float viewRange = 10f;
  [SerializeField]
  [Range(0, 179)]
  private float viewAngle = 90f;

  [SerializeField]
  private float delay = 0.2f;

  [SerializeField]
  private LayerMask targetMask;
  [SerializeField]
  private LayerMask obstructionMask;

  //public UnityEvent EnemyDetectedEvent;
  //public UnityEvent EnemyObscuredEvent;

  private bool enemyDetected = false;
  public bool EnemyDetected
  {
    get { return enemyDetected; }
  }

  private void Awake()
  {
    viewAngle = flashLight.spotAngle;
    thisUnit = GetComponent<Unit>();
  }

  private void Start()
  {
    StartCoroutine(SearchForTargets());
  }

  private IEnumerator SearchForTargets()
  {
    WaitForSeconds wait = new WaitForSeconds(delay);
    while(gameObject.activeInHierarchy)
    {
      yield return wait;
      if(InFieldOfView())
      {
        enemyDetected = true;
      }
      else
      {
        enemyDetected = false;
      }
    }
  }

  /// <summary>
  /// Checks to see if any opposing units is within the field of view of this unit
  /// </summary>
  /// <returns></returns>
  private bool InFieldOfView()
  {
    Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRange, targetMask);
    for (int i = 0; i < targetsInRadius.Length; ++i)
    {
      Unit unit = targetsInRadius[i].GetComponent<Unit>();
      if (unit != null && unit.GetAlliance != thisUnit.GetAlliance)
      {
        Vector3 direction = (unit.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, direction * viewRange, Color.red, delay);
        if (Vector3.Angle(transform.forward, direction) <= viewAngle / 2)
        {
          if(!Physics.Raycast(transform.position, direction, viewRange, obstructionMask))
          {
            return true;
          }
        }
      }
    }
    return false;
  }

  private void OnValidate()
  {
    if(flashLight != null && flashLight.type != LightType.Spot)
    {
      Debug.LogError("Field of View flashLight must be a Spot Light.");
      flashLight = null;
    }
    else if(flashLight != null)
    {
      flashLight.spotAngle = viewAngle;
      flashLight.range = viewRange;
    }
  }

}
