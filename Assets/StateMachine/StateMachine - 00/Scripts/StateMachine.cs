using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  public List<Vector3> pathPoints;

  private Dictionary<StateID, State> states;
  private State currentState;

  private void Awake()
  {
    if(pathPoints == null)
    {
      pathPoints = new List<Vector3>();
    }

    AddState(new PatrolState(this));

  }

  private void AddState(State state)
  {
    if (states.ContainsKey(state.ID))
    {
      Debug.LogError("Cannot add duplicate state: " + state.ID.ToString());
      return;
    }

    if(states.Count == 0)
    {
      currentState = state;
    }

    states.Add(state.ID, state);
  }

  // Update is called once per frame
  void Update()
  {
    currentState.Act();
  }
}
