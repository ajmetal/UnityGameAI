//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class StateController : MonoBehaviour
//{
//  public MonoBehaviour component;
//  StateMachine<MonoBehaviour> fsm;

//  List<State<MonoBehaviour>> states;

//  protected virtual void Awake()
//  {
//    fsm = new StateMachine<MonoBehaviour>(component);
//  }

//  protected virtual void Update()
//  {
//    fsm.OnUpdate(component);
//  }

//  private void OnValidate()
//  {

//  }

//}
