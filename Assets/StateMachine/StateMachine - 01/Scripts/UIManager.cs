using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Managers/UIManager")]
public class UIManager : ScriptableObject
{
  [SerializeField]
  private GameObject healthBarPrefab;

  public HealthBar AddHealthBar()
  {
    var hb = Instantiate(healthBarPrefab).GetComponent<HealthBar>();
    hb.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
    return hb;
  }

}
