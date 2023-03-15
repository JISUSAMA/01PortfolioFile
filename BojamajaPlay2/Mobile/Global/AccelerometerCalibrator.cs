using UnityEngine;

public class AccelerometerCalibrator : MonoBehaviour
{
    //Quaternion�� ȸ���� ǥ���ϱ� ���� ����
  public static Quaternion calibrationRotation { get; private set; }

  public static void Calibrate()
  {
    Vector3 offset = Input.acceleration; 
    //�߽����� �������� ȸ���ϰ� ���� ���� ���͸� ���� ���ش�.
    calibrationRotation = Quaternion.FromToRotation(offset, -Vector3.up);
  }
}