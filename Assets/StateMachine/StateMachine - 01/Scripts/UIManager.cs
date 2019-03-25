using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [SerializeField]
  private GameObject healthBarPrefab;

  public static UIManager Instance
  {
    get
    {
      if (instance != null)
      {
        return instance;
      }
      instance = FindObjectOfType<UIManager>();

      if (instance != null)
      {
        return instance;
      }

      var singletonObject = new GameObject();
      instance = singletonObject.AddComponent<UIManager>();
      singletonObject.name = "InputManager";
      DontDestroyOnLoad(singletonObject);
      return instance;
    }

    private set { }
  }

  private static UIManager instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

  }

  public HealthBar AddHealthBar(Unit unit)
  {
    var hb = Instantiate(healthBarPrefab).GetComponent<HealthBar>();
    hb.transform.parent = transform;
    return hb;
  }



}
