using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CM_VCamCrtl_Busan : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name.Equals("BusanMapMorning") || SceneManager.GetActiveScene().name.Equals("BusanMapNight")
            ||SceneManager.GetActiveScene().name.Equals("BusanGreenLineMorning")|| SceneManager.GetActiveScene().name.Equals("BusanGreenLineNight"))
        {
            virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            virtualCamera2 = GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>();
            virtualCamera3 = GameObject.Find("CM vcam3").GetComponent<CinemachineVirtualCamera>();
        }


        if (other.CompareTag("Right"))
        {
            virtualCamera.Priority = 10;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 11;
        }
        else if(other.CompareTag("R_Right"))
        {
            virtualCamera.Priority = 11;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("Left"))
        {
            virtualCamera.Priority = 10;
            virtualCamera2.Priority = 11;
            virtualCamera3.Priority = 10;
        }
        else if(other.CompareTag("R_Left"))
        {
            virtualCamera.Priority = 11;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 10;
        }

        if (other.CompareTag("MountainUp"))
        {
            Busan_UIManager.instance.mountainUpstate = true;  //산 진입
        }
        else if (other.CompareTag("MountainUpEnd"))
        {
            Busan_UIManager.instance.mountainUpstate = false;  //산 진입
        }

        if (other.CompareTag("MountainDownStart"))
        {
            //Debug.Log("스피드 업");
            Busan_UIManager.instance.mountainDownState = true;    //내릿막길
        }
        else if (other.CompareTag("MountainDownEnd"))
        {
            //Debug.Log("스피드 다운");
            Busan_UIManager.instance.mountainDownState = false;    //내릿막길
        }

        if (other.CompareTag("End"))
        {
            Busan_UIManager.instance.gameEnd = true;
        }



        if(other.CompareTag("LightRot"))
        {
            if(other.gameObject.name.Equals("LightRot1"))
            {
                Busan_UIManager.instance.LightRotionMove(33.558f, 57.085f, 165.195f);
            }
            else if(other.gameObject.name.Equals("LightRot2"))
            {
                Busan_UIManager.instance.LightRotionMove(29.574f, 16.322f, 81.495f);
            }
            else if (other.gameObject.name.Equals("LightRot3"))
            {
                Busan_UIManager.instance.LightRotionMove(34.422f, -16.463f, 18.417f);
            }
            else if (other.gameObject.name.Equals("LightRot4"))
            {
                Busan_UIManager.instance.LightRotionMove(23.481f, -165.267f, -337.375f);
            }
            else if (other.gameObject.name.Equals("LightRot5"))
            {
                Busan_UIManager.instance.LightRotionMove(51.315f, -370.065f, -247.99f);
            }
            else if (other.gameObject.name.Equals("LightRot6"))
            {
                Busan_UIManager.instance.LightRotionMove(11.915f, -161.495f, -78.165f);
            }
            else if (other.gameObject.name.Equals("LightRot7"))
            {
                Busan_UIManager.instance.LightRotionMove(23.281f, -7.436f, 203.219f);
            }
            else if (other.gameObject.name.Equals("LightRot8"))
            {
                Busan_UIManager.instance.LightRotionMove(42.98f, -236.469f, -64.064f);
            }
        }


        if (other.CompareTag("SeaStart"))
        {
            SoundMaixerManager.instance.BeachSoundPlay();
        }
        else if(other.CompareTag("SeaEnd"))
        {
            SoundMaixerManager.instance.BeachSoundStop();
        }



        //자갈밭
        if (other.CompareTag("StonFarmStart"))
        {
            Busan_UIManager.instance.stonFarmState = true;   //자갈밭 진입
        }
        else if (other.CompareTag("StonFarmEnd"))
        {
            Busan_UIManager.instance.stonFarmState = false;   //자갈밭 나옴 
        }

        //웅덩이
        if (other.CompareTag("PuddleStart"))
        {
            Busan_UIManager.instance.puddleState = true;   //웅덩이 진입
        }
        else if (other.CompareTag("PuddleEnd"))
        {
            Busan_UIManager.instance.puddleState = false;   //웅덩이 나옴 
        }

        //모래
        if (other.CompareTag("SandStart"))
        {
            //Debug.Log("모래충돌");
            Busan_UIManager.instance.sandState = true;   //웅덩이 진입
        }
        else if (other.CompareTag("SandEnd"))
        {
            Busan_UIManager.instance.sandState = false;   //웅덩이 나옴 
        }
    }
}
