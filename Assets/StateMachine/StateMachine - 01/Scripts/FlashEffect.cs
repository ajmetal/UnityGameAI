using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light), typeof(ParticleSystem))]
public class FlashEffect : MonoBehaviour
{

  private ParticleSystem particle;
  private Light light;

  private void Awake()
  {
    light = GetComponent<Light>();
    light.intensity = 0;
    particle = GetComponent<ParticleSystem>();
    particle.Stop();
    var main = particle.main;
    main.playOnAwake = false;
    main.loop = false;
  }

  public void Flash(float fadeOutTime, float intensity = 2f, float size = 1)
  {
    light.intensity = intensity;
    particle.Stop();
    particle.transform.localScale = Vector3.one * size;
    particle.Play();
    StartCoroutine(FadeOut(fadeOutTime));
  }

  private IEnumerator FadeOut(float fadeOutTime)
  {
    yield return new WaitForSeconds(fadeOutTime);
    light.intensity = 0;
  }

}
