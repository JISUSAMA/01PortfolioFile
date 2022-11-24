using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame5_UIManager : MonoBehaviour
{
    public GameObject[] PaintGrup;
    public Button[] Find1_Btn;

    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    int FindObjectNum=0;
    public void OnClickBtn_Find(GameObject checkMark)
    {
        checkMark.SetActive(true); //체크 마크 활성화 시켜주기 
        if (FindObjectNum != 4) FindObjectNum += 1;
        else
        { 
            //게임 끝
            DementiaGame5_DataManager.instance.playBool = false;
            DementiaGame5_DataManager.instance.Question_Success();
            Touch_GameManager.instance.Game_LoadScene();
        } 
    }
}
