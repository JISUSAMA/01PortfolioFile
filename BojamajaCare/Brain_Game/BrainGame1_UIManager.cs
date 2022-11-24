using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame1_UIManager : MonoBehaviour
{
    public Button[] Piece_btn;
    public Text[] Piece_btn_text;
    public Text QuestionWord_text;
    public Text FindWord_text;
    public int Click_num = -1;

    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    public void OnClick_PieceBtn(Text Piece_btn_text)
    {
        Click_num += 1;
        AnswerQusetion(Piece_btn_text.text);
    }
    
    public void AnswerQusetion(string Piece_btn_text)
    {
        if(QuestionWord_text.text.Length>= FindWord_text.text.Length)
        {
            FindWord_text.text += Piece_btn_text;
          
            if (FindWord_text.text.Equals(QuestionWord_text.text.Substring(0, Click_num + 1)))
            {
               // Debug.Log("FindWord_text :" + FindWord_text.text + "QuestionWord_text :" + QuestionWord_text.text.Substring(0, Click_num + 1));
                if (QuestionWord_text.text.Equals(FindWord_text.text))
                {
                   BrainGame1_DataManager.instance.Question_Success();
                }
            }
            else
            {
                //Debug.Log("FindWord_text 1 :" + FindWord_text.text + "QuestionWord_text 1:" + QuestionWord_text.text.Substring(0, Click_num + 1));
                BrainGame1_DataManager.instance.Question_Fail();
            }
        }
       
    }
   
}

