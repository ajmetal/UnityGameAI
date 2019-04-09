using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SelectionRect))]
public class CommandManager : MonoBehaviour
{

  private List<Unit> allUnits;
  private List<Unit> alliedUnits;
  private List<Unit> enemyUnits;
  private List<Unit> selectedUnits;

  private SelectionRect selectionRect;

  public LayerMask clickableMask;

  void Awake()
  {
    selectionRect = GetComponent<SelectionRect>();

    selectedUnits = new List<Unit>();

    allUnits = new List<Unit>();
    allUnits.AddRange(FindObjectsOfType<Unit>());

    alliedUnits = new List<Unit>();
    alliedUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.PLAYER)
      .ToArray()
    );

    enemyUnits = new List<Unit>();
    enemyUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.COMPUTER)
      .ToArray()
    );

  }

  void Update()
  {

    //===================================================================
    // Selection
    //===================================================================
    //Release the mouse button
    if (Input.GetMouseButtonUp(0))
    {
      if (!Input.GetKey(KeyCode.LeftShift))
      {
        ClearSelectedUnits();
      }
      for (int i = 0; i < alliedUnits.Count; i++)
      {
        Unit currentUnit = alliedUnits[i];
        if (selectionRect.Contains(currentUnit))
        {
          AddUnit(currentUnit);
        }
      }
    }

    //===================================================================
    // Orders
    //===================================================================
    //right click
    if (Input.GetMouseButtonDown(1))
    {
      if (selectedUnits.Count != 0 && selectedUnits[0].GetAlliance == Unit.Alliance.PLAYER)
      {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f, clickableMask))
        {
          Unit currentUnit = hit.collider.GetComponent<Unit>();
          if (currentUnit != null)
          {
            if (currentUnit.GetAlliance == Unit.Alliance.COMPUTER)
            {
              //if the clicked unit was an enemy, attack it
              IssueAttackOrder(currentUnit);
            }
            else
            {
              //the unit clicked was an ally, move to to it.
              IssueMoveOrder(currentUnit.transform.position);
            }
          }
          else
          {
            //the ground was clicked, move to the point
            IssueMoveOrder(hit.point);
          }
        }
      }
    }
  }

  /// <summary>
  /// Commands the selected unit(s) to move to the location where the player clicked
  /// </summary>
  /// <param name="destination"></param>
  private void IssueMoveOrder(Vector3 destination)
  {
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      selectedUnits[i].Move(destination);
    }
  }

  /// <summary>
  /// Commands the selected unit(s) to attack the target that the player clicked
  /// </summary>
  /// <param name="target"></param>
  private void IssueAttackOrder(Unit target)
  {
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      selectedUnits[i].Attack(target);
    }
  }
  
  /// <summary>
  /// 
  /// </summary>
  /// <param name="unit"></param>
  private void AddUnit(Unit unit)
  {
    selectedUnits.Add(unit);
    unit.SelectUnit();
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearSelectedUnits()
  {
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      selectedUnits[i].DeselectUnit();
    }
    selectedUnits.Clear();
  }

}
