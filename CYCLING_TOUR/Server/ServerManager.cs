using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class Notice
{
    public uint index;
    public string title;
    public string contents;
    public int reading_State;
    public int delete_State;

    public Notice() { /*초기화*/ }

    // 순서대로 저장
    public Notice(uint _index, string _title, string _contents, int _reading_State, int _delete_State)
    {
        index = _index;
        title = _title;
        contents = _contents;
        reading_State = _reading_State;
        delete_State = _delete_State;
    }
}

[System.Serializable]
public class Company_INFO
{
    public string email;
    public string email_PW;

    public Company_INFO() { /*초기화*/ }

    // 순서대로 저장
    public Company_INFO(string _email, string _email_PW)
    {
        email = _email;
        email_PW = _email_PW;
    }
}

[System.Serializable]
public class UserInfo
{
    public string player_UID;
    public string player_ID;
    public string player_PW;
    public string player_NickName;
    public string player_LoginState;
    public int player_Profile;

    public UserInfo() { /*초기화*/ }

    // 순서대로 저장
    public UserInfo(string _player_UID, string _player_ID, string _player_PW, string _player_NickName, string _player_LoginState, int _player_Profile)
    {
        player_UID = _player_UID;
        player_ID = _player_ID;
        player_PW = _player_PW;
        player_NickName = _player_NickName;
        player_LoginState = _player_LoginState;
        player_Profile = _player_Profile;
    }
}

[System.Serializable]
public class CharacterInfo
{
    public string player_Sex;
    public string player_Face;
    public string player_SkinColor;
    public string player_Hair;
    public string player_HairColor;

    public CharacterInfo() { /*초기화*/ }

    // 순서대로 저장
    public CharacterInfo(string _player_Sex, string _player_Face
        , string _player_SkinColor, string _player_Hair, string _player_HairColor)
    {
        player_Sex = _player_Sex;
        player_Face = _player_Face;
        player_SkinColor = _player_SkinColor;
        player_Hair = _player_Hair;
        player_HairColor = _player_HairColor;
    }
}
[System.Serializable]
public class MapProgress
{
    public string mapProgress;

    public MapProgress() { /*초기화*/ }

    // 순서대로 저장
    public MapProgress(string _mapProgress)
    {
        mapProgress = _mapProgress;
    }
}

[System.Serializable]
public class MyTodayQuest
{
    public string quest_Category;
    public int quest_Idx;
    public string quest_State;

    public MyTodayQuest() { /*초기화*/ }

    // 순서대로 저장
    public MyTodayQuest(string _quest_Category, int _quest_Idx, string _quest_State)
    {
        quest_Category = _quest_Category;
        quest_Idx = _quest_Idx;
        quest_State = _quest_State;
    }
}

// 아시아 맵 레코드
[System.Serializable]
public class RecordByAsiaCourse
{
    public string asiaNormal_1C;
    public string asiaNormal_2C;
    public string asiaNormal_3C;
    public string asiaHard_1C;
    public string asiaHard_2C;
    public string asiaHard_3C;

    public RecordByAsiaCourse() { /*초기화*/ }

    public RecordByAsiaCourse(string _asiaNormal_1C, string _asiaNormal_2C, string _asiaNormal_3C, string _asiaHard_1C, string _asiaHard_2C, string _asiaHard_3C)
    {
        asiaNormal_1C = _asiaNormal_1C;
        asiaNormal_2C = _asiaNormal_2C;
        asiaNormal_3C = _asiaNormal_3C;
        asiaHard_1C = _asiaHard_1C;
        asiaHard_2C = _asiaHard_2C;
        asiaHard_3C = _asiaHard_3C;
    }
}

[System.Serializable]
public class MyMapQuest
{
    public string quest_Category;
    public int quest_CompletedState;
    public int quest_Progress;

    public MyMapQuest() { /*초기화*/ }

    // 순서대로 저장
    public MyMapQuest(string _quest_Category, int _quest_CompletedState, int _quest_Progress)
    {
        quest_Category = _quest_Category;
        quest_CompletedState = _quest_CompletedState;
        quest_Progress = _quest_Progress;
    }
}
[System.Serializable]
public class ConnectedStateCheck
{
    public string oneConnectDay;
    public string twoConnectDay;
    public string oneCheck;
    public string twoCheck;
    public string connectTime;

    public ConnectedStateCheck() { /*초기화*/ }

    // 순서대로 저장
    public ConnectedStateCheck(string _oneConnectDay, string _twoConnectDay, string _oneCheck, string _twoCheck, string _connectTime)
    {
        oneConnectDay = _oneConnectDay;
        twoConnectDay = _twoConnectDay;
        oneCheck = _oneCheck;
        twoCheck = _twoCheck;
        connectTime = _connectTime;
    }
}
[System.Serializable]
public class QuestReward
{
    public string reward_Title;
    public string reward_State;

    public QuestReward() { /*초기화*/ }

    // 순서대로 저장
    public QuestReward(string _reward_Title, string _reward_State)
    {
        reward_Title = _reward_Title;
        reward_State = _reward_State;
    }
}

[System.Serializable]
public class Level_and_Gold
{
    public int player_Gold;
    public int player_Level;
    public int player_CurrEXP;
    public int player_TakeEXP;

    public Level_and_Gold() { /*초기화*/ }

    // 순서대로 저장
    public Level_and_Gold(int _player_Gold, int _player_Level, int _player_CurrEXP, int _player_TakeEXP)
    {
        player_Gold = _player_Gold;
        player_Level = _player_Level;
        player_CurrEXP = _player_CurrEXP;
        player_TakeEXP = _player_TakeEXP;
    }
}
[System.Serializable]
public class Record
{
    public float record_MaxSpeed;
    public float record_TodayKal;
    public float record_TodayKm;
    public float record_TodayMaxSpeed;
    public float record_TotalKm;

    public Record() { /*초기화*/ }

    // 순서대로 저장
    public Record(float _record_MaxSpeed, float _record_TodayKal, float _record_TodayKm, float _record_TodayMaxSpeed, float _record_TotalKm)
    {
        record_MaxSpeed = _record_MaxSpeed;
        record_TodayKal = _record_TodayKal;
        record_TodayKm = _record_TodayKm;
        record_TodayMaxSpeed = _record_TodayMaxSpeed;
        record_TotalKm = _record_TotalKm;
    }
}

[System.Serializable]
public class Equipped_Item_List
{
    public string Wear_HairKind;
    public string Wear_GlovesKind;
    public string Wear_HelmetKind;
    public string Wear_JacketKind;
    public string Wear_PantsKind;
    public string Wear_ShoesKind;
    public string Wear_BicycleKind;

    public Equipped_Item_List() { /*초기화*/ }

    // 순서대로 저장
    public Equipped_Item_List(string _Wear_HairKind, string _Wear_GlovesKind, string _Wear_HelmetKind
        , string _Wear_JacketKind, string _Wear_PantsKind, string _Wear_ShoesKind, string _Wear_BicycleKind)
    {
        Wear_HairKind = _Wear_HairKind;
        Wear_GlovesKind = _Wear_GlovesKind;
        Wear_HelmetKind = _Wear_HelmetKind;
        Wear_JacketKind = _Wear_JacketKind;
        Wear_PantsKind = _Wear_PantsKind;
        Wear_ShoesKind = _Wear_ShoesKind;
        Wear_BicycleKind = _Wear_BicycleKind;
    }
}
[System.Serializable]
public class Equipped_ItemStyleName_List
{
    public string Wear_HairStyleName;
    public string Wear_GlovesStyleName;
    public string Wear_HelmetStyleName;
    public string Wear_JacketStyleName;
    public string Wear_PantsStyleName;
    public string Wear_ShoesStyleName;
    public string Wear_BicycleStyleName;

    public Equipped_ItemStyleName_List() { /*초기화*/ }

    // 순서대로 저장
    public Equipped_ItemStyleName_List(string _Wear_HairStyleName, string _Wear_GlovesStyleName,
        string _Wear_HelmetStyleName, string _Wear_JacketStyleName, string _Wear_PantsStyleName, string _Wear_ShoesStyleName, string _Wear_BicycleStyleName)
    {
        Wear_HairStyleName = _Wear_HairStyleName;
        Wear_GlovesStyleName = _Wear_GlovesStyleName;
        Wear_HelmetStyleName = _Wear_HelmetStyleName;
        Wear_JacketStyleName = _Wear_JacketStyleName;
        Wear_PantsStyleName = _Wear_PantsStyleName;
        Wear_ShoesStyleName = _Wear_ShoesStyleName;
        Wear_BicycleStyleName = _Wear_BicycleStyleName;
    }
}

[System.Serializable]
public class Equipped_Item_Number
{
    public int HairNumber;
    public int BodyNumber;
    public int GlovesNumber;
    public int HelmetNumber;
    public int JacketNumber;
    public int PantsNumber;
    public int ShoesNumber;
    public int BicycleNumber;

    public Equipped_Item_Number() { /*초기화*/ }

    // 순서대로 저장
    public Equipped_Item_Number(int _HairNumber, int _BodyNumber, int _GlovesNumber, int _HelmetNumber,
        int _JacketNumber, int _PantsNumber, int _ShoesNumber, int _BicycleNumber)
    {
        HairNumber = _HairNumber;
        BodyNumber = _BodyNumber;
        GlovesNumber = _GlovesNumber;
        HelmetNumber = _HelmetNumber;
        JacketNumber = _JacketNumber;
        PantsNumber = _PantsNumber;
        ShoesNumber = _ShoesNumber;
        BicycleNumber = _BicycleNumber;
    }
}

[System.Serializable]
public class Rank
{
    //public string player_ID;
    public int ranking; // 랭킹
    public string nickname;
    public string map;
    public string mode;
    public string corse;
    public string bike;
    public string best_time;

    public Rank() { /*초기화*/ }

    public Rank(/*string _player_ID,*/int _ranking, string _nickname, string _map, string _mode, string _corse, string _bike, string _best_time)
    {
        //player_ID = _player_ID;
        ranking = _ranking;
        nickname = _nickname;
        map = _map;
        mode = _mode;
        corse = _corse;
        bike = _bike;
        best_time = _best_time;
    }
}

[System.Serializable]
public class MyRank
{
    public string ranking;
    public string map;
    public string mode;
    public string corse;
    public string bike;
    public string best_time;

    public MyRank() { /*초기화*/ }

    public MyRank(string _ranking, string _map, string _mode, string _corse, string _bike, string _best_time)
    {
        ranking = _ranking;
        map = _map;
        mode = _mode;
        corse = _corse;
        bike = _bike;
        best_time = _best_time;
    }
}

[System.Serializable]
public class Setting
{
    public float bgm;
    public float sfx;

    public Setting() { /*초기화*/ }

    public Setting(float _bgm, float _sfx)
    {
        bgm = _bgm;
        sfx = _sfx;
    }
}

public class ServerManager : MonoBehaviour
{
    /// <summary>
    /// AWS, PHP, MariaDB, SimpleJSON Plugin
    /// AWS EC2 FitTag Server IP 3.36.135.181
    /// </summary>
    public static ServerManager Instance { get; private set; }

    [Header("User")]
    //public List<UserInfo> userInfo = new List<UserInfo>();  // 유저 정보
    public UserInfo userInfo;  // 유저 정보
    public CharacterInfo characterInfo;   // 캐릭터 정보

    [Header("Connected State")]
    public ConnectedStateCheck connectedStateCheck;

    [Header("Quest Reward")]
    public List<QuestReward> questReward;

    [Header("Today Quest")]
    public List<MyTodayQuest> myTodayQuest;   // 일일 퀘스트

    [Header("Map Progress")]
    public MapProgress mapProgress;       // 맵 퀘스트

    [Header("Map Quest")]
    public List<MyMapQuest> myMapQuest;       // 맵 퀘스트

    [Header("Level And Gold")]
    public Level_and_Gold level_And_golds;       // 레벨 및 재화

    [Header("Item")]
    public Equipped_Item_Number equipped_item_num; // 장착한 아이템 리스트 ( Number )
    public Equipped_Item_List equipped_item_list;    // 장착한 아이템 리스트 ( String )
    public Equipped_ItemStyleName_List equipped_item_stylename_list;    // 장착한 커스텀 스타일 리스트

    [Header("Record")]
    public Record records;   // 내 정보 기록

    [Header("Record By AsiaCourse")]
    public RecordByAsiaCourse recordByAsiaCourse;   // 내 아시아 경기기록
    
    [Header("Rank")]
    public List<Rank> Ranking = new List<Rank>();    // 랭킹존 리스트
    public List<MyRank> myRank = new List<MyRank>();    // 내 랭크 리스트

    [Header("Company Email")]
    public Company_INFO company_Email;

    [Header("Notice")]
    public List<Notice> notice;

    [Header("Setting")]
    public Setting setting;

    public Text serverLogtext;

    public bool isConnected = false;    // 서버에 연결되었나 확인
    public bool isConnCompleted = false;    // 서버 연결상태체크 완료 확인
    public bool isCharacterRegUpload = false;    // 서버 연결상태체크 완료 확인
    public bool isUserSearchCompleted = false;    // 서버 USER SEARCH SQL 후 완료 상태
    public bool isCharacterDataStackCompleted = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isStoreItemStackCompleted = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isExistNickName = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isNickNameSearchCompleted = false;  // 서버 닉네임 서치 SQL 완료 상태
    public bool isExistID = false;                  // 서버 ID 존재여부
    public bool isIDSearchCompleted = false;          // 서버 ID 존재여부 검색완료 상태체크
    public bool isBuyItemUpdateCompleted = false;          // 서버 아이템 업데이트 완료 상태체크
    public bool isNickNameChangeCompleted = false;          // 서버 닉네임변경 완료 상태체크
    public bool isCompanyEmailSearchCompleted = false;      // 서버 회사 이메일 들고오기 완료 상태체크
    public bool isRankingSearchCompleted = false;          // 서버 랭킹검색 완료 상태체크
    public bool isNoticeSearchCompleted = false;          // 서버 게시판 검색 완료 상태체크
    public bool isDeleteNoticeContentsCompleted = false;          // 서버 게시판 삭제 상태체크
    public bool isGetSettingCompleted = false;          // 세팅 상태 체크 완료
    public bool isRegSettingCompleted = false;          // 세팅 등록 완료
    public bool isTodayQuestStackCompleted = false;         // 일일 퀘스트 받기 완료
    public bool isMapQuestStackCompleted = false;         // 맵 퀘스트 받기 완료
    public bool isConnectedStateStackCompleted = false;         // 접속상태 받기 완료
    public bool isQuestRewardStackCompleted = false;         // 퀘스트보상 상태 받기 완료
    public bool isRegNoticeUpload = false;         // 퀘스트보상 상태 받기 완료
    public bool isRecordByAsiaCourseStackCompleted = false;         // 맵 당 경기기록 받기 완료
    public bool isUpdatedUserRecord = false;         // 맵 당 경기기록 받기 완료
    public bool isMyRankingStackUp = false;
    public bool isGetMapProgressCompleted = false;         // 맵 진척도 받기 완료
    public GameObject networkConnectStatePopUp;

    void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void Reg_Notice()
    {
        StartCoroutine(_Reg_Notice());
    }

    IEnumerator _Reg_Notice()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_Notice.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                isRegNoticeUpload = true;
            }
        }
    }

    public void Get_Notice()
    {
        StartCoroutine(_Get_Notice());
    }

    IEnumerator _Get_Notice()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_Notice.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                _Get_Notice(www.downloadHandler.text);
            }
        }
    }

    private void _Get_Notice(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            notice.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                notice.Add(new Notice(uint.Parse(Array[i]["INDEX"].Value), Array[i]["TITLE"].Value,
                                        Array[i]["CONTENTS"].Value, int.Parse(Array[i]["READING_STATE"].Value),
                                        int.Parse(Array[i]["DELETE_STATE"].Value)));
            }
        }
        else
        {
            notice.Clear();
            //Debug.Log("값이 존재하지 않습니다.");
        }

        isNoticeSearchCompleted = true;
    }

    // 공지사항 삭제 상태 업데이트
    public void Update_NoticeDeleteStateContents(uint _index)
    {
        StartCoroutine(_Update_NoticeDeleteStateContents(_index));
    }

    IEnumerator _Update_NoticeDeleteStateContents(uint _index)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Index", _index.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_NoticeDeleteState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                isDeleteNoticeContentsCompleted = true;
            }
        }
    }

    // 공지사항 읽은 상태 업데이트
    public void UpdateNoticeContentsReadingState(uint _index)
    {
        StartCoroutine(_UpdateNoticeContentsReadingState(_index));
    }

    IEnumerator _UpdateNoticeContentsReadingState(uint _index)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Index", _index.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_NoticeReadingState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // 첫 구글 로그인시 아이디 있는지 없는지 검사하기
    public void Search_USER_Info(string _userID)
    {
        StartCoroutine(_Search_USER_Info(_userID));
    }

    IEnumerator _Search_USER_Info(string _userID)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", _userID);
        //form.AddField("Player_ID", "1234");

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Search_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                GetSearched_USER_Info(www.downloadHandler.text);
            }
        }
    }

    // 아이디 검색해서 가져옴
    public void GetSearched_USER_Info(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                userInfo = new UserInfo(Array[i]["PLAYER_UID"].Value, Array[i]["PLAYER_ID"].Value,
                                        Array[i]["PLAYER_PW"].Value, Array[i]["PLAYER_NICKNAME"].Value,
                                        Array[i]["PLAYER_LOGINSTATE"].Value, int.Parse(Array[i]["PLAYER_PROFILE"].Value));
            }
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }

        //serverLogtext.text = userInfo.player_ID + " :: " + userInfo.player_UID + " :: " + userInfo.player_PW + " :: " + userInfo.player_NickName + " :: " + userInfo.player_LoginState;

        isUserSearchCompleted = true;
    }

    // 로그인 후 캐릭터 정보 가져오기
    public void GetCharacterInfo(string _userID)
    {
        StartCoroutine(_GetCharacterInfo(_userID));
    }

    IEnumerator _GetCharacterInfo(string _userID)
    {
        WWWForm form = new WWWForm();

        // 케릭터 정보를 들고와야하는데 들고와야할 필드 생각해보셈!! player ID 로 다 가져와야함
        //Debug.Log("_userID " + _userID);

        form.AddField("Player_ID", _userID);
        //form.AddField("Player_ID", "1234");

        // 여기부터 PHP 파일만들고 안에 SQL문 추가
        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Search_CharacterInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                GetSearched_CHARACTER_Info(www.downloadHandler.text);
            }
        }
    }

    public void GetSearched_CHARACTER_Info(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {

            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                //userInfo = new UserInfo(Array[i]["PLAYER_UID"].Value, Array[i]["PLAYER_ID"].Value,
                //                        Array[i]["PLAYER_PW"].Value, Array[i]["PLAYER_NICKNAME"].Value,
                //                        Array[i]["PLAYER_LOGINSTATE"].Value);
                characterInfo = new CharacterInfo(Array[i]["PLAYER_SEX"].Value, Array[i]["PLAYER_FACE"].Value,
                                                  Array[i]["PLAYER_SKINCOLOR"].Value, Array[i]["PLAYER_HAIR"].Value,
                                                  Array[i]["PLAYER_HAIRCOLOR"].Value);

                level_And_golds = new Level_and_Gold(Int32.Parse(Array[i]["PLAYER_GOLD"].Value), Int32.Parse(Array[i]["PLAYER_LEVEL"].Value),
                                                     Int32.Parse(Array[i]["PLAYER_CURREXP"].Value), Int32.Parse(Array[i]["PLAYER_TAKEEXP"].Value));

                records = new Record(float.Parse(Array[i]["RECORD_MAXSPEED"].Value), float.Parse(Array[i]["RECORD_TODAYKCAL"].Value), float.Parse(Array[i]["RECORD_TODAYKM"].Value),
                                     float.Parse(Array[i]["RECORD_TODAYMAXSPEED"].Value), float.Parse(Array[i]["RECORD_TOTALKM"].Value));

                equipped_item_list = new Equipped_Item_List(Array[i]["WEAR_HAIRKIND"].Value, Array[i]["WEAR_GLOVESKIND"].Value, Array[i]["WEAR_HELMETKIND"].Value,
                                                            Array[i]["WEAR_JACKETKIND"].Value, Array[i]["WEAR_PANTSKIND"].Value, Array[i]["WEAR_SHOESKIND"].Value,
                                                            Array[i]["WEAR_BICYCLEKIND"].Value);

                equipped_item_stylename_list = new Equipped_ItemStyleName_List(Array[i]["WEAR_HAIRSTYLENAME"].Value, Array[i]["WEAR_GLOVESSTYLENAME"].Value, Array[i]["WEAR_HELMETSTYLENAME"].Value,
                                                                               Array[i]["WEAR_JACKETSTYLENAME"].Value, Array[i]["WEAR_PANTSSTYLENAME"].Value, Array[i]["WEAR_SHOESSTYLENAME"].Value,
                                                                               Array[i]["WEAR_BICYCLESTYLENAME"].Value);

                equipped_item_num = new Equipped_Item_Number(Int32.Parse(Array[i]["HAIRNUMBER"].Value), Int32.Parse(Array[i]["BODYNUMBER"].Value),
                                                             Int32.Parse(Array[i]["GLOVESNUMBER"].Value), Int32.Parse(Array[i]["HELMETNUMBER"].Value),
                                                             Int32.Parse(Array[i]["JACKETNUMBER"].Value), Int32.Parse(Array[i]["PANTSNUMBER"].Value),
                                                             Int32.Parse(Array[i]["SHOESNUMBER"].Value), Int32.Parse(Array[i]["BICYCLENUMBER"].Value));

                recordByAsiaCourse = new RecordByAsiaCourse(Array[i]["N1"].Value, Array[i]["N2"].Value, Array[i]["N3"].Value,
                                            Array[i]["H1"].Value, Array[i]["H2"].Value, Array[i]["H3"].Value);

                //Debug.Log("ashdjflakjsldfjaldf ::" + Array[i]["N1"].Value);                
            }
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }
        

        //serverLogtext.text = characterInfo.player_Sex + " :: " + characterInfo.player_Hair + " :: " + characterInfo.player_HairColor + " :: " + characterInfo.player_Hair + " :: " + characterInfo.player_SkinColor;

        isCharacterDataStackCompleted = true;
    }
    public void Get_CompanyEmail()
    {
        StartCoroutine(_Get_CompanyEmail());
    }

    IEnumerator _Get_CompanyEmail()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_CompanyEmail.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                _Get_CompanyEmail(www.downloadHandler.text);
            }
        }
    }

    private void _Get_CompanyEmail(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                company_Email = new Company_INFO(Array[i]["COMPANY_EMAIL"].Value, Array[i]["COMPANY_PW"].Value);
            }
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }

        isCompanyEmailSearchCompleted = true;
    }

    // 아이디/PW 등록하기
    public void UserInfo_Reg()
    {
        StartCoroutine(_UserInfo_Reg());
    }

    IEnumerator _UserInfo_Reg()
    {
        WWWForm form = new WWWForm();

        //Debug.Log("??: " + PlayerPrefs.GetString("AT_Player_ID"));

        //form.AddField("Player_LoginState", PlayerPrefs.GetString("AT_Player_LoginState"));
        //form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        //form.AddField("Player_UID", PlayerPrefs.GetString("AT_Player_UID"));
        ////form.AddField("Player_NickName", PlayerPrefs.GetString("Player_NickName"));
        //form.AddField("Player_Password", PlayerPrefs.GetString("AT_Player_PassWord"));
        ////form.AddField("Player_Profile", PlayerPrefs.GetString("Player_Profile"));
        //form.AddField("Player_Profile", 1);

        form.AddField("Player_LoginState", PlayerPrefs.GetString("AT_Player_LoginState"));
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_UID", PlayerPrefs.GetString("AT_Player_UID"));
        //form.AddField("Player_NickName", PlayerPrefs.GetString("Player_NickName"));
        form.AddField("Player_Password", PlayerPrefs.GetString("AT_Player_PassWord"));
        //form.AddField("Player_Profile", PlayerPrefs.GetString("Player_Profile"));
        form.AddField("Player_Profile", 1);


        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
            }
        }
    }

    // ID 중복 체크
    public void ID_DoubleCheck(string _userID)
    {
        StartCoroutine(_ID_DoubleCheck(_userID));
    }

    IEnumerator _ID_DoubleCheck(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_ID_DoubleCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                Get_ID_DoubleCheckJSON(www.downloadHandler.text);
            }
        }
    }

    public void Get_ID_DoubleCheckJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isExistID = true;
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
            isExistID = false;
        }

        isIDSearchCompleted = true;
    }

    // 로그인 상태 업데이트
    public void UserProfile_Update()
    {
        StartCoroutine(_UserProfile_Update());
    }

    IEnumerator _UserProfile_Update()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_Profile", PlayerPrefs.GetInt("AT_Player_Profile"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_UserProfileState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
            }
        }
    }

    // 비밀번호 변경
    public void PasswordChange(string _pw)
    {
        StartCoroutine(_PasswordChange(_pw));
    }

    IEnumerator _PasswordChange(string _pw)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_Password", _pw);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_PasswordChange.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                //Debug.Log("변경완료");
                PlayerPrefs.SetString("AT_Player_PassWord", _pw);   //비번 새로 저장
            }
        }
    }

    // 닉네임 중복 체크
    public void NickNameDoubleCheck(string _nickname)
    {
        StartCoroutine(_NickNameDoubleCheck(_nickname));
    }

    IEnumerator _NickNameDoubleCheck(string _nickname)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_NickName", _nickname);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_NickName_DoubleCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                GetNickNameDoubleCheckJSON(www.downloadHandler.text);
            }
        }
    }

    public void GetNickNameDoubleCheckJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isExistNickName = true;
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
            isExistNickName = false;
        }

        isNickNameSearchCompleted = true;
    }

    public void NickNameChange(string _nicknameStr)
    {
        StartCoroutine(_NickNameChange(_nicknameStr));
    }

    IEnumerator _NickNameChange(string _nicknameStr)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_NickName", _nicknameStr);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_NickNameChange.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                //Debug.Log("변경완료");
                PlayerPrefs.SetString("AT_Player_NickName", _nicknameStr);
            }
        }

        isNickNameChangeCompleted = true;
    }

    // 닉네임 등록 및 로그인상태 등록
    public void UserNickNameInfo_Reg()
    {
        StartCoroutine(_UserNickNameInfo_Reg());
    }

    IEnumerator _UserNickNameInfo_Reg()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_LoginState", PlayerPrefs.GetString("AT_Player_LoginState"));
        form.AddField("Player_NickName", PlayerPrefs.GetString("AT_Player_NickName"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_UserNickNameInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
            }
        }
    }

    // 로그인 상태 업데이트
    public void UserLoginState_Update()
    {
        StartCoroutine(_UserLoginState_Update());
    }

    IEnumerator _UserLoginState_Update()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_LoginState", PlayerPrefs.GetString("AT_Player_LoginState"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_UserLoginState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
            }
        }
    }

    // 캐릭터 정보 저장
    public void CharacterInfo_Reg()
    {
        StartCoroutine(_CharacterInfo_Reg());
    }

    IEnumerator _CharacterInfo_Reg()
    {
        WWWForm form = new WWWForm();

        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        //캐릭터 정보
        form.AddField("Player_Sex", PlayerPrefs.GetString("AT_Player_Sex"));
        form.AddField("Player_Face", PlayerPrefs.GetString("AT_Player_Face"));
        form.AddField("Player_SkinColor", PlayerPrefs.GetString("AT_Player_SkinColor"));
        form.AddField("Player_Hair", PlayerPrefs.GetString("AT_Player_Hair"));
        form.AddField("Player_HairColor", PlayerPrefs.GetString("AT_Player_HairColor"));

        //골드 레벨 정보
        form.AddField("Player_Gold", PlayerPrefs.GetInt("AT_Player_Gold"));
        form.AddField("Player_Level", PlayerPrefs.GetInt("AT_Player_Level"));
        form.AddField("Player_CurrExp", PlayerPrefs.GetInt("AT_Player_CurrExp"));
        form.AddField("Player_TakeExp", PlayerPrefs.GetInt("AT_Player_TakeExp"));

        //기록정보
        form.AddField("Record_MaxSpeed", PlayerPrefs.GetFloat("AT_Record_MaxSpeed").ToString());
        form.AddField("Record_TodayKcal", PlayerPrefs.GetFloat("AT_Record_TodayKcal").ToString());
        form.AddField("Record_TodayKm", PlayerPrefs.GetFloat("AT_Record_TodayKm").ToString());
        form.AddField("Record_TodayMaxSpeed", PlayerPrefs.GetFloat("AT_TodayMaxSpeed").ToString());
        form.AddField("Record_TotalKm", PlayerPrefs.GetFloat("AT_Record_TotalKm").ToString());

        //아이템 장착 종류
        form.AddField("Wear_HairKind", PlayerPrefs.GetString("AT_Wear_HairKind"));
        form.AddField("Wear_GlovesKind", PlayerPrefs.GetString("AT_Wear_GlovesKind"));
        form.AddField("Wear_HelmetKind", PlayerPrefs.GetString("AT_Wear_HelmetKind"));
        form.AddField("Wear_JacketKind", PlayerPrefs.GetString("AT_Wear_JacketKind"));
        form.AddField("Wear_PantsKind", PlayerPrefs.GetString("AT_Wear_PantsKind"));
        form.AddField("Wear_ShoesKind", PlayerPrefs.GetString("AT_Wear_ShoesKind"));
        form.AddField("Wear_BicycleKind", PlayerPrefs.GetString("AT_Wear_BicycleKind"));

        //장착 아이템 스타일
        form.AddField("Wear_HairStyleName", PlayerPrefs.GetString("AT_Wear_HairStyleName"));
        form.AddField("Wear_GlovesStyleName", PlayerPrefs.GetString("AT_Wear_GlovesStyleName"));
        form.AddField("Wear_HelmetStyleName", PlayerPrefs.GetString("AT_Wear_HelmetStyleName"));
        form.AddField("Wear_JacketStyleName", PlayerPrefs.GetString("AT_Wear_JacketStyleName"));
        form.AddField("Wear_PantsStyleName", PlayerPrefs.GetString("AT_Wear_PantsStyleName"));
        form.AddField("Wear_ShoesStyleName", PlayerPrefs.GetString("AT_Wear_ShoesStyleName"));
        form.AddField("Wear_BicycleStyleName", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"));

        //각 커스텀 숫자 저장
        form.AddField("HairNumber", PlayerPrefs.GetInt("AT_HairNumber"));
        form.AddField("BodyNumber", PlayerPrefs.GetInt("AT_BodyNumber"));
        form.AddField("GlovesNumber", PlayerPrefs.GetInt("AT_GlovesNumber"));
        form.AddField("HelmetNumber", PlayerPrefs.GetInt("AT_HelmetNumber"));
        form.AddField("JacketNumber", PlayerPrefs.GetInt("AT_JacketNumber"));
        form.AddField("PantsNumber", PlayerPrefs.GetInt("AT_PantsNumber"));
        form.AddField("ShoesNumber", PlayerPrefs.GetInt("AT_ShoesNumber"));
        form.AddField("BicycleNumber", PlayerPrefs.GetInt("AT_BicycleNumber"));

        // 기본 아이템 저장 4개
        // 성별에 따라 이미지 이름이 다르다면 if문으로 다르게 저장해야함!!!!
        //form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        // 처음 커스텀시에 머리는 선택지가 있으므로 다르게 입력해줘야함.

        if (PlayerPrefs.GetInt("AT_HairNumber") == 0)
        {
            form.AddField("BasicHair", "Hair1");
        }
        else if (PlayerPrefs.GetInt("AT_HairNumber") == 1)
        {
            form.AddField("BasicHair", "Hair2");
        }
        else if (PlayerPrefs.GetInt("AT_HairNumber") == 2)
        {
            form.AddField("BasicHair", "Hair3");
        }

        form.AddField("BasicNasi", "BasicNasi");
        form.AddField("BasicShort", "BasicShort");
        form.AddField("BasicSandal", "BasicSandal");
        form.AddField("BasicBicycle", "BasicBicycle");  // 21-05-10

        form.AddField("ExpUp", "ExpUp");
        form.AddField("ExpPlus", "ExpPlus");
        form.AddField("CoinUp", "CoinUp");
        form.AddField("SpeedUp", "SpeedUp");  // 21-05-25

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_CharacterInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Character Reg Form upload complete!");
                isCharacterRegUpload = true;
            }
        }
    }
    // 접속상태 체크
    public void GetConnectedState(string _userID)
    {
        StartCoroutine(_GetConnectedState(_userID));
    }

    IEnumerator _GetConnectedState(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_ConnectedState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                GetConnectedStateJSON(www.downloadHandler.text);
            }
        }
    }

    private void GetConnectedStateJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                connectedStateCheck = new ConnectedStateCheck(Array[i]["ONECONNECTDAY"].Value, Array[i]["TWOCONNECTDAY"].Value,
                                                              Array[i]["ONECHECK"].Value, Array[i]["TWOCHECK"].Value, Array[i]["CONNECT_TIME"].Value);
            }

            isConnectedStateStackCompleted = true;
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }
    }

    // 접속상태 업데이트
    public void UpdateConnectedState(string _way, string _Date)
    {
        StartCoroutine(_UpdateConnectedState(_way, _Date));
    }

    IEnumerator _UpdateConnectedState(string _way, string _Date)
    {
        WWWForm form = new WWWForm();

        // Lobby면 0 , Quest면 1
        //int i_way = _way == "Lobby" ? 0 : 1;
        int i_way = 0;

        if (_way == "TwoCheck")
        {
            i_way = 0;
        }
        else if (_way == "TwoCennectDay")
        {
            i_way = 1;
        }
        else if (_way == "ConnectTime")
        {
            i_way = 2;
        }

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        // Way
        form.AddField("Way", i_way);

        form.AddField("UpdateValue", _Date);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_ConnectedState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
            }
        }
    }

    // 일일퀘스트
    public void GetTodayQuest(string _userID)
    {
        StartCoroutine(_GetTodayQuest(_userID));
    }

    IEnumerator _GetTodayQuest(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_TodayQuest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                GetTodayQuestJSON(www.downloadHandler.text);
            }
        }
    }
    //// Get Store Item JSON
    private void GetTodayQuestJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            myTodayQuest.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                myTodayQuest.Add(new MyTodayQuest(Array[i]["QUEST_CATEGORY"].Value, Int32.Parse(Array[i]["QUEST_IDX"].Value), Array[i]["QUEST_STATE"].Value));
            }

            isTodayQuestStackCompleted = true;
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }
    }
    // 맵 당 경기기록 가져오기
    public void GetRecordByAsiaCourse(string _userID)
    {
        StartCoroutine(_GetRecordByAsiaCourse(_userID));
    }

    IEnumerator _GetRecordByAsiaCourse(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_RecordByAsiaCourse.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                _GetRecordByAsiaCourseJSON(www.downloadHandler.text);
            }
        }
    }

    private void _GetRecordByAsiaCourseJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            //highRanker.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                recordByAsiaCourse = new RecordByAsiaCourse(Array[i]["ASIA_N1"].Value, Array[i]["ASIA_N2"].Value, Array[i]["ASIA_N3"].Value,
                                                            Array[i]["ASIA_H1"].Value, Array[i]["ASIA_H2"].Value, Array[i]["ASIA_H3"].Value);
            }
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }

        isRecordByAsiaCourseStackCompleted = true;
    }
    public void Get_MapProgress(string _userID)
    {
        StartCoroutine(_Get_MapProgress(_userID));
    }

    IEnumerator _Get_MapProgress(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_MapProgress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                _Get_MapProgressJSON(www.downloadHandler.text);
            }
        }
    }

    private void _Get_MapProgressJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                mapProgress = new MapProgress(Array[i]["PROGRESS"].Value);
            }
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }

        isGetMapProgressCompleted = true;
    }
    public void Update_MapProgress()
    {
        StartCoroutine(_Update_MapProgress());
    }

    IEnumerator _Update_MapProgress()
    {
        WWWForm form = new WWWForm();

        //Debug.LogError("Player_ID : " + PlayerPrefs.GetString("AT_Player_ID"));
        //Debug.LogError("OpenMap_CourseName : " + PlayerPrefs.GetString("AT_OpenMap_CourseName"));

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("MapProgress", PlayerPrefs.GetString("AT_OpenMap_CourseName"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_MapProgress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("111Form upload complete!");
                //_Get_MapProgressJSON(www.downloadHandler.text);
            }
        }
    }
    // 맵 퀘스트
    public void GetMapQuest(string _userID)
    {
        StartCoroutine(_GetMapQuest(_userID));
    }

    IEnumerator _GetMapQuest(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_MapQuest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                GetMapQuestJSON(www.downloadHandler.text);
            }
        }
    }
    //// Get Store Item JSON
    private void GetMapQuestJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            myMapQuest.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                myMapQuest.Add(new MyMapQuest(Array[i]["QUEST_CATEGORY"].Value, Int32.Parse(Array[i]["QUEST_COMPLETEDSTATE"].Value), Int32.Parse(Array[i]["QUEST_PROGRESS"].Value)));
            }

            isMapQuestStackCompleted = true;
        }
        else
        {
            //Debug.Log("값이 존재하지 않습니다.");
        }
    }

    public void Update_TodayQuest(string _questCategory, string _questState = "None", int _questIdx = -1)
    {
        StartCoroutine(_Update_TodayQuest(_questCategory, _questState, _questIdx));
    }

    IEnumerator _Update_TodayQuest(string _questCategory, string _questState, int _questIdx)
    {
        WWWForm form = new WWWForm();

        if (_questState == "None")
        {
            // TodayQuest1
            form.AddField("Way", 0);
        }
        else if (_questIdx == -1)
        {
            // TodayQuest2,3,4,5
            form.AddField("Way", 1);
        }
        else if (_questIdx != -1 & _questState != "None")
        {
            // 초기화
            form.AddField("Way", 2);
        }

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Quest_Category", _questCategory);
        form.AddField("Quest_Idx", _questIdx);
        form.AddField("Quest_State", _questState);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_TodayQuest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
            }
        }
    }

    public void Update_MapQuest(string _questCategory, int _questCompletedState = -1, int _questProgress = -1)
    {
        StartCoroutine(_Update_MapQuest(_questCategory, _questCompletedState, _questProgress));
    }

    IEnumerator _Update_MapQuest(string _questCategory, int _questCompletedState, int _questProgress)
    {
        WWWForm form = new WWWForm();

        if (_questCompletedState == -1)
        {
            // _questProgress
            form.AddField("Way", 0);
        }
        else
        {
            // _questCompletedState
            form.AddField("Way", 1);
        }

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Quest_Category", _questCategory);
        form.AddField("Quest_CompletedState", _questCompletedState);
        form.AddField("Quest_Progress", _questProgress);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_MapQuest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
            }
        }
    }

    public void GetQuestReward(string _userID)
    {
        StartCoroutine(_GetQuestReward(_userID));
    }

    IEnumerator _GetQuestReward(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_QuestRewardState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
                GetQuestRewardJSON(www.downloadHandler.text);
            }
        }
    }

    private void GetQuestRewardJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            questReward.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                questReward.Add(new QuestReward(Array[i]["REWARD_TITLE"].Value, Array[i]["REWARD_STATE"].Value));
            }

            isQuestRewardStackCompleted = true;
        }
        else
        {
           // Debug.Log("값이 존재하지 않습니다.");
        }
    }

    public void Update_QuestReward(string _rewardTitle, string _rewardState)
    {
        StartCoroutine(_Update_QuestReward(_rewardTitle, _rewardState));
    }

    IEnumerator _Update_QuestReward(string _rewardTitle, string _rewardState)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Reward_Title", _rewardTitle);
        form.AddField("Reward_State", _rewardState);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_QuestRewardState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
            }
        }
    }

    // 상점 아이템 얻어오기
    public void GetStoreItemList()
    {
        StartCoroutine(_GetStoreItemList());
    }

    IEnumerator _GetStoreItemList()
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_StoreItem.php", form))
        {
            //fb_Get_StoreItem
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
                GetStoreItemJSON(www.downloadHandler.text);
            }
        }
    }
    //// Get Store Item JSON
    public void GetStoreItemJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            //highRanker.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                ItemDataBase.instance.items.Add(new Item(Array[i]["ITEMID"].Value, Array[i]["ITEMSTYLE"].Value,
                    Int32.Parse(Array[i]["PRICE"].Value), Int32.Parse(Array[i]["ITEMCATEGORY"].Value), Array[i]["FOLDER"].Value, Array[i]["IMGNAME"].Value,
                    Int32.Parse(Array[i]["BUYSTATE"].Value) == 1 ? true : false, Int32.Parse(Array[i]["ITEMORDER"].Value)));

                if (Array[i]["IMGNAME"].Value.Equals("ExpUp"))
                {
                    PlayerPrefs.SetInt("AT_ExpUpAmount", Int32.Parse(Array[i]["AMOUNT"].Value));
                }
                else if (Array[i]["IMGNAME"].Value.Equals("ExpPlus"))
                {
                    PlayerPrefs.SetInt("AT_ExpPlusAmount", Int32.Parse(Array[i]["AMOUNT"].Value));
                }
                else if (Array[i]["IMGNAME"].Value.Equals("CoinUp"))
                {
                    PlayerPrefs.SetInt("AT_CoinUpAmount", Int32.Parse(Array[i]["AMOUNT"].Value));
                }
                else if (Array[i]["IMGNAME"].Value.Equals("SpeedUp"))
                {
                    PlayerPrefs.SetInt("AT_SpeedUpAmount", Int32.Parse(Array[i]["AMOUNT"].Value));
                }
            }
        }
        else
        {
          //  Debug.Log("값이 존재하지 않습니다.");
        }
    }
    // 서버 - 구매한 아이템 업데이트(입은것 업데이트)
    public void BuyItems_Update()
    {
        StartCoroutine(_BuyItems_Update());
    }

    IEnumerator _BuyItems_Update()
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        // 골드 업데이트
        form.AddField("Player_Gold", PlayerPrefs.GetInt("AT_Player_Gold"));

        //장착 아이템 스타일
        form.AddField("Wear_HairStyleName", PlayerPrefs.GetString("AT_Wear_HairStyleName"));
        form.AddField("Wear_GlovesStyleName", PlayerPrefs.GetString("AT_Wear_GlovesStyleName"));
        form.AddField("Wear_HelmetStyleName", PlayerPrefs.GetString("AT_Wear_HelmetStyleName"));
        form.AddField("Wear_JacketStyleName", PlayerPrefs.GetString("AT_Wear_JacketStyleName"));
        form.AddField("Wear_PantsStyleName", PlayerPrefs.GetString("AT_Wear_PantsStyleName"));
        form.AddField("Wear_ShoesStyleName", PlayerPrefs.GetString("AT_Wear_ShoesStyleName"));
        form.AddField("Wear_BicycleStyleName", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"));

        //각 커스텀 숫자 저장
        form.AddField("HairNumber", PlayerPrefs.GetInt("AT_HairNumber"));
        form.AddField("BodyNumber", PlayerPrefs.GetInt("AT_BodyNumber"));
        form.AddField("GlovesNumber", PlayerPrefs.GetInt("AT_GlovesNumber"));
        form.AddField("HelmetNumber", PlayerPrefs.GetInt("AT_HelmetNumber"));
        form.AddField("JacketNumber", PlayerPrefs.GetInt("AT_JacketNumber"));
        form.AddField("PantsNumber", PlayerPrefs.GetInt("AT_PantsNumber"));
        form.AddField("ShoesNumber", PlayerPrefs.GetInt("AT_ShoesNumber"));
        form.AddField("BicycleNumber", PlayerPrefs.GetInt("AT_BicycleNumber"));

        //장착 아이템 아이디
        form.AddField("Wear_GlovesKind", PlayerPrefs.GetString("AT_Wear_GlovesKind"));
        form.AddField("Wear_HelmetKind", PlayerPrefs.GetString("AT_Wear_HelmetKind"));
        form.AddField("Wear_JacketKind", PlayerPrefs.GetString("AT_Wear_JacketKind"));
        form.AddField("Wear_PantsKind", PlayerPrefs.GetString("AT_Wear_PantsKind"));
        form.AddField("Wear_ShoesKind", PlayerPrefs.GetString("AT_Wear_ShoesKind"));
        form.AddField("Wear_BicycleKind", PlayerPrefs.GetString("AT_Wear_BicycleKind"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_WearItems.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
              //  Debug.Log(www.error);
            }
            else
            {
             //   Debug.Log("Form upload complete!");
            }
        }

        isBuyItemUpdateCompleted = true;
    }

    public void Update_ConsumableItems()
    {
        StartCoroutine(_Update_ConsumableItems());
    }

    IEnumerator _Update_ConsumableItems()
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Coin_Amount", PlayerPrefs.GetInt("AT_CoinUpAmount"));
        form.AddField("Exp_Amount", PlayerPrefs.GetInt("AT_ExpUpAmount"));
        form.AddField("Speed_Amount", PlayerPrefs.GetInt("AT_SpeedUpAmount"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_ConsumableItems.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
              //  Debug.Log(www.error);
            }
            else
            {
              //  Debug.Log("Form upload complete!");
            }
        }
    }

    public void PurchasingItems(string img_Name)
    {
        StartCoroutine(_PurchasingItems(img_Name));
    }

    IEnumerator _PurchasingItems(string _img_Name)
    {
        WWWForm form = new WWWForm();

        if (_img_Name == "ExpUp" || _img_Name == "ExpPlus" || _img_Name == "CoinUp" || _img_Name == "SpeedUp")
        {
            if (_img_Name == "ExpUp")
            {
                form.AddField("Amount", PlayerPrefs.GetInt("AT_ExpUpAmount"));
            }
            else if (_img_Name == "ExpPlus")
            {
                form.AddField("Amount", PlayerPrefs.GetInt("AT_ExpPlusAmount"));
            }
            else if (_img_Name == "CoinUp")
            {
                form.AddField("Amount", PlayerPrefs.GetInt("AT_CoinUpAmount"));
            }
            else if (_img_Name == "SpeedUp")
            {
                form.AddField("Amount", PlayerPrefs.GetInt("AT_SpeedUpAmount"));
            }

            form.AddField("Way", 0);    // 소모품은 Update 로 갯수 중첩등록되야함
        }
        else
        {
            form.AddField("Way", 1);
        }

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        // 구매하는 아이템의 ImgName 가져오기
        // 갯수가 쌓이는 아이템인지 아닌지 비교해서 나눠주기
        form.AddField("StoreBuy_ImageName", _img_Name);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_PurchasingItems.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
              //  Debug.Log("Form upload complete!");
            }
        }

        //isPurchasingItemsCompleted = true;
    }


    // 중국맵 랭킹정보 등록
    public void Reg_Asia_Ranking(string mode, string corse, string bike, string best_time)
    {
        StartCoroutine(_Reg_Asia_Ranking(mode, corse, bike, best_time));
    }

    IEnumerator _Reg_Asia_Ranking(string _mode, string _corse, string _bike, string _best_time)
    {
        WWWForm form = new WWWForm();

        //form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        //form.AddField("Player_NickName", PlayerPrefs.GetString("Player_NickName"));
        //form.AddField("Map", PlayerPrefs.GetString("Map"));
        //form.AddField("Mode", PlayerPrefs.GetString("Mode"));
        //form.AddField("Corse", PlayerPrefs.GetString("Corse"));
        //form.AddField("Bike", PlayerPrefs.GetString("Bike"));
        //form.AddField("Best_Time", PlayerPrefs.GetString("Best_Time"));
        //Debug.Log("ID : "+ PlayerPrefs.GetString("AT_Player_ID"));
        //Debug.Log("NickName : " + PlayerPrefs.GetString("AT_Player_NickName"));
        //Debug.Log("_mode : " + _mode);
        //Debug.Log("_corse : " + _corse);
        //Debug.Log("Bike : " + _bike);
        //Debug.Log("_best_time : " + _best_time);
        

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Player_NickName", PlayerPrefs.GetString("AT_Player_NickName"));
        form.AddField("Map", "Asia");
        form.AddField("Mode", _mode);
        form.AddField("Corse", _corse);
        form.AddField("Bike", _bike);
        form.AddField("Best_Time", _best_time);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Reg_AsiaMap_Ranking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("통과함 : "+www.downloadHandler.text);
            }
        }
    }

    // Filter China
    public void Get_Asia_RankingZoneData(int amount, string mode, string corse)
    {
        StartCoroutine(_Get_Asia_RankingZoneData(amount, mode, corse));
    }

    IEnumerator _Get_Asia_RankingZoneData(int _amount, string _mode, string _corse)
    {
        WWWForm form = new WWWForm();

        form.AddField("Search_Amount", _amount);

        form.AddField("Map", "Asia");

        if (_mode.Equals("Normal")) { form.AddField("Mode", 1); }
        if (_mode.Equals("Hard")) { form.AddField("Mode", 2); }

        if (_corse.Equals("제1코스")) { form.AddField("Corse", 1); }
        if (_corse.Equals("제2코스")) { form.AddField("Corse", 2); }
        if (_corse.Equals("제3코스")) { form.AddField("Corse", 3); }


        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_AsiaMap_Ranking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log(www.downloadHandler.text);

                //byte[] results = www.downloadHandler.data;
                //text.text = www.downloadHandler.text;
                GetAsiaRankingJSON(www.downloadHandler.text);
            }
        }
    }

    //// Get Ranking JSON
    public void GetAsiaRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            Ranking.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                Ranking.Add(new Rank(Int32.Parse(Array[i]["RANK"].Value), Array[i]["PLAYER_NICKNAME"].Value,
                                    Array[i]["MAP"].Value, Array[i]["MODE"].Value,
                                    Array[i]["CORSE"].Value, Array[i]["BIKE"].Value,
                                    Array[i]["BEST_TIME"].Value));
            }
        }
        else
        {
            Ranking.Clear();
           // Debug.Log("값이 존재하지 않습니다.");
        }

        isRankingSearchCompleted = true;
    }
    /////////////////////////////////////////////////////2021.06.22///////////////////////////////
    // ID 에 따라 N123, H123 6개 가져옴
    // Mode : Normal(1)/Hard(2), Corse : 1/2/3 , Bike : F1234.. , TimeLab : 10:00:00
    public void Get_MyRankingRecordInfo(string map)
    {
        StartCoroutine(_Get_MyRankingRecordInfo(map));
    }

    IEnumerator _Get_MyRankingRecordInfo(string _map)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Map", _map);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_AsiaMap_MyRanking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log(www.downloadHandler.text);

                //byte[] results = www.downloadHandler.data;
                //text.text = www.downloadHandler.text;
                _Get_Asia_MyRankingJSON(www.downloadHandler.text);
            }
        }
    }

    private void _Get_Asia_MyRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            myRank.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 나의 랭킹 확인
                myRank.Add(new MyRank(Array[i]["RANKING"].Value, Array[i]["MAP"].Value,
                                      Array[i]["MODE"].Value, Array[i]["CORSE"].Value,
                                      Array[i]["BIKE"].Value, Array[i]["BEST_TIME"].Value));
            }
        }
        else
        {
            myRank.Clear();
           // Debug.Log("값이 존재하지 않습니다.");
        }

        isMyRankingStackUp = true;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    public void GetMyRankingRecordInfo(string map, string mode, string corse)
    {
        StartCoroutine(_GetMyRankingRecordInfo(map, mode, corse));
    }

    IEnumerator _GetMyRankingRecordInfo(string _map, string _mode, string _corse)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Mode", _mode);
        form.AddField("Corse", _corse);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Get_AsiaMap_MyRanking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log(www.downloadHandler.text);

                //byte[] results = www.downloadHandler.data;
                //text.text = www.downloadHandler.text;
                GetAsiaMyRankingJSON(www.downloadHandler.text);
            }
        }
    }

    public void GetAsiaMyRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            myRank.Clear();
            // 배열에 맞게 들어가는지 확인
            // $rows["MAP"] = $row[2];
            // $rows["MODE"] = $row[3];
            // $rows["CORSE"] = $row[4];
            // $rows["BIKE"] = $row[5];
            // $rows["BEST_TIME"] = $row[6];
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 나의 랭킹 확인
                myRank.Add(new MyRank(Array[i]["RANKING"].Value, Array[i]["MAP"].Value, Array[i]["MODE"].Value,
                                    Array[i]["CORSE"].Value, Array[i]["BIKE"].Value,
                                    Array[i]["BEST_TIME"].Value));
            }
        }
        else
        {
            myRank.Clear();
           // Debug.Log("값이 존재하지 않습니다.");
        }
    }

    // 아시아 맵 레코드 업데이트
    public void Update_RecordByAsiaCourse(string _way, string _course_Record, string _time_Record)
    {
        StartCoroutine(_Update_RecordByAsiaCourse(_way, _course_Record, _time_Record));
    }

    IEnumerator _Update_RecordByAsiaCourse(string _way, string _course_Record, string _time_Record)
    {
        WWWForm form = new WWWForm();
        int i_way;

        if (_way.Equals("Normal-1"))
        {
            i_way = 1;
            form.AddField("Way", i_way.ToString());
        }
        else if (_way.Equals("Normal-2"))
        {
            i_way = 2;
            form.AddField("Way", i_way.ToString());
        }
        else if (_way.Equals("Normal-3"))
        {
            i_way = 3;
            form.AddField("Way", i_way.ToString());
        }
        else if (_way.Equals("Hard-1"))
        {
            i_way = 4;
            form.AddField("Way", i_way.ToString());
        }
        else if (_way.Equals("Hard-2"))
        {
            i_way = 5;
            form.AddField("Way", i_way.ToString());
        }
        else if (_way.Equals("Hard-3"))
        {
            i_way = 6;
            form.AddField("Way", i_way.ToString());
        }
        else
        {
           // Debug.Log("맞는 형식이 없어요");
        }

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Course_Record", _course_Record);
        form.AddField("Time_Record", _time_Record);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_RecordByAsiaCourse.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // 내 정보의 기록 업데이트
    public void Update_UserRecordInfo(float maxSpeed, float todayKcal, float todayMaxSpeed, float todayKm)
    {
        StartCoroutine(_Update_UserRecordInfo(maxSpeed, todayKcal, todayMaxSpeed, todayKm));
    }

    IEnumerator _Update_UserRecordInfo(float _maxSpeed, float _todayKcal, float _todayMaxSpeed, float _todayKm)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        form.AddField("Record_TodayMaxSpeed", _todayMaxSpeed.ToString());
        form.AddField("Record_MaxSpeed", _maxSpeed.ToString());
        form.AddField("Record_TodayKcal", _todayKcal.ToString());
        form.AddField("Record_TodayKm", _todayKm.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_UserRecordInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
                isUpdatedUserRecord = true;
            }
        }
    }

    // 맵 완주 후 골드 업데이트
    public void Update_Gold_Data(int _gold)
    {
        StartCoroutine(_Update_Gold_Data(_gold));
    }

    IEnumerator _Update_Gold_Data(int _gold)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        // Gold
        form.AddField("Player_Gold", _gold);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_Gold.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Form upload complete!");
            }
        }
    }

    // 맵 완주 후 경험치+레벨 업데이트
    public void Update_Level_Data(int _Lv, int _currExp, int _takeExp)
    {
        StartCoroutine(_Update_Level_Data(_Lv, _currExp, _takeExp));
    }

    IEnumerator _Update_Level_Data(int _Lv, int _currExp, int _takeExp)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));
        // Gold
        form.AddField("Player_Level", _Lv);
        form.AddField("Player_CurrExp", _currExp);
        form.AddField("Player_TakeExp", _takeExp);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Update_Level.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
              //  Debug.Log(www.error);
            }
            else
            {
              //  Debug.Log("Form upload complete!");
            }
        }
    }

    public void AccountDropOut()
    {
        StartCoroutine(_AccountDropOut());
    }

    IEnumerator _AccountDropOut()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("AT_Player_ID"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_Delete_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
              //  Debug.Log(www.error);
            }
            else
            {
              //  Debug.Log("계정 탈퇴 성공");
            }
        }
    }

    public void SettingInfo(string s_option, string _userID)
    {
        //string s_option = "INSERT";
        //string s_option = "SELECT";
        //string s_option = "UPDATE";

        if (s_option.Equals("INSERT"))
        {
            int _option = 1;
            StartCoroutine(_Reg_SettingInfo(_option, _userID));
        }
        else if (s_option.Equals("SELECT"))
        {
            int _option = 2;
            StartCoroutine(_Get_SettingInfo(_option, _userID));
        }
        else if (s_option.Equals("UPDATE"))
        {
            int _option = 3;
            StartCoroutine(_Update_SettingInfo(_option, _userID));
        }
        else
        {
          //  Debug.Log("the other option number!");
        }
    }

    IEnumerator _Reg_SettingInfo(int _option, string _userID)
    {
        WWWForm form = new WWWForm();

        form.AddField("Way", _option);
        form.AddField("Player_ID", _userID);
        //form.AddField("BackBGMVol", PlayerPrefs.GetFloat("BackBGMVol").ToString());
        //form.AddField("SFXVol", PlayerPrefs.GetFloat("SFXVol").ToString());
        form.AddField("BackBGMVol", 0);
        form.AddField("SFXVol", -15);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_SettingInfo.php", form))
        {
            yield return www.SendWebRequest();

            while (!www.isDone)
            {
               // Debug.Log(www.downloadProgress);
            }

            if (www.isNetworkError || www.isHttpError)
            {
              //  Debug.Log(www.error);
            }
            else
            {
              //  Debug.Log(www.downloadHandler.text);
                isRegSettingCompleted = true;
            }
        }
    }

    IEnumerator _Update_SettingInfo(int _option, string _userID)
    {
        WWWForm form = new WWWForm();

        form.AddField("Way", _option);
        form.AddField("Player_ID", _userID);
        //form.AddField("BackBGMVol", PlayerPrefs.GetFloat("BackBGMVol").ToString());
        //form.AddField("SFXVol", PlayerPrefs.GetFloat("SFXVol").ToString());
        form.AddField("BackBGMVol", -15);
        form.AddField("SFXVol", -15);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_SettingInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {
                // 종료시 Update
            }
        }
    }

    IEnumerator _Get_SettingInfo(int _option, string _userID)
    {
        WWWForm form = new WWWForm();

        form.AddField("Way", _option);
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_bicycle/fb_SettingInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
               // Debug.Log(www.error);
            }
            else
            {

               // Debug.Log(www.downloadHandler.text);
                _Get_SettingInfo(www.downloadHandler.text);
            }
        }
    }

    public void _Get_SettingInfo(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                // 세팅
                setting = new Setting(float.Parse(Array[i]["BGM"].Value),
                                    float.Parse(Array[i]["SFX"].Value));
            }
        }
        else
        {
          //  Debug.Log("값이 존재하지 않습니다.");
        }

        isGetSettingCompleted = true;
    }

    /***************************************************************************************/

    public void GetNetworkState()
    {
        StartCoroutine(_GetNetworkState());
    }

    IEnumerator _GetNetworkState()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_conn_state.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                isConnected = false;
                // 여기다 네트워크 연결안됬다 팝업창 띄우기
                NetworkErrorPopUp();
            }
            else
            {
                isConnected = true;
               // Debug.Log(www.downloadHandler.text);
            }
        }

        isConnCompleted = true;
    }

    private void NetworkErrorPopUp()
    {
        networkConnectStatePopUp.SetActive(true);
        StartCoroutine(_NetworkErrorPopUp());
    }

    IEnumerator _NetworkErrorPopUp()
    {
        WaitForSeconds ws = new WaitForSeconds(0.8f);
        yield return ws;
        networkConnectStatePopUp.SetActive(false);
    }
}