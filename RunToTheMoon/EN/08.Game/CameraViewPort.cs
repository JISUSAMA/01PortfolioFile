using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraViewPort : MonoBehaviour
{
    public CinemachineVirtualCamera[] vCame; //0:main 1:left 2:right
    public float StartEndCount_Check = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left"))
        {
            Debug.Log("collision_left");
            if (other.gameObject.name.Equals("start"))
            {
                Debug.Log("collision_left-------1");
                StartEndCount_Check = 1;
                vCame[1].m_Priority = 12;
            }
            else if (other.gameObject.name.Equals("end"))
            {
                Debug.Log("collision_left-------2");
                StartEndCount_Check = 2;
                vCame[1].m_Priority = 10;
            }
        }
        else if (other.CompareTag("right"))
        {
            Debug.Log("collision_right");
            if (other.gameObject.name.Equals("start"))
            {
                Debug.Log("collision_right-------1");
                StartEndCount_Check = 1;
                vCame[2].m_Priority = 12;
            }
            else if (other.gameObject.name.Equals("end"))
            {
                Debug.Log("collision_right-------2");
                StartEndCount_Check = 2;
                vCame[2].m_Priority = 10;
            }
        }

    }

}