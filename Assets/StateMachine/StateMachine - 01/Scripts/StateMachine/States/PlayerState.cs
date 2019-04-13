using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Player/State")]
public class PlayerState
  : State<PlayerStateMachine, PlayerAction>
{

  [Serializable]
  public struct PlayerTransition
  {
    PlayerCondition condition;
    PlayerState trueState;
    PlayerState falseState;
  }

  [SerializeField]
  protected List<PlayerTransition> transitions;
}