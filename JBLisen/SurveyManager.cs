using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SurveyManager : MonoBehaviour
{
    public GameObject SurveyBG;
    public GameObject[] SurveyList;
    public GameObject[] Survey_1;
    
    public Button[] SurveyBTN;
    public Sprite[] SurveyBTN_sc;

    public int SurveyNum;
    string thisSurveyName;

    
    public void OnClick_StartSurvey(string name)
    {
        SurveyNum =-1;
        SurveyBG.SetActive(true);
        thisSurveyName = name;
        switch (thisSurveyName)
        {
            case "Survey_1":
                for (int i = 0; i < Survey_1.Length; i++)
                {
                    Survey_1[i].SetActive(false);
                    for (int j = 0; j < SurveyBTN.Length - 1; j++)
                    {
                        SurveyBTN[j].GetComponent<Image>().sprite = SurveyBTN_sc[0];
                    }
                }
                break;
        }
    }

    public void SurveyBtn_Change_Color(Button btn)
    {
        StopCoroutine(_SurveyBtn_Change_Color(btn));
        StartCoroutine(_SurveyBtn_Change_Color(btn));
        Debug.Log("BTN Name :" + btn.gameObject.GetComponent<Image>().sprite.name);
    }
    IEnumerator _SurveyBtn_Change_Color(Button btn)
    {
         if (btn.gameObject.name.Equals("1_SurveyBtn")){
            btn.gameObject.GetComponent<Image>().sprite = SurveyBTN_sc[1];
        }
        yield return null; 
    }
    //닫기 버튼 눌렀을 때 레이어 초기화
    public void OnClick_CloseSurvey()
    {
        StopCoroutine(_Close_ResetLayer());
        StartCoroutine(_Close_ResetLayer());
    }

    IEnumerator _Close_ResetLayer()
    {
        SurveyBG.SetActive(false);
        switch (thisSurveyName)
        {
            case "Survey_1":
                for (int i = 0; i < Survey_1.Length; i++)
                {
                    Survey_1[SurveyNum].SetActive(false);
                }
                break;
        }
        thisSurveyName = "";
        yield return null;
    }
    //설문지 선택후, 설문조사 시작하기 버튼
    public void OnClick_SurveyNext()
    {
        StopCoroutine(_OnClick_SurveyNext());
        StartCoroutine(_OnClick_SurveyNext());
    }
    IEnumerator _OnClick_SurveyNext()
    {
        switch (thisSurveyName)
        {
            case "Survey_1":

                if (!SurveyNum.Equals(-1))
                {
                    Button thisButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                    SurveyBtn_Change_Color(thisButton);
                    yield return new WaitForSeconds(0.2f);
                    if (SurveyNum < Survey_1.Length - 1)
                    {
                        Debug.Log("surveynum : " + SurveyNum + " Survey_1Len : " + Survey_1.Length);
                        SurveyNum += 1;

                        if (!SurveyNum.Equals(0))
                            Survey_1[SurveyNum - 1].SetActive(false);

                        Survey_1[SurveyNum].SetActive(true);
                    }
                    else
                    {
                        Debug.Log("ddddddddddd+:surveynum : " + SurveyNum + " Survey_1Len : " + Survey_1.Length);
                        Finish_Survey();
                    }
                }
                else
                {
                    SurveyNum += 1;
                    if (!SurveyNum.Equals(0))
                        Survey_1[SurveyNum - 1].SetActive(false);

                    Survey_1[SurveyNum].SetActive(true);
                }
              
                break;
        }
        yield return null;
    }
    public void Finish_Survey()
    {
        StopCoroutine(_Finish_Survey());
        StartCoroutine(_Finish_Survey());
    }
    IEnumerator _Finish_Survey()
    {       
        //데이터 저장
        JBApp_UIManager.Instance.Point_Save("지역 배달 APP(전주 맛배달)관련 의견 수렴", "+500");
        OnClick_CloseSurvey();
        AppDataManager.Instance.User_Point += 500;
        JBApp_UIManager.Instance.HaveMoney_Initialization();
        SurveyBG.SetActive(false);

        yield return null;
    }
}
