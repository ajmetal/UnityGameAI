using UnityEngine;
using System.Collections.Generic;

public abstract class StateMachine<StateType> : MonoBehaviour
{
  [SerializeField]
  protected StateType currentState;

  [SerializeField]
  protected List<StateType> states;

  protected abstract void Update();
}

