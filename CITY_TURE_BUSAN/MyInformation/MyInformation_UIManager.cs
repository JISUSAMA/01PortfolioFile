using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class MyInformation_UIManager : MonoBehaviour
{
    public ToggleGroup topTapToggleGroup;   //상단 탭 토글
    public ToggleGroup profileToggleGroup;  //프로필토글 그룹
    public GameObject informationPanel; //내 정보 판넬
    public GameObject rankingPanel; //랭킹 판넬
    public GameObject allRankPanel; //전체 점수 결과 판넬
    public GameObject oneRankPanel; //한개 점수 검색 결과 판넬
    public Image profileImg;    //프로필 이미지
    public InputField nick_Field;   //닉네임입력
    public Text overlapText;    //닉네임 에러 텍스트
    public GameObject nickNameChangePopup;    //닉네임 변경 팝업
    public GameObject errorPopup;   //알림 팝업

    [Header("내 정보")]
    public Text levelText;
    public Text currExpText;
    public Text maxExpText;
    public Text nickNameText;
    public Slider expSlider;
    public Text expText;
    public Text coinText;
    public Text noticText;

    [Header("내 기록")]
    public Text todayKcalText;
    public Text todayKmText;
    public Text totalKmText;
    public Text maxSpeedText;

    [Header("장착한 아이템 이미지")]
    public Image glovesImg;
    public Image helmatImg;
    public Image jacketImg;
    public Image pantsImg;
    public Image shoesImg;
    public Image hairImg;

    int currExp_i, maxExp_i, level_i;
    int profile_s;


    GameObject sensorManager;
    GameObject itemDataBase;
    Transform womanplayer;
    Transform manplayer;


    [Header("My TopRanking")]
    public Text BestRankText;
    public Text BestTimeText;
    public Text BestAllTimeText;

    [Header("My ALLRanking")]
    public MyAllRank[] myAllRankDataOB; //코스 별 오브젝트 6개
    int bestRank = 99999;
    string rankerMode;
    string rankerCorse;


    public Toggle topTapToggleCurrentSelection
    {
        get { return topTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle profileToggleCurrentSelection
    {
        get { return profileToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    private void Awake()
    {
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            womanplayer = GameObject.Find("Woman").GetComponent<Transform>();
            //Debug.Log("position" + womanplayer.localPosition);
            //Debug.Log("Rotation" + womanplayer.localRotation);
            //Debug.Log("Scale" + womanplayer.localScale);
        } 
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
            manplayer = GameObject.Find("Man").GetComponent<Transform>();

        sensorManager = GameObject.Find("SensorManager");
        itemDataBase = GameObject.Find("DataManager");


        MyAllRankingList();
        Initialization();   //초기화
    }

    void Start()
    {
        
    }

    void Initialization()
    {
        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            womanplayer.localPosition = new Vector3(-5.33f, 7.57f, 7.26f);
            womanplayer.localRotation = Quaternion.Euler(0, 109.924f, 0);
            womanplayer.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
            manplayer.localPosition = new Vector3(-5.33f, 7.57f, 7.26f);
            manplayer.localRotation = Quaternion.Euler(0, 109.924f, 0);
            manplayer.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }


        MyInformationInit();    //내 정보 초기화

        //랭킹판넬 비활성화
        Panel_And_Model_ActiveShow(false);

        //랭킹판넬 중 전체 랭킹판 활성화 - 검색랭킹 비활성화
        AllRank_And_OnRank_ActiveShow(true, false);
    }

    void MyInformationInit()
    {
        profile_s = PlayerPrefs.GetInt("Busan_Player_Profile");
        profileImg.sprite = Resources.Load<Sprite>("Profile/" + profile_s);

        level_i = PlayerPrefs.GetInt("Busan_Player_Level");
        currExp_i = PlayerPrefs.GetInt("Busan_Player_CurrExp");
        maxExp_i = level_i * (level_i + 1) * 25;

        expText.text = currExp_i.ToString();
        coinText.text = MyInformation_DataManager.instance.CommaText(MyInformation_DataManager.instance.coin_i);

        nickNameText.text = PlayerPrefs.GetString("Busan_Player_NickName");
        levelText.text = level_i.ToString();
        currExpText.text = currExp_i.ToString();
        maxExpText.text = maxExp_i.ToString() + " EXP";
        expSlider.value = (float)currExp_i / (float)maxExp_i;

        //내 기록
        todayKcalText.text = PlayerPrefs.GetFloat("Busan_Record_TodayKcal").ToString() + " kcal";
        todayKmText.text = PlayerPrefs.GetFloat("Busan_Record_TodayKm").ToString() + " km";
        totalKmText.text = PlayerPrefs.GetFloat("Busan_Record_TotalKm").ToString() + " km";
        maxSpeedText.text = PlayerPrefs.GetFloat("Busan_Record_MaxSpeed").ToString() + " m/s";

        //장착한 이미지 220804수정
        //if(PlayerPrefs.GetString("Busan_Wear_GlovesKind") != "No")
        //{

        //    glovesImg.transform.GetChild(0).gameObject.SetActive(true);
        //    Image gloves = glovesImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    gloves.sprite = Resources.Load<Sprite>("Install Item/Gloves/" + PlayerPrefs.GetString("Busan_Wear_GlovesStyleName"));
        //}
        //else
        //{
        //    glovesImg.transform.GetChild(0).gameObject.SetActive(false);
        //}

        //if (PlayerPrefs.GetString("Busan_Wear_HelmetKind") != "No")
        //{
        //    helmatImg.transform.GetChild(0).gameObject.SetActive(true);
        //    Image helmet = helmatImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    helmatImg.sprite = Resources.Load<Sprite>("Install Item/Helmet/" + PlayerPrefs.GetString("Busan_Wear_HelmetStyleName"));
        //}
        //else
        //{
        //    helmatImg.transform.GetChild(0).gameObject.SetActive(false);
        //}
        //pantsImg.transform.GetChild(0).gameObject.SetActive(true);
        //Image pants = pantsImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //pants.sprite = Resources.Load<Sprite>("Install Item/Pants/" + PlayerPrefs.GetString("Busan_Wear_PantsStyleName"));
        //shoesImg.transform.GetChild(0).gameObject.SetActive(true);
        //Image shoes = shoesImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //shoes.sprite = Resources.Load<Sprite>("Install Item/Shoes/" + PlayerPrefs.GetString("Busan_Wear_ShoesStyleName"));

        //220804수정
        glovesImg.transform.GetChild(0).gameObject.SetActive(true);
        Image gloves = glovesImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        gloves.sprite = Resources.Load<Sprite>("Install Item/Gloves/Gloves1");

        hairImg.transform.GetChild(0).gameObject.SetActive(true);
        Image helmet = hairImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        helmet.sprite = Resources.Load<Sprite>("Install Item/Helmet/Helmet1");

        pantsImg.transform.GetChild(0).gameObject.SetActive(true);
        Image pants = pantsImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        pants.sprite = Resources.Load<Sprite>("Install Item/Pants/Short1");

        shoesImg.transform.GetChild(0).gameObject.SetActive(true);
        Image shoes = shoesImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        shoes.sprite = Resources.Load<Sprite>("Install Item/Shoes/Shoes1");

        jacketImg.transform.GetChild(0).gameObject.SetActive(true);
        Image jacket = jacketImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        jacket.sprite = Resources.Load<Sprite>("Install Item/Jacket/LongShirt1");

        //220804수정
        //if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        //{

        //    //jacketImg.transform.GetChild(0).gameObject.SetActive(true);
        //    //Image jacket = jacketImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    //if (PlayerPrefs.GetString("Busan_Wear_JacketStyleName") == "BasicNasi")
        //    //    jacket.sprite = Resources.Load<Sprite>("Install Item/Jacket/Woman/" + PlayerPrefs.GetString("Busan_Wear_JacketStyleName"));
        //    //else
        //    //    jacket.sprite = Resources.Load<Sprite>("Install Item/Jacket/" + PlayerPrefs.GetString("Busan_Wear_JacketStyleName"));

        //    //hairImg.transform.GetChild(0).gameObject.SetActive(true);
        //    //Image hair = hairImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    //hair.sprite = Resources.Load<Sprite>("Install Item/Hair/Woman/" + PlayerPrefs.GetString("Busan_Wear_HairStyleName"));
        //}
        //else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        //{
        //    //220804수정
        //    //jacketImg.transform.GetChild(0).gameObject.SetActive(true);
        //    //Image jacket = jacketImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    //if (PlayerPrefs.GetString("Busan_Wear_JacketStyleName") == "BasicNasi")
        //    //    jacket.sprite = Resources.Load<Sprite>("Install Item/Jacket/Man/" + PlayerPrefs.GetString("Busan_Wear_JacketStyleName"));
        //    //else
        //    //    jacket.sprite = Resources.Load<Sprite>("Install Item/Jacket/" + PlayerPrefs.GetString("Busan_Wear_JacketStyleName"));

        //    //hairImg.transform.GetChild(0).gameObject.SetActive(true);
        //    //Image hair = hairImg.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //    //hair.sprite = Resources.Load<Sprite>("Install Item/Hair/Man/" + PlayerPrefs.GetString("Busan_Wear_HairStyleName"));
        //}
    }


    //큰 탭 토글 선택 시 이벤트 함수
    public void TopTapToggleChoice()
    {
        if(topTapToggleGroup.ActiveToggles().Any())
        {
            //내 정보 토글
            if(topTapToggleCurrentSelection.name == "InformationToggle")
            {
                //랭킹 비활성화
                Panel_And_Model_ActiveShow(false);
            }
            //랭킹 토글
            else if(topTapToggleCurrentSelection.name == "RankingToggle")
            {
                //랭킹 활성화
                Panel_And_Model_ActiveShow(true);
                AllRank_And_OnRank_ActiveShow(true, false);
            }
        }
    }

    //판넬 활성화-비활성화 함수
    void Panel_And_Model_ActiveShow(bool _ranking)
    {
        rankingPanel.SetActive(_ranking);   //랭킹
    }

    public void MyRankOneSearchButtonOn()
    {
        //랭킹판넬 중 하나 검색 판넬 활성화-전체 판넬 비활성화
        AllRank_And_OnRank_ActiveShow(false, true);
    }

    void AllRank_And_OnRank_ActiveShow(bool _all, bool _one)
    {
        allRankPanel.SetActive(_all);
        oneRankPanel.SetActive(_one);
    }

    //프로필 이미지 토글 이벤트
    public void ProfileImageChoice()
    {
        if(profileToggleGroup.ActiveToggles().Any())
        {
            if(profileToggleCurrentSelection.name == "Profile1")
            {
                profile_s = 1;
            }
            else if (profileToggleCurrentSelection.name == "Profile2")
            {
                profile_s = 2;
            }
            else if (profileToggleCurrentSelection.name == "Profile3")
            {
                profile_s = 3;
            }
            else if (profileToggleCurrentSelection.name == "Profile4")
            {
                profile_s = 4;
            }
            else if (profileToggleCurrentSelection.name == "Profile5")
            {
                profile_s = 5;
            }
            else if (profileToggleCurrentSelection.name == "Profile6")
            {
                profile_s = 6;
            }
            else if (profileToggleCurrentSelection.name == "Profile7")
            {
                profile_s = 7;
            }
            else if (profileToggleCurrentSelection.name == "Profile8")
            {
                profile_s = 8;
            }
            else if (profileToggleCurrentSelection.name == "Profile9")
            {
                profile_s = 9;
            }
            else if (profileToggleCurrentSelection.name == "Profile10")
            {
                profile_s = 10;
            }
            else if (profileToggleCurrentSelection.name == "Profile11")
            {
                profile_s = 11;
            }
            else if (profileToggleCurrentSelection.name == "Profile12")
            {
                profile_s = 12;
            }
        }
    }

    //프로필 변경 확인버튼 
    public void ProfileButtonOn()
    {
        profileImg.sprite = Resources.Load<Sprite>("Profile/" + profile_s);
        PlayerPrefs.SetInt("Busan_Player_Profile", profile_s);

        if(PlayerPrefs.GetString("Busan_ProfileChange").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_ProfileChange", "MissionOk");  //프로필 변경 여부
        else
            PlayerPrefs.SetString("Busan_ProfileChange", "Yes");  //프로필 변경 여부

        ServerManager.Instance.UserProfile_Update();
    }


    //닉네임이름 변경 버튼 클릭
    public void NickNameChangeButtonOn()
    {
        StartCoroutine(_NickNameChangeButtonOn());
    }

    IEnumerator _NickNameChangeButtonOn()
    {
        MyInformation_DataManager.instance.NickName_OverLap_Check(nick_Field.text, overlapText);

        yield return new WaitUntil(() => MyInformation_DataManager.instance.isNickNameCheckProcessEnd);

        MyInformation_DataManager.instance.isNickNameCheckProcessEnd = false;

        string str = coinText.text;
        str = str.Replace(",", ""); //코인에서 , 없애기
        int coin = int.Parse(str);

        //남은 돈이 1000이상일 경우 - 닉네임을 변경할수 있엇욧!
        if (MyInformation_DataManager.instance.SetCoinSub(1000) == true)
        {
            if (overlapText.text == "닉네임 성공" || overlapText.text.Equals("Nickname is successful"))
            {
                coin -= 1000;
                coinText.text = MyInformation_DataManager.instance.CommaText(coin);//.ToString();
                PlayerPrefs.SetString("Busan_Player_NickName", nick_Field.text);
                PlayerPrefs.SetInt("Busan_Player_Gold", coin);
                nickNameText.text = PlayerPrefs.GetString("Busan_Player_NickName");
                nickNameChangePopup.SetActive(false);
            }
        }
        else
        {
            errorPopup.SetActive(true);
            if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                noticText.text = "코인이 부족합니다." + "\n" + "코인을 획득하신 후 다시 이용하시길 바랍니다.";
            else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                noticText.text = "There are not enough coins." + "\n" + "Please use it again after you get the coin.";
        }
    }

    //내 랭킹 불러오기 
    public void MyAllRankingList()
    {
        StartCoroutine(_MyAllRankingList());
    }
    IEnumerator _MyAllRankingList()
    {
        ServerManager.Instance.Get_MyRankingRecordInfo("Asia");
        yield return new WaitUntil(() => ServerManager.Instance.isMyRankingStackUp);
        ServerManager.Instance.isMyRankingStackUp = false;
        int rankDataPos = 0;

        // Mode : 1,2  , Corse : 1,2,3
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)
        {
            //기록이 있을 경우,
            if (!ServerManager.Instance.myRank[i].best_time.Equals(""))
            {
                //Debug.Log(ServerManager.Instance.myRank[i].best_time);
                myAllRankDataOB[rankDataPos].SetMyAllRankingData(ServerManager.Instance.myRank[i].ranking, "Asia",
                    ServerManager.Instance.myRank[i].mode, ServerManager.Instance.myRank[i].corse,
                    ServerManager.Instance.myRank[i].bike, ServerManager.Instance.myRank[i].best_time);
                rankDataPos += 1;
            }
        }
        //없는 빈공간에는 아무 데이터가 들어가지 않도록 해줌
        for (int j = 5; j >= ServerManager.Instance.myRank.Count; j--)
        {
            myAllRankDataOB[j].SetMyAllRankingData("", "", "", "", "", "");
        }
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)
        {
            string rankText_s = myAllRankDataOB[i].RankText.GetComponent<Text>().text.ToString();
            string rankTime_s = myAllRankDataOB[i].TimeText.GetComponent<Text>().text.ToString();
            string rankMap_s = myAllRankDataOB[i].MapText.GetComponent<Text>().text.ToString();
            string rankMode_s = myAllRankDataOB[i].Mode.GetComponent<Image>().sprite.name.ToString();
            string rankCorse_s = myAllRankDataOB[i].CorseText.GetComponent<Text>().text.ToString();
            //Debug.Log(rankText_s + " " + rankTime_s + " " + rankMap_s + " " + rankMode_s + " " + rankCorse_s);
            //    Debug.Log(BestRankText.text + " " + BestTimeText.text + " " + BestMapText.text + " " + compareMode + " " + compareCorse);
            if (bestRank.Equals(99999))
            {
                if (int.Parse(rankText_s) < bestRank)
                {
                    bestRank = int.Parse(rankText_s);
                    BestRankText.text = bestRank.ToString();
                    BestTimeText.text = rankTime_s;
                    rankerMode = rankMode_s;
                    rankerCorse = rankCorse_s;
                    if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                    else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                }
            }
            else
            {
                //랭킹이 다른 경우,
                if (int.Parse(rankText_s) < bestRank)
                {
                    bestRank = int.Parse(rankText_s);
                    BestRankText.text = bestRank.ToString();
                    BestTimeText.text = rankTime_s;
                    rankerMode = rankMode_s;
                    rankerCorse = rankCorse_s;
                    if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                    else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                }
                //랭킹이 같은 경우,
                else if (int.Parse(rankText_s) == bestRank)
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    //형식 - hh:mm:ss.ff
                    string[] bestTimeSplit_str = BestTimeText.text.Split('.', ':');
                    DateTime bestValue = new DateTime(2021, 06, 24,
                        Int32.Parse(bestTimeSplit_str[0]), Int32.Parse(bestTimeSplit_str[1]), Int32.Parse(bestTimeSplit_str[2]), Int32.Parse(bestTimeSplit_str[3]));
                    string bestJoin_str = bestValue.ToString("m:s.ff");
                    int best_ms = int.Parse(bestTimeSplit_str[3]);
                    string[] compareTimeSplit_str = rankTime_s.Split('.', ':');
                    DateTime compareValue = new DateTime(2021, 06, 24,
                        Int32.Parse(compareTimeSplit_str[0]), Int32.Parse(compareTimeSplit_str[1]), Int32.Parse(compareTimeSplit_str[2]), Int32.Parse(compareTimeSplit_str[3]));
                    string compareJoin_str = compareValue.ToString("m:s.ff");
                    int compare_ms = int.Parse(compareTimeSplit_str[3]);
                    DateTime bestDataTime = bestValue;
                    DateTime compareDataTime = compareValue;
                    //DateTime bestDataTime = Convert.ToDateTime(bestJoin_str);
                    //DateTime compareDataTime = Convert.ToDateTime(compareJoin_str);

                    ////////////////////////////////////////////////////////////////////////////////////
                    //같은 모드일 경우,
                    if (rankerMode.Equals(rankMode_s))
                    {
                        //도착한 시간이 같은 경우,
                        //Debug.LogError(bestDataTime + "" + compareDataTime);
                        if (bestDataTime == compareDataTime)
                        {
                            if (compare_ms == best_ms)
                            {
                                if (int.Parse(rankerCorse) < int.Parse(rankCorse_s))
                                {
                                    bestRank = int.Parse(rankText_s);
                                    BestRankText.text = bestRank.ToString();
                                    BestTimeText.text = rankTime_s;
                                    rankerMode = rankMode_s;
                                    rankerCorse = rankCorse_s;
                                    if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                                    else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                                }
                            }
                            else if (compare_ms < best_ms)
                            {
                                bestRank = int.Parse(rankText_s);
                                BestRankText.text = bestRank.ToString();
                                BestTimeText.text = rankTime_s;
                                rankerMode = rankMode_s;
                                rankerCorse = rankCorse_s;
                                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                            }
                        }
                        //비교하는 대상이 최고 점수의 도착한 시간 보다 짧은 경우,
                        else if (compareDataTime < bestDataTime)
                        {
                            bestRank = int.Parse(rankText_s);
                            BestRankText.text = bestRank.ToString();
                            BestTimeText.text = rankTime_s;
                            rankerMode = rankMode_s;
                            rankerCorse = rankCorse_s;
                            if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                            else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                        }
                    }
                    //두개의 모드가 다른 경우,
                    else
                    {
                        //1. 현재 Ranker 가 하드 인 경우, 시간을 비교해서 짧은 사람이 Ranker, 동점일 경우 현재 Ranker유지
                        //Debug.Log("-------------------7");
                        if (rankerMode.Equals("(H)"))
                        {
                            //도착한 시간이 같은 경우,
                            Debug.LogError(bestDataTime + "" + compareDataTime);
                            if (compareDataTime < bestDataTime)
                            {
                                bestRank = int.Parse(rankText_s);
                                BestRankText.text = bestRank.ToString();
                                BestTimeText.text = rankTime_s;
                                rankerMode = rankMode_s;
                                rankerCorse = rankCorse_s;
                                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                            }
                        }
                        //2. 현재 Ranker 가 노말 인 경우, 시간을 비교해서 짧은 사람이 Ranker, 동점일 경우 Ranker 변경
                        else
                        {
                            if (compareDataTime < bestDataTime)
                            {
                                bestRank = int.Parse(rankText_s);
                                BestRankText.text = bestRank.ToString();
                                BestTimeText.text = rankTime_s;
                                rankerMode = rankMode_s;
                                rankerCorse = rankCorse_s;
                                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                    BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                            }
                            else if (compareDataTime == bestDataTime)
                            {
                                if (compare_ms < best_ms)
                                {

                                    bestRank = int.Parse(rankText_s);
                                    BestRankText.text = bestRank.ToString();
                                    BestTimeText.text = rankTime_s;
                                    rankerMode = rankMode_s;
                                    rankerCorse = rankCorse_s;
                                    if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "코스 " + rankMode_s;
                                    else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                                        BestAllTimeText.text = rankMap_s + " " + rankCorse_s + "COURSE " + rankMode_s;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    //백버튼 - 로비화면으로
    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("Lobby");
    }

    //BGM 사운드
    public void BGM_SliderSound()
    {
        SoundMaixerManager.instance.AudioControl();
    }

    //효과음 사운드
    public void Effect_SliderSound()
    {
        SoundMaixerManager.instance.SFXAudioControl();
    }


}
