using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LobbyDayData : MonoBehaviour
{
    public Button nullBtn;
    public Image moonImg;
    public Transform dataText;

    public float y, posY;
    public bool clickOnState;   //클릭 여부(텍스트 활성화 여부)
    float size_Graph;

    void Start()
    {
        Invoke("LengthExtend", 0.01f);  //그래프에 데이터가 들어가기 위한 시간을 벌어준다.
    }

    //그래프 길이 함수
    public void LengthExtend()
    {
        RectTransform btnRect = nullBtn.GetComponent<RectTransform>();
        size_Graph = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, LobbyStepGraphDisplay.instance.top_heightSize, posY));
        btnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_Graph);

        RectTransform rectTran = moonImg.GetComponent<RectTransform>();
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);

        moonImg.transform.localPosition = new Vector3(0, 10, 0);
    }

    //데이터 정보를 보기 위해 그래프 클릭 이벤트
    public void DataButtonOn()
    {
        dataText.gameObject.SetActive(true);    //데이터 정보가 잇는 오브젝트 활성화
        dataText.transform.GetChild(0).gameObject.GetComponent<Text>().text = posY.ToString(); //해당 값
        dataText.localPosition = new Vector3(0, y + 35, 0);
        clickOnState = true;    //클릭했다는 상태 변경

        string strTemp = Regex.Replace(this.gameObject.name, @"\d", "");    //문자추출
        string strInt = Regex.Replace(this.gameObject.name, @"\D", ""); //숫자 추출 

        LobbyStepGraphDisplay.instance.OneTextShowData(strTemp, strInt);
    }
}