using UnityEngine;
using System.Collections.Generic;

namespace FSM
{

  public enum StateID
  {
    NULL_STATE = 0, // Use this ID to represent a non-existing State in your system	
    PATROL_STATE,
    CHASE_STATE,
    ATTACK_STATE
  }

  public abstract class State
  {
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateID;
    public StateID ID { get { return stateID; } }

    public void AddTransition(Transition trans, StateID id)
    {
      if (trans == Transition.NULL_TRANSITION)
      {
        Debug.LogError("State ERROR: NullTransition is not allowed for a real transition");
        return;
      }

      if (id == StateID.NULL_STATE)
      {
        Debug.LogError("State ERROR: NullStateID is not allowed for a real ID");
        return;
      }

      if (map.ContainsKey(trans))
      {
        Debug.LogError("State ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                       "Impossible to assign to another state");
        return;
      }

      map.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
      if (trans == Transition.NULL_TRANSITION)
      {
        Debug.LogError("State ERROR: NullTransition is not allowed");
        return;
      }

      if (map.ContainsKey(trans))
      {
        map.Remove(trans);
        return;
      }

      Debug.LogError("State ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                     " was not on the state's transition list");
    }

    public StateID GetOutputState(Transition trans)
    {
      if (map.ContainsKey(trans))
      {
        return map[trans];
      }
      return StateID.NULL_STATE;
    }

    public virtual void OnEnterState() { }

    public virtual void OnLeaveState() { }

    public abstract Transition Decide();

    public abstract void Act();

  } 
}