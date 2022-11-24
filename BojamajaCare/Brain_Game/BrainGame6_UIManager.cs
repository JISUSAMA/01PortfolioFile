using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame6_UIManager : MonoBehaviour
{
    public Button[] QuestionGrup_Btn;
    public Text[] QuestionGrup_t;
    Color strColor;
    string[][] Split_Str;
    public int Find_Correct_i =0;

    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;
    public void OnClick_MatchColor(int number)
    {
        string str = BrainGame6_DataManager.instance.Random_Color_Lsit[number];
        if (BrainGame6_DataManager.instance.Correct_Color_Lsit.Contains(str)) 
        { 
            Find_Correct_i += 1; 
            QuestionGrup_Btn[number].interactable = false; 
        }
        else
        {
            BrainGame6_DataManager.instance.Question_Fail();
        }
    }
    public void ColorData(int listNumber)
    {
        string text = BrainGame6_DataManager.instance.Random_Color_Lsit[listNumber];
        string[] split_text;
        split_text = text.Split(',');
        QuestionGrup_t[listNumber].text = split_text[0];
        string color_c = split_text[1];
        if (ColorUtility.TryParseHtmlString(color_c, out strColor))
        {
            QuestionGrup_t[listNumber].color = strColor;
        }
    }
}
