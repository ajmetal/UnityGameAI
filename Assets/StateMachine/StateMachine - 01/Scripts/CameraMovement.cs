using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

  public float xBorder = 100f;
  public float yBorder = 100f;

  public float panSpeed = 10f;
  public float orthoSpeedAdjust = 1.5f;
  public float scrollSpeed = 10f;

  public float minZoom = 1f;
  public float maxZoom = 20f;

  public float minXClamp = -1000;
  public float maxXClamp = 1000;
  public float minZClamp = -1000;
  public float maxZClamp = 1000;

  void Update()
  {
    Vector3 delta = Vector3.zero;

    if (Input.GetKey("a") || Input.mousePosition.x <= xBorder)
    {
      delta += Vector3.left;
    }
    else if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - xBorder)
    {
      delta += Vector3.right;
    }
    if(Input.GetKey("s") || Input.mousePosition.y <= yBorder)
    {
      if (Camera.main.orthographic)
      {
        delta += Vector3.back * orthoSpeedAdjust;
      }
      else
      {
        delta += Vector3.back;
      }
    }
    else if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - yBorder)
    {
      if (Camera.main.orthographic)
      {
        delta += Vector3.forward * orthoSpeedAdjust;
      }
      else {
        delta += Vector3.forward;
      }
    }
    transform.Translate(delta * panSpeed * Time.deltaTime);

    transform.position = new Vector3(
      Mathf.Clamp(transform.position.x, minXClamp, maxXClamp),
      transform.position.y,
      Mathf.Clamp(transform.position.z, minZClamp, maxZClamp)
    );

    float scroll = Input.GetAxis("Mouse ScrollWheel") * -1;
    Camera.main.orthographicSize += scroll * scrollSpeed * Time.deltaTime;
    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

  }
}
