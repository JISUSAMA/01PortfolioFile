using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame5_UIManager : MonoBehaviour
{
    public Button [] NumberBtn;
    public Text[] NumberBtn_text;
 
    public int CurrnetOrderNum =-1;

    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    public void OnClick_WordBtn(Text word)
    {
        CurrnetOrderNum += 1;
        //Debug.Log("word.text:" + word.text + "CurrnetOrderNum" + CurrnetOrderNum);
        AnswerQusetion(word.text, word.gameObject.GetComponentInParent<Button>());
    }
    public void AnswerQusetion(string wordStr ,Button Parent)
    {
        //정답
        if (BrainGame5_DataManager.instance.Order_word[CurrnetOrderNum].Equals(wordStr))
        {
            Parent.interactable = false;
            if (CurrnetOrderNum > 25)
            {
                BrainGame5_DataManager.instance.Question_Success();
            }
        }
        //틀림
        else
        {
            BrainGame5_DataManager.instance.Question_Fail();
        }
    }
}
