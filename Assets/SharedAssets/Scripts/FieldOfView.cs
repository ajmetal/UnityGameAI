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
  private Vector3 eyeHeight = Vector3.up;

  [SerializeField]
  private float delay = 0.2f;

  [SerializeField]
  private LayerMask targetMask;
  [SerializeField]
  private LayerMask obstructionMask;

  private Unit unit;

  private Unit lastDetectedUnit = null;
  public Unit LastDetectedUnit
  {
    get { return lastDetectedUnit; }
  }

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
    while(gameObject.activeSelf)
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
        if (Vector3.Angle(transform.forward, direction) <= viewAngle / 2 &&
            InLineOfSight(unit.transform.position))
        {
          lastDetectedUnit = unit;
          return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Checks if this unit has unobstrcuted line of sight to the target position.
  /// </summary>
  /// <param name="position"></param>
  /// <returns> if the target is in line of sight </returns>
  public bool InLineOfSight(Vector3 position)
  {
    Vector3 eyePosition = transform.position + eyeHeight;
    Vector3 direction = (position + eyeHeight - eyePosition);
    Debug.DrawRay(eyePosition, direction, Color.red, delay);
    if (!Physics.Raycast(eyePosition, direction.normalized, direction.magnitude, obstructionMask))
    {
      return true;
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
