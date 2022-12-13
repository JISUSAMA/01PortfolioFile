using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame4_UIManager : MonoBehaviour
{
    public GameObject[] LeftGrup; //도형이 담긴 그룹
    public GameObject[] RigthGrup;//도형이 담긴 그룹
    public List<Image> QuestionLeft_img; // 왼쪽 문제 도형의 이미지 접근
    public List<Image> QuestionRight_img;  // 오른쪽 문제 도형의 이미지 접근
    Color newColor;
    [SerializeField] string newColor_code;
    [Header("DrawLine")]
    public GameObject handle;
    public GameObject linePrefab;

    public Text DebugText_0;
    public Text DebugText_1;
    public Text DebugText_2;

    public List<TouchLocation> touches = new List<TouchLocation>();
    public List<GameObject> touches_ob = new List<GameObject>();
    public GameObject currentLine;

    public Vector2[] delta = new Vector2[5];

    //private void Update()
    //{
    //    DebugText_0.text = "l1 : " + BrainGame4_DataManager.instance.L_checkPoint1
    //        + "l2 : " + BrainGame4_DataManager.instance.L_checkPoint2
    //        + "l3 : " + BrainGame4_DataManager.instance.L_checkPoint3
    //        + "l4 : " + BrainGame4_DataManager.instance.L_checkPoint4
    //        + "l5 : " + BrainGame4_DataManager.instance.L_checkPoint5;
    //    DebugText_1.text = "r1 : " + BrainGame4_DataManager.instance.R_checkPoint1
    //     + "r2 : " + BrainGame4_DataManager.instance.R_checkPoint2
    //     + "r3 : " + BrainGame4_DataManager.instance.R_checkPoint3
    //     + "r4 : " + BrainGame4_DataManager.instance.R_checkPoint4
    //     + "r5 : " + BrainGame4_DataManager.instance.R_checkPoint5;

    //}
    public void DrawLine_Start()
    {
        StopCoroutine("_DrawLine_Start");
        StartCoroutine("_DrawLine_Start");
    }
    IEnumerator _DrawLine_Start()
    {
        Set_QuestionShape();
        BrainGame4_DataManager.instance.DrawLine_GameRun = true;
        while (BrainGame4_DataManager.instance.DrawLine_GameRun)
        {

            int count = Input.touchCount;
            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);
                int id = touch.fingerId;
                //터치좌표
                Vector2 pos = touch.position;
                //begin이라면 무조건 delta에 넣어줌
                if (touch.phase == TouchPhase.Began)
                {
                    GameObject ob = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                    ob.GetComponent<BrainGame4_DrawCtrl>().index = id;
                    delta[id] = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (BrainGame4_DataManager.instance.L_checkPoint1 && BrainGame4_DataManager.instance.L_checkPoint2
                    && BrainGame4_DataManager.instance.L_checkPoint3 && BrainGame4_DataManager.instance.L_checkPoint4
                    && BrainGame4_DataManager.instance.L_checkPoint5 && BrainGame4_DataManager.instance.R_checkPoint1
                    && BrainGame4_DataManager.instance.R_checkPoint2 && BrainGame4_DataManager.instance.R_checkPoint3
                    && BrainGame4_DataManager.instance.R_checkPoint4 && BrainGame4_DataManager.instance.R_checkPoint5)
                    {

                        BrainGame4_DataManager.instance.UIManager.DebugText_2.text = "성공!";
                        SetUIGrup.instance.Question_Success();
                        BrainGame4_DataManager.instance.DrawLine_GameRun = false;
                    }
                    else
                    {
                        //  BrainGame4_DataManager.instance.UIManager.DebugText_2.text = "실패!!";
                        //  SetUIGrup.instance.Question_Fail();
                        //  BrainGame4_DataManager.instance.DrawLine_GameRun = false;

                        SceneSoundCtrl.Instance.GameFailSound();
                        //콜라이더 초기화 
                        BrainGame4_DataManager.instance.L_checkPoint1 = false;
                        BrainGame4_DataManager.instance.L_checkPoint2 = false;
                        BrainGame4_DataManager.instance.L_checkPoint3 = false;
                        BrainGame4_DataManager.instance.L_checkPoint4 = false;
                        BrainGame4_DataManager.instance.L_checkPoint5 = false;
                        BrainGame4_DataManager.instance.R_checkPoint1 = false;
                        BrainGame4_DataManager.instance.R_checkPoint2 = false;
                        BrainGame4_DataManager.instance.R_checkPoint3 = false;
                        BrainGame4_DataManager.instance.R_checkPoint4 = false;
                        BrainGame4_DataManager.instance.R_checkPoint5 = false;
                       
                        newColor_code = "#FFFFF";
                        for (int j =0; j<5; j++)
                        {
                            if (ColorUtility.TryParseHtmlString(newColor_code, out newColor))
                            {
                                QuestionLeft_img[i].color = newColor;
                            }
                        }
                    }
                }
            }


            yield return null;
        }

    }

    public void Set_QuestionShape()
    {
        BrainGame4_DataManager.instance.QuestionLeft_Num = Random.Range(0, LeftGrup.Length);
        BrainGame4_DataManager.instance.QuestionRight_Num = Random.Range(0, RigthGrup.Length);
        LeftGrup[BrainGame4_DataManager.instance.QuestionLeft_Num].SetActive(true);
        RigthGrup[BrainGame4_DataManager.instance.QuestionRight_Num].SetActive(true);

        GameObject leftOb = LeftGrup[BrainGame4_DataManager.instance.QuestionLeft_Num];
        GameObject rightOb = RigthGrup[BrainGame4_DataManager.instance.QuestionRight_Num];
        
        for(int i =1; i<6; i++)
        {
            QuestionLeft_img.Add(leftOb.transform.GetChild(i).gameObject.GetComponent<Image>());
            QuestionRight_img.Add(rightOb.transform.GetChild(i).gameObject.GetComponent<Image>());
        }
        
    }
}

