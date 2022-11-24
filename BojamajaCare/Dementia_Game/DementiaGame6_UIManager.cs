using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame6_UIManager : MonoBehaviour
{
    public GameObject[] PaintGrup;
    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;


    public int PainGrupNUM;
    int FindObjectNum = 0;
    public void SetCurrentPainFindBtn()
    {
        PainGrupNUM = Random.Range(0, PaintGrup.Length);
        PaintGrup[PainGrupNUM].gameObject.SetActive(true);
        //부모 저장(오른쪽 왼쪽 2개)
        for (int i = 0; i < 2; i++)
        {
            DementiaGame6_DataManager.instance.CurrnetPaint_Parent.Add(PaintGrup[PainGrupNUM].transform.GetChild(i).gameObject);
        }
        //왼쪽 부모 자식 저장
        for (int i = 0; i < DementiaGame6_DataManager.instance.CurrnetPaint_Parent[0].transform.childCount; i++)
        {
            DementiaGame6_DataManager.instance.CurrnetPaint_Child_left.Add(DementiaGame6_DataManager.instance.CurrnetPaint_Parent[0].transform.GetChild(i).gameObject);
        }
        //오른쪽 부모 자식 저장
        for (int i = 0; i < DementiaGame6_DataManager.instance.CurrnetPaint_Parent[1].transform.childCount; i++)
        {
            DementiaGame6_DataManager.instance.CurrnetPaint_Child_right.Add(DementiaGame6_DataManager.instance.CurrnetPaint_Parent[1].transform.GetChild(i).gameObject);
        }
    }

    public void OnClick_FindBtn(GameObject btnOb)
    {
        int number = 0;
        if (btnOb.name.Equals("Find1")) number = 0;
        else if (btnOb.name.Equals("Find2")) number = 1;
        else if (btnOb.name.Equals("Find3")) number = 2;
        else if (btnOb.name.Equals("Find4")) number = 3;
        else if (btnOb.name.Equals("Find5")) number = 4;

        Debug.Log(btnOb.name);
        Debug.Log(number);

        DementiaGame6_DataManager.instance.CurrnetPaint_Child_left[number].transform.GetChild(0).gameObject.SetActive(true);
        DementiaGame6_DataManager.instance.CurrnetPaint_Child_left[number].GetComponent<Button>().interactable = false;
        DementiaGame6_DataManager.instance.CurrnetPaint_Child_right[number].transform.GetChild(0).gameObject.SetActive(true);
        DementiaGame6_DataManager.instance.CurrnetPaint_Child_right[number].GetComponent<Button>().interactable = false;

        if (FindObjectNum != 4) FindObjectNum += 1;
        else
        {
            //게임 끝
            DementiaGame6_DataManager.instance.playBool = false;
            DementiaGame6_DataManager.instance.Question_Success();
            Touch_GameManager.instance.Game_LoadScene();
        }

    }
}
