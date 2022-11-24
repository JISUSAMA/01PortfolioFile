using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DementiaGame1_UIManager : MonoBehaviour
{
    [Header("질문")]
    public Text[] Word_Text; 
    public GameObject[] Word_Blank;

    [Header("보기 질문")]
    public Text SubContent_text;
    public Text[] CheckAnswer_text;
    public Button[] AnswerBtn;
    public GameObject[] AnswerCheckMark;
    int AnswerNum_i; 
    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    public void SubmitAnswer()
    {
        StopCoroutine("_SubmitAnswer");
        StartCoroutine("_SubmitAnswer");
    }
    IEnumerator _SubmitAnswer()
    {
            for (int i = 0; i <Word_Blank.Length; i++)
            {
               Word_Blank[i].SetActive(false);
            }
            yield return new WaitForSeconds(1.5f);
     
        yield return null;
    }

    public void AnswerToggle_Btn(GameObject ob)
    {
        if (ob.name.Equals("Number 1")) { AnswerBtn_Action(1,true, false, false, false); }
        else if (ob.name.Equals("Number 2")) { AnswerBtn_Action(2,false, true, false, false); }
        else if (ob.name.Equals("Number 3")) { AnswerBtn_Action(3,false, false, true, false); }
        else if (ob.name.Equals("Number 4")) { AnswerBtn_Action(4,false, false, false, true); }
        AnswerQusetion();

    }
    public void AnswerBtn_Action(int num, bool num1, bool num2, bool num3, bool num4)
    {
        AnswerCheckMark[0].SetActive(num1);
        AnswerCheckMark[1].SetActive(num2);
        AnswerCheckMark[2].SetActive(num3);
        AnswerCheckMark[3].SetActive(num4);
        AnswerNum_i = num;
    }
  
    public void AnswerQusetion()
    {
        AnswerTimer_text.text = " ";
        //맞춤
        if (AnswerNum_i.Equals(DementiaGame1_DataManager.instance.SubmitAnswerNum_i))
        {
            DementiaGame1_DataManager.instance.Question_Success();
        }
        //틀림
        else
        {
            DementiaGame1_DataManager.instance.Question_Fail();
        }
      
    }
}
