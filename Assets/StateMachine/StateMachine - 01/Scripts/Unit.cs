using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

abstract public class Unit : MonoBehaviour
{

  [SerializeField]
  protected GameObject objective;
  [SerializeField]
  protected GameObject selectionIcon;

  private static int unitCount = 0;

  public enum Alliance
  {
    PLAYER,
    COMPUTER
  }
  protected Alliance alliance = Alliance.COMPUTER;
  public Alliance GetAlliance
  {
    get { return alliance; }
  }

  //fields
  [SerializeField]
  protected int health = 1;

  public int Health
  {
    get { return health; }
  }

  [SerializeField]
  protected float maxHealth = 1f;

  public float MaxHealth
  {
    get { return maxHealth; }
  }

  [SerializeField]
  protected float attackRange = 1f;
  [SerializeField]
  protected float attackSpeed = 1f;
  [SerializeField]

  //abstract methods
  public abstract void SelectUnit();
  public abstract void DeselectUnit();
  public abstract void Move(Vector3 destination);
  public abstract void Attack(Unit target);

  //the unit this unit is attacking
  protected Unit currentTarget;
  public Unit CurrentTarget
  {
    get { return currentTarget; }
  }

  protected int unitID;
  public int UnitID
  {
    get { return unitID; }
  }

  //Health UI
  protected HealthBar healthBar;
  protected Slider healthSlider;
  private RectTransform healthTransform;
  [SerializeField]
  private Vector3 healthOffset;

  protected virtual void Awake()
  {
    unitID = unitCount++;
    healthBar = UIManager.Instance.AddHealthBar(this);
    healthSlider = healthBar.GetComponent<Slider>();
    healthSlider.maxValue = maxHealth;
    healthSlider.value = health;
    healthTransform = healthSlider.GetComponent<RectTransform>();
    //objective = new GameObject();
    //commands = new Queue<Command>();
  }

  protected virtual void Update()
  {
    //Debug.Log(currentCommand);
    //if(currentCommand == null && commands.Count > 0)
    //{
    //  Command command = commands.Dequeue();
    //  switch(command.type)
    //  {
    //    case CommandType.ATTACK:
    //      currentCommand = StartCoroutine(Attack(command));
    //      break;
    //    case CommandType.MOVE:
    //      currentCommand = StartCoroutine(Move(command));
    //      break;
    //  }
    //}
  }

  protected virtual void LateUpdate()
  {
    healthTransform.position = Camera.main.WorldToScreenPoint(transform.position + healthOffset);
  }

  //public void IssueCommand(Command command)
  //{
  //  commands.Enqueue(command);
  //}

  public void TakeDamage(int damage)
  {
    Debug.Log("damage taken by: " + name);
    health -= damage;
    if (health <= 0)
    {
      Destroy(gameObject);
      Destroy(healthBar.gameObject);
    }
    healthBar.UpdateHealth(health);
  }

  private void OnValidate()
  {
    if (maxHealth < health) maxHealth = health;
  }

}

