using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class MuzzleFlashEffect : MonoBehaviour
{
  //new keyword hides deprecated gameObject.light
  private new Light light;

  private ParticleSystem projectile;

  private void Awake()
  {
    light = GetComponent<Light>();
    light.intensity = 0;
    projectile = GetComponent<ParticleSystem>();
  }

  public void Flash(float fadeOutTime, float intensity = 2f, float size = 1)
  {
    light.intensity = intensity;
    projectile.Stop();
    light.areaSize = Vector2.one * size;
    projectile.Play();
    StartCoroutine(FadeOut(fadeOutTime));
  }

  private IEnumerator FadeOut(float fadeOutTime)
  {
    yield return new WaitForSeconds(fadeOutTime);
    light.intensity = 0;
  }
}
