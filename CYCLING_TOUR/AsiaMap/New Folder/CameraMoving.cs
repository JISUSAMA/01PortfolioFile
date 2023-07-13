using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{

    public Transform target;           // target 
    public float dist = 10.0f;           // 거리
    public float height = 5.0f;         // 높이
    public float dampRotate = 5.0f;  //회전 속도
    public float cameraX;

    private Transform tr;             // 카메라 

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        //Mathf.LerpAngle(float s, float e, flaot t) = t시간 동안 s부터 e까지 각도를 변환하는 것.
        float cur_Y_Angle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, dampRotate * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, cur_Y_Angle, 0);

        //tr.eulerAngles = new Vector3(cameraX, tr.rotation.y, tr.rotation.y);
        //타겟 위치 - 카메라위치 = 카메라가 타겟 뒤로 따라가기
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);

        tr.LookAt(target);
    }


    void Update()
    {
    }
}
