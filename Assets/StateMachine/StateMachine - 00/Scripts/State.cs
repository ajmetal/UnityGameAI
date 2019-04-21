using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
  NULL = 0,
  PATROL,
  CHASE
}

public enum TransitionID
{
  NULL = 0,
  PLAYER_DETECTED
}

public class PatrolState : State
{
  private List<Vector3> pathPoints;
  private int currentPoint = 0;
  private Unit unit;

  public PatrolState(StateMachine fsm)
  {
    stateID = StateID.PATROL;

    AddTransition(TransitionID.PLAYER_DETECTED, StateID.CHASE);

    pathPoints = fsm.pathPoints;
    unit = fsm.GetComponent<Unit>();
  }

  public override void Act()
  {
    //Patrol between points
    if (unit.NavMeshAgentStopped())
    {
      currentPoint = (currentPoint + 1) % pathPoints.Count;
      unit.Move(pathPoints[currentPoint]);
    }

  }

  public override TransitionID Decide()
  {
    if(/*something*/)
    {
      return TransitionID.PLAYER_DETECTED;
    }
  }

}

public abstract class State
{
  protected Dictionary<TransitionID, StateID> transitions;

  protected StateID stateID;
  public StateID ID
  {
    get { return stateID; }
  }

  protected void AddTransition(TransitionID t, StateID s)
  {
    if (transitions.ContainsKey(t))
    {
      Debug.LogError("Cannot add duplicate transition: " + t.ToString());
      return;
    }

    transitions.Add(t, s);
  }

  public abstract void Act();
  public abstract TransitionID Decide();
}
