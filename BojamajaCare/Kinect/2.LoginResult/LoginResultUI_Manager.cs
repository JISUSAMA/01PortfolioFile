using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginResultUI_Manager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //ȸ������
    public void JoinOnButton()
    {
        SceneManager.LoadScene("3.FaceShoot");
    }

    //�Խ�Ʈ����
    public void GuestButtonOn()
    {
        SceneManager.LoadScene("5.GuestExercise");
    }

    //�ٽ��ϱ�
    public void RePlayButton()
    {
        SceneManager.LoadScene("1.Login");
    }

    public void LoginButtonOn()
    {
        SceneManager.LoadScene("5.UserExercise");
    }

    public void SystemCloseBtnClickOn()
    {
        Application.Quit();
    }

}
