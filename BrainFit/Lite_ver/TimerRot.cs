using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRot : MonoBehaviour
{
    
    void Update()
    {
        //Ÿ�̸� �̹��� ���ۺ���
        this.transform.Rotate(new Vector3(0, 0, -130f * Time.deltaTime));
    }
}
