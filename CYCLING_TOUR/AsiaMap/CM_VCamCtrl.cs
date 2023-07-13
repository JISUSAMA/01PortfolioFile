using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CM_VCamCtrl : MonoBehaviour
{
    CinemachineVirtualCamera virtualCameraStop;
    CinemachineVirtualCamera virtualCamera;
    CinemachineVirtualCamera virtualCamera_1;
    CinemachineVirtualCamera virtualCamera_2;
    CinemachineVirtualCamera virtualCamera2;
    CinemachineVirtualCamera virtualCamera3;
    CinemachineVirtualCamera virtualCamera_CourseEnd1;
    CinemachineVirtualCamera virtualCamera_CourseEnd2;
    CinemachineVirtualCamera virtualCamera_CourseEnd2_Goalin;
    CinemachineVirtualCamera virtualCamera_MountainView1;

    CinemachineBasicMultiChannelPerlin virtualCamera_1Parlin;
    CinemachineBasicMultiChannelPerlin virtualCameraPerlin;
    CinemachineBasicMultiChannelPerlin virtualCamera_2Perlin;
    CinemachineBasicMultiChannelPerlin virtualCamera2Parlin;
    CinemachineBasicMultiChannelPerlin virtualCamera3Parlin;




    private void OnTriggerEnter(Collider other)
    {

        if (SceneManager.GetActiveScene().name == "AsiaMap 3")
        {
            //Debug.Log("들어왓는데");
            if(AsiaMap_UIManager.instance.track_Course.m_Speed == 0f)
            {
                virtualCameraStop = GameObject.Find("CM vcam Stop").GetComponent<CinemachineVirtualCamera>();
                
            } 
            else if(AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
            {
                virtualCamera_1 = GameObject.Find("CM vcam1_2").GetComponent<CinemachineVirtualCamera>();
                virtualCamera_1Parlin = virtualCamera_1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
            {
                virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
                virtualCameraPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            } 
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
            {
                virtualCamera_2 = GameObject.Find("CM vcam1_3").GetComponent<CinemachineVirtualCamera>();
                virtualCamera_2Perlin = virtualCamera_2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
                

            virtualCamera2 = GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>();
            virtualCamera2Parlin = virtualCamera2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            virtualCamera3 = GameObject.Find("CM vcam3").GetComponent<CinemachineVirtualCamera>();
            virtualCamera3Parlin = virtualCamera3.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            virtualCamera_CourseEnd1 = GameObject.Find("CM vcam_End1").GetComponent<CinemachineVirtualCamera>();
            virtualCamera_CourseEnd2 = GameObject.Find("CM vcam_End2").GetComponent<CinemachineVirtualCamera>();
            virtualCamera_CourseEnd2_Goalin = GameObject.Find("CM vcam_CourseEnd_Goalin").GetComponent<CinemachineVirtualCamera>();
            virtualCamera_MountainView1 = GameObject.Find("CM vcam_MountainView1").GetComponent<CinemachineVirtualCamera>();
        }

        if (other.CompareTag("Right"))
        {
            //Debug.Log("Right");

            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
            {
                //Debug.Log("머임 - 오");
                virtualCamera_2.Priority = 10;
            }
                

            virtualCamera2.Priority = 11;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("R_Right"))
        {
            //Debug.Log("R_Right");
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
                virtualCamera_2.Priority = 11;

            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("Left"))
        {
            //Debug.Log("Left");
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
            {
                //Debug.Log("머임 - 왼");
                virtualCamera_2.Priority = 10;
            }
            virtualCamera3.Priority = 11;
            virtualCamera2.Priority = 10;
        }
        else if(other.CompareTag("R_Left"))
        {
            //Debug.Log("Left R");
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
                virtualCamera_2.Priority = 11;

            virtualCamera3.Priority = 10;
            virtualCamera2.Priority = 10;
        }
        else if(other.CompareTag("Course_End1"))
        {
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
                virtualCamera_2.Priority = 10;

            virtualCamera_CourseEnd1.Priority = 11;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("R_Course_End1"))
        {
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 11;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
                virtualCamera_2.Priority = 11;

            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
            virtualCamera_CourseEnd1.Priority = 10;
        }
        else if(other.CompareTag("Course_End2"))
        {
            virtualCamera_CourseEnd2.Priority = 11;
            virtualCamera2.Priority = 10;
        }
        else if(other.CompareTag("R_Course_End2"))
        {
            virtualCamera_CourseEnd2.Priority = 10;
            virtualCamera_CourseEnd2_Goalin.Priority = 11;
        }
        else if(other.CompareTag("MountainView_Start1"))
        {
            if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 5.0f)
                virtualCamera_1.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed <= 10.0f && AsiaMap_UIManager.instance.track_Course.m_Speed > 5.0f)
                virtualCamera.Priority = 10;
            else if (AsiaMap_UIManager.instance.track_Course.m_Speed > 10.0f)
                virtualCamera_2.Priority = 10;

            virtualCamera_MountainView1.Priority = 11;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("MountainView_End1"))
        {
            virtualCamera_MountainView1.Priority = 10;
        }
        
        
        if(other.CompareTag("MountainUp"))
        {
            AsiaMap_UIManager.instance.mountainUpstate = true;  //산 진입
        }
        else if(other.CompareTag("MountainUpEnd"))
        {
            AsiaMap_UIManager.instance.mountainUpstate = false;  //산 진입
        }

        if(other.CompareTag("MountainDownStart"))
        {
            //Debug.Log("스피드 업");
            AsiaMap_UIManager.instance.mountainDownState = true;    //내릿막길
        }
        else if(other.CompareTag("MountainDownEnd"))
        {
            //Debug.Log("스피드 다운");
            AsiaMap_UIManager.instance.mountainDownState = false;    //내릿막길
        }


        if (other.CompareTag("End"))
        {
            AsiaMap_UIManager.instance.gameEnd = true;
        }

        //자갈밭
        if(other.CompareTag("StonFarmStart"))
        {
            AsiaMap_UIManager.instance.stonFarmState = true;   //자갈밭 진입

            
        }
        else if(other.CompareTag("StonFarmEnd"))
        {
            AsiaMap_UIManager.instance.stonFarmState = false;   //자갈밭 나옴 

        }

        //웅덩이
        if (other.CompareTag("PuddleStart"))
        {
            AsiaMap_UIManager.instance.puddleState = true;   //웅덩이 진입
        }
        else if (other.CompareTag("PuddleEnd"))
        {
            AsiaMap_UIManager.instance.puddleState = false;   //웅덩이 나옴 
        }

        //모래
        if(other.CompareTag("SandStart"))
        {
            //Debug.Log("모래충돌");
            AsiaMap_UIManager.instance.sandState = true;   //웅덩이 진입
        }
        else if(other.CompareTag("SandEnd"))
        {
            AsiaMap_UIManager.instance.sandState = false;   //웅덩이 나옴 
        }

    }

}
