using System;

[Serializable]
public enum LanguageType {
    English,
    Korean,
}
[Serializable]
enum MapType {
    Busan,
}
[Serializable]
enum ModeType {
    Normal,
    Hard,
}
[Serializable]
enum CourseType {
    Course1,
    Course2,
}
[Serializable]
enum TodayQuestConnectMission {
    Connect,
    StayFor10m,
    StayFor30m,
    StayFor1h,
}
[Serializable]
enum TodayQuestVisitMission {
    VisitStore,
    VisitMyInfo,
    VisitMyInventory,
    VisitRankingZone,
}
[Serializable]
enum TodayQuestGameUseMission {
    EditProfile,
    PlayGame,
    RankUp,
    UsingCoin,
    UsingItem,
    PurchasingItem,
}
[Serializable]
enum TodayQuestBurnUpMission {
    Over300kcal,
    Over400kcal,
    Over500kcal,
    Over600kcal,
}
[Serializable]
enum TodayQuestMaxSpeed {
    MaxSpeed_25,
    MaxSpeed_30,
    MaxSpeed_35,
    MaxSpeed_40,
    MaxSpeed_45,
    MaxSpeed_50,
    MaxSpeed_55,
}

[Serializable]
enum MapQuestTargetTime {
    Target_5_00,
    Target_4_40,
    Target_4_20,
    Target_4_00,
    Target_3_40,
    Target_3_20,
    Target_3_00,
    Target_2_40,
    Target_2_20,
    Target_2_00,
    Target_1_40,
    Target_1_20,
}
[Serializable]
enum LandMarksType {
    Nurimaru,
    HaeundaeBeach,
    DiamondBridge,
    BEXCO,
    CinemaCenter,
}