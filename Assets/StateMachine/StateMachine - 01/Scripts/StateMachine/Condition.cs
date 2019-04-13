using UnityEngine;

public abstract class Condition<StateMachineType> : ScriptableObject
{
  public abstract bool Check(StateMachineType fsm);

}

public abstract class EnemyCondition : Condition<EnemyStateMachine> { }

public abstract class PlayerCondition : Condition<PlayerStateMachine> { }
