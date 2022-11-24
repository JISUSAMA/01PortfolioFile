using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame7_UIManager : MonoBehaviour
{
    [Header("UI")]
  //  public GameObject[] QuestionGrup;
    public Image[] Question_img_left; 
    public Image[] Question_img_right;
    public Sprite[] LeftIMG_sprite;
    public Sprite[] RightIMG_sprite;
    public Toggle[] Q1_toggle, Q2_toggle, Q3_toggle, Q4_toggle, Q5_toggle;
    public bool Q1, Q2, Q3, Q4, Q5;
    string Q1_str, Q2_str, Q3_str, Q4_str, Q5_str;
   
    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;
  
    public void Toggle_Check()
    {
        if(Q1_toggle[0].isOn) { Q1 = true; } else { Q1 = false; }
        if(Q2_toggle[0].isOn) { Q2 = true; } else { Q2 = false; }
        if(Q3_toggle[0].isOn) { Q3 = true; } else { Q3 = false; }
        if(Q4_toggle[0].isOn) { Q4 = true; } else { Q4 = false; }
        if(Q5_toggle[0].isOn) { Q5 = true; } else { Q5 = false; }
     
    }
    public void OnClick_OK_button()
    {
        Q1_str = Q1.ToString(); Q2_str = Q2.ToString(); Q3_str = Q3.ToString(); Q4_str = Q4.ToString(); Q5_str = Q5.ToString();
        //정답일경우 
        if (BrainGame7_DataManager.instance.Answer_StrList[0].Equals(Q1_str)
            && BrainGame7_DataManager.instance.Answer_StrList[1].Equals(Q2_str) 
            && BrainGame7_DataManager.instance.Answer_StrList[2].Equals(Q3_str) 
            && BrainGame7_DataManager.instance.Answer_StrList[3].Equals(Q4_str) 
            && BrainGame7_DataManager.instance.Answer_StrList[4].Equals(Q5_str)) 
        {
            BrainGame7_DataManager.instance.Question_Success();
        }
        else
        {
            BrainGame7_DataManager.instance.Question_Fail();
        }
    }
}
