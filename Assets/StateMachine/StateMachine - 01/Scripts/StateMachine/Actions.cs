using UnityEngine;

public abstract class Action<StateMachineType> : ScriptableObject
{
  public abstract void Act(StateMachineType fsm);
}

public abstract class EnemyAction : Action<EnemyStateMachine> { }

public abstract class PlayerAction : Action<PlayerStateMachine> { }

