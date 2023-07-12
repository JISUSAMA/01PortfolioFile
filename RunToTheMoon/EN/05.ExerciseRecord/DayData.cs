using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DayData : MonoBehaviour
{
    public Image moonImg;
    public Transform dataText;
    public Text typeText;

    public float y, posY;
    public string time;
    public float max;
    public bool clickOnState;   //클릭 여부(텍스트 활성화여부)
    float size_Graph;
    float size_Dot;

    public GameObject ParentName;

    void Start()
    {
        Invoke("LengthExtend", 0.01f);  //그래프에 데이터가 들어가기 위한 시간을 벌어준다.
                                        // max = 950 / 3;
    }

    //그래프 길이 함수
    //y값 만큼 이미지의 사이즈를 늘여준다
    public void LengthExtend()
    {
        //걸은수
        if (ParentName.gameObject.name.Equals("RunStep_Scroll View"))
        {
            //
            if (ExricaiseRecord_UIManager.instance.month_bool.Equals(true))
            {
                RunStepGraphDisplay.instance._month_day_text.text = "MONTH";
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
            else
            {
                RunStepGraphDisplay.instance._month_day_text.text = "DAY";
                RectTransform rectTran = moonImg.GetComponent<RectTransform>();
                size_Graph = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, RunStepGraphDisplay.instance.top_heightSize, y));
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_Graph + 55);
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);

            }

        }
        //주행거리 
        else if (ParentName.gameObject.name.Equals("RunDistance_Scroll View"))
        {

            if (ExricaiseRecord_UIManager.instance.month_bool.Equals(true))
            {
                RunDistanceGraphDisplay.instance._month_day_text.text = "MONTH";
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
            else
            {
                RunDistanceGraphDisplay.instance._month_day_text.text = "DAY";
                RectTransform rectTran = moonImg.GetComponent<RectTransform>();
                size_Graph = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, RunDistanceGraphDisplay.instance.top_heightSize, y));
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_Graph + 55);
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }

        }

        //소비 칼로리
        else if (ParentName.gameObject.name.Equals("RunKcal_Scroll View"))
        {

            if (ExricaiseRecord_UIManager.instance.month_bool.Equals(true))
            {
                RunKcalGraphDisplay.instance._month_day_text.text = "MONTH";
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
            else
            {
                RunKcalGraphDisplay.instance._month_day_text.text = "DAY";
                RectTransform rectTran = moonImg.GetComponent<RectTransform>();
                size_Graph = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, RunKcalGraphDisplay.instance.top_heightSize, y));
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_Graph + 55);
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
        }
        //걸은 시간
        else if (ParentName.gameObject.name.Equals("RunTime_Scroll View"))
        {

            if (ExricaiseRecord_UIManager.instance.month_bool.Equals(true))
            {
                RunTimeGraphDisplay.instance._month_day_text.text = "MONTH";
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
            else
            {
                RunTimeGraphDisplay.instance._month_day_text.text = "DAY";
                RectTransform rectTran = moonImg.GetComponent<RectTransform>();
                size_Graph = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, RunTimeGraphDisplay.instance.top_heightSize, y));
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_Graph + 55);
                moonImg.transform.localPosition = new Vector3(0, posY + 55, 0);
            }
        }
    }
    //월별 그래프 이미지
    public void DotImg_Change()
    {
        moonImg.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("05.ExerciseRecord/12 nonSelect_stardot");
        RectTransform rectTran = moonImg.GetComponent<RectTransform>();
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 66);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 66);
    }
    //일별 그래프 이미지
    public void GraphImg_Change()
    {
        moonImg.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("05.ExerciseRecord/14 nonselect_graph_bar");

    }
    //데이터 정보를 보기 위해 그래프 클릭 이벤트
    public void DataButtonOn()
    {
        dataText.gameObject.SetActive(true);    //데이터 정보가 잇는 오브젝트 활성화

        if (typeText.text == "Walking Time")
        {
            dataText.transform.GetChild(0).gameObject.GetComponent<Text>().text = time; //해당 값
        }
        else if (typeText.text == "Calories Burned")
        {
            dataText.transform.GetChild(0).gameObject.GetComponent<Text>().text = y.ToString() + " Kcal"; //해당 값
        }
        else if (typeText.text == "Step Count")
        {
            dataText.transform.GetChild(0).gameObject.GetComponent<Text>().text = y.ToString() + " Step"; //해당 값
        }
        else if (typeText.text == "Distance Walked")
        {
            dataText.transform.GetChild(0).gameObject.GetComponent<Text>().text = y.ToString() + " Km"; //해당 값
        }

        dataText.localPosition = new Vector3(10, size_Graph + 55f, 0);
        clickOnState = true;    //클릭했다는 상태 변경

        string strTemp = Regex.Replace(this.gameObject.name, @"\d", "");    //문자추출
        string strInt = Regex.Replace(this.gameObject.name, @"\D", ""); //숫자 추출 

        //현재 클릭한 정보를 넘겨준다.
        if (typeText.text == "Walking Time")
            RunTimeGraphDisplay.instance.OneTextShowData(strTemp, strInt);
        else if (typeText.text == "Calories Burned")
            RunKcalGraphDisplay.instance.OneTextShowData(strTemp, strInt);
        else if (typeText.text == "Step Count")
            RunStepGraphDisplay.instance.OneTextShowData(strTemp, strInt);
        else if (typeText.text == "Distance Walked")
            RunDistanceGraphDisplay.instance.OneTextShowData(strTemp, strInt);
    }
}