using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FSM;

public class EnemyStateMachine : MonoBehaviour
{
  public List<Vector3> pathPoints;

  private Dictionary<StateID, State> states;

  private StateID currentStateID;
  public StateID CurrentStateID { get { return currentStateID; } }
  private State currentState;
  public State CurrentState { get { return currentState; } }

  void Awake()
  {
    states = new Dictionary<StateID, State>();

    AddState(new PatrolState(gameObject));
    AddState(new AttackState(gameObject));
    AddState(new ChasePlayerState(gameObject));
  }

  private void Update()
  {
    Transition t = currentState.Decide();
    if (t != Transition.NULL_TRANSITION)
    {
      PerformTransition(t);
    }
    currentState.Act();
  }

  private void OnDrawGizmosSelected()
  {
    if (pathPoints != null)
    {
      for (int i = 0; i < pathPoints.Count; ++i)
      {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(pathPoints[i], Vector3.one);
        Handles.Label(pathPoints[i] + Vector3.up, i.ToString());
      }
    }

    if (currentState != null)
    {
      Handles.Label(transform.position + Vector3.up, currentStateID.ToString());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  public void AddState(State s)
  {
    if (s == null)
    {
      Debug.LogError("FSM ERROR: Null reference is not allowed");
    }

    if (states.ContainsKey(s.ID))
    {
      Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
             " because state has already been added");
      return;
    }
    else if (states.Count == 0)
    {
      currentState = s;
      currentStateID = s.ID;
    }

    states.Add(s.ID, s);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  public void DeleteState(StateID id)
  {
    // Check for NullState before deleting
    if (id == StateID.NULL_STATE)
    {
      Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
      return;
    }

    if (!states.ContainsKey(id))
    {
      Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
             ". It was not on the list of states");
      return;
    }

    states.Remove(id);

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="trans"></param>
  public void PerformTransition(Transition trans)
  {
    if (trans == Transition.NULL_TRANSITION)
    {
      Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
      return;
    }

    StateID id = currentState.GetOutputState(trans);
    if (id == StateID.NULL_STATE)
    {
      Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                     " for transition " + trans.ToString());
      return;
    }

    currentState.OnLeaveState();
    currentState = states[id];
    Debug.Log("Changed to state: " + currentState.ID.ToString());
    currentState.OnEnterState();

  }

}
