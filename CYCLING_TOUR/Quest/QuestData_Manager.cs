using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class QuestData_Manager : MonoBehaviour
{
    public static QuestData_Manager instance { get; private set; }

    public GameObject gameEndPopup; //게임종료팝업
    public string[] toDayArr;   //오늘 날짜 배열

    string toDay;

    public int connectCount; //접속하기 미션 보상받기 카운트
    int visitNum;   //방문하기 미션 번호
    int useNum; //게임사용 미션 번호
    int kcalNum;    //오늘의 칼로리소모 번호
    int maxSpeedNum;    //오늘의 최고속도 번호

    GameObject connetTime;

    //---Map 퀘스트 ------------------------
    //아시아맵 완주 변수
    int asiaMap_Count, asiaNormal2_Count;
    //제한시간 맥스값
    int maxTime = 300;
    //아시아맵 시간제한 완주변수
    int asiaTimeLimit_Count;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("GoldUse"));
        GetTodayInitialization();
        //Debug.Log("2---GoldUse " + PlayerPrefs.GetString("GoldUse"));
        //Debug.Log("----------- " + PlayerPrefs.GetString("CustomChange"));

        connetTime = GameObject.Find("ConnetTimeManager");

        //Debug.Log("--- "+CourseOpenTimeState(PlayerPrefs.GetString("Asia Hard 1Course")));

    }


    void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameEndPopup.SetActive(true);
            }
        }
    }

    //오늘 날짜 초기화
    void GetTodayInitialization()
    {
        toDay = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        toDayArr = new string[4];
        toDayArr[0] = DateTime.Now.ToString("yyyy");
        toDayArr[1] = DateTime.Now.ToString("MM");
        toDayArr[2] = DateTime.Now.ToString("dd");
        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");

        //strSub = toDayArr[2].Substring(0, 1);

        ////1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
        //if (strSub == "0")
        //    toDayArr[2] = toDayArr[2].Substring(1, 1);
    }

    

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    //코인 보상 받기
    public void GetCoinReward(Text _totalText, Text _priceText)
    {
        //Debug.Log("코인 : " + _totalText.text + "  " + _priceText.text);
        int totalCoin = int.Parse(_totalText.text.Replace(",", ""));    //총 코인 int화
        int priceCoin = int.Parse(_priceText.text.Replace(",", ""));    //보상 가격 int화
        //Debug.Log("코인 : " + totalCoin + "  " + priceCoin);

        totalCoin += priceCoin;
        //Debug.Log("코인 : -- " + totalCoin);

        PlayerPrefs.SetInt("AT_Player_Gold", totalCoin);

        ServerManager.Instance.Update_Gold_Data(totalCoin);

        _totalText.text = CommaText(totalCoin);
    }


    //// 일일퀘스트 -------------------------------------------------------------
    //접속하기 미션 함수
    public void Quest_ConnectMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
    {
        //currTime = PlayerPrefs.GetFloat("ConnectTime"); //현재 접속한 시간

        connectCount = PlayerPrefs.GetInt("AT_TodayQuest1");
        //Debug.Log("connectCount " + connectCount);

        //접속하기 미션 
        if (connectCount == 0)
        {
            _title.text = "접속하기";
            _slider.value = 1;
            _content.text = "1/1";
            _coin.text = CommaText(1000);
            _completeImg.SetActive(true);   //미션완료 도장 활성화
            _takeBtn.interactable = true;   //버튼 활성화
        }
        //10분간 접속 유지 - (접속하기 미션 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(1))
        else if(connectCount == 1)
        {
            _title.text = "10분간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 10분이 넘었으면
            if (ConnetTime.instance.currTime >= 600f)
            {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            }
            else
            {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
            } 
        }
        //30분간 접속 유지 - (10분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(2))
        else if(connectCount == 2)
        {
            _title.text = "30분간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 30분이 넘었으면
            if (ConnetTime.instance.currTime >= 1800f)
            {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            }
            else
            {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
            }
        }
        //1시간 접속 유지 - (20분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(3))
        else if(connectCount == 3)
        {
            _title.text = "1시간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 60분이 넘었으면
            if (ConnetTime.instance.currTime >= 3600f)
            {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            }
            else
            {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(false);   //받기완료 비활성화
                //_takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        }
        else if(connectCount > 3)
        {
            //Debug.Log("여기 들어오지 ?");
            _title.text = "1시간 접속 유지하기";
            _completeImg.SetActive(true);   //미션완료 도장 활성화
            _slider.value = 1;
            _content.text = "1/1";
            _takeBtn.interactable = false;   //버튼 비활성화
            _takeImg.SetActive(true);   //받기완료 활성화
            _takeBtn.GetComponent<Image>().sprite = _takeSprite;
        }
    }

    //퀘스트2 오늘 처음 들어왔을 때 미션 랜덤으로 세팅
    public int VisitRandom()
    {
        //0 : 스토어, 1: 내정보, 2: 가방, 3: 랭킹존
        int rand = UnityEngine.Random.Range(0, 4);
        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "No", rand);
        ServerManager.Instance.myTodayQuest[1].quest_Idx = rand;
        return rand;
    }

    //퀘스트3번 처음 들어왔을 때 미션 랜덤으로 세팅
    public int GameUseRandom()
    {
        //0: 프로필 변경, 1: 게임 한번 하기, 2: 현재 순위 올리기, 3: 커스텀 변경하기, 4:골드 사용하기, 5: 아이템 사용하기, 6: 아이템 구매하기
        int rand = UnityEngine.Random.Range(0, 7);
        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "No", rand);
        ServerManager.Instance.myTodayQuest[2].quest_Idx = rand;
        return rand;
    }
    //퀘스트4번 - 처음 들어왔을 때 미션 랜덤세팅 - 칼로리 소모
    public int BurnUp_CalroieToday()
    {
        //0: 300, 1:400, 2:500, 3:600
        int rand = UnityEngine.Random.Range(0, 4);
        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "No", rand);
        ServerManager.Instance.myTodayQuest[3].quest_Idx = rand;
        return rand;
    }
    //퀘스트5번 - 처음 들어왔을 때 미션 랜덤세팅 - 오늘의 최고속도
    public int MaxSpeedToday()
    {
        //0:25, 1: 30, 2:35, 3:40, 4:45, 5:50 6: 55
        int rand = UnityEngine.Random.Range(0, 7);
        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "No", rand);
        ServerManager.Instance.myTodayQuest[4].quest_Idx = rand;
        return rand;
    }


    //방문하기 미션 함수
    public void Quest_VisitMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
    {
        visitNum = PlayerPrefs.GetInt("AT_TodayQuest2");

        //스토어 방문
        if (visitNum == 0)
        {
            //Debug.Log("스토어 " + PlayerPrefs.GetString("AT_StoreVisit"));
            if (PlayerPrefs.GetString("AT_StoreVisit") == "Yes")
            {
                _title.text = "스토어 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_StoreVisit") == "No")
            {
                _title.text = "스토어 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            }
            else if (PlayerPrefs.GetString("AT_StoreVisit") == "MissionOk")
            {
                _title.text = "스토어 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        }
        //내정보 방문
        else if (visitNum == 1)
        {
            if (PlayerPrefs.GetString("AT_MyInfoVisit") == "Yes")
            {
                _title.text = "내 정보 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_MyInfoVisit") == "No")
            {
                _title.text = "내 정보 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            }
            else if (PlayerPrefs.GetString("AT_MyInfoVisit") == "MissionOk")
            {
                _title.text = "내 정보 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        }
        //가방 방문
        else if(visitNum == 2)
        {
            if (PlayerPrefs.GetString("AT_InventoryVisit") == "Yes")
            {
                _title.text = "내 배낭 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_InventoryVisit") == "No")
            {
                _title.text = "내 배낭 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            }
            else if(PlayerPrefs.GetString("AT_InventoryVisit") == "MissionOk")
            {
                //Debug.Log("??????????????????");
                _title.text = "내 배낭 방문하기 ";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        }
        //랭킹존 방문
        else if (visitNum == 3)
        {
            if (PlayerPrefs.GetString("AT_RankJonVisit") == "Yes")
            {
                _title.text = "랭킹존 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_RankJonVisit") == "No")
            {
                _title.text = "랭킹존 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            }
            else if (PlayerPrefs.GetString("AT_RankJonVisit") == "MissionOk")
            {
                _title.text = "랭킹존 방문하기 ";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        }
    }

    //게임미션 함수
    public void Quest_GameUseMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
    {
        useNum = PlayerPrefs.GetInt("AT_TodayQuest3");
        //Debug.Log("useNum " + useNum);

        //프로필 변경
        if (useNum == 0)
        {
            if(PlayerPrefs.GetString("AT_ProfileChange") == "No")
            {
                _title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_ProfileChange") == "Yes")
            {
                _title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_ProfileChange") == "MissionOk")
            {
                _title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //게임 한번 하기
        else if (useNum == 1)
        {
            if (PlayerPrefs.GetString("AT_GameOnePlay") == "No")
            {
                _title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_GameOnePlay") == "Yes")
            {
                _title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_GameOnePlay") == "MissionOk")
            {
                _title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //현재 순위 올리기
        else if (useNum == 2)
        {
            if (PlayerPrefs.GetString("AT_RankUp") == "No")
            {
                _title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_RankUp") == "Yes")
            {
                _title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_RankUp") == "MissionOk")
            {
                _title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //커스텀 변경하기
        else if (useNum == 3)
        {
            if (PlayerPrefs.GetString("AT_CustomChange") == "No")
            {
                _title.text = "캐릭터 커스텀 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_CustomChange") == "Yes")
            {
                _title.text = "캐릭터 커스텀 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_CustomChange") == "MissionOk")
            {
                _title.text = "캐릭터 커스텀 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //골드 사용하기
        else if (useNum == 4)
        {
            if (PlayerPrefs.GetString("AT_GoldUse") == "No")
            {
                _title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_GoldUse") == "Yes")
            {
                _title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_GoldUse") == "MissionOk")
            {
                _title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //아이템 사용하기
        else if (useNum == 5)
        {
            //Debug.Log("ItemUse " + PlayerPrefs.GetString("AT_ItemUse"));
            if (PlayerPrefs.GetString("AT_ItemUse") == "No")
            {
                _title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_ItemUse") == "Yes")
            {
                _title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_ItemUse") == "MissionOk")
            {
                _title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
        //아이템 구매하기
        else if (useNum == 6)
        {
            if (PlayerPrefs.GetString("AT_ItemPurchase") == "No")
            {
                _title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            }
            else if (PlayerPrefs.GetString("AT_ItemPurchase") == "Yes")
            {
                _title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            }
            else if (PlayerPrefs.GetString("AT_ItemPurchase") == "MissionOk")
            {
                _title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화
                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
            }
        }
    }

    //오늘의 칼로리 소모
    public void Quest_BurnUpCalroieTodayMission(GameObject _obj, Text _titleText, Slider _slider, Text _sliderText, Sprite _sprite)
    {
        kcalNum = PlayerPrefs.GetInt("AT_TodayQuest4");    //칼로리 소모

        if(kcalNum == 0)
        {
            if(PlayerPrefs.GetFloat("AT_Record_TodayKcal") < 300)
            {
                _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else if(PlayerPrefs.GetFloat("AT_Record_TodayKcal") >= 300)
            {
                if(PlayerPrefs.GetString("AT_KcalBurnUp") == "MissionOk")
                {
                    _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                }
                else
                {
                    _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if(kcalNum == 1)
        {
            if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") < 400)
            {
                _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") >= 400)
            {
                if (PlayerPrefs.GetString("AT_KcalBurnUp") == "MissionOk")
                {
                    _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                }
                else
                {
                    _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if(kcalNum == 2)
        {
            if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") < 500)
            {
                _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") >= 500)
            {
                if (PlayerPrefs.GetString("AT_KcalBurnUp") == "MissionOk")
                {
                    _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                }
                else
                {
                    _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if(kcalNum == 3)
        {
            if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") < 600)
            {
                _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else if (PlayerPrefs.GetFloat("AT_Record_TodayKcal") >= 600)
            {
                if (PlayerPrefs.GetString("AT_KcalBurnUp") == "MissionOk")
                {
                    _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                }
                else
                {
                    _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    //오늘의 최고 속도
    public void Quest_MaxSpeedTodayMission(GameObject _obj, Text _title, Slider _slider, Text _sliderText, Sprite _sprite)
    {
        maxSpeedNum = PlayerPrefs.GetInt("AT_TodayQuest5");    //최고속도

        if(maxSpeedNum == 0)
        {
            //TodayMaxSpeed 프리팹 - 로비에서 초기화. 아시아맵데이터에서 갱신
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 25)
            {
                _title.text = "오늘의 최대 속도 : 25 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if(PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 25)
            {
                if(PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 25 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 25 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if(maxSpeedNum == 1)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 30)
            {
                _title.text = "오늘의 최대 속도 : 30 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 30)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 30 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 30 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (maxSpeedNum == 2)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 35)
            {
                _title.text = "오늘의 최대 속도 : 35 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 35)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 35 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 35 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (maxSpeedNum == 3)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 40)
            {
                _title.text = "오늘의 최대 속도 : 40 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 40)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 40 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 40 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (maxSpeedNum == 4)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 45)
            {
                _title.text = "오늘의 최대 속도 : 45 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 45)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 45 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 45 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (maxSpeedNum == 5)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 50)
            {
                _title.text = "오늘의 최대 속도 : 50 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 50)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 50 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 50 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (maxSpeedNum == 6)
        {
            if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") < 55)
            {
                _title.text = "오늘의 최대 속도 : 55 km/s 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            }
            else if (PlayerPrefs.GetFloat("AT_TodayMaxSpeed") >= 55)
            {
                if (PlayerPrefs.GetString("AT_MaxSpeedToday") == "MissionOk")
                {
                    _title.text = "오늘의 최대 속도 : 55 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                }
                else
                {
                    _title.text = "오늘의 최대 속도 : 55 km/s 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    //// 맵퀘스트 -----------------------------------------------------------------
    //코스별 시간 기록 가져오는 함수
    float CourseOpenTimeState(string _data)
    {
        //Debug.Log("------------ " + _data);
        float courseTime = 0;
        if (_data != "")
        {
            string[] openTime;
            string[] _openTime;

            char sp = '/';
            openTime = _data.Split(sp);   //2021,12,10:00 따로 저장
            //Debug.Log("------------========= " + openTime[1]);

            char bar = ':';
            _openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

            courseTime = (int.Parse(_openTime[0]) * 60 * 60) + int.Parse(_openTime[1]) * 60 + float.Parse(_openTime[2]);
            //Debug.Log("------------========= " + courseTime);
        }
        return courseTime;
    }
    //코스맵 이름
    string CourseOpenNameState(string _data)
    {
        string courseTime = "";
        if (_data != "")
        {
            string[] openTime;
            string[] _openTime;

            char sp = '/';
            openTime = _data.Split(sp);   //2021,12,10:00 따로 저장

            //char bar = ':';
            //_openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

            courseTime = openTime[0];
        }
        return courseTime;
    }

    //아시아 맵 완주 - 노멀 하드
    public void AsiaMapCourse_Finish(int _finishCount, int _finishAmount, GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn)
    {
        //Debug.Log("___ 제한속도 " + PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount"));
        //Debug.Log("갯수 :: " + PlayerPrefs.GetInt("AsiaNormal1Finish"));
        asiaMap_Count = _finishCount;// PlayerPrefs.GetInt("AsiaNormal1Finish");
        //Debug.Log("_mapCourse " + _mapCourse + " asiaNormal1_Count " + asiaMap_Count + " ::: " + CourseOpenTimeState(_mapCourse));

        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
        if (CourseOpenTimeState(_mapCourse) == 0 || CourseOpenTimeState(_mapCourse) == 362439)
        {
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _acceptBtn.interactable = false;
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
        }
        //코스 1에 기록이 잇을 경우  - 완주를 했다.
        else if(CourseOpenTimeState(_mapCourse) > 0 || CourseOpenTimeState(_mapCourse) != 362439)
        {
            if(asiaMap_Count == 0)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _acceptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if(asiaMap_Count == 1)
            {
                //완주 5회 미만-  미션 미완료
                if(_finishAmount < 5)
                {
                    _slider.value = (float)_finishAmount / 5;
                    _sliderText.text = _finishAmount + "/5";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if(_finishAmount >= 5)
                {
                    _slider.value = (float)_finishAmount / 5;
                    _sliderText.text = _finishAmount + "/5";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if(asiaMap_Count == 2)
            {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 10)
                {
                    _slider.value = (float)_finishAmount / 10;
                    _sliderText.text = _finishAmount + "/10";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 10)
                {
                    _slider.value = (float)_finishAmount / 10;
                    _sliderText.text = _finishAmount + "/10";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (asiaMap_Count == 3)
            {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 15)
                {
                    _slider.value = (float)_finishAmount / 15;
                    _sliderText.text = _finishAmount + "/15";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 15)
                {
                    _slider.value = (float)_finishAmount / 15;
                    _sliderText.text = _finishAmount + "/15";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (asiaMap_Count == 4)
            {
                //Debug.Log("여긴데 ==== " + _finishAmount);
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 20)
                {
                    _slider.value = (float)_finishAmount / 20;
                    _sliderText.text = _finishAmount + "/20";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 20)
                {
                    _slider.value = (float)_finishAmount / 20;
                    _sliderText.text = _finishAmount + "/20";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if(asiaMap_Count >= 5)
            {
                _slider.value = 1;
                _sliderText.text = "20/20";
                _acceptBtn.interactable = false;
                _mapObj.transform.GetChild(1).gameObject.SetActive(true);    //보상완료이미지 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    ////아시아 맵 완주 초기버전 - 사용하지 않음(참고용)
    public void AsiaNormal2Course_Finish(GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn)
    {
        asiaNormal2_Count = PlayerPrefs.GetInt("AT_AsiaNormal2Finish");

        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
        if (CourseOpenTimeState(_mapCourse) == 0)
        {
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _acceptBtn.interactable = false;
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
        }
        //코스 1에 기록이 잇을 경우  - 완주를 했다.
        else if (CourseOpenTimeState(_mapCourse) > 0)
        {
            if (asiaNormal2_Count == 0)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _acceptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaNormal2_Count == 1)
            {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 5)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 5;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/5";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") == 5)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 5;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/5";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (asiaNormal2_Count == 2)
            {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 10)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 10;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/10";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") == 10)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 10;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/10";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (asiaNormal2_Count == 3)
            {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 15)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 15;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/15";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") == 15)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 15;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/15";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (asiaNormal2_Count == 4)
            {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 20)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 20;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/20";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") >= 20)
                {
                    _slider.value = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") / 20;
                    _sliderText.text = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + "/20";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
        }
    }



    //아시아 맵 시간제한 완주 - 노멀 하드
    public void AsiaCourse_TimeLimitFinish(int _limitCount, string _finishTime, Text _titleText, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj, Sprite _takeSprite)
    {
        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);
        //Debug.Log("+++시간제한 " + asiaTimeLimit_Count + " PlayQuestState " + PlayerPrefs.GetString("PlayQuestState"));

        //Debug.Log("아.......... -_-  " + _finishTime + "  " + PlayerPrefs.GetString("AT_PlayQuestState"));
        if (asiaTimeLimit_Count == 0)   //5분
        {
            //Debug.Log("들어왓음");
            //노멀코스1 완주 시간이 없거나 300초(5분)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "5:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
            }
            //완주시간이 300초 안이면
            else if(CourseOpenTimeState(_finishTime) <= maxTime && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") && 
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 362439))
            {
                _titleText.text = "5:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 1) // 4분40
        {
            //Debug.Log("..............  " + PlayerPrefs.GetString("AT_PlayQuestState"));
            //완주 시간이 없거나 280초(4분40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 20
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                //Debug.Log("-_-..............");
                _titleText.text = "4:40";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
            }
            //완주시간이 280초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 20 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                //Debug.Log("장난치니 ?????");
                _titleText.text = "4:40";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 2)  //4:20
        {
            //완주 시간이 없거나 260초(4:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 40
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "4:20";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 260초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 40 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "4:20";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 3)  //4:00
        {
            //완주 시간이 없거나 240초(4:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 60
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "4:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 240초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "4:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 4)  //3:40
        {
            //완주 시간이 없거나 220초(3:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 80
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "3:40";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 220초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 80 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "3:40";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 5)  //3:20
        {
            //완주 시간이 없거나 200초(3:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 100
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "3:20";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 200초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 100 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "3:20";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 6)  //3:00
        {
            //완주 시간이 없거나 180초(3:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 120
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "3:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 180초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "3:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 7)  //2:40
        {
            //완주 시간이 없거나 160초(2:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 140
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "2:40";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 160초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 140 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "2:40";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 8)  //2:20
        {
            //완주 시간이 없거나 140초(2:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 160
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "2:20";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 140초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 160 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "2:20";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 9)  //2:00
        {
            //완주 시간이 없거나 120초(2:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 180
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "2:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 120초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "2:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count == 10) //1:40
        {
            //완주 시간이 없거나 100초(1:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 200
                || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
            {
                _titleText.text = "1:40";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("PlayQuestState", "No");
            }
            //완주시간이 100초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 200 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
            {
                _titleText.text = "1:40";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        }
        else if (asiaTimeLimit_Count >= 11) //끝
        {
            //Debug.Log("12번째 끝난거 아님?");
            _titleText.text = "1:40";
            _slider.value = 1f;
            _sliderText.text = "1/1";
            _accptBtn.interactable = false; //보상받기 버튼 비활성화
            _accptBtn.GetComponent<Image>().sprite = _takeSprite;
            _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 활성화
            _mapObj.transform.GetChild(1).gameObject.SetActive(true);  //바상받기완료 활성화
        }
        //else if (asiaTimeLimit_Count == 11) //1:20
        //{
        //    //완주 시간이 없거나 270초(1:20)이 넘었을 경우
        //    if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 220
        //        || PlayerPrefs.GetString("AT_PlayQuestState").Equals("No"))
        //    {
        //        Debug.Log("11번째 안걸림");
        //        _titleText.text = "1:20";
        //        _slider.value = 0f;
        //        _sliderText.text = "0/1";
        //        _accptBtn.interactable = false; //보상받기 버튼 비활성화
        //        _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
        //        //PlayerPrefs.SetString("PlayQuestState", "No");
        //    }
        //    //완주시간이 270초 안이면
        //    else if (CourseOpenTimeState(_finishTime) <= maxTime - 220 && PlayerPrefs.GetString("AT_PlayQuestState").Equals("Yes") &&
        //        (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
        //    {
        //        Debug.Log("11번째 걸림");
        //        _titleText.text = "1:20";
        //        _slider.value = 1f;
        //        _sliderText.text = "1/1";
        //        _accptBtn.interactable = true; //보상받기 버튼 활성화
        //        _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
        //    }
        //}
        //else if(asiaTimeLimit_Count >= 12) //끝
        //{
        //    Debug.Log("12번째 끝난거 아님?");
        //    _titleText.text = "1:20";
        //    _slider.value = 1f;
        //    _sliderText.text = "1/1";
        //    _accptBtn.interactable = false; //보상받기 버튼 비활성화
        //    _accptBtn.GetComponent<Image>().sprite = _takeSprite;
        //    _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 활성화
        //    _mapObj.transform.GetChild(1).gameObject.SetActive(true);  //바상받기완료 활성화
        //}


    }
    /////아시아 맵 시간제한 완주 - 노멀 하드(참고용)
    public void AsiaCourse_TimeLimitFinish2(int _limitCount, string _finishTime, Text _titleText, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj)
    {
        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);

        //노멀코스1 완주 시간이 없거나 600초(10분)이 넘었을 경우
        if (CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime)
        {
            _titleText.text = "10:00";
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _accptBtn.interactable = false; //보상받기 버튼 비활성화
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
        }
        //노멀코스1 완주 시간이 600초(10분)보다 작거나, 570초(9분30분)크면 --- 10분
        else if (CourseOpenTimeState(_finishTime) <= maxTime && CourseOpenTimeState(_finishTime) > maxTime - 30)
        {
            if (asiaTimeLimit_Count == 1)
            {
                _titleText.text = "10:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;  //보상받기 버튼 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);   //미션완료 이미지 활성화
            }
            else if (asiaTimeLimit_Count != 1)
            {
                _titleText.text = "9:30";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //9분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 30 && CourseOpenTimeState(_finishTime) > maxTime - 60)
        {
            if (asiaTimeLimit_Count == 2)
            {
                _titleText.text = "9:30";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 2)
            {
                _titleText.text = "9:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //9분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && CourseOpenTimeState(_finishTime) > maxTime - 90)
        {
            if (asiaTimeLimit_Count == 3)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 3)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //8분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 90 && CourseOpenTimeState(_finishTime) > maxTime - 120)
        {
            if (asiaTimeLimit_Count == 4)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 4)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //8분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && CourseOpenTimeState(_finishTime) > maxTime - 150)
        {
            if (asiaTimeLimit_Count == 5)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 5)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //7분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 150 && CourseOpenTimeState(_finishTime) > maxTime - 180)
        {
            if (asiaTimeLimit_Count == 6)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 6)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //7분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && CourseOpenTimeState(_finishTime) > maxTime - 210)
        {
            if (asiaTimeLimit_Count == 7)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 7)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //6분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 210 && CourseOpenTimeState(_finishTime) > maxTime - 240)
        {
            if (asiaTimeLimit_Count == 8)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 8)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //6분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 240 && CourseOpenTimeState(_finishTime) > maxTime - 270)
        {
            if (asiaTimeLimit_Count == 9)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 9)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //5분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 270 && CourseOpenTimeState(_finishTime) > maxTime - 300)
        {
            if (asiaTimeLimit_Count == 10)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 10)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //5분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 300 && CourseOpenTimeState(_finishTime) > maxTime - 330)
        {
            if (asiaTimeLimit_Count == 11)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 11)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //4분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 330 && CourseOpenTimeState(_finishTime) > maxTime - 360)
        {
            if (asiaTimeLimit_Count == 12)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count != 12)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //4분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 360)// && CourseOpenTimeState(_finishTime) > maxTime - 390)
        {
            if (asiaTimeLimit_Count == 13)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (asiaTimeLimit_Count < 13)
            {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
            else if (asiaTimeLimit_Count > 13)
            {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(1).gameObject.SetActive(true);   //보상받기 완료 이미지 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }


    //아시아 맵 한번씩 완주
    public void AsiaMap_AllOneFinish(GameObject _obj, Slider _slider, Text _sliderText)
    {
        int sliderValue1 =  0, sliderValue2 = 0, sliderValue3 = 0, sliderValue4 = 0, sliderValue5 = 0, sliderValue6 = 0;
        int total = 0;

        if(PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") < 1)
            sliderValue1 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") >= 1)
            sliderValue1 = 1;

        if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 1)
            sliderValue2 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") >= 1)
            sliderValue2 = 1;

        if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") < 1)
            sliderValue3 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") >= 1)
            sliderValue3 = 1;

        if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") < 1)
            sliderValue4 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") >= 1)
            sliderValue4 = 1;

        if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") < 1)
            sliderValue5 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") >= 1)
            sliderValue5 = 1;

        if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") < 1)
            sliderValue6 = 0;
        else if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") >= 1)
            sliderValue6 = 1;

        total = sliderValue1 + sliderValue2 + sliderValue3 + sliderValue4 + sliderValue5 + sliderValue6;

        if(total < 6)
        {
            _slider.value = (float)total / 6;
            _sliderText.text = total + "/6";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if (total == 6)
        {
            if(PlayerPrefs.GetString("AT_AllOneFinish") == "MissionOk")
            {
                _slider.value = (float)6 / 6;
                _sliderText.text = "6/6";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = (float)6 / 6;
                _sliderText.text = "6/6";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AsiaMap_AllTenFinish(GameObject _obj, Slider _slider, Text _sliderText)
    {
        int sliderValue1 = 0, sliderValue2 = 0, sliderValue3 = 0, sliderValue4 = 0, sliderValue5 = 0, sliderValue6 = 0;
        int total = 0;

        if (PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") < 10)
            sliderValue1 = PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") >= 10)
            sliderValue1 = 10;

        if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 10)
            sliderValue2 = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") >= 10)
            sliderValue2 = 10;

        if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") < 10)
            sliderValue3 = PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") >= 10)
            sliderValue3 = 10;

        if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") < 10)
            sliderValue4 = PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") >= 10)
            sliderValue4 = 10;

        if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") < 10)
            sliderValue5 = PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") >= 10)
            sliderValue5 = 10;

        if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") < 10)
            sliderValue6 = PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") >= 10)
            sliderValue6 = 10;

        total = sliderValue1 + sliderValue2 + sliderValue3 + sliderValue4 + sliderValue5 + sliderValue6;

        if(total < 60)
        {
            _slider.value = (float)total / 60;
            _sliderText.text = total + "/60";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if(total == 60)
        {
            if (PlayerPrefs.GetString("AT_AllTenFinish") == "MissionOk")
            {
                _slider.value = (float)60 / 60;
                _sliderText.text = "60/60";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = (float)60 / 60;
                _sliderText.text = "60/60";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AsiaMap_AllTwentyFinish(GameObject _obj, Slider _slider, Text _sliderText)
    {
        int sliderValue1 = 0, sliderValue2 = 0, sliderValue3 = 0, sliderValue4 = 0, sliderValue5 = 0, sliderValue6 = 0;
        int total = 0;

        if (PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") < 20)
            sliderValue1 = PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") >= 20)
            sliderValue1 = 20;

        if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") < 20)
            sliderValue2 = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") >= 20)
            sliderValue2 = 20;

        if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") < 20)
            sliderValue3 = PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") >= 20)
            sliderValue3 = 20;

        if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") < 20)
            sliderValue4 = PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") >= 20)
            sliderValue4 = 20;

        if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") < 20)
            sliderValue5 = PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") >= 20)
            sliderValue5 = 20;

        if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") < 20)
            sliderValue6 = PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount");
        else if (PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") >= 20)
            sliderValue6 = 20;

        total = sliderValue1 + sliderValue2 + sliderValue3 + sliderValue4 + sliderValue5 + sliderValue6;

        if (total < 120)
        {
            _slider.value = (float)total / 120;
            _sliderText.text = total + "/120";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if (total == 120)
        {
            if (PlayerPrefs.GetString("AT_AllTwentyFinish") == "MissionOk")
            {
                _slider.value = (float)120 / 120;
                _sliderText.text = "120/120";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = (float)120 / 120;
                _sliderText.text = "120/120";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AsiaMap500KmPass(GameObject _obj, Slider _slider, Text _sliderText)
    {
        if(PlayerPrefs.GetFloat("AT_Record_TotalKm") < 500f)
        {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if (PlayerPrefs.GetFloat("AT_Record_TotalKm") >= 500f)
        {
            if(PlayerPrefs.GetString("AT_Distance500Km") == "MissionOk")
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AsiaMap1000KmPass(GameObject _obj, Slider _slider, Text _sliderText)
    {
        if (PlayerPrefs.GetFloat("AT_Record_TotalKm") < 1000f)
        {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if (PlayerPrefs.GetFloat("AT_Record_TotalKm") >= 1000f)
        {
            if (PlayerPrefs.GetString("AT_Distance1000Km") == "MissionOk")
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AsiaMap1500KmPass(GameObject _obj, Slider _slider, Text _sliderText)
    {
        if (PlayerPrefs.GetFloat("AT_Record_TotalKm") < 1500f)
        {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        }
        else if (PlayerPrefs.GetFloat("AT_Record_TotalKm") >= 1500f)
        {
            if (PlayerPrefs.GetString("AT_Distance1500Km") == "MissionOk")
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            }
            else
            {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }



    public void ConnectButtonOn()
    {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
    }

}
