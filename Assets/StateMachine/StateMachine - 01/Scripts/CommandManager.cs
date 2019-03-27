using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandManager : MonoBehaviour
{
  //public static CommandManager Instance
  //{
  //  get
  //  {
  //    if (instance != null)
  //    {
  //      return instance;
  //    }
  //    instance = FindObjectOfType<CommandManager>();

  //    if (instance != null)
  //    {
  //      return instance;
  //    }

  //    var singletonObject = new GameObject();
  //    instance = singletonObject.AddComponent<CommandManager>();
  //    singletonObject.name = "InputManager";
  //    DontDestroyOnLoad(singletonObject);
  //    return instance;
  //  }

  //  private set { }
  //}

  //private static CommandManager instance;

  private List<Unit> allUnits;
  private List<Unit> alliedUnits;
  private List<Unit> enemyUnits;
  private List<Unit> selectedUnits;

  [SerializeField]
  private RectTransform squareTransform;

  //To determine if we are clicking with left mouse or holding down left mouse
  private float delay = 0.2f;
  private float clickTime = 0f;
  //The start and end coordinates of the square we are making
  private Vector3 squareStartPos;
  private Vector3 squareEndPos;
  private Vector3 startCameraPos;
  private Vector3 lastCameraPos;
  //The selection square's 4 corner positions
  private Vector3 TL, TR, BL, BR;

  private AudioSource audioSource;

  [SerializeField]
  private List<AudioClip> attackVoiceOvers;
  [SerializeField]
  private List<AudioClip> moveVoiceOvers;

  void Awake()
  {
    //if (instance == null)
    //{
    //  instance = this;
    //}
    //else
    //{
    //  Destroy(gameObject);
    //  return;
    //}

    //DontDestroyOnLoad(gameObject);

    selectedUnits = new List<Unit>();
    squareTransform.gameObject.SetActive(false);

    allUnits = new List<Unit>();
    allUnits.AddRange(FindObjectsOfType<Unit>());

    alliedUnits = new List<Unit>();
    alliedUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.PLAYER)
      .ToArray()
    );

    enemyUnits = new List<Unit>();
    enemyUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.COMPUTER)
      .ToArray()
    );

    audioSource = GetComponent<AudioSource>();
    //attackVoiceOvers = new List<AudioClip>();
    //moveVoiceOvers = new List<AudioClip>();

  }

  void Update()
  {

    //===================================================================
    // Selection
    //===================================================================
    //Click the mouse button
    if (Input.GetMouseButtonDown(0))
    {
      clickTime = Time.time;
      squareStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      lastCameraPos = Camera.main.transform.position;
    }

    //mouse button held down
    if (Input.GetMouseButton(0) && (Time.time - clickTime >= delay))
    {
      //Get the latest coordinate of the square
      squareEndPos = Input.mousePosition;

      DisplaySquare();
    }

    //Release the mouse button
    if (Input.GetMouseButtonUp(0))
    {
      if (!Input.GetKey(KeyCode.LeftShift))
      {
        ClearSelectedUnits();
      }

      //clicked once
      if (Time.time - clickTime <= delay)
      {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
        {
          Unit unit = hit.collider.gameObject.GetComponent<Unit>();
          if (unit != null)
          {
            AddUnit(unit);
          }
        }
      }

      //click was held
      else
      {
        squareTransform.gameObject.SetActive(false);

        for (int i = 0; i < alliedUnits.Count; i++)
        {
          Unit currentUnit = alliedUnits[i];
          Vector3 screenPos = Camera.main.WorldToScreenPoint(currentUnit.transform.position);

          //TODO: Make this take into account the size of the unit's selection collider
          if (screenPos.x >= BL.x && screenPos.x <= BR.x && screenPos.y >= BL.y && screenPos.y <= TL.y)
          {
            AddUnit(currentUnit);
          }
        }
      }
    }

    //===================================================================
    // Orders
    //===================================================================
    if (Input.GetMouseButtonDown(1))
    {
      if (selectedUnits.Count != 0)
      {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
        {
          Unit currentUnit = hit.collider.GetComponent<Unit>();
          if (currentUnit != null)
          {
            if (currentUnit.GetAlliance == Unit.Alliance.COMPUTER)
            {
              IssueAttackOrder(currentUnit);

            }
            else
            {
              IssueMoveOrder(currentUnit.transform.position);
            }
          }
          else
          {
            IssueMoveOrder(hit.point);
          }
        }
      }
    }
  }

  private void IssueMoveOrder(Vector3 destination)
  {
    if (Random.Range(0, 4) <= 1)
    {
      audioSource.PlayOneShot(
        moveVoiceOvers[Random.Range(0, moveVoiceOvers.Count)],
        1
      );
    }
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      //selectedUnits[i].IssueCommand(new Command(CommandType.MOVE, destination));
      selectedUnits[i].Move(destination);
    }
  }

  private void IssueAttackOrder(Unit target)
  {
    if (Random.Range(0, 4) <= 1)
    {
      audioSource.PlayOneShot(
        attackVoiceOvers[Random.Range(0, attackVoiceOvers.Count)],
        1
      );
    }
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      //selectedUnits[i].IssueCommand(new Command(CommandType.ATTACK, target));
      selectedUnits[i].Attack(target);
    }
  }
  
  /// <summary>
  /// 
  /// </summary>
  /// <param name="unit"></param>
  private void AddUnit(Unit unit)
  {
    selectedUnits.Add(unit);
    unit.SelectUnit();
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearSelectedUnits()
  {
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      selectedUnits[i].DeselectUnit();
    }
    selectedUnits.Clear();
  }

  //Display the selection with a GUI square
  void DisplaySquare()
  {
    squareTransform.gameObject.SetActive(true);

    Vector3 startPos = Camera.main.WorldToScreenPoint(squareStartPos);

    //Get the middle position of the square
    Vector3 middle = (startPos + squareEndPos) / 2f;

    //Set the middle position of the GUI square
    squareTransform.position = middle;

    //Change the size of the square
    float sizeX = Mathf.Abs(startPos.x - squareEndPos.x);
    float sizeY = Mathf.Abs(startPos.y - squareEndPos.y);

    //Set the size of the square
    squareTransform.sizeDelta = new Vector2(sizeX, sizeY);

    TL = new Vector3(middle.x - sizeX / 2f, middle.y + sizeY / 2f, 0f);
    TR = new Vector3(middle.x + sizeX / 2f, middle.y + sizeY / 2f, 0f);
    BL = new Vector3(middle.x - sizeX / 2f, middle.y - sizeY / 2f, 0f);
    BR = new Vector3(middle.x + sizeX / 2f, middle.y - sizeY / 2f, 0f);

  }

}
