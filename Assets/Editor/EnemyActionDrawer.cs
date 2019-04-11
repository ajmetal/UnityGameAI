using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyAction))]
public class EnemyActionDrawer : ExtendedScriptableObjectDrawer
{

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {

    base.OnGUI(position, property, label);
  }
}