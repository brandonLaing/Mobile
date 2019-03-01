using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
  public void RotateX(float x)
    => transform.Rotate(Vector3.up, x * 100 * Time.deltaTime);

  public void RotateY(float y)
  {
    float angleEulerLimit = Camera.main.transform.eulerAngles.x;

    if (angleEulerLimit > 180)
      angleEulerLimit -= 360;
    if (angleEulerLimit < -180)
      angleEulerLimit += 360;

    int invertYInt = -1;

    var targetYRotation = angleEulerLimit + y * 100 * invertYInt * Time.deltaTime;

    if (targetYRotation < 90 && targetYRotation > -90)
      Camera.main.transform.eulerAngles += new Vector3(y * 100 * invertYInt * Time.deltaTime, 0, 0);
  }

  public void RotateCamera(float x, float y)
  {
    RotateX(x); RotateY(y);
  }

  public void RotateCamera(Vector2 direction)
  {
    RotateX(direction.x); RotateY(direction.y);
  }
}
