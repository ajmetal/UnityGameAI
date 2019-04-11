using UnityEngine;
using System.Collections;

public class PlayerStateMachine : StateMachine<PlayerState>
{
  protected override void Update()
  {
    currentState.OnUpdate(this);
  }
}
