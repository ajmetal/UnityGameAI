using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit), typeof(AudioSource), typeof(AmmoParticles))]
public class Gun : MonoBehaviour
{
  [SerializeField]
  private GameObject flashEffectPrefab;
  private FlashEffect impactFlash;
  private FlashEffect muzzleFlash;

  private AudioSource audioSource;
  [SerializeField]
  private AudioClip gunShotClip;
  [SerializeField]
  [Range(0, 1)]
  private float gunShotVolume = 1f;

  [SerializeField]
  private float flashIntensity;
  [SerializeField]
  private float flashSize;

  [SerializeField]
  private Transform fireTransform;

  private AmmoParticles ammoParticles;

  private Unit parentUnit;

  private void Awake()
  {
    impactFlash = Instantiate(flashEffectPrefab).GetComponent<FlashEffect>();
    muzzleFlash = Instantiate(flashEffectPrefab).GetComponent<FlashEffect>();
    audioSource = GetComponent<AudioSource>();
    audioSource.volume = gunShotVolume;
    parentUnit = GetComponent<Unit>();
    ammoParticles = GetComponent<AmmoParticles>();
  }

  public void Fire(float flashTime)
  {
    if (parentUnit.CurrentTarget == null) return;
    muzzleFlash.transform.position = fireTransform.position;
    muzzleFlash.transform.rotation = fireTransform.rotation;
    muzzleFlash.Flash(flashTime, flashIntensity, flashSize);
    impactFlash.transform.position = parentUnit.CurrentTarget.transform.position;
    parentUnit.CurrentTarget.TakeDamage(1);
    impactFlash.Flash(flashTime, flashIntensity, flashSize);
    audioSource.Stop();
    audioSource.PlayOneShot(gunShotClip, gunShotVolume);
    ammoParticles.EjectShell();
  }

}
