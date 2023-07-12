using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class QuestData_Manager : MonoBehaviour {
    //enum TodayQuestConnectMission {
    //    Connect,
    //    StayFor10m,
    //    StayFor30m,
    //    StayFor1h,
    //}
    //enum TodayQuestVisitMission {
    //    VisitStore,
    //    VisitMyInfo,
    //    VisitMyInventory,
    //    VisitRankingZone,
    //}

    //enum TodayQuestGameUseMission {
    //    EditProfile,
    //    PlayGame,
    //    RankUp,
    //    UsingCoin,
    //    UsingItem,
    //    PurchasingItem,
    //}

    //enum TodayQuestBurnUpMission {
    //    Over300kcal,
    //    Over400kcal,
    //    Over500kcal,
    //    Over600kcal,
    //}

    //enum TodayQuestMaxSpeed {
    //    MaxSpeed_25,
    //    MaxSpeed_30,
    //    MaxSpeed_35,
    //    MaxSpeed_40,
    //    MaxSpeed_45,
    //    MaxSpeed_50,
    //    MaxSpeed_55,
    //}

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

    [Space(10)]
    [Header("LocalizedSprite")]
    public LocalizedSprite localizedTakeCompletedButtonSprite = new LocalizedSprite();
    [Space(10)]
    [Header("Today Quest LocalizedString")]
    public LocalizedString[] questConnectMission;
    public LocalizedString[] questVisitMission;
    public LocalizedString[] questGameUseMission;
    public LocalizedString[] questBurnUpCalroieTodayMission;
    public LocalizedString[] questMaxSpeedTodayMission;

    public Text[] todayQuest_TitleText;

    private void Awake() {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    private void OnEnable() {

        #region ConnectMission
        questConnectMission[(int)TodayQuestConnectMission.Connect].StringChanged += Connect_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor10m].StringChanged += StayFor10m_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor30m].StringChanged += StayFor30m_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor1h].StringChanged += StayFor1h_StringChanged;
        #endregion

        #region VisitMission
        questVisitMission[(int)TodayQuestVisitMission.VisitStore].StringChanged += VisitStore_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitMyInfo].StringChanged += VisitMyInfo_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitMyInventory].StringChanged += VisitMyInventory_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitRankingZone].StringChanged += VisitRankingZone_StringChanged;
        #endregion

        #region GameUseMission
        questGameUseMission[(int)TodayQuestGameUseMission.EditProfile].StringChanged += EditProfile_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.PlayGame].StringChanged += PlayGame_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.RankUp].StringChanged += RankUp_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.UsingCoin].StringChanged += UsingCoin_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.UsingItem].StringChanged += UsingItem_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.PurchasingItem].StringChanged += PurchasingItem_StringChanged;
        #endregion

        #region BurnUpCalroie
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over300kcal].StringChanged += Over300kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over400kcal].StringChanged += Over400kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over500kcal].StringChanged += Over500kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over600kcal].StringChanged += Over600kcal_StringChanged;
        #endregion

        #region MaxSpeed
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_25].StringChanged += MaxSpeed_25_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_30].StringChanged += MaxSpeed_30_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_35].StringChanged += MaxSpeed_35_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_40].StringChanged += MaxSpeed_40_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_45].StringChanged += MaxSpeed_45_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_50].StringChanged += MaxSpeed_50_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_55].StringChanged += MaxSpeed_55_StringChanged;
        #endregion
    }

    private void OnDisable() {

        #region ConnectMission
        questConnectMission[(int)TodayQuestConnectMission.Connect].StringChanged -= Connect_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor10m].StringChanged -= StayFor10m_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor30m].StringChanged -= StayFor30m_StringChanged;
        questConnectMission[(int)TodayQuestConnectMission.StayFor1h].StringChanged -= StayFor1h_StringChanged;
        #endregion

        #region VisitMission
        questVisitMission[(int)TodayQuestVisitMission.VisitStore].StringChanged -= VisitStore_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitMyInfo].StringChanged -= VisitMyInfo_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitMyInventory].StringChanged -= VisitMyInventory_StringChanged;
        questVisitMission[(int)TodayQuestVisitMission.VisitRankingZone].StringChanged -= VisitRankingZone_StringChanged;
        #endregion

        #region GameUseMission
        questGameUseMission[(int)TodayQuestGameUseMission.EditProfile].StringChanged -= EditProfile_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.PlayGame].StringChanged -= PlayGame_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.RankUp].StringChanged -= RankUp_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.UsingCoin].StringChanged -= UsingCoin_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.UsingItem].StringChanged -= UsingItem_StringChanged;
        questGameUseMission[(int)TodayQuestGameUseMission.PurchasingItem].StringChanged -= PurchasingItem_StringChanged;
        #endregion

        #region BurnUpCalroie
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over300kcal].StringChanged -= Over300kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over400kcal].StringChanged -= Over400kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over500kcal].StringChanged -= Over500kcal_StringChanged;
        questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over600kcal].StringChanged -= Over600kcal_StringChanged;
        #endregion

        #region MaxSpeed
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_25].StringChanged -= MaxSpeed_25_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_30].StringChanged -= MaxSpeed_30_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_35].StringChanged -= MaxSpeed_35_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_40].StringChanged -= MaxSpeed_40_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_45].StringChanged -= MaxSpeed_45_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_50].StringChanged -= MaxSpeed_50_StringChanged;
        questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_55].StringChanged -= MaxSpeed_55_StringChanged;
        #endregion
    }

    private void MaxSpeed_55_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_55) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_50_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_50) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_45_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_45) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_40_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_40) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_35_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_35) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_30_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_30) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void MaxSpeed_25_StringChanged(string value) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_25) {
            todayQuest_TitleText[4].text = value;
        }
    }

    private void Over600kcal_StringChanged(string value) {
        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
        if (kcalNum == (int)TodayQuestBurnUpMission.Over600kcal) {
            todayQuest_TitleText[3].text = value;
        }
    }

    private void Over500kcal_StringChanged(string value) {
        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
        if (kcalNum == (int)TodayQuestBurnUpMission.Over500kcal) {
            todayQuest_TitleText[3].text = value;
        }
    }

    private void Over400kcal_StringChanged(string value) {
        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
        if (kcalNum == (int)TodayQuestBurnUpMission.Over400kcal) {
            todayQuest_TitleText[3].text = value;
        }
    }

    private void Over300kcal_StringChanged(string value) {
        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
        if (kcalNum == (int)TodayQuestBurnUpMission.Over300kcal) {
            todayQuest_TitleText[3].text = value;
        }
    }

    private void PurchasingItem_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.PurchasingItem) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void UsingItem_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.UsingItem) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void UsingCoin_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.UsingCoin) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void RankUp_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.RankUp) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void PlayGame_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.PlayGame) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void EditProfile_StringChanged(string value) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        if (useNum == (int)TodayQuestGameUseMission.EditProfile) {
            todayQuest_TitleText[2].text = value;
        }
    }

    private void VisitRankingZone_StringChanged(string value) {
        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");
        if (visitNum == (int)TodayQuestVisitMission.VisitRankingZone) {
            todayQuest_TitleText[1].text = value;
        }
    }

    private void VisitMyInventory_StringChanged(string value) {
        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");
        if (visitNum == (int)TodayQuestVisitMission.VisitMyInventory) {
            todayQuest_TitleText[1].text = value;
        }
    }

    private void VisitMyInfo_StringChanged(string value) {
        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");
        if (visitNum == (int)TodayQuestVisitMission.VisitMyInfo) {
            todayQuest_TitleText[1].text = value;
        }
    }

    private void VisitStore_StringChanged(string value) {
        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");
        if (visitNum == (int)TodayQuestVisitMission.VisitStore) {
            todayQuest_TitleText[1].text = value;
        }
    }

    private void StayFor1h_StringChanged(string value) {
        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        if (connectCount == (int)TodayQuestConnectMission.StayFor1h) {
            todayQuest_TitleText[0].text = value;
        }
    }

    private void StayFor30m_StringChanged(string value) {
        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        if (connectCount == (int)TodayQuestConnectMission.StayFor30m) {
            todayQuest_TitleText[0].text = value;
        }
    }

    private void StayFor10m_StringChanged(string value) {
        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        if (connectCount == (int)TodayQuestConnectMission.StayFor10m) {
            todayQuest_TitleText[0].text = value;
        }
    }

    private void Connect_StringChanged(string value) {
        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        if (connectCount == (int)TodayQuestConnectMission.Connect) {
            todayQuest_TitleText[0].text = value;
        }
    }

    void Start() {
        GetTodayInitialization();
        connetTime = GameObject.Find("ConnetTimeManager");
    }

    void Update() {
        if (Application.platform.Equals(RuntimePlatform.Android)) {
            if (Input.GetKey(KeyCode.Escape)) {
                gameEndPopup.SetActive(true);
            }
        }
    }

    //오늘 날짜 초기화
    void GetTodayInitialization() {
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
    public string CommaText(int _data) {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    //코인 보상 받기
    public void GetCoinReward(Text _totalText, Text _priceText) {
        int totalCoin = int.Parse(_totalText.text.Replace(",", ""));    //총 코인 int화
        int priceCoin = int.Parse(_priceText.text.Replace(",", ""));    //보상 가격 int화

        totalCoin += priceCoin;
        PlayerPrefs.SetInt("Busan_Player_Gold", totalCoin);

        ServerManager.Instance.Update_Gold_Data(totalCoin);

        _totalText.text = CommaText(totalCoin);
    }


    //// 일일퀘스트 -------------------------------------------------------------
    //접속하기 미션 함수
    public async UniTaskVoid Quest_ConnectMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg) {
        //currTime = PlayerPrefs.GetFloat("ConnectTime"); //현재 접속한 시간

        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        //Debug.Log("connectCount " + connectCount);

        //접속하기 미션 
        if (connectCount == (int)TodayQuestConnectMission.Connect) {
            //LanguageSelectorManager.instance.GetStringLocale(questConnectMission[(int)TodayQuestConnectMission.Connect], out string result);
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questConnectMission[(int)TodayQuestConnectMission.Connect]);
            //_title.text = result;       // 위의 Event Callback 에서 바꾸는데 쓰이는 이유 >> 보상을 받았을 때 퀘스트제목이 바뀌기 위해서
            //_title.text = "접속하기";
            _slider.value = 1;
            _content.text = "1/1";
            _coin.text = CommaText(1000);
            _completeImg.SetActive(true);   //미션완료 도장 활성화
            _takeBtn.interactable = true;   //버튼 활성화
        }
        //10분간 접속 유지 - (접속하기 미션 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(1))
        else if (connectCount == (int)TodayQuestConnectMission.StayFor10m) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questConnectMission[(int)TodayQuestConnectMission.StayFor10m]);
            //_title.text = result;
            //_title.text = "10분간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 10분이 넘었으면
            if (ConnetTime.instance.currTime >= 600f) {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            } else {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
            }
        }
        //30분간 접속 유지 - (10분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(2))
        else if (connectCount == (int)TodayQuestConnectMission.StayFor30m) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questConnectMission[(int)TodayQuestConnectMission.StayFor30m]);
            //_title.text = "30분간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 30분이 넘었으면
            if (ConnetTime.instance.currTime >= 1800f) {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            } else {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
            }
        }
        //1시간 접속 유지 - (20분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(3))
        else if (connectCount == (int)TodayQuestConnectMission.StayFor1h) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questConnectMission[(int)TodayQuestConnectMission.StayFor1h]);
            //_title.text = result;
            //_title.text = "1시간 접속 유지하기";
            _coin.text = CommaText(1000);

            //접속한 시간이 60분이 넘었으면
            if (ConnetTime.instance.currTime >= 3600f) {
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _slider.value = 1;
                _content.text = "1/1";
                _takeBtn.interactable = true;   //버튼 활성화
            } else {
                _completeImg.SetActive(false);   //미션완료 도장 바활성화
                _slider.value = 0;
                _content.text = "0/1";
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(false);   //받기완료 활성화
                //_takeBtn.GetComponent<Image>().sprite = _takeSprite;
            }
        } else if (connectCount > (int)TodayQuestConnectMission.StayFor1h) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questConnectMission[(int)TodayQuestConnectMission.StayFor1h]);
            //_title.text = result;
            //_title.text = "1시간 접속 유지하기";
            _completeImg.SetActive(true);   //미션완료 도장 활성화
            _slider.value = 1;
            _content.text = "1/1";
            _takeBtn.interactable = false;   //버튼 비활성화
            _takeImg.SetActive(true);   //받기완료 활성화

            //LanguageSelectorManager.instance.GetSpriteLocale(localizedTakeCompletedButtonSprite, out Sprite completedButton);
            _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            //_takeBtn.GetComponent<Image>().sprite = completedButton;
        }
    }

    //퀘스트2 오늘 처음 들어왔을 때 미션 랜덤으로 세팅
    public int VisitRandom() {
        //0 : 스토어, 1: 내정보, 2: 가방, 3: 랭킹존
        int rand = UnityEngine.Random.Range(0, 4);
        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "No", rand);
        ServerManager.Instance.myTodayQuest[1].quest_Idx = rand;
        return rand;
    }

    //퀘스트3번 처음 들어왔을 때 미션 랜덤으로 세팅
    public int GameUseRandom() {
        //0: 프로필 변경, 1: 게임 한번 하기, 2: 현재 순위 올리기,  3:골드 사용하기, 4: 아이템 사용하기, 5: 아이템 구매하기
        int rand = UnityEngine.Random.Range(0, 6);
        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "No", rand);
        ServerManager.Instance.myTodayQuest[2].quest_Idx = rand;
        return rand;
    }
    //퀘스트4번 - 처음 들어왔을 때 미션 랜덤세팅 - 칼로리 소모
    public int BurnUp_CalroieToday() {
        //0: 300, 1:400, 2:500, 3:600
        int rand = UnityEngine.Random.Range(0, 4);
        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "No", rand);
        ServerManager.Instance.myTodayQuest[3].quest_Idx = rand;
        return rand;
    }
    //퀘스트5번 - 처음 들어왔을 때 미션 랜덤세팅 - 오늘의 최고속도
    public int MaxSpeedToday() {
        //0:25, 1: 20, 2:35, 3:40, 4:45, 5:50 6: 55
        int rand = UnityEngine.Random.Range(0, 7);
        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "No", rand);
        ServerManager.Instance.myTodayQuest[4].quest_Idx = rand;
        return rand;
    }


    //방문하기 미션 함수
    public async UniTask Quest_VisitMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg) {
        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");

        //스토어 방문
        if (visitNum == (int)TodayQuestVisitMission.VisitStore) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questVisitMission[(int)TodayQuestVisitMission.VisitStore]);
            //_title.text = result;

            if (PlayerPrefs.GetString("Busan_StoreVisit") == "Yes") {
                //_title.text = "스토어 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            } else if (PlayerPrefs.GetString("Busan_StoreVisit") == "No") {
                //_title.text = "스토어 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            } else if (PlayerPrefs.GetString("Busan_StoreVisit") == "MissionOk") {
                //_title.text = "스토어 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
                //_takeBtn.GetComponent<Image>().sprite = completedButton;
            }
        }
        //내정보 방문
        else if (visitNum == (int)TodayQuestVisitMission.VisitMyInfo) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questVisitMission[(int)TodayQuestVisitMission.VisitMyInfo]);
            //_title.text = result;

            if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "Yes") {
                //_title.text = "내 정보 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            } else if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "No") {
                //_title.text = "내 정보 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            } else if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "MissionOk") {
                //_title.text = "내 정보 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            }
        }
        //가방 방문
        else if (visitNum == (int)TodayQuestVisitMission.VisitMyInventory) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questVisitMission[(int)TodayQuestVisitMission.VisitMyInventory]);

            if (PlayerPrefs.GetString("Busan_InventoryVisit") == "Yes") {
                //_title.text = "내 배낭 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            } else if (PlayerPrefs.GetString("Busan_InventoryVisit") == "No") {
                //_title.text = "내 배낭 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            } else if (PlayerPrefs.GetString("Busan_InventoryVisit") == "MissionOk") {
                //Debug.Log("??????????????????");
                //_title.text = "내 배낭 방문하기 ";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            }
        }
        //랭킹존 방문
        else if (visitNum == (int)TodayQuestVisitMission.VisitRankingZone) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questVisitMission[(int)TodayQuestVisitMission.VisitRankingZone]);

            if (PlayerPrefs.GetString("Busan_RankJonVisit") == "Yes") {
                //_title.text = "랭킹존 방문하기";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화

                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
            } else if (PlayerPrefs.GetString("Busan_RankJonVisit") == "No") {
                //_title.text = "랭킹존 방문하기";
                _content.text = "0/1";
                _slider.value = 0;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 비활성화
            } else if (PlayerPrefs.GetString("Busan_RankJonVisit") == "MissionOk") {
                //_title.text = "랭킹존 방문하기 ";
                _content.text = "1/1";
                _slider.value = 1;
                _coin.text = CommaText(1000);
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);   //받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            }
        }
    }

    //게임미션 함수
    public async UniTaskVoid Quest_GameUseMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg) {
        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
        //Debug.Log("useNum " + useNum);

        //프로필 변경
        if (useNum == (int)TodayQuestGameUseMission.EditProfile) {

            //LanguageSelectorManager.instance.GetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.EditProfile], out string result);
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.EditProfile]);
            //_title.text = result;

            if (PlayerPrefs.GetString("Busan_ProfileChange") == "No") {
                //_title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_ProfileChange") == "Yes") {
                //_title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_ProfileChange") == "MissionOk") {
                //_title.text = "프로필 변경하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);                
            }
        }
        //게임 한번 하기
        else if (useNum == (int)TodayQuestGameUseMission.PlayGame) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.PlayGame]);
            //_title.text = result;

            if (PlayerPrefs.GetString("Busan_GameOnePlay") == "No") {
                //_title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_GameOnePlay") == "Yes") {
                //_title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_GameOnePlay") == "MissionOk") {
                //_title.text = "게임 한번 하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
            }
        }
        //현재 순위 올리기
        else if (useNum == (int)TodayQuestGameUseMission.RankUp) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.RankUp]);
            //_title.text = result;

            if (PlayerPrefs.GetString("Busan_RankUp") == "No") {
                //_title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_RankUp") == "Yes") {
                //_title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_RankUp") == "MissionOk") {
                //_title.text = "현재 순위 올리기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
            }
        }
        //골드 사용하기
        else if (useNum == (int)TodayQuestGameUseMission.UsingCoin) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.UsingCoin]);
            
            if (PlayerPrefs.GetString("Busan_GoldUse") == "No") {
                //_title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_GoldUse") == "Yes") {
                //_title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_GoldUse") == "MissionOk") {
                //_title.text = "코인 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
            }
        }
        //아이템 사용하기
        else if (useNum == (int)TodayQuestGameUseMission.UsingItem) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.UsingItem]);

            //Debug.Log("ItemUse " + PlayerPrefs.GetString("Busan_ItemUse"));
            if (PlayerPrefs.GetString("Busan_ItemUse") == "No") {
                //_title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_ItemUse") == "Yes") {
                //_title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_ItemUse") == "MissionOk") {
                //_title.text = "아이템(소모품) 사용하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
            }
        }
        //아이템 구매하기
        else if (useNum == (int)TodayQuestGameUseMission.PurchasingItem) {

            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questGameUseMission[(int)TodayQuestGameUseMission.PurchasingItem]);

            if (PlayerPrefs.GetString("Busan_ItemPurchase") == "No") {
                //_title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "0/1";
                _slider.value = 0;
                _completeImg.SetActive(false);   //미션완료 도장 비활성화
                _takeBtn.interactable = false;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 비활성화
            } else if (PlayerPrefs.GetString("Busan_ItemPurchase") == "Yes") {
                //_title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = true;   //버튼 활성화
                _takeImg.SetActive(false);  //보상받기완료 활성화
                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
            } else if (PlayerPrefs.GetString("Busan_ItemPurchase") == "MissionOk") {
                //_title.text = "아이템(소모품) 구매하기";
                _coin.text = CommaText(1000);
                _content.text = "1/1";
                _slider.value = 1;
                _completeImg.SetActive(true);   //미션완료 도장 활성화
                _takeBtn.interactable = false;   //버튼 비활성화
                _takeImg.SetActive(true);  //보상받기완료 활성화

                _takeBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
            }
        }
    }

    //오늘의 칼로리 소모
    public async UniTaskVoid Quest_BurnUpCalroieTodayMission(GameObject _obj, Text _titleText, Slider _slider, Text _sliderText) {
        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모

        if (kcalNum == (int)TodayQuestBurnUpMission.Over300kcal) {

            _titleText.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over300kcal]);
            //_titleText.text = result;

            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 300) {
                //_titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            } else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 300) {
                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk") {
                    //_titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화

                    //LanguageSelectorManager.instance.GetSpriteLocale(localizedTakeCompletedButtonSprite, out Sprite completedButton);
                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);                    
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                } else {
                    //_titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over400kcal) {
            _titleText.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over400kcal]);
            
            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 400) {
                //_titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            } else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 400) {
                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk") {
                    //_titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                } else {
                    //_titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over500kcal) {

            _titleText.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over500kcal]);
            //_titleText.text = result;

            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 500) {
                //_titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            } else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 500) {
                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk") {
                    //_titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                } else {
                    //_titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over600kcal) {
            _titleText.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questBurnUpCalroieTodayMission[(int)TodayQuestBurnUpMission.Over600kcal]);

            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 600) {
                //_titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
            } else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 600) {
                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk") {
                    //_titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //보상받기 완료 이미지 변경

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
                } else {
                    //_titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    //오늘의 최고 속도
    public async UniTaskVoid Quest_MaxSpeedTodayMission(GameObject _obj, Text _title, Slider _slider, Text _sliderText) {
        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도

        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_25) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_25]);
            
            //TodayMaxSpeed 프리팹 - 로비에서 초기화. 아시아맵데이터에서 갱신
            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 25) {
                //_title.text = "오늘의 최대 속도 : 25 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 25) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 25 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);                    
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 25 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_30) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_30]);
            
            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 30) {
                //_title.text = "오늘의 최대 속도 : 30 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 30) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 30 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 30 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_35) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_35]);
            
            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 35) {
                //_title.text = "오늘의 최대 속도 : 35 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 35) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 35 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 35 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_40) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_40]);
            //_title.text = result;

            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 40) {
                //_title.text = "오늘의 최대 속도 : 40 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 40) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 40 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 40 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_45) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_45]);
            //_title.text = result;

            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 45) {
                //_title.text = "오늘의 최대 속도 : 45 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 45) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 45 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 45 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_50) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_50]);

            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 50) {
                //_title.text = "오늘의 최대 속도 : 50 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 50) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 50 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 50 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
                }
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_55) {
            _title.text = await LanguageSelectorManager.instance.ImmediateGetStringLocale(questMaxSpeedTodayMission[(int)TodayQuestMaxSpeed.MaxSpeed_55]);

            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 55) {
                //_title.text = "오늘의 최대 속도 : 55 km 이상";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
            } else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 55) {
                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk") {
                    //_title.text = "오늘의 최대 속도 : 55 km 이상";
                    _slider.value = 1f;
                    _sliderText.text = "1/1";
                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화

                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
                } else {
                    //_title.text = "오늘의 최대 속도 : 55 km 이상";
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
    float CourseOpenTimeState(string _data) {
        //Debug.Log("------------ " + _data);
        float courseTime = 0;
        if (_data != "") {
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
    string CourseOpenNameState(string _data) {
        string courseTime = "";
        if (_data != "") {
            string[] openTime;

            char sp = '/';
            openTime = _data.Split(sp);   //2021,12,10:00 따로 저장

            //char bar = ':';
            //_openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

            courseTime = openTime[0];
        }
        return courseTime;
    }

    //아시아 맵 완주 - 노멀 하드
    public void AsiaMapCourse_Finish(int _finishCount, int _finishAmount, GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn) {
        //Debug.Log("갯수 :: " + PlayerPrefs.GetInt("AsiaNormal1Finish"));
        asiaMap_Count = _finishCount;// PlayerPrefs.GetInt("AsiaNormal1Finish");
        //Debug.Log("_mapCourse " + _mapCourse + " asiaNormal1_Count " + asiaMap_Count + " ::: " + CourseOpenTimeState(_mapCourse));

        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
        if (CourseOpenTimeState(_mapCourse) == 0 || CourseOpenTimeState(_mapCourse) == 362439) {
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _acceptBtn.interactable = false;
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
        }
        //코스 1에 기록이 잇을 경우  - 완주를 했다.
        else if (CourseOpenTimeState(_mapCourse) > 0 || CourseOpenTimeState(_mapCourse) != 362439) {
            if (asiaMap_Count == 0) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _acceptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaMap_Count == 1) {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 5) {
                    _slider.value = (float)_finishAmount / 5;
                    _sliderText.text = _finishAmount + "/5";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 5) {
                    _slider.value = (float)_finishAmount / 5;
                    _sliderText.text = _finishAmount + "/5";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaMap_Count == 2) {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 10) {
                    _slider.value = (float)_finishAmount / 10;
                    _sliderText.text = _finishAmount + "/10";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 10) {
                    _slider.value = (float)_finishAmount / 10;
                    _sliderText.text = _finishAmount + "/10";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaMap_Count == 3) {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 15) {
                    _slider.value = (float)_finishAmount / 15;
                    _sliderText.text = _finishAmount + "/15";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 15) {
                    _slider.value = (float)_finishAmount / 15;
                    _sliderText.text = _finishAmount + "/15";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaMap_Count == 4) {
                //완주 5회 미만-  미션 미완료
                if (_finishAmount < 20) {
                    _slider.value = (float)_finishAmount / 20;
                    _sliderText.text = _finishAmount + "/20";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (_finishAmount >= 20) {
                    _slider.value = (float)_finishAmount / 20;
                    _sliderText.text = _finishAmount + "/20";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaMap_Count >= 5) {
                _slider.value = 1;
                _sliderText.text = "20/20";
                _acceptBtn.interactable = false;
                _mapObj.transform.GetChild(1).gameObject.SetActive(true);    //보상완료이미지 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    ////아시아 맵 완주 초기버전 - 사용하지 않음(참고용)
    public void AsiaNormal2Course_Finish(GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn) {
        asiaNormal2_Count = PlayerPrefs.GetInt("AsiaNormal2Finish");

        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
        if (CourseOpenTimeState(_mapCourse) == 0) {
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _acceptBtn.interactable = false;
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
        }
        //코스 1에 기록이 잇을 경우  - 완주를 했다.
        else if (CourseOpenTimeState(_mapCourse) > 0) {
            if (asiaNormal2_Count == 0) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _acceptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaNormal2_Count == 1) {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 5) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 5;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/5";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 5) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 5;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/5";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaNormal2_Count == 2) {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 10) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 10;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/10";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 10) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 10;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/10";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaNormal2_Count == 3) {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 15) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 15;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/15";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 15) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 15;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/15";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            } else if (asiaNormal2_Count == 4) {
                //완주 5회 미만-  미션 미완료
                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 20) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 20;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/20";
                    _acceptBtn.interactable = false;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
                }
                //완주 5회 미션 성공
                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") >= 20) {
                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 20;
                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/20";
                    _acceptBtn.interactable = true;
                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
        }
    }



    //아시아 맵 시간제한 완주 - 노멀 하드
    public async UniTask AsiaCourse_TimeLimitFinish(int _limitCount, string _finishTime, LocalizedString questTitle, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj) {
        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);
        //Debug.Log("+++시간제한 " + asiaTimeLimit_Count + " PlayQuestState " + PlayerPrefs.GetString("PlayQuestState"));

        if (asiaTimeLimit_Count == 0)   //5분
        {
            //Debug.Log("들어왓음");
            //노멀코스1 완주 시간이 없거나 300초(5분)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "5:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);

                //_titleText.text = "5:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
            }
            //완주시간이 300초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 362439)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "5:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);

                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 1) // 4분40
          {
            //완주 시간이 없거나 280초(4분40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 20
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);

                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
            }
            //완주시간이 280초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 20 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);

                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 2)  //4:20
          {
            //완주 시간이 없거나 260초(4:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 40
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 260초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 40 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 3)  //4:00
          {
            //완주 시간이 없거나 240초(4:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 60
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 240초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "4:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 4)  //3:40
          {
            //완주 시간이 없거나 220초(3:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 80
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 220초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 80 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 5)  //3:20
          {
            //완주 시간이 없거나 200초(3:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 100
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 200초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 100 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 6)  //3:00
          {
            //완주 시간이 없거나 180초(3:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 120
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 180초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "3:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 7)  //2:40
          {
            //완주 시간이 없거나 160초(2:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 140
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 160초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 140 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 8)  //2:20
          {
            //완주 시간이 없거나 140초(2:20)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 160
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 140초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 160 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:20" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 9)  //2:00
          {
            //완주 시간이 없거나 120초(2:00)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 180
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 120초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "2:00" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count == 10) //1:40
          {
            //완주 시간이 없거나 100초(1:40)이 넘었을 경우
            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 200
                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No")) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "1:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
            }
            //완주시간이 100초 안이면
            else if (CourseOpenTimeState(_finishTime) <= maxTime - 200 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039)) {
                Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "1:40" }
                };

                LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true; //보상받기 버튼 비활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
            }
        } else if (asiaTimeLimit_Count >= 11) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
                {
                    { "timeLimit", "1:40" }
                };

            LanguageSelectorManager.instance.SetLocalVariablesString(questTitle, localVariables);
            _slider.value = 1f;
            _sliderText.text = "1/1";
            _accptBtn.interactable = false; //보상받기 버튼 비활성화

            //LanguageSelectorManager.instance.GetSpriteLocale(localizedTakeCompletedButtonSprite, out Sprite completedButton);
            _accptBtn.GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            //_accptBtn.GetComponent<Image>().sprite = completedButton;

            _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 활성화
            _mapObj.transform.GetChild(1).gameObject.SetActive(true);  //바상받기완료 활성화
        }
    }
    /////아시아 맵 시간제한 완주 - 노멀 하드(참고용)
    public void AsiaCourse_TimeLimitFinish2(int _limitCount, string _finishTime, Text _titleText, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj) {
        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);

        //노멀코스1 완주 시간이 없거나 600초(10분)이 넘었을 경우
        if (CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime) {
            _titleText.text = "10:00";
            _slider.value = 0f;
            _sliderText.text = "0/1";
            _accptBtn.interactable = false; //보상받기 버튼 비활성화
            _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
        }
        //노멀코스1 완주 시간이 600초(10분)보다 작거나, 570초(9분30분)크면 --- 10분
        else if (CourseOpenTimeState(_finishTime) <= maxTime && CourseOpenTimeState(_finishTime) > maxTime - 30) {
            if (asiaTimeLimit_Count == 1) {
                _titleText.text = "10:00";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;  //보상받기 버튼 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);   //미션완료 이미지 활성화
            } else if (asiaTimeLimit_Count != 1) {
                _titleText.text = "9:30";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //9분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 30 && CourseOpenTimeState(_finishTime) > maxTime - 60) {
            if (asiaTimeLimit_Count == 2) {
                _titleText.text = "9:30";
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 2) {
                _titleText.text = "9:00";
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //9분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && CourseOpenTimeState(_finishTime) > maxTime - 90) {
            if (asiaTimeLimit_Count == 3) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 3) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //8분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 90 && CourseOpenTimeState(_finishTime) > maxTime - 120) {
            if (asiaTimeLimit_Count == 4) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 4) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //8분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && CourseOpenTimeState(_finishTime) > maxTime - 150) {
            if (asiaTimeLimit_Count == 5) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 5) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //7분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 150 && CourseOpenTimeState(_finishTime) > maxTime - 180) {
            if (asiaTimeLimit_Count == 6) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 6) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //7분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && CourseOpenTimeState(_finishTime) > maxTime - 210) {
            if (asiaTimeLimit_Count == 7) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 7) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //6분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 210 && CourseOpenTimeState(_finishTime) > maxTime - 240) {
            if (asiaTimeLimit_Count == 8) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 8) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //6분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 240 && CourseOpenTimeState(_finishTime) > maxTime - 270) {
            if (asiaTimeLimit_Count == 9) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 9) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //5분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 270 && CourseOpenTimeState(_finishTime) > maxTime - 300) {
            if (asiaTimeLimit_Count == 10) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 10) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //5분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 300 && CourseOpenTimeState(_finishTime) > maxTime - 330) {
            if (asiaTimeLimit_Count == 11) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 11) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //4분30분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 330 && CourseOpenTimeState(_finishTime) > maxTime - 360) {
            if (asiaTimeLimit_Count == 12) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count != 12) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //4분
        else if (CourseOpenTimeState(_finishTime) <= maxTime - 360)// && CourseOpenTimeState(_finishTime) > maxTime - 390)
        {
            if (asiaTimeLimit_Count == 13) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = true;
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            } else if (asiaTimeLimit_Count < 13) {
                _slider.value = 0f;
                _sliderText.text = "0/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
            } else if (asiaTimeLimit_Count > 13) {
                _slider.value = 1f;
                _sliderText.text = "1/1";
                _accptBtn.interactable = false;
                _mapObj.transform.GetChild(1).gameObject.SetActive(true);   //보상받기 완료 이미지 활성화
                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }


    //아시아 맵 한번씩 완주
    public async UniTask AsiaMap_AllOneFinish(GameObject _obj, Slider _slider, Text _sliderText) {
        int sliderValue1 = 0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
        int total = 0;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 1)
            sliderValue1 = 0;
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 1)
            sliderValue1 = 1;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 1)
            sliderValue2 = 0;
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 1)
            sliderValue2 = 1;

        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 1)
            sliderValue4 = 0;
        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 1)
            sliderValue4 = 1;

        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 1)
            sliderValue5 = 0;
        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 1)
            sliderValue5 = 1;

        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

        if (total < 4) {
            _slider.value = (float)total / 4;
            _sliderText.text = total + "/4";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (total == 4) {
            if (PlayerPrefs.GetString("Busan_AllOneFinish") == "MissionOk") {
                _slider.value = (float)4 / 4;
                _sliderText.text = "4/4";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);                
            } else {
                _slider.value = (float)4 / 4;
                _sliderText.text = "4/4";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public async UniTaskVoid AsiaMap_AllTenFinish(GameObject _obj, Slider _slider, Text _sliderText) {
        int sliderValue1 = 0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
        int total = 0;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 10)
            sliderValue1 = PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 10)
            sliderValue1 = 10;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 10)
            sliderValue2 = PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 10)
            sliderValue2 = 10;

        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 10)
            sliderValue4 = PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 10)
            sliderValue4 = 10;

        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 10)
            sliderValue5 = PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 10)
            sliderValue5 = 10;

        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

        if (total < 40) {
            _slider.value = (float)total / 40;
            _sliderText.text = total + "/40";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (total == 40) {
            if (PlayerPrefs.GetString("Busan_AllTenFinish") == "MissionOk") {
                _slider.value = (float)40 / 40;
                _sliderText.text = "40/40";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;

                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
                //_obj.transform.GetChild(7).GetComponent<Image>().sprite = completedButton;
            } else {
                _slider.value = (float)40 / 40;
                _sliderText.text = "40/40";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public async UniTaskVoid AsiaMap_AllTwentyFinish(GameObject _obj, Slider _slider, Text _sliderText) {
        int sliderValue1 = 0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
        int total = 0;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 20)
            sliderValue1 = PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 20)
            sliderValue1 = 20;

        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 20)
            sliderValue2 = PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 20)
            sliderValue2 = 20;

        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 20)
            sliderValue4 = PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 20)
            sliderValue4 = 20;

        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 20)
            sliderValue5 = PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount");
        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 20)
            sliderValue5 = 20;

        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

        if (total < 80) {
            _slider.value = (float)total / 80;
            _sliderText.text = total + "/80";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (total == 80) {
            if (PlayerPrefs.GetString("Busan_AllTenFinish") == "MissionOk") {
                _slider.value = (float)80 / 80;
                _sliderText.text = "80/80";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;

                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
                //_obj.transform.GetChild(7).GetComponent<Image>().sprite = completedButton;
            } else {
                _slider.value = (float)80 / 80;
                _sliderText.text = "80/80";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public async UniTaskVoid AsiaMap500KmPass(GameObject _obj, Slider _slider, Text _sliderText) {
        if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 500f) {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 500f) {
            if (PlayerPrefs.GetString("Busan_Distance500Km") == "MissionOk") {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;

                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
                
            } else {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public async UniTaskVoid AsiaMap1000KmPass(GameObject _obj, Slider _slider, Text _sliderText) {
        if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 1000f) {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 1000f) {
            if (PlayerPrefs.GetString("Busan_Distance1000Km") == "MissionOk") {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;

                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);                
            } else {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public async UniTaskVoid AsiaMap1500KmPass(GameObject _obj, Slider _slider, Text _sliderText) {
        if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 1500f) {
            _slider.value = 0;
            _sliderText.text = "0/1";
            _obj.transform.GetChild(3).gameObject.SetActive(false);
            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
        } else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 1500f) {
            if (PlayerPrefs.GetString("Busan_Distance1500Km") == "MissionOk") {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
                _obj.transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
            } else {
                _slider.value = 1;
                _sliderText.text = "1/1";
                _obj.transform.GetChild(3).gameObject.SetActive(true);
                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ConnectButtonOn() {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
    }

    //게임 종료
    public void GameEndButtonOn() {
        Application.Quit(); //게임 종료
    }

}



//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestData_Manager : MonoBehaviour
//{
//    public static QuestData_Manager instance { get; private set; }

//    public GameObject gameEndPopup; //게임종료팝업
//    public string[] toDayArr;   //오늘 날짜 배열

//    string toDay;

//    public int connectCount; //접속하기 미션 보상받기 카운트
//    int visitNum;   //방문하기 미션 번호
//    int useNum; //게임사용 미션 번호
//    int kcalNum;    //오늘의 칼로리소모 번호
//    int maxSpeedNum;    //오늘의 최고속도 번호

//    GameObject connetTime;

//    //---Map 퀘스트 ------------------------
//    //아시아맵 완주 변수
//    int asiaMap_Count, asiaNormal2_Count;
//    //제한시간 맥스값
//    int maxTime = 300;
//    //아시아맵 시간제한 완주변수
//    int asiaTimeLimit_Count;


//    private void Awake()
//    {
//        if (instance != null)
//            Destroy(this);
//        else instance = this;
//    }

//    void Start()
//    {
//        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("Busan_GoldUse"));
//        GetTodayInitialization();
//        //Debug.Log("2---GoldUse " + PlayerPrefs.GetString("Busan_GoldUse"));
//        //Debug.Log("----------- " + PlayerPrefs.GetString("Busan_CustomChange"));

//        connetTime = GameObject.Find("ConnetTimeManager");

//        //Debug.Log("--- "+CourseOpenTimeState(PlayerPrefs.GetString("Asia Hard 1Course")));

//    }


//    void Update()
//    {
//        if (Application.platform.Equals(RuntimePlatform.Android))
//        {
//            if (Input.GetKey(KeyCode.Escape))
//            {
//                gameEndPopup.SetActive(true);
//            }
//        }
//    }

//    //오늘 날짜 초기화
//    void GetTodayInitialization()
//    {
//        toDay = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
//        toDayArr = new string[4];
//        toDayArr[0] = DateTime.Now.ToString("yyyy");
//        toDayArr[1] = DateTime.Now.ToString("MM");
//        toDayArr[2] = DateTime.Now.ToString("dd");
//        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");

//        //strSub = toDayArr[2].Substring(0, 1);

//        ////1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
//        //if (strSub == "0")
//        //    toDayArr[2] = toDayArr[2].Substring(1, 1);
//    }



//    //숫자 콤마 찍는 함수
//    public string CommaText(int _data)
//    {
//        if (_data != 0)
//            return string.Format("{0:#,###}", _data);
//        else
//            return "0";
//    }

//    //코인 보상 받기
//    public void GetCoinReward(Text _totalText, Text _priceText)
//    {
//        int totalCoin = int.Parse(_totalText.text.Replace(",", ""));    //총 코인 int화
//        int priceCoin = int.Parse(_priceText.text.Replace(",", ""));    //보상 가격 int화

//        totalCoin += priceCoin;
//        PlayerPrefs.SetInt("Busan_Player_Gold", totalCoin);

//        ServerManager.Instance.Update_Gold_Data(totalCoin);

//        _totalText.text = CommaText(totalCoin);
//    }


//    //// 일일퀘스트 -------------------------------------------------------------
//    //접속하기 미션 함수
//    public void Quest_ConnectMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
//    {
//        //currTime = PlayerPrefs.GetFloat("ConnectTime"); //현재 접속한 시간

//        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
//        //Debug.Log("connectCount " + connectCount);

//        //접속하기 미션 
//        if (connectCount == 0)
//        {
//            _title.text = "접속하기";
//            _slider.value = 1;
//            _content.text = "1/1";
//            _coin.text = CommaText(1000);
//            _completeImg.SetActive(true);   //미션완료 도장 활성화
//            _takeBtn.interactable = true;   //버튼 활성화
//        }
//        //10분간 접속 유지 - (접속하기 미션 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(1))
//        else if(connectCount == 1)
//        {
//            _title.text = "10분간 접속 유지하기";
//            _coin.text = CommaText(1000);

//            //접속한 시간이 10분이 넘었으면
//            if (ConnetTime.instance.currTime >= 600f)
//            {
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _slider.value = 1;
//                _content.text = "1/1";
//                _takeBtn.interactable = true;   //버튼 활성화
//            }
//            else
//            {
//                _completeImg.SetActive(false);   //미션완료 도장 바활성화
//                _slider.value = 0;
//                _content.text = "0/1";
//                _takeBtn.interactable = false;   //버튼 비활성화
//            } 
//        }
//        //30분간 접속 유지 - (10분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(2))
//        else if(connectCount == 2)
//        {
//            _title.text = "30분간 접속 유지하기";
//            _coin.text = CommaText(1000);

//            //접속한 시간이 30분이 넘었으면
//            if (ConnetTime.instance.currTime >= 1800f)
//            {
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _slider.value = 1;
//                _content.text = "1/1";
//                _takeBtn.interactable = true;   //버튼 활성화
//            }
//            else
//            {
//                _completeImg.SetActive(false);   //미션완료 도장 바활성화
//                _slider.value = 0;
//                _content.text = "0/1";
//                _takeBtn.interactable = false;   //버튼 비활성화
//            }
//        }
//        //1시간 접속 유지 - (20분간 접속 유지 성공으로 보상 받기 버튼 클릭 시 카운터 1 상승(3))
//        else if(connectCount == 3)
//        {
//            _title.text = "1시간 접속 유지하기";
//            _coin.text = CommaText(1000);

//            //접속한 시간이 60분이 넘었으면
//            if (ConnetTime.instance.currTime >= 3600f)
//            {
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _slider.value = 1;
//                _content.text = "1/1";
//                _takeBtn.interactable = true;   //버튼 활성화
//            }
//            else
//            {
//                _completeImg.SetActive(false);   //미션완료 도장 바활성화
//                _slider.value = 0;
//                _content.text = "0/1";
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(false);   //받기완료 활성화
//                //_takeBtn.GetComponent<Image>().sprite = _takeSprite;
//            }
//        }
//        else if(connectCount > 3)
//        {
//            _title.text = "1시간 접속 유지하기";
//            _completeImg.SetActive(true);   //미션완료 도장 활성화
//            _slider.value = 1;
//            _content.text = "1/1";
//            _takeBtn.interactable = false;   //버튼 비활성화
//            _takeImg.SetActive(true);   //받기완료 활성화
//            _takeBtn.GetComponent<Image>().sprite = _takeSprite;
//        }
//    }

//    //퀘스트2 오늘 처음 들어왔을 때 미션 랜덤으로 세팅
//    public int VisitRandom()
//    {
//        //0 : 스토어, 1: 내정보, 2: 가방, 3: 랭킹존
//        int rand = UnityEngine.Random.Range(0, 4);
//        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "No", rand);
//        ServerManager.Instance.myTodayQuest[1].quest_Idx = rand;
//        return rand;
//    }

//    //퀘스트3번 처음 들어왔을 때 미션 랜덤으로 세팅
//    public int GameUseRandom()
//    {
//        //0: 프로필 변경, 1: 게임 한번 하기, 2: 현재 순위 올리기,  3:골드 사용하기, 4: 아이템 사용하기, 5: 아이템 구매하기
//        int rand = UnityEngine.Random.Range(0, 6);
//        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "No", rand);
//        ServerManager.Instance.myTodayQuest[2].quest_Idx = rand;
//        return rand;
//    }
//    //퀘스트4번 - 처음 들어왔을 때 미션 랜덤세팅 - 칼로리 소모
//    public int BurnUp_CalroieToday()
//    {
//        //0: 300, 1:400, 2:500, 3:600
//        int rand = UnityEngine.Random.Range(0, 4);
//        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "No", rand);
//        ServerManager.Instance.myTodayQuest[3].quest_Idx = rand;
//        return rand;
//    }
//    //퀘스트5번 - 처음 들어왔을 때 미션 랜덤세팅 - 오늘의 최고속도
//    public int MaxSpeedToday()
//    {
//        //0:25, 1: 20, 2:35, 3:40, 4:45, 5:50 6: 55
//        int rand = UnityEngine.Random.Range(0, 7);
//        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "No", rand);
//        ServerManager.Instance.myTodayQuest[4].quest_Idx = rand;
//        return rand;
//    }


//    //방문하기 미션 함수
//    public void Quest_VisitMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
//    {
//        visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");

//        //스토어 방문
//        if (visitNum == 0)
//        {
//            //Debug.Log("스토어 " + PlayerPrefs.GetString("Busan_StoreVisit"));
//            if (PlayerPrefs.GetString("Busan_StoreVisit") == "Yes")
//            {
//                _title.text = "스토어 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_StoreVisit") == "No")
//            {
//                _title.text = "스토어 방문하기";
//                _content.text = "0/1";
//                _slider.value = 0;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_StoreVisit") == "MissionOk")
//            {
//                _title.text = "스토어 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);   //받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
//            }
//        }
//        //내정보 방문
//        else if (visitNum == 1)
//        {
//            if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "Yes")
//            {
//                _title.text = "내 정보 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화

//                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "No")
//            {
//                _title.text = "내 정보 방문하기";
//                _content.text = "0/1";
//                _slider.value = 0;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_MyInfoVisit") == "MissionOk")
//            {
//                _title.text = "내 정보 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);   //받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
//            }
//        }
//        //가방 방문
//        else if(visitNum == 2)
//        {
//            if (PlayerPrefs.GetString("Busan_InventoryVisit") == "Yes")
//            {
//                _title.text = "내 배낭 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화

//                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_InventoryVisit") == "No")
//            {
//                _title.text = "내 배낭 방문하기";
//                _content.text = "0/1";
//                _slider.value = 0;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//            }
//            else if(PlayerPrefs.GetString("Busan_InventoryVisit") == "MissionOk")
//            {
//                //Debug.Log("??????????????????");
//                _title.text = "내 배낭 방문하기 ";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);   //받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
//            }
//        }
//        //랭킹존 방문
//        else if (visitNum == 3)
//        {
//            if (PlayerPrefs.GetString("Busan_RankJonVisit") == "Yes")
//            {
//                _title.text = "랭킹존 방문하기";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화

//                ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_RankJonVisit") == "No")
//            {
//                _title.text = "랭킹존 방문하기";
//                _content.text = "0/1";
//                _slider.value = 0;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_RankJonVisit") == "MissionOk")
//            {
//                _title.text = "랭킹존 방문하기 ";
//                _content.text = "1/1";
//                _slider.value = 1;
//                _coin.text = CommaText(1000);
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);   //받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;
//            }
//        }
//    }

//    //게임미션 함수
//    public void Quest_GameUseMission(Text _title, Text _content, Slider _slider, Text _coin, GameObject _completeImg, Button _takeBtn, GameObject _takeImg, Sprite _takeSprite)
//    {
//        useNum = PlayerPrefs.GetInt("Busan_TodayQuest3");
//        //Debug.Log("useNum " + useNum);

//        //프로필 변경
//        if (useNum == 0)
//        {
//            if(PlayerPrefs.GetString("Busan_ProfileChange") == "No")
//            {
//                _title.text = "프로필 변경하기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_ProfileChange") == "Yes")
//            {
//                _title.text = "프로필 변경하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_ProfileChange") == "MissionOk")
//            {
//                _title.text = "프로필 변경하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//        //게임 한번 하기
//        else if (useNum == 1)
//        {
//            if (PlayerPrefs.GetString("Busan_GameOnePlay") == "No")
//            {
//                _title.text = "게임 한번 하기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_GameOnePlay") == "Yes")
//            {
//                _title.text = "게임 한번 하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_GameOnePlay") == "MissionOk")
//            {
//                _title.text = "게임 한번 하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//        //현재 순위 올리기
//        else if (useNum == 2)
//        {
//            if (PlayerPrefs.GetString("Busan_RankUp") == "No")
//            {
//                _title.text = "현재 순위 올리기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_RankUp") == "Yes")
//            {
//                _title.text = "현재 순위 올리기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_RankUp") == "MissionOk")
//            {
//                _title.text = "현재 순위 올리기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//        //골드 사용하기
//        else if (useNum == 3)
//        {
//            if (PlayerPrefs.GetString("Busan_GoldUse") == "No")
//            {
//                _title.text = "코인 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_GoldUse") == "Yes")
//            {
//                _title.text = "코인 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_GoldUse") == "MissionOk")
//            {
//                _title.text = "코인 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//        //아이템 사용하기
//        else if (useNum == 4)
//        {
//            //Debug.Log("ItemUse " + PlayerPrefs.GetString("Busan_ItemUse"));
//            if (PlayerPrefs.GetString("Busan_ItemUse") == "No")
//            {
//                _title.text = "아이템(소모품) 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_ItemUse") == "Yes")
//            {
//                _title.text = "아이템(소모품) 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_ItemUse") == "MissionOk")
//            {
//                _title.text = "아이템(소모품) 사용하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//        //아이템 구매하기
//        else if (useNum == 5)
//        {
//            if (PlayerPrefs.GetString("Busan_ItemPurchase") == "No")
//            {
//                _title.text = "아이템(소모품) 구매하기";
//                _coin.text = CommaText(1000);
//                _content.text = "0/1";
//                _slider.value = 0;
//                _completeImg.SetActive(false);   //미션완료 도장 비활성화
//                _takeBtn.interactable = false;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 비활성화
//            }
//            else if (PlayerPrefs.GetString("Busan_ItemPurchase") == "Yes")
//            {
//                _title.text = "아이템(소모품) 구매하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = true;   //버튼 활성화
//                _takeImg.SetActive(false);  //보상받기완료 활성화
//                ServerManager.Instance.Update_TodayQuest("TodayQuest3", "Yes");
//            }
//            else if (PlayerPrefs.GetString("Busan_ItemPurchase") == "MissionOk")
//            {
//                _title.text = "아이템(소모품) 구매하기";
//                _coin.text = CommaText(1000);
//                _content.text = "1/1";
//                _slider.value = 1;
//                _completeImg.SetActive(true);   //미션완료 도장 활성화
//                _takeBtn.interactable = false;   //버튼 비활성화
//                _takeImg.SetActive(true);  //보상받기완료 활성화
//                _takeBtn.GetComponent<Image>().sprite = _takeSprite;    //보상받기 완료 이미지 변경
//            }
//        }
//    }

//    //오늘의 칼로리 소모
//    public void Quest_BurnUpCalroieTodayMission(GameObject _obj, Text _titleText, Slider _slider, Text _sliderText, Sprite _sprite)
//    {
//        kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모

//        if(kcalNum == 0)
//        {
//            if(PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 300)
//            {
//                _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else if(PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 300)
//            {
//                if(PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk")
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
//                }
//                else
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 300 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if(kcalNum == 1)
//        {
//            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 400)
//            {
//                _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 400)
//            {
//                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk")
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
//                }
//                else
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 400 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if(kcalNum == 2)
//        {
//            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 500)
//            {
//                _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 500)
//            {
//                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk")
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
//                }
//                else
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 500 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if(kcalNum == 3)
//        {
//            if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") < 600)
//            {
//                _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else if (PlayerPrefs.GetFloat("Busan_Record_TodayKcal") >= 600)
//            {
//                if (PlayerPrefs.GetString("Busan_KcalBurnUp") == "MissionOk")
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 비활성화
//                }
//                else
//                {
//                    _titleText.text = "오늘의 칼로리 소모 : 600 kcal 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//    }

//    //오늘의 최고 속도
//    public void Quest_MaxSpeedTodayMission(GameObject _obj, Text _title, Slider _slider, Text _sliderText, Sprite _sprite)
//    {
//        maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도

//        if(maxSpeedNum == 0)
//        {
//            //TodayMaxSpeed 프리팹 - 로비에서 초기화. 아시아맵데이터에서 갱신
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 25)
//            {
//                _title.text = "오늘의 최대 속도 : 25 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if(PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 25)
//            {
//                if(PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 25 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 25 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if(maxSpeedNum == 1)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 30)
//            {
//                _title.text = "오늘의 최대 속도 : 30 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 30)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 30 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 30 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if (maxSpeedNum == 2)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 35)
//            {
//                _title.text = "오늘의 최대 속도 : 35 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 35)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 35 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 35 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if (maxSpeedNum == 3)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 40)
//            {
//                _title.text = "오늘의 최대 속도 : 40 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 40)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 40 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 40 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if (maxSpeedNum == 4)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 45)
//            {
//                _title.text = "오늘의 최대 속도 : 45 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 45)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 45 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 45 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if (maxSpeedNum == 5)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 50)
//            {
//                _title.text = "오늘의 최대 속도 : 50 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 50)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 50 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 50 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//        else if (maxSpeedNum == 6)
//        {
//            if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") < 55)
//            {
//                _title.text = "오늘의 최대 속도 : 55 km 이상";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(false); //미션완료 이미지 비활성화
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//            }
//            else if (PlayerPrefs.GetFloat("Busan_TodayMaxSpeed") >= 55)
//            {
//                if (PlayerPrefs.GetString("Busan_MaxSpeedToday") == "MissionOk")
//                {
//                    _title.text = "오늘의 최대 속도 : 55 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true); //미션완료 이미지 활성화
//                    _obj.transform.GetChild(1).gameObject.SetActive(true);  //보상완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Image>().sprite = _sprite;
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = false; //버튼 못누르게
//                }
//                else
//                {
//                    _title.text = "오늘의 최대 속도 : 55 km 이상";
//                    _slider.value = 1f;
//                    _sliderText.text = "1/1";
//                    _obj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료 이미지 활성화
//                    _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//                }
//            }
//        }
//    }

//    //// 맵퀘스트 -----------------------------------------------------------------
//    //코스별 시간 기록 가져오는 함수
//    float CourseOpenTimeState(string _data)
//    {
//        //Debug.Log("------------ " + _data);
//        float courseTime = 0;
//        if (_data != "")
//        {
//            string[] openTime;
//            string[] _openTime;

//            char sp = '/';
//            openTime = _data.Split(sp);   //2021,12,10:00 따로 저장
//            //Debug.Log("------------========= " + openTime[1]);

//            char bar = ':';
//            _openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

//            courseTime = (int.Parse(_openTime[0]) * 60 * 60) + int.Parse(_openTime[1]) * 60 + float.Parse(_openTime[2]);
//            //Debug.Log("------------========= " + courseTime);
//        }
//        return courseTime;
//    }
//    //코스맵 이름
//    string CourseOpenNameState(string _data)
//    {
//        string courseTime = "";
//        if (_data != "")
//        {
//            string[] openTime;
//            string[] _openTime;

//            char sp = '/';
//            openTime = _data.Split(sp);   //2021,12,10:00 따로 저장

//            //char bar = ':';
//            //_openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

//            courseTime = openTime[0];
//        }
//        return courseTime;
//    }

//    //아시아 맵 완주 - 노멀 하드
//    public void AsiaMapCourse_Finish(int _finishCount, int _finishAmount, GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn)
//    {
//        //Debug.Log("갯수 :: " + PlayerPrefs.GetInt("AsiaNormal1Finish"));
//        asiaMap_Count = _finishCount;// PlayerPrefs.GetInt("AsiaNormal1Finish");
//        //Debug.Log("_mapCourse " + _mapCourse + " asiaNormal1_Count " + asiaMap_Count + " ::: " + CourseOpenTimeState(_mapCourse));

//        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
//        if (CourseOpenTimeState(_mapCourse) == 0 || CourseOpenTimeState(_mapCourse) == 362439)
//        {
//            _slider.value = 0f;
//            _sliderText.text = "0/1";
//            _acceptBtn.interactable = false;
//            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//        }
//        //코스 1에 기록이 잇을 경우  - 완주를 했다.
//        else if(CourseOpenTimeState(_mapCourse) > 0 || CourseOpenTimeState(_mapCourse) != 362439)
//        {
//            if(asiaMap_Count == 0)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _acceptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if(asiaMap_Count == 1)
//            {
//                //완주 5회 미만-  미션 미완료
//                if(_finishAmount < 5)
//                {
//                    _slider.value = (float)_finishAmount / 5;
//                    _sliderText.text = _finishAmount + "/5";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if(_finishAmount >= 5)
//                {
//                    _slider.value = (float)_finishAmount / 5;
//                    _sliderText.text = _finishAmount + "/5";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if(asiaMap_Count == 2)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (_finishAmount < 10)
//                {
//                    _slider.value = (float)_finishAmount / 10;
//                    _sliderText.text = _finishAmount + "/10";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (_finishAmount >= 10)
//                {
//                    _slider.value = (float)_finishAmount / 10;
//                    _sliderText.text = _finishAmount + "/10";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if (asiaMap_Count == 3)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (_finishAmount < 15)
//                {
//                    _slider.value = (float)_finishAmount / 15;
//                    _sliderText.text = _finishAmount + "/15";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (_finishAmount >= 15)
//                {
//                    _slider.value = (float)_finishAmount / 15;
//                    _sliderText.text = _finishAmount + "/15";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if (asiaMap_Count == 4)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (_finishAmount < 20)
//                {
//                    _slider.value = (float)_finishAmount / 20;
//                    _sliderText.text = _finishAmount + "/20";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (_finishAmount >= 20)
//                {
//                    _slider.value = (float)_finishAmount / 20;
//                    _sliderText.text = _finishAmount + "/20";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if(asiaMap_Count >= 5)
//            {
//                _slider.value = 1;
//                _sliderText.text = "20/20";
//                _acceptBtn.interactable = false;
//                _mapObj.transform.GetChild(1).gameObject.SetActive(true);    //보상완료이미지 활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//        }
//    }
//    ////아시아 맵 완주 초기버전 - 사용하지 않음(참고용)
//    public void AsiaNormal2Course_Finish(GameObject _mapObj, string _mapCourse, Slider _slider, Text _sliderText, Button _acceptBtn)
//    {
//        asiaNormal2_Count = PlayerPrefs.GetInt("AsiaNormal2Finish");

//        //코스 1에 기록이 없을 경우 - 완주를 하지 않았다
//        if (CourseOpenTimeState(_mapCourse) == 0)
//        {
//            _slider.value = 0f;
//            _sliderText.text = "0/1";
//            _acceptBtn.interactable = false;
//            _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//        }
//        //코스 1에 기록이 잇을 경우  - 완주를 했다.
//        else if (CourseOpenTimeState(_mapCourse) > 0)
//        {
//            if (asiaNormal2_Count == 0)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _acceptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaNormal2_Count == 1)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 5)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 5;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/5";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 5)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 5;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/5";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if (asiaNormal2_Count == 2)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 10)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 10;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/10";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 10)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 10;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/10";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if (asiaNormal2_Count == 3)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 15)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 15;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/15";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") == 15)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 15;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/15";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//            else if (asiaNormal2_Count == 4)
//            {
//                //완주 5회 미만-  미션 미완료
//                if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") < 20)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 20;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/20";
//                    _acceptBtn.interactable = false;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//                }
//                //완주 5회 미션 성공
//                else if (PlayerPrefs.GetInt("AsiaNormal2FinishAmount") >= 20)
//                {
//                    _slider.value = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") / 20;
//                    _sliderText.text = PlayerPrefs.GetInt("AsiaNormal2FinishAmount") + "/20";
//                    _acceptBtn.interactable = true;
//                    _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//                }
//            }
//        }
//    }



//    //아시아 맵 시간제한 완주 - 노멀 하드
//    public void AsiaCourse_TimeLimitFinish(int _limitCount, string _finishTime, Text _titleText, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj, Sprite _takeSprite)
//    {
//        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
//        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);
//        Debug.Log("+++시간제한 " + asiaTimeLimit_Count + " PlayQuestState " + PlayerPrefs.GetString("PlayQuestState"));

//        if (asiaTimeLimit_Count == 0)   //5분
//        {
//            //Debug.Log("들어왓음");
//            //노멀코스1 완주 시간이 없거나 300초(5분)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "5:00";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//            }
//            //완주시간이 300초 안이면
//            else if(CourseOpenTimeState(_finishTime) <= maxTime && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") && 
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 362439))
//            {
//                _titleText.text = "5:00";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 1) // 4분40
//        {
//            //완주 시간이 없거나 280초(4분40)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 20
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "4:40";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//            }
//            //완주시간이 280초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 20 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "4:40";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 2)  //4:20
//        {
//            //완주 시간이 없거나 260초(4:20)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 40
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "4:20";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 260초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 40 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "4:20";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 3)  //4:00
//        {
//            //완주 시간이 없거나 240초(4:00)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 60
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "4:00";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 240초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "4:00";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 4)  //3:40
//        {
//            //완주 시간이 없거나 220초(3:40)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 80
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "3:40";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 220초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 80 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "3:40";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 5)  //3:20
//        {
//            //완주 시간이 없거나 200초(3:20)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 100
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "3:20";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 200초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 100 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "3:20";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 6)  //3:00
//        {
//            //완주 시간이 없거나 180초(3:00)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 120
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "3:00";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 180초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "3:00";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 7)  //2:40
//        {
//            //완주 시간이 없거나 160초(2:40)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 140
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "2:40";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 160초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 140 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "2:40";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 8)  //2:20
//        {
//            //완주 시간이 없거나 140초(2:20)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 160
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "2:20";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 140초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 160 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "2:20";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 9)  //2:00
//        {
//            //완주 시간이 없거나 120초(2:00)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 180
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "2:00";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 120초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "2:00";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if (asiaTimeLimit_Count == 10) //1:40
//        {
//            //완주 시간이 없거나 100초(1:40)이 넘었을 경우
//            if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 200
//                || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//            {
//                _titleText.text = "1:40";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//                //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//            }
//            //완주시간이 100초 안이면
//            else if (CourseOpenTimeState(_finishTime) <= maxTime - 200 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//                (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//            {
//                _titleText.text = "1:40";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true; //보상받기 버튼 비활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//            }
//        }
//        else if(asiaTimeLimit_Count >= 11)
//        {
//            _titleText.text = "1:40";
//            _slider.value = 1f;
//            _sliderText.text = "1/1";
//            _accptBtn.interactable = false; //보상받기 버튼 비활성화
//            _accptBtn.GetComponent<Image>().sprite = _takeSprite;
//            _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 활성화
//            _mapObj.transform.GetChild(1).gameObject.SetActive(true);  //바상받기완료 활성화
//        }
//        //else if (asiaTimeLimit_Count == 11) //1:20
//        //{
//        //    //완주 시간이 없거나 270초(1:20)이 넘었을 경우
//        //    if (CourseOpenTimeState(_finishTime) == 362439 || CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime - 220
//        //        || PlayerPrefs.GetString("Busan_PlayQuestState").Equals("No"))
//        //    {
//        //        _titleText.text = "1:20";
//        //        _slider.value = 0f;
//        //        _sliderText.text = "0/1";
//        //        _accptBtn.interactable = false; //보상받기 버튼 비활성화
//        //        _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//        //        //PlayerPrefs.SetString("Busan_PlayQuestState", "No");
//        //    }
//        //    //완주시간이 270초 안이면
//        //    else if (CourseOpenTimeState(_finishTime) <= maxTime - 220 && PlayerPrefs.GetString("Busan_PlayQuestState").Equals("Yes") &&
//        //        (CourseOpenTimeState(_finishTime) != 0 || CourseOpenTimeState(_finishTime) != 6039))
//        //    {
//        //        _titleText.text = "1:20";
//        //        _slider.value = 1f;
//        //        _sliderText.text = "1/1";
//        //        _accptBtn.interactable = true; //보상받기 버튼 비활성화
//        //        _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 비활성화
//        //    }
//        //}
//        //else if(asiaTimeLimit_Count >= 12) //끝
//        //{
//        //    Debug.Log("꿑난지않나");
//        //    _titleText.text = "1:20";
//        //    _slider.value = 1f;
//        //    _sliderText.text = "1/1";
//        //    _accptBtn.interactable = false; //보상받기 버튼 비활성화
//        //    _accptBtn.GetComponent<Image>().sprite = _takeSprite;
//        //    _mapObj.transform.GetChild(3).gameObject.SetActive(true);  //미션완료이미지 활성화
//        //    _mapObj.transform.GetChild(1).gameObject.SetActive(true);  //바상받기완료 활성화
//        //}

//    }
//    /////아시아 맵 시간제한 완주 - 노멀 하드(참고용)
//    public void AsiaCourse_TimeLimitFinish2(int _limitCount, string _finishTime, Text _titleText, Slider _slider, Text _sliderText, Button _accptBtn, GameObject _mapObj)
//    {
//        asiaTimeLimit_Count = _limitCount;// PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish1");
//        //Debug.Log("asiaTimeLimit_Count " + asiaTimeLimit_Count + " _finishTime " + _finishTime);

//        //노멀코스1 완주 시간이 없거나 600초(10분)이 넘었을 경우
//        if (CourseOpenTimeState(_finishTime) == 0 || CourseOpenTimeState(_finishTime) > maxTime)
//        {
//            _titleText.text = "10:00";
//            _slider.value = 0f;
//            _sliderText.text = "0/1";
//            _accptBtn.interactable = false; //보상받기 버튼 비활성화
//            _mapObj.transform.GetChild(3).gameObject.SetActive(false);  //미션완료이미지 비활성화
//        }
//        //노멀코스1 완주 시간이 600초(10분)보다 작거나, 570초(9분30분)크면 --- 10분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime && CourseOpenTimeState(_finishTime) > maxTime - 30)
//        {
//            if (asiaTimeLimit_Count == 1)
//            {
//                _titleText.text = "10:00";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;  //보상받기 버튼 활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);   //미션완료 이미지 활성화
//            }
//            else if (asiaTimeLimit_Count != 1)
//            {
//                _titleText.text = "9:30";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //9분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 30 && CourseOpenTimeState(_finishTime) > maxTime - 60)
//        {
//            if (asiaTimeLimit_Count == 2)
//            {
//                _titleText.text = "9:30";
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 2)
//            {
//                _titleText.text = "9:00";
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //9분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 60 && CourseOpenTimeState(_finishTime) > maxTime - 90)
//        {
//            if (asiaTimeLimit_Count == 3)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 3)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //8분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 90 && CourseOpenTimeState(_finishTime) > maxTime - 120)
//        {
//            if (asiaTimeLimit_Count == 4)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 4)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //8분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 120 && CourseOpenTimeState(_finishTime) > maxTime - 150)
//        {
//            if (asiaTimeLimit_Count == 5)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 5)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //7분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 150 && CourseOpenTimeState(_finishTime) > maxTime - 180)
//        {
//            if (asiaTimeLimit_Count == 6)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 6)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //7분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 180 && CourseOpenTimeState(_finishTime) > maxTime - 210)
//        {
//            if (asiaTimeLimit_Count == 7)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 7)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //6분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 210 && CourseOpenTimeState(_finishTime) > maxTime - 240)
//        {
//            if (asiaTimeLimit_Count == 8)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 8)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //6분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 240 && CourseOpenTimeState(_finishTime) > maxTime - 270)
//        {
//            if (asiaTimeLimit_Count == 9)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 9)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //5분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 270 && CourseOpenTimeState(_finishTime) > maxTime - 300)
//        {
//            if (asiaTimeLimit_Count == 10)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 10)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //5분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 300 && CourseOpenTimeState(_finishTime) > maxTime - 330)
//        {
//            if (asiaTimeLimit_Count == 11)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 11)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //4분30분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 330 && CourseOpenTimeState(_finishTime) > maxTime - 360)
//        {
//            if (asiaTimeLimit_Count == 12)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count != 12)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//        }
//        //4분
//        else if (CourseOpenTimeState(_finishTime) <= maxTime - 360)// && CourseOpenTimeState(_finishTime) > maxTime - 390)
//        {
//            if (asiaTimeLimit_Count == 13)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = true;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//            else if (asiaTimeLimit_Count < 13)
//            {
//                _slider.value = 0f;
//                _sliderText.text = "0/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(3).gameObject.SetActive(false);
//            }
//            else if (asiaTimeLimit_Count > 13)
//            {
//                _slider.value = 1f;
//                _sliderText.text = "1/1";
//                _accptBtn.interactable = false;
//                _mapObj.transform.GetChild(1).gameObject.SetActive(true);   //보상받기 완료 이미지 활성화
//                _mapObj.transform.GetChild(3).gameObject.SetActive(true);
//            }
//        }
//    }


//    //아시아 맵 한번씩 완주
//    public void AsiaMap_AllOneFinish(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        int sliderValue1 =  0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
//        int total = 0;

//        if(PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 1)
//            sliderValue1 = 0;
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 1)
//            sliderValue1 = 1;

//        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 1)
//            sliderValue2 = 0;
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 1)
//            sliderValue2 = 1;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 1)
//            sliderValue4 = 0;
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 1)
//            sliderValue4 = 1;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 1)
//            sliderValue5 = 0;
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 1)
//            sliderValue5 = 1;

//        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

//        if(total < 4)
//        {
//            _slider.value = (float)total / 4;
//            _sliderText.text = total + "/4";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if (total == 4)
//        {
//            if(PlayerPrefs.GetString("Busan_AllOneFinish") == "MissionOk")
//            {
//                _slider.value = (float)4 / 4;
//                _sliderText.text = "4/4";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = (float)4 / 4;
//                _sliderText.text = "4/4";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }

//    public void AsiaMap_AllTenFinish(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        int sliderValue1 = 0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
//        int total = 0;

//        if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 10)
//            sliderValue1 = PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 10)
//            sliderValue1 = 10;

//        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 10)
//            sliderValue2 = PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 10)
//            sliderValue2 = 10;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 10)
//            sliderValue4 = PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 10)
//            sliderValue4 = 10;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 10)
//            sliderValue5 = PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 10)
//            sliderValue5 = 10;

//        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

//        if(total < 40)
//        {
//            _slider.value = (float)total / 40;
//            _sliderText.text = total + "/40";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if(total == 40)
//        {
//            if (PlayerPrefs.GetString("Busan_AllTenFinish") == "MissionOk")
//            {
//                _slider.value = (float)40 / 40;
//                _sliderText.text = "40/40";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = (float)40 / 40;
//                _sliderText.text = "40/40";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }

//    public void AsiaMap_AllTwentyFinish(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        int sliderValue1 = 0, sliderValue2 = 0, sliderValue4 = 0, sliderValue5 = 0;
//        int total = 0;

//        if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") < 20)
//            sliderValue1 = PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount") >= 20)
//            sliderValue1 = 20;

//        if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") < 20)
//            sliderValue2 = PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount") >= 20)
//            sliderValue2 = 20;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") < 20)
//            sliderValue4 = PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount") >= 20)
//            sliderValue4 = 20;

//        if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") < 20)
//            sliderValue5 = PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount");
//        else if (PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount") >= 20)
//            sliderValue5 = 20;

//        total = sliderValue1 + sliderValue2 + sliderValue4 + sliderValue5;

//        if (total < 80)
//        {
//            _slider.value = (float)total / 80;
//            _sliderText.text = total + "/80";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if (total == 80)
//        {
//            if (PlayerPrefs.GetString("Busan_AllTenFinish") == "MissionOk")
//            {
//                _slider.value = (float)80 / 80;
//                _sliderText.text = "80/80";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = (float)80 / 80;
//                _sliderText.text = "80/80";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }

//    public void AsiaMap500KmPass(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        if(PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 500f)
//        {
//            _slider.value = 0;
//            _sliderText.text = "0/1";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 500f)
//        {
//            if(PlayerPrefs.GetString("Busan_Distance500Km") == "MissionOk")
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }

//    public void AsiaMap1000KmPass(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 1000f)
//        {
//            _slider.value = 0;
//            _sliderText.text = "0/1";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 1000f)
//        {
//            if (PlayerPrefs.GetString("Busan_Distance1000Km") == "MissionOk")
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }

//    public void AsiaMap1500KmPass(GameObject _obj, Slider _slider, Text _sliderText)
//    {
//        if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") < 1500f)
//        {
//            _slider.value = 0;
//            _sliderText.text = "0/1";
//            _obj.transform.GetChild(3).gameObject.SetActive(false);
//            _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//        }
//        else if (PlayerPrefs.GetFloat("Busan_Record_TotalKm") >= 1500f)
//        {
//            if (PlayerPrefs.GetString("Busan_Distance1500Km") == "MissionOk")
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(1).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = false;
//            }
//            else
//            {
//                _slider.value = 1;
//                _sliderText.text = "1/1";
//                _obj.transform.GetChild(3).gameObject.SetActive(true);
//                _obj.transform.GetChild(7).GetComponent<Button>().interactable = true;
//            }
//        }
//    }



//    public void ConnectButtonOn()
//    {
//        GameObject sensor = GameObject.Find("SensorManager");
//        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

//        sensor_Script.StartProcess();
//    }

//    //게임 종료
//    public void GameEndButtonOn()
//    {
//        Application.Quit(); //게임 종료
//    }

//}
