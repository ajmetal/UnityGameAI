using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class TestObj : ScriptableObject
{

  private void Awake()
  {
    Debug.Log("Test Object was created");
  }

  private void OnValidate()
  {

  }
}
