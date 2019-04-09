using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Gun : MonoBehaviour
{
  //dependency
  private Unit parentUnit;

  //VFX
  [SerializeField]
  private MuzzleFlashEffect muzzleFlash;
  [SerializeField]
  private float muzzleFlashSize = 1f;
  [SerializeField]
  private float muzzleFlashIntensity = 1f;

  //Gameplay
  [SerializeField]
  private Transform fireTransform;
  [SerializeField]
  private int damage = 1;
  public float attackSpeed = 1.0f;
  public float attackRange = 15f;

  private void Awake()
  {
    parentUnit = GetComponent<Unit>();
  }

  public void Fire(float flashDuration)
  {
    if (parentUnit.CurrentTarget == null) return;

    muzzleFlash.transform.position = fireTransform.position;
    muzzleFlash.transform.rotation = fireTransform.rotation;
    muzzleFlash.Flash(flashDuration, muzzleFlashIntensity, muzzleFlashSize);

    parentUnit.CurrentTarget.TakeDamage(damage);

  }

}
