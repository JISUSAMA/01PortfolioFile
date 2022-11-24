﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Suro : MonoBehaviour
{
    [Header("Suro")]
    public GameObject SuroOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

    public Button fail_btn;
    public Button success_btn;

    public Animator Suro_Anl;

    // Start is called before the first frame update
    void Start()
    {
        Suro_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Suro_SetDialog(); //처음 대사 저장
            StartCoroutine(_Suro_Event_Start());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            SuroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Suro_CompleteDialog());
        }

    }
    //처음 시작 이벤트 
   IEnumerator _Suro_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Suro_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("수로 1",2.5f);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        SuroOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
        SoundManager.Instance.PlayCharacterDialog("수로 1-1",6.5f);
        ////////////// 찾아줘! //////////////////////////
        Suro_body(false, true, false, false);
        Suro_Gesture(true, false, false);
        Suro_Anl.SetTrigger("Find");
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        //   SuroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        Suro_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!
        yield return null;
    }
    //미션 성공 "FoundScene" 에서 사용됨
    public void Suro_CompleteDialog()
    {
        StartCoroutine(_Suro_CompleteDialog());
    }
    IEnumerator _Suro_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Suro_Clear", "true");
        GameManager.instance.Change_ClearState();
  
        DialogManager.instance.DialogStr("오오~ 네 덕분에 나라를 다스릴 기회를 얻었어.\n나중에 가야로 꼭 초대할게!");
      
        ////////////// 맞았어!!! //////////////////////////
        Suro_body(false,false, false, true);
        Suro_Gesture(false, false, true);
        StartCoroutine(_Suro_Complete_FaceChange());
        Suro_Anl.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("수로 2",6.4f);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        if (GameManager.instance.Mission_Complete)
        {
            SceneManager.LoadScene("06Ending");
        }
        else
        {
            SceneManager.LoadScene("03ChooseFriends");
        }
        yield return null;
    }
    public void Suro_FailDialog()
    {
        StartCoroutine(_Suro_FailDialog());
    }
    //미션 실패 //Mission씬에서 사용됨
    IEnumerator _Suro_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        SuroOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Suro_body(false, false, true,false); //x
        Suro_Gesture(false, true, false);
        StartCoroutine(_Suro_Fail_FaceChange());
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("칼 끝부분은 고리 모양이야!\n조금만 더 힘내봐!");
 
        Suro_Anl.SetTrigger("X");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("수로 3",3.8f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        SuroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Suro_Default_motion();
        GameManager.instance.Ongoing = false;
        Mission_UIManager.instance.MissionCheck_sc.noRelicOneShow = false;
        yield return new WaitForSeconds(1f); //4초 후에 활성화 시키기
        Mission_UIManager.instance.TargetGrup.SetActive(true);
    }
    public void Suro_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "안녕, 나는 수로라고 해.";
        DialogManager.instance.Dialog_str[1] = "나라를 세우기 위해선 고리자루 큰 칼이 꼭 필요한데,\n멋진 칼을 함께 찾아주겠어?";
    }
    IEnumerator _Suro_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Debug.Log("--------------------------------------------facechange2");
            if (DialogManager.instance.Dialog_count.Equals(1))
            {
                Panic_FaceChange();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Defalt_FaceChange();
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        Suro_Face(true, false, false, false, false);
        yield return null;
       
    }
    IEnumerator _Suro_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Suro_Default_motion();
        yield return null;
    }
    IEnumerator _Suro_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Suro_Default_motion();
        yield return null;
    }
    public void Suro_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Suro_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Suro_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Suro_Default_motion()
    {
        Suro_Face(true, false, false, false, false);
        Suro_Gesture(false, false, false);
        Suro_body(true, false, false,false);
    }
    int Panic_CurrentRange = -1;
    int Defalt_CurrentRange = -1;
    int Happy_CurrentRange = -1;
    void Panic_FaceChange()
    {
        int Range = Random.Range(0, 2);
        //Debug.Log(Range);
        if (Panic_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Suro_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Suro_Face(false, false, false, false, true);
            Panic_CurrentRange = 1;
        }
    }
    void Defalt_FaceChange()
    {
        int Range = Random.Range(0, 2);
        Debug.Log(Range);
        if (Defalt_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Suro_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Suro_Face(false, true, false, false, false);
            Defalt_CurrentRange = 1;
        }
    }
    void Happy_FaceChange()
    {
        int Range = Random.Range(0, 2);
        Debug.Log(Range);
        if (Happy_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Suro_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Suro_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
}
