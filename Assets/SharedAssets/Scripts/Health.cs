using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  //fields
  [SerializeField]
  protected int currentHealth = 1;

  public int CurrentHealth
  {
    get { return currentHealth; }
  }

  [SerializeField]
  protected float maxHealth = 1f;

  public float MaxHealth
  {
    get { return maxHealth; }
  }

  protected bool alive = true;
  public bool IsAlive
  {
    get { return alive; }
  }

  [SerializeField]
  protected UIManager uiManager;

  //Health UI
  protected HealthBar healthBar;
  protected Slider healthSlider;
  private RectTransform healthTransform;
  [SerializeField]
  private Vector3 healthOffset;

  //Death Effects
  [SerializeField]
  protected GameObject deathEffectPrefab;
  [SerializeField]
  protected Material deathEffectMaterial;

  protected virtual void Awake()
  {
    healthBar = uiManager.AddHealthBar();
    healthSlider = healthBar.GetComponent<Slider>();
    healthSlider.maxValue = maxHealth;
    healthSlider.value = currentHealth;
    healthTransform = healthSlider.GetComponent<RectTransform>();
  }

  protected virtual void LateUpdate()
  {
    healthTransform.position = Camera.main.WorldToScreenPoint(transform.position + healthOffset);
  }

  public virtual void TakeDamage(int damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
    healthBar.UpdateHealth(currentHealth);
  }

  protected virtual void Die()
  {
    alive = false;
    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    healthBar.gameObject.SetActive(false);
    GameObject deathEffect = Instantiate(deathEffectPrefab);
    deathEffect.transform.position = transform.position;
    deathEffect.transform.rotation = transform.rotation;
    deathEffect.GetComponent<ParticleSystemRenderer>().material = deathEffectMaterial;
    Destroy(deathEffect, deathEffect.GetComponent<ParticleSystem>().main.duration);
    Destroy(this.gameObject);
  }

  private void OnValidate()
  {
    if (maxHealth < currentHealth) maxHealth = currentHealth;
  }
}

