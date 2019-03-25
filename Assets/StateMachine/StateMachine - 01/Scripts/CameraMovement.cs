using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

  public float xBorder = 100f;
  public float yBorder = 100f;

  public float panSpeed = 10f;
  public float scrollSpeed = 10f;

  public float minZoom = 1f;
  public float maxZoom = 20f;


  void Update()
  {
    if (Input.GetKey("a") || Input.mousePosition.x <= xBorder)
    {
      transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
    }
    else if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - xBorder)
    {
      transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
    }
    if(Input.GetKey("s") || Input.mousePosition.y <= yBorder)
    {
      transform.Translate(-transform.forward * panSpeed * Time.deltaTime);
    }
    else if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - yBorder)
    {
      transform.Translate(transform.forward * panSpeed * Time.deltaTime);
    }

    float scroll = Input.GetAxis("Mouse ScrollWheel") * -1;
    Camera.main.orthographicSize += scroll * scrollSpeed * Time.deltaTime;
    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

  }
}
