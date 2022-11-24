using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Narration : MonoBehaviour
{
    public Button SkipButton;

    void Start()
    {
        SkipButton.onClick.AddListener(() => Game_TypeWriterEffect.instance.TypingBtn_speedUP());
    }
    //필수 나레이션
    public void EventTextList(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.EventText = new string[11];
            //우주휴게소 암시
            Game_TypeWriterEffect.instance.EventText[0] = "어? 저 푸른빛은 뭐지?";

            //산소통 줍기(획득 전)
            Game_TypeWriterEffect.instance.EventText[1] = "어? 이건 뭐지? 산소통이야, 누가 놔둔 걸까 ?\n" +
                "나 같은 사람이 이 길을 걷게 될 거라는 걸 알았던 걸까? \n" +
                "마침 잘 됐어!산소가 떨어지고 있었는데..\n 산소가 떨어지면 쓰러질 거야.\n"
                + "오른쪽 위에 있는 가방에서 산소통을 꺼내 사용하자.";


            //별가루 줍기(획득 전)
            Game_TypeWriterEffect.instance.EventText[2] = "이 반짝이는 건 뭐지?\n" +
                   "두 개나 있어...둘 다 별가루인 것 같아, 별처럼 반짝거려!\n" +
                   "이건 어디에 쓰는 물건일까? 한번 열어볼까 ?\n" +
                   "오른쪽 위에 있는 가방에서 별가루를 꺼내 사용해보자!";

            //별가루 줍기(사용 후)
            Game_TypeWriterEffect.instance.EventText[3] = "어..! 별가루를 여니까 발걸음이 가벼워...\n" +
                " 평소보다 두배로 빠르게 걸어지는 것 같아!";

            //우주 라디오 켜기(획득 전)
            Game_TypeWriterEffect.instance.EventText[4] = "/라디오: 치이익- 치익- 여기는 - 치익/\n 이게 무슨 소리지? ... 어라, 라디오가 있어!";
            Game_TypeWriterEffect.instance.EventText[5] = "무슨 문제가 있나봐 라디오에서 아무런 소리가 나지 않아.\n" +
                "설마 연료 같은 게 필요할까? ...!\n 이 라디오에 묻은 가루가 달가루와 비슷해!\n" +
                "달가루를 뿌리면 라디오를 들을 수 있을지 몰라\n 가방에서 달가루를 꺼내 라디오에 뿌려봐야겠어.";

            //(3-2) 우주 라디오 켜기 (행동 이후)
            Game_TypeWriterEffect.instance.EventText[6] = "/치이익 - 서울은 오늘도 치익- 다음 소식은 해운대의 치지지직 -\n/ 지구의 소식이잖아!";

            //(4) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[7] = "이런 곳에 갈림길이라니...\n 달까지 가려면 어디로 가야 하지?...선택을 해야 해.\n 에잇,,!이쪽이다.잘못 선택했으면 돌아오면 되지.";

            //길 사라짐으로 인한 뛰기(전)
            Game_TypeWriterEffect.instance.EventText[8] = "... 이상한 느낌이 들어, 소름이 끼치기도 해...\n 어라 ? 길이...점점 투명해지고 있어 ...설마...!길이 사라지고 있잖아!\n" +
                "빨리 뛰어야겠어. 길이 사라지기 전에 벗어나야 할 것 같아 느낌이 안 좋아";

            //길 사라짐으로 인한 뛰기(후)
            Game_TypeWriterEffect.instance.EventText[9] = "이럴 때 별가루가 있으면 좋을텐데!\n 부지런히 달리자...!";
            //(6) 비상식량 발견
            Game_TypeWriterEffect.instance.EventText[10] = "어라 이건 지구의 통조림과 비슷하게 생겼어....\n 사람이 먹을 수 있다고 적혀있는 것 같은데..." +
                "\n 사람이 먹지 못하는 것도 있을 수 있겠다 잘 확인하고 먹어야겠어.\n 이곳에 얼마나 오래 있었던 걸까? 먹어도 되겠지?";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 누군가의 쪽지 (획득 후)
            Game_TypeWriterEffect.instance.EventText[0] = "알아볼 수 없는 글자들이 가득해. 나에게 무엇을 전하고 싶은 걸까.";

            //(2) 외로움 쫓기(미션 전)
            Game_TypeWriterEffect.instance.EventText[1] = "자꾸만 안 좋은 생각을 하게 돼.\n 외로움을 쫓아야 해. 나쁜 생각을 없애기 위해 있는 힘껏 달려야겠어.";

            //(3) 갈림길 (갈림길 도착 시)
            Game_TypeWriterEffect.instance.EventText[2] = "갈림길에 표지판이 있어서 정말 다행이야.";
            //(4) 의문의 조각함 (획득 후)
            Game_TypeWriterEffect.instance.EventText[3] = "어? 이 상자 같은 건 뭐지? 조각함이네...?\n 10개의 조각을 끼울 수 있는 조각판이 있구나. 혹시 모르니 챙겨가야겠어.";
            //(5) 별가루 줍기 (획득 후)
            Game_TypeWriterEffect.instance.EventText[4] = "이야호, 별가루를 주웠어! 분명 여기저기 필요한 곳이 많을거야.";
            //(6)비상식량 발견 (획득 후)
            Game_TypeWriterEffect.instance.EventText[5] = "이건 통조림이잖아. 나는 운이 좋은 거 같아! 아주 맛있겠어!";
        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.EventText = new string[5];
            //(1) 비상 식량 발견 (발견 후)
            Game_TypeWriterEffect.instance.EventText[0] = "이건 번개 문양이 그려진 통조림이야.\n 사람이 먹을 수 있다고 그림이 그려져있어.\n 이런 게 어떻게 여기 있는 걸까?";

            //(2) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[1] = "또 갈림길이다...";

            //(3) 사람의 왕래 벽화 (발견 후)
            Game_TypeWriterEffect.instance.EventText[2] = "달을 왔다 갔다 했던 사람이 있었던 것 같아. 벽화가 말해주고 있잖아.\n " +
                "사람 말고 다른 존재도 있었던 것 같아.\n " +
                "이 길을 통해서 달을 왕래했던 걸까?";
            //(4) 조각함의 존재 (발견 후)
            Game_TypeWriterEffect.instance.EventText[3] = "벽화에 의하면 얼마 전에 주웠던 조각함에 대해 적혀있어.\n" +
                "달의 가운데 그려져있는데...어떤 조각을 담아둘 수 있나 봐.\n " +
                "꼭 필요한 것 같은데... 이 조각을 다 모아야 하는 것 같아.";
            //(5) 통로에서 1.5배 빨리 닳음 (시작부터)
            Game_TypeWriterEffect.instance.EventText[4] = "뭔가 숨이 부족한 것 같아. 맙소사. 산소가 왜 이렇게 빨리 닳는 거지...!!\n" +
                " 부족하기 전에 얼른 달려가야겠어.\n 여기만 지나가면 괜찮아지겠지...?";
        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 비상 식량 발견 (발견 후)
            Game_TypeWriterEffect.instance.EventText[0] = "이번에는 해가 그려져있는 스낵이야.\n 역시나 사람이 먹을 수 있는 표시가 되어있어.\n 대체 누가 만들어놓은 걸까?";

            //(2) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[1] = "하나는 해, 하나는 달이 그려져있네. 나는 달로 가야지.";

            //(3) 소형 산소통 줍기 (주운 후)
            Game_TypeWriterEffect.instance.EventText[2] = "이건 소형 산소통이야. 필요할 때 적절히 사용할 수 있을 것 같아! ";

            //(4) 길 사라짐으로 인한 뛰기(구간 빨리 달리기 전)
            Game_TypeWriterEffect.instance.EventText[3] = "점점 길이 사라지는 것 같아... 그냥 느낌 탓인가?\n" +
                " 으앗...!!!길이 정말 사라지고 있잖아 ? !!!\n " +
                "빨리 달려야 해!! 안 그러면 나도 함께 사라질지도 몰라!!!";

            //(5) 징검다리
            Game_TypeWriterEffect.instance.EventText[4] = "어라.. 길의 간격이 너무 넓어... 그냥 걸어가면 길에서 멀어져 버릴 것 같아.\n " +
                "달려서 징검다리를 뛰어넘어가자!";
            // (6) 조각난 우주선의 파편(발견 후)
            Game_TypeWriterEffect.instance.EventText[5] = "우주선의 파편인 건가...? 누군가 여기 우주선을 타고 온 걸까?\n 아니면 내가 착륙했을 때 여기까지 날아온 걸까?\n 이건 필요 없으니 얼른 지나가자.";
        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //아기별 만나기 전
            Game_TypeWriterEffect.instance.EventText[0] = "방금 이상한 소리가 들린 것 같아. \n" +
                "/라디오 : 도...와...줘 치익 \n/ ...또... 들렸어!";

            //아기별 만난 후
            Game_TypeWriterEffect.instance.EventText[1] = "아기 별님아 엄마를 잃어버렸니?\n " +
                "/라디오 : 응... 흑흑..나를 도와줘.\n/" +
                "너는 이름이 뭐야?\n " +
                "/라디오 : 미..츄야.\n/ 미츄구나, 지구의 meet you와 닮은 이름이야\n" +
                "/라디오 : 엄마를 잃어버렸어... 엄마를 찾는 걸 도와줄 수 있어?\n/ " +
                "물론이지, 내가 너의 엄마를 찾아줄게! 나와 함께 가자.";
            //갈림길 (전)
            Game_TypeWriterEffect.instance.EventText[2] = "미츄, 갈림길에서는 당황하지 말고 표지판을 따라가면 돼.\n " +
                "/라디오 : 우리도 달을 향해 가는 중이었어. 같은 방향이어서 다행이야.\n/ " +
                "그래? 정말 잘 됐다!";
            ////////////////////////////////////////////////////////////
            //비상식량, 발견전
            Game_TypeWriterEffect.instance.EventText[3] = "어, 이건 행성이 그려진 통조림이네.\n " +
                "이걸 먹으니 갈증과 배고픔이 순식간에 사라졌어.";
            //엄마별 만나기 (전)
            Game_TypeWriterEffect.instance.EventText[4] = "어..! 저 별이 점점 이곳에 다가오는 것 같아\n " +
              "/라디오 : ...어?\n/" +
              "미츄 너랑 똑같이 생겼어!\n" + "저기요!! 미츄를 찾고 있나요?\n" +
              "여기에요!";

            //엄마별 만나기 (후)
            Game_TypeWriterEffect.instance.EventText[5] = "미츄 너의 엄마야 !\n " +
                "/라디오 : 맞아! 우리 엄마야! 엄마아! 흑흑.\n/" +
                "나도 엄마가 보고 싶어지네... 미츄, 엄마를 찾아서 정말 다행이야.\n " +
                "/라디오 : 아기별을 찾아줘서 고마워요. 보답으로 이 달의 조각을 줄게요.\n/ " +
                "/라디오 : 너랑 걷는 동안 즐거웠어! \n/ " +
                "나도 덕분에 외롭지 않았어. 넌 우주에서 사귄 첫 친구야! 절대 잊지 않을게.\n 잘 가!";
            //별가루 사용!
            Game_TypeWriterEffect.instance.EventText[6] = "기분이 너무 좋아.\n " +
              "달가루를 사용해서 조금 빨리 달려볼까?\n";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[0] = "갈림길이 도대체 몇 개일까...? 저기는 길이 없는 것 같으니 이쪽으로 가야겠어.";

            //(2) 길 사라짐으로 인한 뛰기 (도달 전)
            Game_TypeWriterEffect.instance.EventText[1] = "어? 뭔가 불길한데...??\n 역시...!!길이 사라지는 것 같아.뛰어야겠어...!";
            //(3) 손전등 (발견 전)
            Game_TypeWriterEffect.instance.EventText[2] = "어 발에 뭔가가 걸리는 것 같아.\n" +
                "달 무늬가 새겨진 손전등이야..!!이게 켜지면 조금 더 밝아질 것 같아.\n" +
                "엇! 켜졌다! 훨씬 더 나은 걸?";
            ////////////////////////////////////////////////////////////
            //(4) 비상식량 (발견 후)
            Game_TypeWriterEffect.instance.EventText[3] = "달과 별이 그려져있네.\n  무슨 맛인지 설명할 수 없지만, 맛있는 건 분명해...!\n 덕분에 허기가 사라지는 것 같아.";

            //(5) 누군가의 쪽지 (발견 후)
            Game_TypeWriterEffect.instance.EventText[4] = "어? 쪽지다. 여기엔 글이 적혀있어!!\n 런 투더 문.... ?";

            //(6) 산소통 구멍 (구멍 난 후)
            Game_TypeWriterEffect.instance.EventText[5] = "취이이익- 취이이이익-\n어라...? 이게 무슨 소리지 ?\n" +
                "산소통에 구멍이 난 것 같아...!! 산소가 빨리 닳기 시작했어!\n" +
                "산소가 부족해지기 전에 달려서 다음 휴게소에 도착해야겠어!";
            //(7) 징검다리
            Game_TypeWriterEffect.instance.EventText[6] = "어라.. 길의 간격이 너무 넓어... \n " +
                "달려서 징검다리를 뛰어넘어가자!";
        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[0] = "달 그림이 그려진 표지판이 보여. 이쪽으로 가는 게 맞겠지?";

            //(2) 비상식량 (후)
            Game_TypeWriterEffect.instance.EventText[1] = "얏호! 비상식량이다! 덕분에 배가 부르겠어.";
            //(3) 별가루 줍기 (전)
            Game_TypeWriterEffect.instance.EventText[2] = "어?! 별가루다! 아이템 함에 넣어둬야겠어.";

            //(4) 고래 꼬리 쫓아 달리기 (전)
            Game_TypeWriterEffect.instance.EventText[3] = " 저 멀리서 무언가가 다가오고 있어...\n" +
                "... 저건... 고래...?\n 고래와 무척 닮은 다른 존재이겠지?\n" +
                "어...? 고래의 끝에 무엇인가가 달려있어...! 달의 조각이다!\n " +
                "달에 도달할 때 필요해보였는데...! 저 고래를 쫓아가봐야겠어!";

            //(4-1) 고래 꼬리 쫓아 달리기 (후)
            Game_TypeWriterEffect.instance.EventText[4] = "색이 다른 달의 조각이야. 예쁘다...\n조각함에 잘 보관해야겠어.";

            //(5) 다 쓴 대형 산소통 줍기 (후)
            Game_TypeWriterEffect.instance.EventText[5] = "앗, 이건 구멍이 나있잖아? 산소가 하나도 없어.\n 다 써버린 대형 산소통이어서 아쉽다...";
            //(6) 징검다리 구간(전)
            Game_TypeWriterEffect.instance.EventText[6] = "여기는 징검다리 구간이야! 달가루를 써서 가야겠어!";
        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 대형산소통 줍기
            Game_TypeWriterEffect.instance.EventText[0] = "우와! 대형 산소통을 주웠다! 산소통에 문제가 생겼을 때 사용하면 되겠어!";

            //(2) 달의 포자에서 살아남기 (전)
            Game_TypeWriterEffect.instance.EventText[1] = "어...? 예쁘다.. 이건 뭐지...?\n" +
                "포자같이 생겼네? 달의 포자라고 불러야겠어!\n" +
                "근데 숨이 좀 부족해지는 것 같은데, 뭐지 ?\n 포자가 있는 구간은 빨리 지나쳐가야겠어!!";
            //(3) 갈림길 (후)
            Game_TypeWriterEffect.instance.EventText[2] = "표지판을 따라 걷기는 하는데, 갑자기 길이 없어지거나 하지는 않겠지?";

            //(4) 비상식량 발견 (후)
            Game_TypeWriterEffect.instance.EventText[3] = "식량이다...!! 흐음~ 좋은 냄새. 이 길에서 찾은 비상식량은 전부 다 맛있어...!\n" +
                "먹을 수 있는 게 없었다면 난 여기까지 오지 못했겠지...?";

            //(5) 달의 포자 구간 (전)
            Game_TypeWriterEffect.instance.EventText[4] = "뭐야...! 또 달의 포자가 있잖아.\n 후읍. 숨을 아껴 써야 해.\n 숨쉬기 어려워지기 전에 얼른 뛰어가자!!!";

            //(6) 비어있는 별가루 줍기 (후)
            Game_TypeWriterEffect.instance.EventText[5] = "별가루통이다! 후... 근데 텅 비어있잖아?\n 이건 필요 없을 것 같아.";

        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 비상식량 (후)
            Game_TypeWriterEffect.instance.EventText[0] = " 이건 조금 낯선 맛인 것 같아. 그래도 지구의 음식보다 훨씬 더 든든한 것 같아.\n" +
                "지구의 다른 음식과 달리 먹으면 배고픔이 늦게 오는 것 같아.";

            //(2) 우주 식물도감 (후)
            Game_TypeWriterEffect.instance.EventText[1] = " 이건 백과사전 같은 건가...?\n" +
                "우주의 생명체에 대해 기록해놓은 것 같아.\n" +
                "지구어로도 번역되어 있는 것들이 있네...?!\n" +
                "꽤 도움이 될 것 같아.";
            //(3) 별가루 줍기 (전)
            Game_TypeWriterEffect.instance.EventText[2] = "어, 저건 별가루인 것 같아! 얼른 줍자!";

            //(4) 우주꽃 흐느낌 듣기
            Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 치..치직...나를 도와줘\n/ " +
                "어 ? 이건 무슨 소리지...?\n /라디오 : 여기야..여기...\n/ 앗, 시들어가는 꽃이잖아...! 네가 나를 불렀니?\n" +
                "/라디오 : 나는 달가루가 필요해... 달가루가 없으면 예쁘게 피어있을 수가 없어.\n"
                + "나를 위해 달가루를 뿌려줄 수 있니...?\n/ " +
                "마침 오는 길에 달가루를 주웠어!\n 너를 도와주기 위해서였나봐!\n 기다려봐, 지금 바로 뿌려줄게!";
            //갈림길
            Game_TypeWriterEffect.instance.EventText[4] = "달에도 이런 안내판이 있다는 게 신기해.\n" +
                "지구와 달을 자유롭게 다녔을 때 세워둔 거겠지...?";
            //고장난 산소통
            Game_TypeWriterEffect.instance.EventText[5] = "에잇, 이건 고장난 산소통이야...";
           // (4-1) 우주꽃 흐느낌 듣기
            Game_TypeWriterEffect.instance.EventText[6] = "/라디오 : 도와줘서 고마워, 나도 너를 도와주고 싶어.\n 내가 가지고 있는 달의 조각을 가져가렴./ ";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달전)
            Game_TypeWriterEffect.instance.EventText[0] = "이번 갈림길은.... 이쪽이다. 누가 표지판을 반대로 바꾸거나 하지 않았겠지?";

            //(2) 우주 식물도감 (후)
            Game_TypeWriterEffect.instance.EventText[1] = "저 불빛들은 뭐지?....\n" +
                "마치 불꽃놀이를 보는 것 같아. 보기만 해도 마음이 따스해져\n" +
                "누가 터트리고 있는지 당장 확인하러 가야겠어!\n" +
                "어라... 자세히 보니..저 불빛은 누군가 터트리는 게 아니야.\n" +
                "운석들이 부딪혀서 나는 빛이야...\n " +
                "아름답다...";

            //(3) 횟불대 줍기 (후)
            Game_TypeWriterEffect.instance.EventText[2] = "이건 뭐지? 달이 그려진 횃불대 인것 같아...\n" +
                "이게 왜 여기 있던 걸까?\n" +
                "혹시 모르니까 챙겨가야겠어.";

            //(4) 길 사라짐으로 인한 뛰기(전)
            Game_TypeWriterEffect.instance.EventText[3] = "길이 또 사라지기 시작했어! 뛰어야 해!";
            //(5) 비상식량 발견(획득후)
            Game_TypeWriterEffect.instance.EventText[4] = "우주 통조림을 발견했어! 배고팠는데 잘됐다!";
            //(6) 산소 부족(전)
            Game_TypeWriterEffect.instance.EventText[5] = "몸이 좋지 않아서인지 숨이 잘 쉬어지지 않아...\n" +
                "숨을 너무 가쁘게 쉬었나 봐..산소가 빨리 달기 시작했어..\n" +
                "산소가 바닥나기 전에 빨리 휴게소에 가야겠어...!";
            //(7) 쓰러지기 (전)
            Game_TypeWriterEffect.instance.EventText[6] = "너무 추워... 으슬으슬해... 열도 나는 거 같아..\n" +
                "앞이 점..점 안보...이.....네.....";
        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 정신을 차리기 (전)
            Game_TypeWriterEffect.instance.EventText[0] = "분명 너무 추웠는데, 지금은 따뜻한 것 같아...\n 잠시 정신을 잃은 것 같아... 여긴 어디지.. ?\n " +
                "엄마의 품일까...? 따뜻해....";

            //(2) 산소 닳음 (전)
            Game_TypeWriterEffect.instance.EventText[1] = "여기는 달의 통로를 지날 때처럼 산소통의 공기가 빨리 닳고 있어.\n" +
                "휴게소에 들릴 때마다 반드시 산소통을 구매해야 쓰러지지 않겠어!";
            //(3) 정신을 차린 (후)
            Game_TypeWriterEffect.instance.EventText[2] = "달빛이 내 주위를 맴도는 것 같아.\n 이 빛 때문에 몸이 따뜻했구나.\n" +
                "아, 맞아.가방 안에 횃불대가 있었어...!\n 혹시 이 빛을 담아 갈 수 있으려나?\n" +
                "달빛이 옮겨붙었어! 어두운 통로를 갈 때 도움이 될 것 같아!";

            //(4) 달의 포자(전)
            Game_TypeWriterEffect.instance.EventText[3] = " 어.. 저기 흩날리는 건 달의 포자인 것 같아...!\n" +
                "산소가 부족해지기 전에 달려서 저 구간을 지나야겠어!";
            //(5) 달의 포자 (후)
            Game_TypeWriterEffect.instance.EventText[4] = "헉.. 헉... 심장이 너무 빨리 뛰어. 산소가 부족했어서 더 그런가 봐.\n" +
                "앞으로도 달의 포자는 조심해야겠어.";
            //(6) 소원석과 관련된 벽화1 (후)
            Game_TypeWriterEffect.instance.EventText[5] = "달의 신전에는 소원석이 있는 것 같아.\n" +
                "소원석에 소원을 빌면 이뤄주는 걸까...?\n" +
                "그곳에 도착하면, 지구로 돌아가게 해달라고 해야 할까 ? ";
            //(7) 소원석과 관련된 벽화2 (후)
            Game_TypeWriterEffect.instance.EventText[6] = "소원석과 관련된 벽화가 또 있네? \n" +
                "이름을 새기면 소원을 빌 수 있는 것 같은데...";
            //(8) 소원석 통로 지나가기 (전)
            Game_TypeWriterEffect.instance.EventText[7] = " 어..? 이 길의 끝에 있는 저건 뭐지...???";
            //(9) 달의 조각 발견
            Game_TypeWriterEffect.instance.EventText[8] = "달의 조각이야!!!\n" + "또 다른 색을 가졌네.정말 예쁘다.\n " +
         "이 통로로 나가면 또 어떤 곳이 나타날까...?";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 표지판 등장(발견 후)
            Game_TypeWriterEffect.instance.EventText[0] = "  저기 뭐가 보여... 표지판이다!\n" +
                "이 표지판이 없었다면 이 길을 걷는다는 느낌이 들지 않았을 거야...\n" +
                "누가 세워둔 표지판일까?";

            //(2) 달의 실타래 줍기(획득 후)
            Game_TypeWriterEffect.instance.EventText[1] = "이건 실타래야...\n" +
                "반짝반짝한 빛이 흘러나오는 게 너무 아름다워,\n" +
                "곧 필요하게 될지도 몰라, 가져가야겠어.";
            //(3) 비상식량 줍기( 획득 후)
            Game_TypeWriterEffect.instance.EventText[2] = "간간이 비상식량이 떨어져 있어서 다행이야.\n" +
                "너무 배고팠어... 급하게 먹으면 체할지도 모르니 천천히 먹어야겠어.";

            //(4) 대형 산소통 줍기(획득후)
            Game_TypeWriterEffect.instance.EventText[3] = "줍기 전에는 전처럼 구멍 난 대형 산소통일까 했는데...\n 이건 산소가 가득한 산소통이야!\n" +
                "지금 건강이 좋지 않지만... 오늘 운은 정말 좋은 편인 것 같아";
            //(5) 갈림길(도달 전)
            Game_TypeWriterEffect.instance.EventText[4] = " 또 갈림길이야... 이젠 적응이 된 것 같아. 이쪽으로 가야 해.";
            //(6) 별가루 줍기(획득 후)
            Game_TypeWriterEffect.instance.EventText[5] = "반짝거려... 계속 이 빛만 바라보고 싶어.\n 챙겨둬야겠어. 유용하게 쓰일 거야.";
            //답답함 떨치기 2022.02.15
            Game_TypeWriterEffect.instance.EventText[6] = "이곳은 답답해...";
        }
        //룬과의 만남
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 영혼 등장 (전)
            Game_TypeWriterEffect.instance.EventText[0] = " /라디오: 치직 - 안녕...? 치지직 / \n" +
                "/라디오: 나는 빛을 잃은 영혼이야.\n 우주꽃 열 송이를 모아서 나에게 주면, 나는 다시 빛을 찾을 수 있어.\n" +
                "하지만, 난 더 이상 꽃을 찾을 힘이 없어.\n 이러다가 곧 사라지고 말거야. 너무 무서워./\n" +
                "내가 모아줄게, 나와 함께 가자.\n" +
                "/라디오: 하지만 나는 더 이상 움직일 힘이 없어.../";

            //(1-2) 영혼 등장 (후)
            Game_TypeWriterEffect.instance.EventText[1] = "그건 걱정하지마... 좋은 생각이 났어.";
            Game_TypeWriterEffect.instance.EventText[2] = "너와 나의 팔에 실을 묶어서 연결했어.자 이제 됐지?";


            //(2) 비상식량
            Game_TypeWriterEffect.instance.EventText[3] = "이번 통조림은... 어떤 맛일까?\n" +
                "매번 어떤 비상식량을 찾게 될지 기대하게 되는 것 같아.\n" +
                "이렇게 기대를 하다 보면... 실망도 커질 텐데 말이야.";

            //(3) 우주꽃을 모으기(전)
            Game_TypeWriterEffect.instance.EventText[4] = "우주 꽃 열 송이를 다 모았어... 여기 꽃을 받아.\n" +
                "/라디오: 고마워!/";
            //(3-1) 우주꽃을 모으기(후)
            Game_TypeWriterEffect.instance.EventText[5] = "네게서 빛이 나기 시작했어...! 눈부셔! \n" + "/라디오: 너로 인해 새롭게 태어났어. 고마워!/ \n ...! 이게 네 원래 모습이구나!\n" +
                "/라디오: 응! 네 덕분에 원래 모습을 되찾았어. \n 라디오: 감사의 인사로 내가 가지고 있던 달의 조각을 줄게.\n 라디오: 내가 도와줄 수 있는 건 없을까?/ \n" +
                "사실... 혼자 가는 길이 너무 외로워. 네가 괜찮다면 나와 함께 걸어줄래 ?\n" +
                "/라디오 : 좋아, 너의 도착지까지 함께 동행할게./ ";
            //(4) 갈림길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[6] = "갈림길이야... 이쪽으로 가도 되겠지?";
            //(5) 별과의 동행 (후)
            Game_TypeWriterEffect.instance.EventText[7] = ".. 있잖아. 너와 함께하게 되어서 너무 기뻐.\n 나는 오랫동안 이 길에서 혼잣말을 했거든.\n" +
                "/라디오: 내가 너의 말을 들어줄께/ \n" +
                "이렇게 대답이 돌아오는 대화도 오랜만이야. 든든한걸!\n" +
                "/라디오: 너는 외로움을 많이 타는구나? 어떻게 외로움을 견뎠니 ?/ \n" +
                "엄마와 친구와 함께했던 추억을 생각하고 함께할 미래를 상상 했어!\n" +
                "/라디오: 너는 주변 사람들을 사랑하는구나.\n 너의 말 한마디에 그들에 대한 애정이 묻어나./ ";

            //(6) 달의 포자 길 건너기(발견 후)
            Game_TypeWriterEffect.instance.EventText[8] = "... 빛나는 포자야. 여기에 오기 전에 이런 곳을 지나왔었어.\n" +
                "예쁘게 빛나지만 산소통의 산소를 빨아들여서 오래 있을 수 없어...\n" +
                "이곳을 빨리 지나가야겠어.";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1)갈림길(도달 전)
            Game_TypeWriterEffect.instance.EventText[0] = "네가.... 그러고 보니 영혼아 네게도 이름이 있니?\n" +
                "/라디오: 응, 나의 이름은 룬이야./ \n 예쁜 이름이구나, 룬 내가 이쪽으로 가는 걸 어떻게 생각해 ?\n /라디오 : 좋은 선택인걸./ ";
            //(2)비상식량(획득 후)
            Game_TypeWriterEffect.instance.EventText[1] = "그러고 보니 룬은 이런 비상식량을 먹어본적이 있니?\n" +
                "/라디오: 아니, 네가 먹어보고 어떤 건지 설명해줘./ \n" +
                "음 어떻게 설명을 해줘야 할까... 고민할 시간을 줘!";

            //(3)징검다리 구간(도달 전)
            Game_TypeWriterEffect.instance.EventText[2] = "이크, 징검다리 구간이야. 달가루를 사용해야겠다.";

            //(4)빛새(발견후)
            Game_TypeWriterEffect.instance.EventText[3] = "빛나는 새야! ...어라? 새가 아닌가? 동그랗게 생겼네.\n" +
                "빛이 날아다니는 것 같아!";
            //(5)크리스탈 결정체(도달 후)
            Game_TypeWriterEffect.instance.EventText[4] = "우와... 크리스탈이가득해... 그러고 보니 달의 조각을 닮았어\n" +
                "이 광석들 혹시 달의 조각 아니야 ?\n" +
                "/라디오 : 맞아, 누군가의 진실한 마음이 담겨야만 이 광석이 달의 조각이 될 수 있어./ \n" +
                "그럼 내가 모은 조각들은 모두, 마음이 담긴 조각들이었구나..마음이 든든해졌어.";
            //(6)길의 차가움(전)
            Game_TypeWriterEffect.instance.EventText[5] = "이곳에 계속 있으니까 점점 몸이 시려와...\n 온도가 많이 낮은 곳인가 봐\n" +
                "/라디오: 어떻게 할 거야 ?/ \n 달릴 거야, 이곳에 오래 있으면 안 될 것 같은 기분이 들어...\n 날 놓치지 말고 잘 따라와!";

        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 맵 진입 후 바로
            Game_TypeWriterEffect.instance.EventText[0] = "눈처럼 하얀 입자가 날리는 곳이네. 예쁘다.";
            //(2) 갈림길 (전)
            Game_TypeWriterEffect.instance.EventText[1] = "갈림길이네. 룬, 그림이 좀 희미하지만 이쪽 방향이 맞는 것 같지?\n" +
                "/라디오 : 응.달이 그려져있잖아!/\n 이 머나먼 길도 너와 함께하니 즐거워\n /라디오 : 나도 그래, 특별한 추억이 될 것 같아./ ";
            //(3) 비상 식량 (후)
            Game_TypeWriterEffect.instance.EventText[2] = "별 그림이 많은 비상식량이야.\n 톡톡 튀는 맛이 아주 신기하네? 지구에는 없는 맛이야.\n " +
                "/라디오 : 나도 지구라는 곳이 궁금해. 늘 멀리서만 봐왔거든./ \n" +
                "너와 함께 지구로 돌아가면 좋겠어. 내가 보여주고 싶은 게 너무 많아. ";
            //(4) 별가루 줍기 (후)
            Game_TypeWriterEffect.instance.EventText[3] = "별가루통이야. 반짝반짝 빛나는 성분이 들어있나 봐.\n " +
                "/라디오 : 이 별가루를 지구로 가져가면 재로 변해버린데.\n 우주에 있을 때만 빛날 수 있어./ \n " +
                "정말 ? 지구의 사람들에게 보여주고 싶었는데... 아쉽다.";
            //(5) 목걸이 발견 (전)
            Game_TypeWriterEffect.instance.EventText[4] = "저기 유독 빛나는 게 있는데... 저게 뭐지?";
            //(6) 목걸이 발견 (후)
            Game_TypeWriterEffect.instance.EventText[5] = "어? 이건 목걸이잖아...? 달의 무늬가 새겨져있네.\n" +
                "/라디오 : 누군가가 이 길을 걸어가다 잃어버렸나 봐./ ";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {
            Game_TypeWriterEffect.instance.EventText = new string[8];
            //(1) 맵 진입 후 바로
            Game_TypeWriterEffect.instance.EventText[0] = "저기 보이는 거 산소통 맞지? 역시!\n 룬은 산소가 필요 없으니 편할 것 같아.\n" +
                "/라디오 : 나는 너와 달리 이곳에서 태어나고 자랐으니까/ ";
            //(2) 갈림길 (전)
            Game_TypeWriterEffect.instance.EventText[1] = "룬, 이번에는 표지판이 거의 보이지 않아... 같이 길을 선택해 줘\n" +
                "/라디오: 저기 바닥에 희미하게 달 그림이 보이는 것 같아./ \n" +
                "그럼 저기다! 저기로 가야겠어!";
            //(3) 징검다리 길 (도달 전)
            Game_TypeWriterEffect.instance.EventText[2] = "징검다리야, 이곳은 그냥 지나가기 힘들겠어... 달가루를 사용하자.\n" +
                "룬 너는 필요 없어 ?\n" +
                "/라디오 : ....나는 치이이이이익 - 픽 -/ ";
            //(4) 우주라디오 켜기(전)
            Game_TypeWriterEffect.instance.EventText[3] = "룬? ..... 라디오가 고장 났나!?\n" +
                "뭐 ? 아니라고 ? 아! 라디오의 달가루가 다 소모됐구나!";
            //(4-1) 우주라디오 켜기(후)
            Game_TypeWriterEffect.instance.EventText[4] = "너와 다시 대화를 못하게 될까 봐 놀랐었어.\n" +
                "/라디오 : 그래도 침착하게 잘 대처했어./ \n" +
                "맞아! 그거 알아 룬?\n  언제부턴가 라디오에서 지구의 소식이 아닌\n 너와의 대화에만 집중한 거 있지!\n" +
                "/라디오 : 영광인걸, 자 다시 힘내서 가자.";
            //(5) 별자리 소개(발견 후)
            Game_TypeWriterEffect.instance.EventText[5] = " ! 저기 별자리들이 있어.\n 우리별에서 물병자리라고 부르는 별자리야.\n" +
                "/라디오 : 지구에서도 우주를 제대로 보고 있구나./ \n" +
                "당연하지! 사람들은 우주를 사랑하는걸!\n" +
                "/라디오 : 부끄럽지만 저 푸른 별은 볼게 너무 많아서\n 우주를 볼 시간이 없을 거라 생각했어./ \n" +
                "하하 그럴 리 없지! 우주가 훨씬 넓은걸?\n 저기 봐, 저 별자리는 오리온자리야!\n" +
                "/라디오 : 들떠 보이네./ \n" +
                "당연하지, 이렇게 가까이서 별자리를 보게 될 줄 몰랐어.\n어라 저 별자리는 처음 보는 별자리야.\n" +
                "/라디오 : 아, 저 별자리는 작아서 우주에서만 볼 수 있을 거야.\n 네가 괜찮다면 내가 소개해줄께/ \n" +
                "당연히 괜찮지! 하나씩 다 소개해 줘!";
            //(6) 맹독의 달의 포자(포자 길 걷기 전)
            Game_TypeWriterEffect.instance.EventText[6] = "또 포자야... 산소를 빼앗기기 전에 빨리 벗어나야겠어.\n" +
                "여기는 다른 포자와 다르게 붉은색을 띠고있어..\n뭔가 다른걸까? 괜히 더 불길한걸..";
            //(7) 맹독의 달의 포자(걷는 중) + 쓰러짐
            Game_TypeWriterEffect.instance.EventText[7] = "걷고 있는데... 몸에 힘이 잘 안 들어가....\n" +
                "... 이런... 왜 자꾸 눈이 감기지... 안되는데....\n 이 포자에... 다른 포자랑 다른게 있나 봐.....\n" +
                "...참아야해... 빨리 벗어나야 하는데........눈이 감겨... 졸린 것 같아...\n" +
                "........";
        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //((1) 눈 뜸
            Game_TypeWriterEffect.instance.EventText[0] = "! 여기는 ....어디지? 룬? ...어디 있어? \n ... 여기는 우주가 아닌 것 같아.별도 달도 보이지 않아..\n " +
                "지구의 물건들이야...여긴 지구인가?..\n 분명....우주의 길을 걷다가 마지막에 포자 길을 걸은 뒤로 기억이 없어...\n" +
                "언제부터 이곳에 있었던 거지? ..일단...걸어볼까 ? ";
            //(2) 내가  쓰던 침대( 발견 후)
            Game_TypeWriterEffect.instance.EventText[1] = "어! 저건 내가 썼던 침대야...\n 왜 이런 곳에 있지? 누가 가져다 둔 걸까?\n" +
                "누워자고 싶어....\n가까이 다가가니 침대가 사라지잖아...\n 실제로 있는 게 아니야...\n" +
                "... 매일 밤 동화를 읽어주던 엄마가 그리워.\n 그 날들이 가장 행복한 날들이었던 것 같아.\n" +
                "저 침대는 내가 아플 때에도, 잠을 잘 때에도 늘 있었는데...\n" +
                "엄마가 이불빨래를 하는 날이면\n 침대에서도 뽀송한 햇빛 향이 가득했었어.\n 하지만 누울 수 없네...";

            //(3) 즐겨먹던 아이스크림( 발견 후)
            Game_TypeWriterEffect.instance.EventText[2] = "저건 내가 제일 좋아하는 아이스크림이잖아!\n" +
                "아마... 저것도 다가가면 사라지겠지?\n ...희망고문이라는 단어는 이럴 때 쓰는 말인가 봐.\n" +
                "저 아이스크림에도 추억이 있어.\n 놀이터에서 주운 동전을 모아 사 먹었던 기억도 있어\n" +
                "하지만 무엇보다 엄마랑 외출했다\n 사주신 아이스크림이 가장 맛있었지.\n" +
                "사자마자 떨어트렸을 때는 너무 서러워서 울었지만...\n 지금 생각하니 그렇게 울 일은 아니었던 것 같아.\n" +
                "어렸을 때 친구랑 돈을 모아 한 개를 사곤\n 한입씩 나눠먹었던 적도 있어.";

            //(4) 가장 좋아하는 책( 발견 후)
            Game_TypeWriterEffect.instance.EventText[3] = "이건... 내가 좋아한 책이잖아.\n 나는 이 책을 몇 번이고 읽었어.\n" +
                "친구들은 같은 책을 계속 읽으면\n 부모님들이 좋아하지 않는다 했지만\n 우리 엄마는 별말씀을 하지 않으셨어\n" +
                "이 책은... 작은 아이의 이야기야.\n 그 아이가 회색 신사들로부터 마을을 구하지!\n" +
                "엄마도 이 책을 좋아했어.\n 내가 몰라 접어둔 페이지가 가끔 펴져있는 것을 보았을 때\n 엄마도 이 책을 읽고 있구나 싶었지!\n" +
                "아껴서 소중하게 읽었지만 책의 모퉁이가 닳아버렸어.\n" +
                "앗 이건 이 책도 좋아했어.\n 고슴도치의 이야기야.\n 혼자 있던 고슴도치가 친구를 가득 사귀는 책이었지...\n" +
                "다시 읽어보고 싶은데...\n 이곳에 오기 전에 많이 읽을 걸 그랬어.";
            //(5) 사진앨범(발견 후)
            Game_TypeWriterEffect.instance.EventText[4] = "앨범이야! 그것도 우리 집 앨범!\n 엄마의 사진을 볼 수 있을지 몰라!\n" +
                "... 손을 대면 사라져...\n 안의 사진은 볼 수 없는 걸까?...\n 어! 저절로 펼쳐지고 있어.\n" +
                "아, 나의 아기 때 모습이야..\n 엄마의 얼굴은 나오지 않았지만 날 안고 있는 저 팔은 엄마야.\n" +
                "이건 처음으로 유치원에서 상을 받았을 때야.\n뿌듯해서 상장을 들고 엄마에게 사진을 찍어달라 했었어.\n" +
                "엄마야! 앗 너무 빨리 페이지가 넘어가버렸어...\n" +
                "엄마의 모습을 보니..더욱 보고 싶어,\n 가슴이 답답하고 슬픈 기분이 들어...\n 많이 그리운가 봐.\n" +
                "엄마와 어떻게 이별을 했었는지 떠오르지 않아,\n 내가 여기 잘 있다는 걸 엄마가 알아야 하는데.\n" +
                "여긴 꿈의 공간인가봐...\n 나의 기억들로 가득하잖아.\n" +
                "... 뒤로 갈 수 록... 혼자찍은 사진이 많네,\n 엄마가 사진을 찍어주고 있겠지?\n" +
                "병원 침대는 왜 찍은거지? \n...이부분에 대한 기억은 없어. 분명 몇번이고 봤었는데...\n" +
                "..앨범이 끝났어. ";
            //(6) 꿈에서 깨기 위해 달려라 (달리기 전)
            Game_TypeWriterEffect.instance.EventText[5] = "... 역시 이곳은 꿈이야, 가도 가도 똑같은 길이 이어져있어...\n" +
                "무엇보다 아무리 걸어도 다리가 아프지 않아\n" +
                "이곳에서 나가고 싶지 않지만... 깨어나야 할 것 같아... 그런 예감이 들어.\n" +
                "벗어나려면 어떻게 해야 하지? 여기는 어떻게 들어온거지?\n" +
                "... 이때까지 위기를 벗어난 방법을 생각하면 답은 한 개야.\n" +
                "달려야겠어";
            // (5) 바람개비(발견 후)
            Game_TypeWriterEffect.instance.EventText[6] = "바람개비야! \n 공원에서 발견하고 엄마한테 사달라고 떼를 썼었어. \n" +
        "한동안 어딜가든 이 바람개비와 함께였지!\n" + "하하, 이름이 없는게 흠이구나! \n 바람개비에 이렇게 그리운 기분이 들줄 몰랐어. \n" +
        "열심히 가지고 놀았는데 언제부턴가 사라져버렸어, \n 왜 그렇게 금방 잊어버렸을까 ? \n" +
         "난 이 바람개비가 내 생각보다 훨씬 마음에 들었나봐. \n 이렇게 기억들이 나는걸 보면 말이야.\n" +
        "몇몇의 어른은 별 것 아닌것에 추억하는데 시간을 쏟지 말라고 해. \n 그건 어떻게 구별하는 거야?";
        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.EventText = new string[8];
            //(0) 깨어남
            Game_TypeWriterEffect.instance.EventText[0] = "/라디오 : 괜찮아? 꿈을 많이 꾼 것 같았어./\n" +
                "... 누구... 아! 룬! 나 깨어났어. 잠들어 버렸구나.\n" +
                "/라디오 : 응, 이제 포자들은 없어졌어.기운을 차리면 다시 걷자/\n" +
                "너무 그리운 꿈을 꾸었던 것 같아.어라 달의 조각이야.\n" +
                "/라디오 : 꿈에서 얻었나 봐, 달의 조각은 어디에도 있을 수 있거든!/ ";
            //(1) 갈림길(도달 전)
            Game_TypeWriterEffect.instance.EventText[1] = "갈림길이야, 꿈속에서는 갈림길이 없었는데. 돌아온 게 실감 나.\n" +
                "이쪽 길인 것 같아.";
            //(2) 비상식량(획득 후)
            Game_TypeWriterEffect.instance.EventText[2] = "통조림이네!, 사실 꿈을 꾸면서 지구에서 먹었던 아이스크림을 보았어.\n" +
                "/라디오 : 아이스크림이 뭐야?/\n" +
                "디저트라고 하는데 음..밥으로 먹지 않고 간식으로 먹어!\n 아마 이곳에선 먹을 수 없겠지?\n" +
                "/라디오 : 아마도, 통조림은 많이 봤지만\n 이외의 다른 생명체들이 먹을 수 있는 건 본 적 없는 것 같아./ ";
            //(3) 징검다리(도달 전)
            Game_TypeWriterEffect.instance.EventText[3] = "징검다리야, 달가루를 사용해서 건너야겠어!\n" +
                "룬 가방에 있는 달가루를 꺼내줘!";
            //(4) 대형 산소통 줍기(획득 후)
            Game_TypeWriterEffect.instance.EventText[4] = "산소통이야! 그것도 큰 산소통. 지금 내게 가장 필요한 거야.\n" +
                "룬 내가 이 길을 걸으면서 가장 필요한 요소가 세 가지가 있는데 뭔지 알아?\n" +
                "/라디오 : 흠..너무 어려운데? 하나는 산소야, 다른 하나는... 통조림 ?/\n" +
                "맞아! 똑똑한걸, 그리고 남은 하나는 룬, 바로 너야!";
            //((5) 하늘섬(전)
            Game_TypeWriterEffect.instance.EventText[5] = "저기 ...하늘섬에 무슨 일이 일어나고 있는 것 같아.\n" +
                "이 소리는 뭐지? 이런 파편들이 떨어진다!!\n" +
                "/라디오 : 섬에 문제가 생겼나 봐./\n" +
                "응 그런 것 같아...\n 이 정도면 섬의 파편이 길에도 떨어질 것 같아...\n이곳을 벗어나야겠어.\n" +
                "파편에 맞을 뻔했어...! 조심해야 해!";
            //(5-1) 하늘섬(후) ---사용안함
            Game_TypeWriterEffect.instance.EventText[6] = "응 그런 것 같아... 이 정도면 섬의 파편이 길에도 떨어질 것 같아... 이곳을 벗어나야겠어.\n" +
                "파편에 맞을 뻔했어...!조심해야 해!";
            //(6) 산소통 구멍
            Game_TypeWriterEffect.instance.EventText[7] = "앗! 산소통에 섬에서 떨어진 파편에 맞아서 구멍이 났어.\n" +
                "/라디오 : 괜찮아? 버틸 수 있겠어?/\n" +
                "산소를 채워도 채워도 금방 빠져나가... 산소가 부족할 것 같아.\n" +
                "최대한 빨리 달려야 해!";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //룬의 몸이 깜빡임(시작시)
            Game_TypeWriterEffect.instance.EventText[0] = "... ! 룬 네 몸이 깜빡이고 있어!... 무슨 일이지..?\n" +
                "꽃이 더 필요한 거야 ? ..이럴 때만 대답을 안 하는구나.";
            //(1) 비상식량)
            Game_TypeWriterEffect.instance.EventText[1] = "길에서 만나서 반가운걸? 룬, 비상식량! 마침 배고팠는데 잘 됐어.\n" +
                "/라디오 : 배고픔을 몰라서 네 기분을 다 이해할 수 없어서 아쉽다./ ";
            //(2) 갈림길
            Game_TypeWriterEffect.instance.EventText[2] = "이제는 갈림길을 선택하는 게 많이 무섭지 않아.\n" +
                "여전히 불안함은 있지만... 벌써 달이 이렇게 가까워졌는걸.";
            //(3) 파란빛 영혼 만나기(발견 후)
            Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 저기, 누군가 있어/\n" +
                "... ! 저 영혼은 푸른빛을 띠고 있어. 망은.. 꽃을 구하는 중인가?\n" +
                "/라디오 : 아마도 별을 구하고 있을 거야.\n 파란빛의 영혼은 색을 잃은 별들을 거두어 영혼을 만들어. \n 그를 도와주면 좋은 일이 생길 거야./ ";
            //((4) 잠자리망(만난 후)
            Game_TypeWriterEffect.instance.EventText[4] = " 파란빛 영혼아, 내가 도와줄까?\n" +
                " /라디오 : 성수기였는데 네가 도와준다니 다행이야.\n 라디오 : 이 망으로 색을 잃어 떨어지는 별을 담아줘/ ";
            //(5) 별가루 줍기(획득 후)
            Game_TypeWriterEffect.instance.EventText[5] = "별을 모으려고 했더니 별가루가 떨어져 있네!\n" +
                "이 별가루는 남은 길에 유용하게 쓰일 거야, 룬도 그렇게 생각하지?\n " +
                "/라디오: ......치지직 - / ";
            //(6) 별 모으기(모은 후)
            Game_TypeWriterEffect.instance.EventText[6] = "우와, 바쁘게 별들을 잡다 보니 벌써 30개를 모두 모았어!\n" +
                "/라디오: 고마워, 네 덕분에 일이 훨씬 줄어줄었어./\n" +
                "네게 도움이 되어서 다행이야,\n 덕분에 이 길을 지루하지 않게 걸을 수 있었어.\n" +
                "/라디오: 흠, 혹시 달가루 좀 있어?/\n" +
                "필요하니 ? 방금 주운 달가루가 있을 거야\n" +
                "/라디오: 그럼 그 달가루를 네가 모은 별들에 뿌려봐,\n 특별한 일이 일어날 거야./\n" +
                "그럼...! 뿌려볼게!";
            //((7) 별 모으기(달가루를 뿌린 후)
            Game_TypeWriterEffect.instance.EventText[7] = "...! 별들이 빛나고있어 !  달의 조각이야!";
            //((8) 달의 조각 획득
            Game_TypeWriterEffect.instance.EventText[8] = "/라디오: 이 달의 조각은 네가 가지도록 해. 감사의 선물이야.\n  이제 얼마 남지 않았어. 힘내/ ";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 비상식량(후)
            Game_TypeWriterEffect.instance.EventText[0] = "달이 그려져 있는 식량이야. 식량 중에 제일 맛있는 것 같아!";
            //(2) 갈림길 (전)
            Game_TypeWriterEffect.instance.EventText[1] = "이쪽으로 가면 되겠다.\n 표지판이 없었다면, 정말 어쩔 뻔했을까. 아찔해.\n" +
                "룬, 달에 거의 다 와가는 것 같아.\n " +
                "/라디오 : 너와 함께 여기까지 올 수 있어서 기뻐!/ \n " +
                "이 길의 끝에서 우리는 헤어져야 하는 거야?\n " +
                "/라디오 : 헤어짐의 끝에는 또 다른 만남이 있잖아. 너무 슬퍼하지 마./ ";
            //(3) 조각의 문 (전)
            Game_TypeWriterEffect.instance.EventText[2] = "이건 처음 보는 문인데...? \n우주 휴게소 게이트와는 달라!\n" +
                "/라디오 : 가지고 있는 달의 조각을 끼워봐./\n" +
                "룬, 달의 조각 10개가 있어야 열리는 것 같아.\n그런데 난 한 조각이 부족해...\n" +
                "/라디오 : 치칙...치지이이익/\n" +
                "룬, 내 말 듣고 있어? 문을 열 수가 없어.";

            //(3) 우주라디오 부서짐 (전)
            Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 치..치지직....치지직 치지이이익 -/ \n" +
                "뭐야, 갑자기 라디오가 왜이러지?\n 룬의 목소리를 들을 수가 없어.\n" +
                "/라디오 : 치...치지지 - 이이익 -/\n" +
                "탁탁!! 이 중요한 시점에 고장 나버리면 어떡해...!!";
            ;
            //(4) 우주라디오 부서짐 (후)
            Game_TypeWriterEffect.instance.EventText[4] = "제발!! 탁탁!!! 엇 ? !라디오를 떨어뜨렸어....!!! 콰지직\n" +
                "어.. ? 이건 ?! 달의 조각이야! 룬!! 보고 있어? \n" +
                "라디오가 고장 나서 너의 목소리를 들을 수 없어.";
            //조각의 문 앞에서 
            //(4) 룬과 이별
            //"(룬이 핑그르르 내 주위를 돈다)"
            Game_TypeWriterEffect.instance.EventText[5] = "룬, 나에게 작별 인사하는거야...? 흑.\n" +
                "나와 여기까지 함께해 줘서 고마워. 절대 잊지 못할 거야.";

            // "(룬이 제자리에서 핑그르르 돌며 사라진다.)";
            //(5) 조각 끼우기
            Game_TypeWriterEffect.instance.EventText[6] = "조각함에서 버튼을 누르면 문이 열리나 봐...!!\n" +
                "아앗...! 눈부셔!";

            // 명예의 전당에서 사용되는 나레이션
            //(5) 조각 끼우기
            Game_TypeWriterEffect.instance.EventText[7] = "조각함에서 버튼을 누르면 문이 열리나 봐...!!\n" +
                "아앗...!눈부셔!";
            //(6) 산소통 구멍 (구멍 난 후)
            Game_TypeWriterEffect.instance.EventText[8] = "취이이익- 취이이이익-\n어라...? 이게 무슨 소리지 ?\n" +
                "산소통에 구멍이 난 것 같아...!! 산소가 빨리 닳기 시작했어!\n" +
                "산소가 부족해지기 전에 달려서 다음 휴게소에 도착해야겠어!";
        }
    }
    //랜덤 나레이션
    public void EventRandomTextList(int wayPoint)
    {
        Game_TypeWriterEffect.instance.RandomText = new string[50];
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "여긴 어딜까…?";
            Game_TypeWriterEffect.instance.RandomText[1] = "이 길의 끝에는 뭐가 있을까?";
            Game_TypeWriterEffect.instance.RandomText[2] = "저 반짝이는 불빛들은 뭘까?";
            Game_TypeWriterEffect.instance.RandomText[3] = "우와 예쁘다…";
            Game_TypeWriterEffect.instance.RandomText[4] = "…";
            Game_TypeWriterEffect.instance.RandomText[5] = "산소가 떨어지면 어떻게 되는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[6] = "여기 아무도 없나요…?";
            Game_TypeWriterEffect.instance.RandomText[7] = "하아.. 조금 추운 것 같기도…";
            Game_TypeWriterEffect.instance.RandomText[8] = "어 이건 뭐지?";
            Game_TypeWriterEffect.instance.RandomText[9] = "아 배고프다.";
            Game_TypeWriterEffect.instance.RandomText[10] = "지금은 몇 시일까?";
            Game_TypeWriterEffect.instance.RandomText[11] = "오늘은 몇 월 며칠일까?";
            Game_TypeWriterEffect.instance.RandomText[12] = "할 수 있는게 걷는 것 밖에 없어.";
            Game_TypeWriterEffect.instance.RandomText[13] = "우주엔 신기한 게 많이 있구나";
            Game_TypeWriterEffect.instance.RandomText[14] = "나는 어쩌다 이곳에 온 거지?";
            Game_TypeWriterEffect.instance.RandomText[15] = "... 기억 나는 게 많이 없어.";
            Game_TypeWriterEffect.instance.RandomText[16] = "기억들이 희미해...";
            Game_TypeWriterEffect.instance.RandomText[17] = "고요하다는 건 우주를 위한 말인가 봐";
            Game_TypeWriterEffect.instance.RandomText[18] = "소리가 없는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[19] = "혼잣말이 늘어났어";
            Game_TypeWriterEffect.instance.RandomText[20] = "지구에 있는 것들을 계속 생각해";
            Game_TypeWriterEffect.instance.RandomText[21] = "생각하지 않다 보면 잊어버릴 것 같은 게 있어";
            Game_TypeWriterEffect.instance.RandomText[22] = "그때 보았던 무지개를 다시 볼 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[23] = "국수가 먹고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[24] = "수금지화목토천해...";
            Game_TypeWriterEffect.instance.RandomText[25] = "저게 수성일까?";
            Game_TypeWriterEffect.instance.RandomText[26] = "금성은 영어로 비너스라고 한대";
            Game_TypeWriterEffect.instance.RandomText[27] = "만약 우주에 비가 내렸다면...";
            Game_TypeWriterEffect.instance.RandomText[28] = "우주에 생명이 가득했을 거야.";
            Game_TypeWriterEffect.instance.RandomText[29] = "보이지 않네";
            Game_TypeWriterEffect.instance.RandomText[30] = "흐흐흥, 흐흥~♪";
            Game_TypeWriterEffect.instance.RandomText[31] = "가끔 친구가 흥얼거리던 노래가 그리워";
            Game_TypeWriterEffect.instance.RandomText[32] = "즐거울 때 있었던 일은 오래 기억에 남는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[33] = "어디에나 있을 것 같은 것들이 생각나.";
            Game_TypeWriterEffect.instance.RandomText[34] = "비둘기가 우주에 없는 건 의외다.";
            Game_TypeWriterEffect.instance.RandomText[35] = "길의 양쪽에 가로등과 나무를 심으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[36] = "자동차가 있으면 이 길을 금방 지날 수 있겠지?";
            Game_TypeWriterEffect.instance.RandomText[37] = "평소에는 내가 어떤 생각을 하는지 몰랐어";
            Game_TypeWriterEffect.instance.RandomText[38] = "이 짧은 시간에도 별만큼의 생각을 하는구나,";
            Game_TypeWriterEffect.instance.RandomText[39] = "어린 왕자가 이런 기분이었을 거야";
            Game_TypeWriterEffect.instance.RandomText[40] = "나의 장미는 어디에 있을까?";
            Game_TypeWriterEffect.instance.RandomText[41] = "내일도 오늘과 같을까?";
            Game_TypeWriterEffect.instance.RandomText[42] = "우주는 영원히 변하지 않을 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[43] = "지금도 변하고 있겠지.";
            Game_TypeWriterEffect.instance.RandomText[44] = "지구는 오늘도 많은 일들이 일어나고 있겠지";
            Game_TypeWriterEffect.instance.RandomText[45] = "이게 사실 꿈이라면... 생생한 꿈을 자랑해야지!";
            Game_TypeWriterEffect.instance.RandomText[46] = "꿈이 아니야...";
            Game_TypeWriterEffect.instance.RandomText[47] = "지금이 최악의 상황은 아니라 생각해";
            Game_TypeWriterEffect.instance.RandomText[48] = "여행을 하고 있는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[49] = "이 발걸음이 여정의 시작인 거지";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "누구 없어요?";
            Game_TypeWriterEffect.instance.RandomText[1] = "....";
            Game_TypeWriterEffect.instance.RandomText[2] = "이상한 것들이 떠다녀...";
            Game_TypeWriterEffect.instance.RandomText[3] = ".... 갑자기 레이저 같은 걸 쏘지 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[4] = "저기요?...";
            Game_TypeWriterEffect.instance.RandomText[5] = "저는 선량한 소년이에요.. 쏘지 말아요..";
            Game_TypeWriterEffect.instance.RandomText[6] = "배고파.. 먹을 것이 있다면..";
            Game_TypeWriterEffect.instance.RandomText[7] = "빵집에서 나는 고소한 빵 냄새가 그리워";
            Game_TypeWriterEffect.instance.RandomText[8] = "친구에게 그때 그렇게 말하는 게 아니었어";
            Game_TypeWriterEffect.instance.RandomText[9] = "미안해...";
            Game_TypeWriterEffect.instance.RandomText[10] = "... 라디오의 소리가 끊겼어.";
            Game_TypeWriterEffect.instance.RandomText[11] = "이대로 아무도 만나지 못하면 어떻게 하지";
            Game_TypeWriterEffect.instance.RandomText[12] = "이 길에 끝이 없는 건 아닐까?";
            Game_TypeWriterEffect.instance.RandomText[13] = "평생 이대로 걷기만 하면 어떡해";
            Game_TypeWriterEffect.instance.RandomText[14] = "누가 도와주세요!";
            Game_TypeWriterEffect.instance.RandomText[15] = "여긴 너무 추워요...";
            Game_TypeWriterEffect.instance.RandomText[16] = "우주 속의 나는 너무 초라해...";
            Game_TypeWriterEffect.instance.RandomText[17] = "이 두려움을 말할 사람이 없다니..";
            Game_TypeWriterEffect.instance.RandomText[18] = "곁에 아무도 없어...";
            Game_TypeWriterEffect.instance.RandomText[19] = "엄마와 찍은 사진이 있으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[20] = "... 엄마는 어떤 사람이었더라?";
            Game_TypeWriterEffect.instance.RandomText[21] = "기억이 날듯 말듯해";
            Game_TypeWriterEffect.instance.RandomText[22] = "사실 나는 여기에 평생 있었던 게 아닐까?";
            Game_TypeWriterEffect.instance.RandomText[23] = "그때 널 오해해서 상처받게 했어";
            Game_TypeWriterEffect.instance.RandomText[24] = "... 내일이 올까?";
            Game_TypeWriterEffect.instance.RandomText[25] = "이 길이 옳은지 아무도 알려주지 않아...";
            Game_TypeWriterEffect.instance.RandomText[26] = "미로 속에서 뱅글뱅글 돌고 있는 거라면..";
            Game_TypeWriterEffect.instance.RandomText[27] = "라디오야 말해봐, 지구의 소식을 전해줘";
            Game_TypeWriterEffect.instance.RandomText[28] = "고장 났나 봐...";
            Game_TypeWriterEffect.instance.RandomText[29] = "춥고 배고파...";
            Game_TypeWriterEffect.instance.RandomText[30] = "누가 따라오는 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[31] = ".... 따라오지 마요!";
            Game_TypeWriterEffect.instance.RandomText[32] = "도망가야 해...";
            Game_TypeWriterEffect.instance.RandomText[33] = "무서워...";
            Game_TypeWriterEffect.instance.RandomText[34] = "외로워.. 너무 외로워...";
            Game_TypeWriterEffect.instance.RandomText[35] = "누군가, 얘기를 나눌 딱 한 명만 있다면";
            Game_TypeWriterEffect.instance.RandomText[36] = "혼자서는 할 수 없어 ...";
            Game_TypeWriterEffect.instance.RandomText[37] = "다른 라디오는 없을까?";
            Game_TypeWriterEffect.instance.RandomText[38] = "넌 불량품이어서 버려진 걸 거야!";
            Game_TypeWriterEffect.instance.RandomText[39] = "힘들어... 언제 끝이 나는 걸까...";
            Game_TypeWriterEffect.instance.RandomText[40] = "나는 ........그래도 가야만 해.";
            Game_TypeWriterEffect.instance.RandomText[41] = "빨리... 더 빨리 ...";
            Game_TypeWriterEffect.instance.RandomText[42] = "따뜻한 물에 씻고 싶어";
            Game_TypeWriterEffect.instance.RandomText[43] = "침대에 눕고 싶어";
            Game_TypeWriterEffect.instance.RandomText[44] = "힘들어... 자고 싶어";
            Game_TypeWriterEffect.instance.RandomText[45] = "집에 가고 싶어...";
            Game_TypeWriterEffect.instance.RandomText[46] = "언제 갈 수 있을까..";
            Game_TypeWriterEffect.instance.RandomText[47] = "누군가 날 집에 보내줘요 !";
            Game_TypeWriterEffect.instance.RandomText[48] = "....내가 가야 해.";
            Game_TypeWriterEffect.instance.RandomText[49] = "가지 않으면, 영원히 이곳에 있을 수 밖에 없어";
        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            //필수 랜덤 나레이션
            Game_TypeWriterEffect.instance.RandomText[0] = "다시 라디오가 돼서 기뻐";
            Game_TypeWriterEffect.instance.RandomText[1] = "이상한 곳이었어...";
            Game_TypeWriterEffect.instance.RandomText[2] = "누가 우주에 벽화가 있을 거라고 생각했을까?";
            Game_TypeWriterEffect.instance.RandomText[3] = "지구에 알린다면 위대한 발견이 될 거야";
            Game_TypeWriterEffect.instance.RandomText[4] = "이게 집에 갈 유일한 단서일지도 몰라";
            Game_TypeWriterEffect.instance.RandomText[5] = "난 ... 내 선택을 믿어.";
            Game_TypeWriterEffect.instance.RandomText[6] = "때로는 맞지 않을 선택을 할 때도 있지만";
            Game_TypeWriterEffect.instance.RandomText[7] = "나의 선택들이 쌓여 지금 여기까지 왔겠지?";
            Game_TypeWriterEffect.instance.RandomText[8] = "언제나 후회하지 않을 수 없어";
            Game_TypeWriterEffect.instance.RandomText[9] = "선택하지 않고 둘 다 할 수 있으면 좋을 텐데";
            Game_TypeWriterEffect.instance.RandomText[10] = "라디오야! 오늘의 날씨는!";
            Game_TypeWriterEffect.instance.RandomText[11] = "가끔은 라디오가 나에게 말을 거는 것 같아 깜짝 놀라";
            Game_TypeWriterEffect.instance.RandomText[12] = "아이쿠! 넘어질 뻔했어";
            Game_TypeWriterEffect.instance.RandomText[13] = "건강한 게 중요하다 했어, 조심해서 걷자.";
            Game_TypeWriterEffect.instance.RandomText[14] = "여기엔 누군가를 만날 수 있지 않을까";
            Game_TypeWriterEffect.instance.RandomText[15] = "이 벽화... 그림을 참 잘 그렸네!";
            Game_TypeWriterEffect.instance.RandomText[16] = "여기에 적힌 언어는 어디의 언어일까?";
            Game_TypeWriterEffect.instance.RandomText[17] = "지구의 언어는 아닌 것 같아";
            Game_TypeWriterEffect.instance.RandomText[18] = "...이건 우리말의 '희망'과 비슷하게 생긴 단어야";
            Game_TypeWriterEffect.instance.RandomText[19] = "희망이 생긴 것 같아";
            Game_TypeWriterEffect.instance.RandomText[20] = "발걸음이 조금 가벼워진 것 같아";
            Game_TypeWriterEffect.instance.RandomText[21] = "이대로만 하자!";
            Game_TypeWriterEffect.instance.RandomText[22] = "조금만 더 가면 될 거야!";
            Game_TypeWriterEffect.instance.RandomText[23] = "달은 언제부터 위에 존재했을까?";
            Game_TypeWriterEffect.instance.RandomText[24] = "....달에 간다고 하니 뭔가 이상한 느낌이야";
            Game_TypeWriterEffect.instance.RandomText[25] = "우주선을 타고 가면 금방일 텐데!";
            Game_TypeWriterEffect.instance.RandomText[26] = "...난 왜 우주선을 타고 있었을까?";
            Game_TypeWriterEffect.instance.RandomText[27] = "난... 제일 이쁜 행성이 지구인 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[28] = "이 벽은...그냥 돌이 아니겠지?";
            Game_TypeWriterEffect.instance.RandomText[29] = "한명이 만든 건 아니겠지?";
            //갈림길 나레이션 
            Game_TypeWriterEffect.instance.RandomText[30] = "이 길은 집으로 가는 길이 될 거야";
            Game_TypeWriterEffect.instance.RandomText[31] = "전에 있던 그곳은 너무 무서웠어";
            Game_TypeWriterEffect.instance.RandomText[32] = "여기는 조용한 걸 빼면 괜찮은 것 같아";
            Game_TypeWriterEffect.instance.RandomText[33] = "말동무가 있으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[34] = "누군가 내 말을 듣고 있을지도!";
            Game_TypeWriterEffect.instance.RandomText[35] = "아아! 듣고 있어요?";
            Game_TypeWriterEffect.instance.RandomText[36] = "난 우주에 외계인이 살 줄 알았는데";
            Game_TypeWriterEffect.instance.RandomText[37] = "이 벽을 보면 정말 외계인이 있을지도";
            Game_TypeWriterEffect.instance.RandomText[38] = "혹시 외계인을 만나면 어떻게 대화를 하지";
            Game_TypeWriterEffect.instance.RandomText[39] = "외계인들은 말을 할 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[40] = "영화를 보면 막... 손가락으로... 소통을";
            Game_TypeWriterEffect.instance.RandomText[41] = "...흠";
            Game_TypeWriterEffect.instance.RandomText[42] = "다리가 조금 아픈 것 같아";
            Game_TypeWriterEffect.instance.RandomText[43] = "쉬었다 갈까?";
            Game_TypeWriterEffect.instance.RandomText[44] = "흐흥..~ 흐흐흥~";
            Game_TypeWriterEffect.instance.RandomText[45] = "어제 꿈을 꿨는데... 금세 잊어버렸어.";
            Game_TypeWriterEffect.instance.RandomText[46] = "생각하고 있던 것들을 왜 금방 잊어버리고 마는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[47] = "어떤 사람은 망각이 인간의 축복이래";
            Game_TypeWriterEffect.instance.RandomText[48] = "기억을 떠올리고 싶어";
            Game_TypeWriterEffect.instance.RandomText[49] = "이 벽화들처럼 기록해 둘 걸 그랬어!";
        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "힘내자!";
            Game_TypeWriterEffect.instance.RandomText[1] = "우주를 걷는 사람은 내가 처음일지도";
            Game_TypeWriterEffect.instance.RandomText[2] = "어릴 때 네잎클러버를 열심히 찾았었어";
            Game_TypeWriterEffect.instance.RandomText[3] = "네잎클러버를 결국 찾았지";
            Game_TypeWriterEffect.instance.RandomText[4] = "항상 그랬듯 나는 이겨낼 거야!";
            Game_TypeWriterEffect.instance.RandomText[5] = "어린 왕자의 별을 찾고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[6] = "생각보다 많은 것을 당연하게 여겨왔나 봐";
            Game_TypeWriterEffect.instance.RandomText[7] = "당연한 건 아무것도 없는데 말이야";
            Game_TypeWriterEffect.instance.RandomText[8] = "분명 많은 선택을 했을 거야";
            Game_TypeWriterEffect.instance.RandomText[9] = "가끔은 이렇게 계획이 없는 것도 괜찮지";
            Game_TypeWriterEffect.instance.RandomText[10] = "내 미래를 보고 싶어";
            Game_TypeWriterEffect.instance.RandomText[11] = "미래의 나야 무사히 목적지에 도착해있지?";
            Game_TypeWriterEffect.instance.RandomText[12] = "오늘도 새로운 시작을 할 수 있어";
            Game_TypeWriterEffect.instance.RandomText[13] = "누군가를 만날 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "누구 있어요~?";
            Game_TypeWriterEffect.instance.RandomText[15] = "우주는 상식이 통하지 않는 유일한 공간 같아";
            Game_TypeWriterEffect.instance.RandomText[16] = "무한한 것을 우주에 비유하곤 했는데 적절한 비유였던 것 같아";
            Game_TypeWriterEffect.instance.RandomText[17] = "언제나 좋은 일만 있을 수 없다는 걸 알아";
            Game_TypeWriterEffect.instance.RandomText[18] = "난 좋은 일들을 만드는 마법을 알고 있어";
            Game_TypeWriterEffect.instance.RandomText[19] = "좋은 마음을 가지면 좋은 일들이 생겨";
            Game_TypeWriterEffect.instance.RandomText[20] = "그때 피지 않았던 꽃은 지금은 피었겠지?";
            Game_TypeWriterEffect.instance.RandomText[21] = "지구에서의 마지막 날에 누구랑 있었던 것 같은데 누구였을까?";
            Game_TypeWriterEffect.instance.RandomText[22] = "기억을 찾고 싶어";
            Game_TypeWriterEffect.instance.RandomText[23] = "달에 토끼가 산다던데 진짜일까?";
            Game_TypeWriterEffect.instance.RandomText[24] = "두려움보다 희망이 낫지!";
            Game_TypeWriterEffect.instance.RandomText[25] = "아마 토끼는 별 가루 덕분에 껑충껑충 뛸 수 있을거야";
            Game_TypeWriterEffect.instance.RandomText[26] = "언제나 최악은 아니야.";
            Game_TypeWriterEffect.instance.RandomText[27] = "쉬고 싶을 때 쉬어도 돼";
            Game_TypeWriterEffect.instance.RandomText[28] = "빨리 도착하는 것만이 답은 아니니까";
            Game_TypeWriterEffect.instance.RandomText[29] = "다시 우주야";
            Game_TypeWriterEffect.instance.RandomText[30] = "마치 방금 벽화를 봤던 건 꿈인 것 같아";
            Game_TypeWriterEffect.instance.RandomText[31] = "하지만 꿈이 아니겠지!";
            Game_TypeWriterEffect.instance.RandomText[32] = "오로라야, 지구에서도 못 봤는데!";
            Game_TypeWriterEffect.instance.RandomText[33] = "예쁘다... 우주가 물결치는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[34] = "... 지구에 간다면 누군가와 함께 오로라를 보러 갈 거야";
            Game_TypeWriterEffect.instance.RandomText[35] = "예쁘다.....";
            Game_TypeWriterEffect.instance.RandomText[36] = "이 오로라를 접어서 가지고 다니고 싶어";
            Game_TypeWriterEffect.instance.RandomText[37] = "오로라를 보고 싶을 때마다 꺼내어 볼 수 있으면 좋을 텐데";
            Game_TypeWriterEffect.instance.RandomText[38] = "분명 지친 사람들에게 위안을 줄 수 있을 거야";
            Game_TypeWriterEffect.instance.RandomText[39] = "내가 위안을 받았듯";
            Game_TypeWriterEffect.instance.RandomText[40] = "다들 행복하면 좋겠다.";
            Game_TypeWriterEffect.instance.RandomText[41] = "라디오야 너도 이 오로라를 보고 있니?";
            Game_TypeWriterEffect.instance.RandomText[42] = "보고 있다면 지구의 소식을 알려줘";
            Game_TypeWriterEffect.instance.RandomText[43] = "어릴 때 우주를 그린 적이 있어.";
            Game_TypeWriterEffect.instance.RandomText[44] = "10년 뒤면 우주에 사람들이 살고 있을지도 몰라";
            Game_TypeWriterEffect.instance.RandomText[45] = "흠흠 이 길을 발견한 사람은 늠름한 소년으로! 흠 다른 멘트를 생각해야겠어";
            Game_TypeWriterEffect.instance.RandomText[46] = "엄마는 내가 여기 있다는 걸 알고 있을까?";
            Game_TypeWriterEffect.instance.RandomText[47] = "집으로 돌아가면 모두 얘기해 줘야지!";
            Game_TypeWriterEffect.instance.RandomText[48] = "날이 바뀌지않으니 몇 일이 지났는 지 잘 모르겠어";
            Game_TypeWriterEffect.instance.RandomText[49] = "반짝반짝 , 아름답게 빛나네~";
        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.RandomText = new string[50];
            Game_TypeWriterEffect.instance.RandomText[0] = "지구에 돌아가면 할 버킷 리스트를 만들었어";
            Game_TypeWriterEffect.instance.RandomText[1] = "뷔페 가서 맛있는 음식 마음껏 먹기...";
            Game_TypeWriterEffect.instance.RandomText[2] = "최신 영화 몰아보기...";
            Game_TypeWriterEffect.instance.RandomText[3] = "엄마에게 사랑한다 말하기";
            Game_TypeWriterEffect.instance.RandomText[4] = "달에서 살아남는 법 저자가 되기";
            Game_TypeWriterEffect.instance.RandomText[5] = "이런 생각을 하면 외로움이 잊혀져";
            Game_TypeWriterEffect.instance.RandomText[6] = "누군가를 만나니 좋다";
            Game_TypeWriterEffect.instance.RandomText[7] = "여기에서 아무도 못 만날 줄 알았어";
            Game_TypeWriterEffect.instance.RandomText[8] = "점점 좋은 일들이 일어날 것 같다는 예감이 들어";
            Game_TypeWriterEffect.instance.RandomText[9] = "이 길에도 끝이 있을 것 같아";
            Game_TypeWriterEffect.instance.RandomText[10] = "다른 곳에서 새 시작을 할 수 있겠지";
            Game_TypeWriterEffect.instance.RandomText[11] = "사실 누굴 만날 거라 생각 못 한 곳에서...소리가 들려서 무서웠지 뭐야!";
            Game_TypeWriterEffect.instance.RandomText[12] = "소행성들이 가득해";
            Game_TypeWriterEffect.instance.RandomText[13] = "엄마와의 추억들을 계속 떠올려봐,";
            Game_TypeWriterEffect.instance.RandomText[14] = "추억이 엄마에게 데려다 줄거야";
            Game_TypeWriterEffect.instance.RandomText[15] = "엄마를 잃었다는 건, 엄마도 날 잃었다는 거야";
            Game_TypeWriterEffect.instance.RandomText[16] = "분명 슬퍼하고 계시겠지...";
            Game_TypeWriterEffect.instance.RandomText[17] = "엄마를 잃어버린 사람이 있다면 꼭 엄마에게 데려다주겠다 생각했어";
            Game_TypeWriterEffect.instance.RandomText[18] = "음... 생각해보자";
            Game_TypeWriterEffect.instance.RandomText[19] = "어쩌다 엄마를 잃어버렸을까?";
            Game_TypeWriterEffect.instance.RandomText[20] = "저 소행성에 바오밥 나무가 자라지 않아야 할 텐데";
            Game_TypeWriterEffect.instance.RandomText[21] = "나는… 사실 엄마에 대한 기억이 뚜렷하게 나지 않아";
            Game_TypeWriterEffect.instance.RandomText[22] = "이곳에 도착하면서 사고가 났으니까...";
            Game_TypeWriterEffect.instance.RandomText[23] = "그 사고 때문에 기억을 잃은 것 같아";
            Game_TypeWriterEffect.instance.RandomText[24] = "엄마가 무척 따뜻한 사람이라는 건 알아!";
            Game_TypeWriterEffect.instance.RandomText[25] = "좋은 추억이 없었다면 난 여기까지 오지 못했을 거야";
            Game_TypeWriterEffect.instance.RandomText[26] = "... 이 길을 걸으면서 드는 생각이 많아";
            Game_TypeWriterEffect.instance.RandomText[27] = "같이 대화할 아기별이 있으면 좋겠다고 쭉 생각했어";
            Game_TypeWriterEffect.instance.RandomText[28] = "내 바람이 미츄를 여기로 이끌었을 수도 있겠다.";
            Game_TypeWriterEffect.instance.RandomText[29] = "엄마를 찾으면 제일 먼저 어떤 이야기를 할까";
            Game_TypeWriterEffect.instance.RandomText[30] = "하고 싶은 말이 많아서 오랫동안 고민했어";
            Game_TypeWriterEffect.instance.RandomText[31] = "하고 싶은 말이 너무 많아...";
            Game_TypeWriterEffect.instance.RandomText[32] = "역시 그리운 사람을 만나면 처음은 사랑한다고 말 할 거야";
            Game_TypeWriterEffect.instance.RandomText[33] = "지금부터 더 생각해 봐야겠다";
            Game_TypeWriterEffect.instance.RandomText[34] = "다른 사람들은 지금 무슨 생각을 할까?";
            Game_TypeWriterEffect.instance.RandomText[35] = "라디오에서 가끔 지구의 이야기를 들려줘";
            Game_TypeWriterEffect.instance.RandomText[36] = "물론 고장이 났는지 온전히 들린 적은 없지만,";
            Game_TypeWriterEffect.instance.RandomText[37] = "가끔은 이 라디오가 나에게 말을 거는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[38] = "내 생각보다 더 오래 혼자 있었나 봐";
            Game_TypeWriterEffect.instance.RandomText[39] = "외로울 때 만난 생명과 헤어지면 허전할 것 같아";
            Game_TypeWriterEffect.instance.RandomText[40] = "함께했던 기억으로 더 나아갈 수 있겠지";
            Game_TypeWriterEffect.instance.RandomText[41] = "엄마를 찾는 모험에 나와 함께해 줘";
            Game_TypeWriterEffect.instance.RandomText[42] = "그거 알아? 고맙다는 말은 아끼면 안된다고 해";
            Game_TypeWriterEffect.instance.RandomText[43] = "마음이 따뜻해, 난 이 감정의 이름을 알아.";
            Game_TypeWriterEffect.instance.RandomText[44] = "기쁨이나, 행복이라고 부를 거야.";
            Game_TypeWriterEffect.instance.RandomText[45] = "이 감정을 처음 나에게 알려준 사람은 엄마였을 거야.";
            Game_TypeWriterEffect.instance.RandomText[46] = "추억 속의 사람들도… 알 게 모르게 나에게 많은 것을 알려주었겠지";
            Game_TypeWriterEffect.instance.RandomText[47] = "뭔가가 속삭이는 소리들이 들리는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[48] = "저 소행성에도 노을이 지겠지?";
            Game_TypeWriterEffect.instance.RandomText[49] = "다른 삶을 살았는데도 함께하면 서로를 닮아가는 게 신기해.";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "... 이곳은... 이상한 곳이네";
            Game_TypeWriterEffect.instance.RandomText[1] = "블랙홀 안에 들어온 것만 같아";
            Game_TypeWriterEffect.instance.RandomText[2] = "너무 어두워...";
            Game_TypeWriterEffect.instance.RandomText[3] = "... 내 눈이 잘못된 걸까?";
            Game_TypeWriterEffect.instance.RandomText[4] = "너무 어두워서 숨이 턱 막혀와...";
            Game_TypeWriterEffect.instance.RandomText[5] = "손에 땀이 흐르는 것 같아..";
            Game_TypeWriterEffect.instance.RandomText[6] = "이 길에서 떨어지면 수많은 어둠의 하나가 될 것 같아";
            Game_TypeWriterEffect.instance.RandomText[7] = "무서워서 몸이 떨려...";
            Game_TypeWriterEffect.instance.RandomText[8] = "어둠에 빠지면 헤어 나올 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[9] = "모든 게 흑과 백으로만 보여..";
            Game_TypeWriterEffect.instance.RandomText[10] = "... 내가 죽은 걸까?";
            Game_TypeWriterEffect.instance.RandomText[11] = "아냐.. 이곳도 우주의 일부겠지...";
            Game_TypeWriterEffect.instance.RandomText[12] = "사실 우주가 사후 공간이라면!?";
            Game_TypeWriterEffect.instance.RandomText[13] = "터무니없는 공간이야.";
            Game_TypeWriterEffect.instance.RandomText[14] = "... 빨리 끝났으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[15] = "이럴 때일수록 조심해야 하는데";
            Game_TypeWriterEffect.instance.RandomText[16] = "... 여긴 어쩌다 이렇게 된 걸까";
            Game_TypeWriterEffect.instance.RandomText[17] = "누군가가 내린 저주 같아";
            Game_TypeWriterEffect.instance.RandomText[18] = "나에게 내려진 시련의 공간일까?";
            Game_TypeWriterEffect.instance.RandomText[19] = "헉, 위험할 뻔했어";
            Game_TypeWriterEffect.instance.RandomText[20] = "조심하자... 조심히...";
            Game_TypeWriterEffect.instance.RandomText[21] = "... 미츄는 엄마와 조심히 갔을까?";
            Game_TypeWriterEffect.instance.RandomText[22] = "이런 곳에 있으니 미츄가 더 그리워";
            Game_TypeWriterEffect.instance.RandomText[23] = "이곳도 누군가와 함께였다면.. 이렇게 무섭지 않았겠지?";
            Game_TypeWriterEffect.instance.RandomText[24] = "혼자여서 무서운가 봐";
            Game_TypeWriterEffect.instance.RandomText[25] = "이런 상황에 익숙해지는 게 싫어";
            Game_TypeWriterEffect.instance.RandomText[26] = "익숙해지면 습관이 되고 그건 떨치기 힘들어";
            Game_TypeWriterEffect.instance.RandomText[27] = "함께인 것에 익숙하고 싶어";
            Game_TypeWriterEffect.instance.RandomText[28] = "... 지금의 최선은 그저 익숙해지는 건가 봐";
            Game_TypeWriterEffect.instance.RandomText[29] = "최근에 그런말을 봤어 최선이라는 말이 나올 때는 최악의 상황이라고,";
            Game_TypeWriterEffect.instance.RandomText[30] = "지금 최악의 상황은 아니겠지?";
            Game_TypeWriterEffect.instance.RandomText[31] = "방금 오싹한 기분이 들었어.";
            Game_TypeWriterEffect.instance.RandomText[32] = "따뜻한 침대에 누워 자고 싶다";
            Game_TypeWriterEffect.instance.RandomText[33] = ".... 여전히 어둡네";
            Game_TypeWriterEffect.instance.RandomText[34] = "언제 이곳을 벗어날 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[35] = "다시 한번 전에 보았던 오로라를 보고 싶어";
            Game_TypeWriterEffect.instance.RandomText[36] = ".... 누구 없어요?";
            Game_TypeWriterEffect.instance.RandomText[37] = "이런 곳은 유난히 추운 것 같아";
            Game_TypeWriterEffect.instance.RandomText[38] = "이 길이 맞는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[39] = "... 사실 반대 방향으로 가고 있는 게 아닐까?";
            Game_TypeWriterEffect.instance.RandomText[40] = "아까부터 의심이 되기 시작했어";
            Game_TypeWriterEffect.instance.RandomText[41] = "그전에 들어섰던 갈림길... 사실 잘 못 선택한 게 아닐까?";
            Game_TypeWriterEffect.instance.RandomText[42] = "앞으로 만나게 될 갈림길에 대한 자신이 없어..";
            Game_TypeWriterEffect.instance.RandomText[43] = "내가 올바른 선택을 하고 있는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[44] = "내 선택을 의심하는 건 바보 같은 일이야.";
            Game_TypeWriterEffect.instance.RandomText[45] = "이미 선택한 건 수습은 할 수 있지만 선택하기 전으로 되돌릴 순 없어";
            Game_TypeWriterEffect.instance.RandomText[46] = "너무 피곤한 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[47] = "의심하지 말자! 나도 이 공간도...!";
            Game_TypeWriterEffect.instance.RandomText[48] = "잘못 들어섰다면 다시 돌아가면 되지";
            Game_TypeWriterEffect.instance.RandomText[49] = "... 가자, 언제고 이곳에 있을 수 없어.";
        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "......그곳을 벗어나니 이런 공간이 있네!";
            Game_TypeWriterEffect.instance.RandomText[1] = "이곳을 위해 무서운 공간들을 지나왔는지 몰라";
            Game_TypeWriterEffect.instance.RandomText[2] = "예쁘다... 우주가 파도치는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[3] = "고래가 우주에 살고 있을지 몰랐어...";
            Game_TypeWriterEffect.instance.RandomText[4] = "고래라고 부르는 게 맞는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[5] = ".... 우주가 아니야, 바다 속일지도.";
            Game_TypeWriterEffect.instance.RandomText[6] = "언제 바닷속에 들어온 거야! 하하!";
            Game_TypeWriterEffect.instance.RandomText[7] = "미츄도 이걸 봤어야 했는데!";
            Game_TypeWriterEffect.instance.RandomText[8] = "미츄는 이미 이 광경을 봤을지도 몰라";
            Game_TypeWriterEffect.instance.RandomText[9] = "나도 고래가 되고 싶다...";
            Game_TypeWriterEffect.instance.RandomText[10] = "이런 길이 계속 이어지면 좋겠다.";
            Game_TypeWriterEffect.instance.RandomText[11] = "저들의 움직임이 나에게 위안을 주는 느낌이야";
            Game_TypeWriterEffect.instance.RandomText[12] = "여기까지 오느라 수고했어,라고  고래들이 말하는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[13] = "저들은 별들을 먹고 살까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "... 그러고 보니 배가 고프네";
            Game_TypeWriterEffect.instance.RandomText[15] = "반짝이는 게 보기 좋아";
            Game_TypeWriterEffect.instance.RandomText[16] = "까마귀는 반짝이는 물건을 모으는 습성이 있대, 이곳은 까마귀들이 좋아하겠다!";
            Game_TypeWriterEffect.instance.RandomText[17] = "고래를 보니 시원한 느낌이야";
            Game_TypeWriterEffect.instance.RandomText[18] = "지구 사람들이 이 곳에 오면 바쁘겠어! 이 풍경을 하나도 놓치지 않으려면 말이야!";
            Game_TypeWriterEffect.instance.RandomText[19] = "고래야! 안녕!!";
            Game_TypeWriterEffect.instance.RandomText[20] = "어! 저 고래가 내 말을 알아들은 것 같아";
            Game_TypeWriterEffect.instance.RandomText[21] = "하하! 고래랑 눈이 마주친 것 같은데... 우연이었나봐!";
            Game_TypeWriterEffect.instance.RandomText[22] = "매일매일이 이런 이벤트로 가득하다면 모두가 행복해할 거야.";
            Game_TypeWriterEffect.instance.RandomText[23] = "무슨 생각을 하면서 우주를 유영하는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[24] = "산소만 아니었다면 이곳에 서서 여유롭게 구경했을 텐데...";
            Game_TypeWriterEffect.instance.RandomText[25] = "꿈일지도 몰라.. 이 모든 게 말이야!";
            Game_TypeWriterEffect.instance.RandomText[26] = "고래의 등을 타고 갈 수 없을까?";
            Game_TypeWriterEffect.instance.RandomText[27] = "그러면 우주선 못지않게 빠를 거야";
            Game_TypeWriterEffect.instance.RandomText[28] = "고래야~! 나를 태워줄 수 있니?";
            Game_TypeWriterEffect.instance.RandomText[29] = "흠 고래들은 바쁜가 봐, 아니면 나를 발견하지 못했거나";
            Game_TypeWriterEffect.instance.RandomText[30] = "왜 몸이 크면 작은 것들이 잘 보이지 않을까?";
            Game_TypeWriterEffect.instance.RandomText[31] = "저 고래들도 길의 끝에선 보이지 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[32] = "흐흐흥 흐흥~";
            Game_TypeWriterEffect.instance.RandomText[33] = "오늘 고래들과 바닷속을 헤엄치는 꿈을 꿨어";
            Game_TypeWriterEffect.instance.RandomText[34] = "고래들은 꿈을 꿀까? 꾼다면 어떤 꿈을 꿀까?";
            Game_TypeWriterEffect.instance.RandomText[35] = "자유롭게 헤엄치는 모습이 행복해 보여";
            Game_TypeWriterEffect.instance.RandomText[36] = "길에서 벗어나 자유롭게 헤엄치는 고래...!";
            Game_TypeWriterEffect.instance.RandomText[37] = "저들은 목적지가 있을까?";
            Game_TypeWriterEffect.instance.RandomText[38] = "마음이 가는 대로 발이 닿는 대로 갈 수 있다면 좀 더 즐거웠을거야";
            Game_TypeWriterEffect.instance.RandomText[39] = "나는 마음을 조금 가볍게 먹어야 할 필요가 있어";
            Game_TypeWriterEffect.instance.RandomText[40] = "다른 생각들을 하다 보면 금방 도착해 있겠지?";
            Game_TypeWriterEffect.instance.RandomText[41] = "헉! 저 고래 등에서 별가루들이 쏟아져 나왔어";
            Game_TypeWriterEffect.instance.RandomText[42] = "고래는 2초 동안 2,000t의 공기를 들이마실 수 있대";
            Game_TypeWriterEffect.instance.RandomText[43] = "아아, 오늘의 우주 라디오 말씀 전합니다";
            Game_TypeWriterEffect.instance.RandomText[44] = "오늘의 우주는 맑고 끝없이 펼쳐져 있으며 빛나는 고래떼가 지나갈 예정입니다~";
            Game_TypeWriterEffect.instance.RandomText[45] = "우주에 고래가 있다면 고양이나 강아지도 있을지 몰라!";
            Game_TypeWriterEffect.instance.RandomText[46] = "지구에 있는 생물은 다 우주에서 온 외계인일지도!";
            Game_TypeWriterEffect.instance.RandomText[47] = "나도 고래처럼 크고 싶어";
            Game_TypeWriterEffect.instance.RandomText[48] = "그럼 지구에서도 날 관측할 수 있겠지!";
            Game_TypeWriterEffect.instance.RandomText[49] = "지구 여러분! 제가 여기에 있어요!";
        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "이게 뭐지.. 민들레 씨앗 같은 포자야..";
            Game_TypeWriterEffect.instance.RandomText[1] = "점점 숨이 가빠 오는 것 같은데...";
            Game_TypeWriterEffect.instance.RandomText[2] = "내 생각에 숨이 가빠지는 건 여기에 나타난 포자 때문인 것 같아..";
            Game_TypeWriterEffect.instance.RandomText[3] = "예쁘게 생겼는데 ..";
            Game_TypeWriterEffect.instance.RandomText[4] = "이곳을 빨리 벗어나야겠어";
            Game_TypeWriterEffect.instance.RandomText[5] = "달려야 해, 달리자...";
            Game_TypeWriterEffect.instance.RandomText[6] = "여길 벗어나면 또 괜찮아질거야";
            Game_TypeWriterEffect.instance.RandomText[7] = "이 포자들은 어디서 오는 거지?";
            Game_TypeWriterEffect.instance.RandomText[8] = "너무 많아... 이대로라면 위험할 거야";
            Game_TypeWriterEffect.instance.RandomText[9] = "누가 일부러 뿌린 건 아니겠지?";
            Game_TypeWriterEffect.instance.RandomText[10] = "포자들의 출처를 알면 해결하기도 좋을 텐데!";
            Game_TypeWriterEffect.instance.RandomText[11] = "발아래에 빛이 물결치는 게 느껴져";
            Game_TypeWriterEffect.instance.RandomText[12] = "벽화처럼 누군가 이 상황을 메모한 게 없을까?";
            Game_TypeWriterEffect.instance.RandomText[13] = "콜록 콜록,";
            Game_TypeWriterEffect.instance.RandomText[14] = "우주복 안에 포자가 들어오진 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[15] = "포자가 우주복에 들어온 건 아니지만 너무 찝찝해";
            Game_TypeWriterEffect.instance.RandomText[16] = "... 새 산소통을 준비해야 할 것 같아";
            Game_TypeWriterEffect.instance.RandomText[17] = ".... 콜록콜록";
            Game_TypeWriterEffect.instance.RandomText[18] = "주변에 민들 레같은 게 심어져있나?";
            Game_TypeWriterEffect.instance.RandomText[19] = "포자를 밟으면 빛 아지랑이가 올라와";
            Game_TypeWriterEffect.instance.RandomText[20] = "윽... 숨쉬기가 힘들어";
            Game_TypeWriterEffect.instance.RandomText[21] = "차라리 숨을 쉬지 않은 생물이면 좋을 텐데!";
            Game_TypeWriterEffect.instance.RandomText[22] = "계곡에서 숨참기 대회를 하면 항상 1등을 했었는데...";
            Game_TypeWriterEffect.instance.RandomText[23] = "삶은 가끔 이런 시련을 주는 걸 즐기는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[24] = "매일매일이 시련투성이라면 어떤 마음이 들까";
            Game_TypeWriterEffect.instance.RandomText[25] = "괜찮은 날도 꽤 있어 나는 그런 날을 즐기는 걸 좋아해";
            Game_TypeWriterEffect.instance.RandomText[26] = "친구와 산에서 놀 때 이런 포자를 봤던 거 같아";
            Game_TypeWriterEffect.instance.RandomText[27] = "저 흰색 결정체가 길로 떨어지지는 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[28] = "... 벗어나려면 멀었나?";
            Game_TypeWriterEffect.instance.RandomText[29] = "얼마 걷지 않은 것 같은데 벌써 지쳐...";
            Game_TypeWriterEffect.instance.RandomText[30] = "저 흰색 결정체는 날카로워 보여";
            Game_TypeWriterEffect.instance.RandomText[31] = "... 이때까지 며칠을 걸었더라...";
            Game_TypeWriterEffect.instance.RandomText[32] = "누군가 날 찾아줬으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[33] = "가끔은 이대로 서서 아무것도 하고 싶지 않아";
            Game_TypeWriterEffect.instance.RandomText[34] = "아무것도 하기 싫다는 건 지쳤다는 증거겠지 ...";
            Game_TypeWriterEffect.instance.RandomText[35] = "우울한 날들이 습관이 되는 걸 원치 않아";
            Game_TypeWriterEffect.instance.RandomText[36] = "다시 걸어야지, 당장을 벗어나야 해.";
            Game_TypeWriterEffect.instance.RandomText[37] = "엄마가 말했던 것 같아... 나는 뭐든지 할 수 있고 뭐든 될 수 있다고";
            Game_TypeWriterEffect.instance.RandomText[38] = "저렇게 멀리서 보는 빛들은 이쁜데, 가까이 오면 위협적일 때가 있어?";
            Game_TypeWriterEffect.instance.RandomText[39] = "지금 나는 내가 할 수 있는 걸 하기 위해 가는 중이야";
            Game_TypeWriterEffect.instance.RandomText[40] = "어른들이 종종 인생을 길에 비유하는 이유를 알 거 같아";
            Game_TypeWriterEffect.instance.RandomText[41] = "어떤 길을 걷느냐가 중요한 걸까 그 길을 걷는 내가 중요한 걸까?";
            Game_TypeWriterEffect.instance.RandomText[42] = "하품을 하면 산소가 빨리 없어질까?";
            Game_TypeWriterEffect.instance.RandomText[43] = "지금은 길을 선택할 수 없으니 나를 중요하게 생각해야지";
            Game_TypeWriterEffect.instance.RandomText[44] = "마음을 다잡자, 난 할 수 있어";
            Game_TypeWriterEffect.instance.RandomText[45] = "포자들이 없는 곳으로 가야 해";
            Game_TypeWriterEffect.instance.RandomText[46] = "오랫동안 이 길을 걸으니 좋은 점들을 발견했어 ...";
            Game_TypeWriterEffect.instance.RandomText[47] = "다른 사람들도 나처럼 이 길의 좋은 점을 발견하겠지?";
            Game_TypeWriterEffect.instance.RandomText[48] = "오래 보아야 그걸 이해할 수 있으니까";
            Game_TypeWriterEffect.instance.RandomText[49] = "이 고드름 같은 빛은 뭘까?";
        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "이제 좀 숨쉬기가 편해졌어";
            Game_TypeWriterEffect.instance.RandomText[1] = "으음, 주변에 난 이건... 꽃이 맞겠지?";
            Game_TypeWriterEffect.instance.RandomText[2] = "꽃은 왜 예쁘게 느껴질까? 작고 소중해서?";
            Game_TypeWriterEffect.instance.RandomText[3] = "누가 처음 꽃들을 이곳에 심은 걸까?";
            Game_TypeWriterEffect.instance.RandomText[4] = "꽃들은 이곳에 스스로 찾아왔을지 몰라, 나처럼 갈림길을 선택하면서 말이야.";
            Game_TypeWriterEffect.instance.RandomText[5] = "지구의 꽃들이 그리워져,";
            Game_TypeWriterEffect.instance.RandomText[6] = "자신이 평생 자랄 곳을 선택하는 식물들은 어떤 기분일까";
            Game_TypeWriterEffect.instance.RandomText[7] = "행복해지는 책에서 스스로를 위해 꽃을 사라고 했어";
            Game_TypeWriterEffect.instance.RandomText[8] = "꽃이 주변에 가득하다면 꽃의 가치는 떨어질까?";
            Game_TypeWriterEffect.instance.RandomText[9] = "어떤 향기가 나는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[10] = "따듯하고 포근한 향기야, 눈을감으면 별과 달이 떠오르는...";
            Game_TypeWriterEffect.instance.RandomText[11] = "별과 우주의 꽃일까?";
            Game_TypeWriterEffect.instance.RandomText[12] = "왠지 오늘은 운이 좋을 것 같은 예감이 들어";
            Game_TypeWriterEffect.instance.RandomText[13] = "난 꽃이 많으면 많을수록 꽃들의 가치가 올라간다 생각해";
            Game_TypeWriterEffect.instance.RandomText[14] = "지구에 돌아가면 날 위한 꽃을 사야겠어";
            Game_TypeWriterEffect.instance.RandomText[15] = "홀로 핀 꽃보다 옹기종기 모여있는 꽃밭이 더 아름답게 느껴져.";
            Game_TypeWriterEffect.instance.RandomText[16] = "꽃이 가득 핀 식물원에 간 적이 있었는데... 그곳의 핫도그가 정말 맛있었어!";
            Game_TypeWriterEffect.instance.RandomText[17] = "... 난 행운의 소년일지도 몰라, 누가 이런 경험을 했겠어?";
            Game_TypeWriterEffect.instance.RandomText[18] = " 어린 왕자의 꽃은 외로움을 잘 탔었는데";
            Game_TypeWriterEffect.instance.RandomText[19] = "꽃들아 너희들은 함께니까 외롭지 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[20] = "너희는 다 아름다우니 서로 싸우지 않도록 해,";
            Game_TypeWriterEffect.instance.RandomText[21] = "꽃들아 너희는 이곳을 지나간 다른 사람들을 본 적 있니";
            Game_TypeWriterEffect.instance.RandomText[22] = "계속 이곳에 있는 건 어떤 기분일까?";
            Game_TypeWriterEffect.instance.RandomText[23] = "내가 돌아가는 길에 이곳을 지난다면 꽃들이 인사해 주겠지?";
            Game_TypeWriterEffect.instance.RandomText[24] = "꽃은 가만히 보고 있을 뿐인데 마음이 안정돼";
            Game_TypeWriterEffect.instance.RandomText[25] = "나도 주변 사람의 마음을 편하게 할 수 있는 사람이 되고 싶어";
            Game_TypeWriterEffect.instance.RandomText[26] = "너희는 행운의 꽃이야, 나에겐 희망의 꽃이고";
            Game_TypeWriterEffect.instance.RandomText[27] = "꽃이 좋은 이유를 알았어, 사람들은 기쁜 일이 있을때 꽃을 선물로 주고받거든.\n  행복의 중심에는 꽃이 꼭 함께해.";
            Game_TypeWriterEffect.instance.RandomText[28] = "너희의 꽃말은 뭘까? 없다면 내가 꽃말을 지어줄게.";
            Game_TypeWriterEffect.instance.RandomText[29] = "방금 전에 엄마를 잃어버린 별을 도와줬어 그때 가슴이 따뜻하고 만족스러웠지...";
            Game_TypeWriterEffect.instance.RandomText[30] = "혼자 있다면 누구를 도우면서 느끼는 충족감을 느낄 수 없을거야";
            Game_TypeWriterEffect.instance.RandomText[31] = "... 지금 잠시 이 풍경을 즐겨야겠어.";
            Game_TypeWriterEffect.instance.RandomText[32] = "긍정적인 감정에 익숙해져야 버티고 걸을 수 있을 것 같아.\n  비록 이 길뿐만 아닌, 내 인생의 길에서도 말이야";
            Game_TypeWriterEffect.instance.RandomText[33] = "신화 속 꽃들은 사연이 있었어, 너희도 그럴까?";
            Game_TypeWriterEffect.instance.RandomText[34] = "너희도 이 길이 끝나면 다시 볼 수 없겠지?";
            Game_TypeWriterEffect.instance.RandomText[35] = "내가 가도 너희를 도와줄 멋진 이들이 올 거야";
            Game_TypeWriterEffect.instance.RandomText[36] = "... 흐흐흥~ 흐흥~";
            Game_TypeWriterEffect.instance.RandomText[37] = "라디오에서 노래가 가끔씩 흘러나올 때 그 노래 사이 가사를 맞추는 게 재밌어";
            Game_TypeWriterEffect.instance.RandomText[38] = "꽃들도 노래를 부르곤 하니?";
            Game_TypeWriterEffect.instance.RandomText[39] = "꽃잎 하나에 하나의 음절을 가지고 있을 것 같아";
            Game_TypeWriterEffect.instance.RandomText[40] = "너희의 목소리도 다 다르겠지.";
            Game_TypeWriterEffect.instance.RandomText[41] = "다음에 모두의 목소리를 들을 수 있으면 좋겠다.";
            Game_TypeWriterEffect.instance.RandomText[42] = "이렇게 걸은지 오래되었어";
            Game_TypeWriterEffect.instance.RandomText[43] = "앞에도 끝없이 길이 있고 계속 걸어야 할지도 몰라";
            Game_TypeWriterEffect.instance.RandomText[44] = "이곳은 비교적 따뜻한 것 같아";
            Game_TypeWriterEffect.instance.RandomText[45] = "꽃가루 알레르기가 없어서 다행이야!";
            Game_TypeWriterEffect.instance.RandomText[46] = "사실 이렇게 우주를 걷는 거랑 지구에서 사는 건 크게 다르지 않은 거 같아. 감정면에서 말이야.";
            Game_TypeWriterEffect.instance.RandomText[47] = "내 친구는 꽃가루 알레르기가 심해서 봄에는 항상 티슈를 들고 다녔어";
            Game_TypeWriterEffect.instance.RandomText[48] = "우주에도 식물원이 있을까? 식물원에는 다양한 꽃들이 있었는데 너희는 없었어.";
            Game_TypeWriterEffect.instance.RandomText[49] = "너희들은 새로운 인연이야";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "....? 저건... 불꽃놀이잖아...";
            Game_TypeWriterEffect.instance.RandomText[1] = "불꽃놀이를 우주에서 보게 될 줄은 정말 몰랐는데!";
            Game_TypeWriterEffect.instance.RandomText[2] = "누가 폭죽을 터트리고 있는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[3] = "운석들이 부딪히면서 나는 불꽃이었잖아!";
            Game_TypeWriterEffect.instance.RandomText[4] = "멀리서 보니 나를 환영하는 빛처럼 보여";
            Game_TypeWriterEffect.instance.RandomText[5] = "옛날에 바다에서 엄마와 불꽃놀이를 했던 적이 있었던 것 같아";
            Game_TypeWriterEffect.instance.RandomText[6] = "쭈그려 앉아 막대의 불꽃놀이를 한동안 구경하곤 했었는데...";
            Game_TypeWriterEffect.instance.RandomText[7] = "운석의 파편이 이곳에 떨어지지 않겠지?";
            Game_TypeWriterEffect.instance.RandomText[8] = "예쁜 것과 별개로 어쩔 수 없이 운석의 파편이 떨어질까 봐 걱정하게 되는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[9] = "이 운석들은 어디서 왔을까?";
            Game_TypeWriterEffect.instance.RandomText[10] = "불꽃놀이는 그저 보는 건데 그게 왜 그렇게 좋았던 걸까...";
            Game_TypeWriterEffect.instance.RandomText[11] = "불꽃놀이를 하는 사람들은 모두 기분이 좋아 보였던 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[12] = "이런 불꽃은 들뜬 기분을 대신 표현해 주는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[13] = "어지러운 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[14] = "내가 아는 놀이들은 다 즐거운 기분이 드는데 불꽃놀이가 딱 그래";
            Game_TypeWriterEffect.instance.RandomText[15] = "이런 운석들이 떠있는 걸 보면 중력이 없는 우주라는 게 실감돼";
            Game_TypeWriterEffect.instance.RandomText[16] = "가만히 있던 운석은 돌처럼만 보였는데 생각보다 크구나";
            Game_TypeWriterEffect.instance.RandomText[17] = "우주에는 내 상상만으로는 생각할 수 없는 일들이 가득해";
            Game_TypeWriterEffect.instance.RandomText[18] = "불꽃이 따뜻해 보여,";
            Game_TypeWriterEffect.instance.RandomText[19] = "흑백의 공간에 이 작은 불빛을 띄울 수 있었다면 그렇게 춥게 느끼진 않았을 거야";
            Game_TypeWriterEffect.instance.RandomText[20] = "지구의 폭죽은 어떻게 만드는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[21] = "선생님이 그랬어, 실수는 수습이 가능하니 겁내지 말라고.";
            Game_TypeWriterEffect.instance.RandomText[22] = "동양에선 불꽃놀이는 악한 귀신을 몰아내는 힘이 있다고 믿었데";
            Game_TypeWriterEffect.instance.RandomText[23] = "이 길은 얼마나 긴 걸까?";
            Game_TypeWriterEffect.instance.RandomText[24] = "빛이 금방 사라지는 게 아쉽다.";
            Game_TypeWriterEffect.instance.RandomText[25] = "... 어라 뭔가 불꽃들이 두개로 보여";
            Game_TypeWriterEffect.instance.RandomText[26] = "더운 것 같기도 하고";
            Game_TypeWriterEffect.instance.RandomText[27] = "..... 인정해야겠어 나는 지금 아픈가 봐.";
            Game_TypeWriterEffect.instance.RandomText[28] = "내가 어디까지 왔는지 알 수 있으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[29] = "아프면 아무리 마음이 단단하고 강인한 사람이라도 무너 진대";
            Game_TypeWriterEffect.instance.RandomText[30] = "내가 오늘 응석 부리고 싶은 이유는 내가 약해서가 아니라 아파서일 거야.";
            Game_TypeWriterEffect.instance.RandomText[31] = "아플 때 혼자 있으면 서럽다고 하잖아, 그건 누군가에게 의지하고 싶은데 없어서 인가 봐.";
            Game_TypeWriterEffect.instance.RandomText[32] = "아플 땐 왜인지 응석도 부리고 싶고, 걱정도 해줬으면 좋겠고…";
            Game_TypeWriterEffect.instance.RandomText[33] = "... 달에 가면 달에 사는 주민들이 있을까?";
            Game_TypeWriterEffect.instance.RandomText[34] = "만약... 달에 도착하지 못해도, 그렇게 나쁜 여행은 아닌 거 같아.";
            Game_TypeWriterEffect.instance.RandomText[35] = "매일 보고 싶었지만 오늘 유독 엄마가 보고 싶어..";
            Game_TypeWriterEffect.instance.RandomText[36] = "저기 깜빡이는 빛은 뭘까..?";
            Game_TypeWriterEffect.instance.RandomText[37] = "블랙홀처럼 보이는 빛이야...";
            Game_TypeWriterEffect.instance.RandomText[38] = "숨을 쉬기가 힘들어... 그렇게 느끼는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[39] = "오늘은 조금 쉬어도 괜찮을 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[40] = "으슬으슬한 기분이야, 체력을 너무 사용했나 봐";
            Game_TypeWriterEffect.instance.RandomText[41] = "따뜻한 계란죽이 먹고 싶어";
            Game_TypeWriterEffect.instance.RandomText[42] = "..... 아파요....";
            Game_TypeWriterEffect.instance.RandomText[43] = "태양은 산소가 없지만 끊임없이 불타고 있어... ";
            Game_TypeWriterEffect.instance.RandomText[44] = "우주에서 우리가 아는 상식은 쓸모없는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[45] = "힘을 내자, 그럼 우주 휴게소에서 잠시 쉴 수 있을거야";
            Game_TypeWriterEffect.instance.RandomText[46] = "휴게소에 도착하면 푹 쉬어야겠어.";
            Game_TypeWriterEffect.instance.RandomText[47] = "흑백의 공간에서 아프지 않아 다행이야.";
            Game_TypeWriterEffect.instance.RandomText[48] = "포기하지 마, 힘내자!";
            Game_TypeWriterEffect.instance.RandomText[49] = "내가 아플 때 엄마는 따뜻하게 데운 우유에 설탕을 조금 타서 나에게 주었어.";
        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "... 몸이 따뜻해..아니 더운가?";
            Game_TypeWriterEffect.instance.RandomText[1] = "....엄마...";
            Game_TypeWriterEffect.instance.RandomText[2] = "가는 길에 약이 떨어져있다면 얼마나 좋을까...";
            Game_TypeWriterEffect.instance.RandomText[3] = "빛들이 주변을 맴돌고 있어";
            Game_TypeWriterEffect.instance.RandomText[4] = "잠시 정신을 잃었었나봐.";
            Game_TypeWriterEffect.instance.RandomText[5] = "여긴 뭘까?... 주변의 풍경이 바뀌었어.";
            Game_TypeWriterEffect.instance.RandomText[6] = "...그림이 그려져있는 것 같네.";
            Game_TypeWriterEffect.instance.RandomText[7] = "저번에 봤던 벽화와 이어지는 내용일까?";
            Game_TypeWriterEffect.instance.RandomText[8] = "...이 포자는 저번에 보았던 그것과 닮아있어";
            Game_TypeWriterEffect.instance.RandomText[9] = ".... 숨을 쉬기 힘든것 같아. 이건 익숙해 지지 않네";
            Game_TypeWriterEffect.instance.RandomText[10] = "지구로 돌아가면 나는 제일 오래 숨을 참을 수 있는 사람이 돼있을 것 같아";
            Game_TypeWriterEffect.instance.RandomText[11] = "이 길은 태양을 향해 가는 길인걸까? 더워..";
            Game_TypeWriterEffect.instance.RandomText[12] = "발이 델듯이 뜨거운것 같아...";
            Game_TypeWriterEffect.instance.RandomText[13] = "이 길은 태양을 향해 가는 길인걸까?... 더워..";
            Game_TypeWriterEffect.instance.RandomText[14] = "땀으로 온몸이 젖은것 같아...";
            Game_TypeWriterEffect.instance.RandomText[15] = "이 벽화의 내용은....이 벽화에도 달이 그려져있네.";
            Game_TypeWriterEffect.instance.RandomText[16] = "이 그림은 돌인가? 조각? 뭔가 중요한 것 같은데...";
            Game_TypeWriterEffect.instance.RandomText[17] = "... 아무래도 이 곳을 빨리 벗어나야 할 것같아.";
            Game_TypeWriterEffect.instance.RandomText[18] = "더운데다 숨도 쉬기 힘들어... 아팠던것 때문에 더 견디기 힘든 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[19] = "...누가 도와줘요...";
            Game_TypeWriterEffect.instance.RandomText[20] = ".... 도와줄 수 있는 생명이 없을까?";
            Game_TypeWriterEffect.instance.RandomText[21] = "말을 하기가 조금 힘드네...";
            Game_TypeWriterEffect.instance.RandomText[22] = "목이 마르고 까끌까끌해졌어.";
            Game_TypeWriterEffect.instance.RandomText[23] = "포자들이 더 많아 진 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[24] = "사방이 턱 막혀있어서 온도가 계속 오르는 것 같아...";
            Game_TypeWriterEffect.instance.RandomText[25] = "도망가고 싶은 곳이야...";
            Game_TypeWriterEffect.instance.RandomText[26] = "... 이미 돌아 갈 수 없을 만큼 많은 거리를 지났어";
            Game_TypeWriterEffect.instance.RandomText[27] = "고진감래하고 하잖아... 낙이 올거야.";
            Game_TypeWriterEffect.instance.RandomText[28] = "이번 일들을 견디면 나는 조금 성장해 있을까?";
            Game_TypeWriterEffect.instance.RandomText[29] = "내가 꽃이었다면 이곳에 자리잡지 않았을거야.";
            Game_TypeWriterEffect.instance.RandomText[30] = "... 별이 보고싶어. 우주의 하늘은 나의 낙이었나봐";
            Game_TypeWriterEffect.instance.RandomText[31] = "요즘 꿈을 자주 꾸는 것 같아. 꿈에서 뭘 보고싶은걸까?";
            Game_TypeWriterEffect.instance.RandomText[32] = "오늘은 꿈에서 엄마를 보았어, 엄마가 나를 꼭 안아주었는데 너무 따뜻한 거 있지?";
            Game_TypeWriterEffect.instance.RandomText[33] = "... 엄마의 품을 벗어나기 싫어 떼를 쓰려고 하니 눈이 떠졌어.";
            Game_TypeWriterEffect.instance.RandomText[34] = "꿈은 과거를 다시 되새기기도, 자신이 원하는 상황을 보여주기도 한대";
            Game_TypeWriterEffect.instance.RandomText[35] = "악몽은 내가 두려워하는 상황을 보여주기도 해.";
            Game_TypeWriterEffect.instance.RandomText[36] = "이곳에 와서 악몽을 꾼 적이 많아.";
            Game_TypeWriterEffect.instance.RandomText[37] = "가장 많이 꾼 악몽은 ... 아무 말도 하지 못하고 계속 걷기만 하는 거였어.";
            Game_TypeWriterEffect.instance.RandomText[38] = "할머니는 즐거울 때 아리랑을 부르곤 했었는데 갑자기 생각났어.";
            Game_TypeWriterEffect.instance.RandomText[39] = "휴일에 아침 늦게 일어나서 먹던 시리얼을 좋아했는데";
            Game_TypeWriterEffect.instance.RandomText[40] = "내가 시리얼을 먹는 이유는 시리얼의 달콤함이 묻은 우유를 마시고 싶어서였어.";
            Game_TypeWriterEffect.instance.RandomText[41] = "고래는 이 통로에 들어오지 못하겠다. 뜨겁기도 하고 좁기도 해.";
            Game_TypeWriterEffect.instance.RandomText[42] = "별들은 뜨거운 걸 느낄 수 있을까? 미츄에게 물어볼 걸 그랬어";
            Game_TypeWriterEffect.instance.RandomText[43] = "내가 좋아했던 영화에서 주인공은 노래를 불렀어, 제목이 기억해 줘 였어.";
            Game_TypeWriterEffect.instance.RandomText[44] = "우리는 왜 누군가의 기억 속에 남고 싶은 걸까?";
            Game_TypeWriterEffect.instance.RandomText[45] = "... 숨이 턱 막히는 기분이야.";
            Game_TypeWriterEffect.instance.RandomText[46] = "어릴 때 엄마를 따라 들어간 찜질방에 들어간 느낌?";
            Game_TypeWriterEffect.instance.RandomText[47] = "이곳에 물이 있었으면 수증기가 엄청났을거야.";
            Game_TypeWriterEffect.instance.RandomText[48] = "계란을 이 길에 올려두면 익을까? 익기도 전에 삶은 계란이 됐을 거야.";
            Game_TypeWriterEffect.instance.RandomText[49] = "부지런하게 걸어야 해.";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = ".... 여기가 어디더라...";
            Game_TypeWriterEffect.instance.RandomText[1] = "..... 다리가 마음대로 움직이는 것 같아";
            Game_TypeWriterEffect.instance.RandomText[2] = "이 길에서 내가 통제할 수 있는 건 없을지도.";
            Game_TypeWriterEffect.instance.RandomText[3] = "아무것도 없어...";
            Game_TypeWriterEffect.instance.RandomText[4] = "조용해...";
            Game_TypeWriterEffect.instance.RandomText[5] = "앞으로 나아가고 있는 게 맞는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[6] = "가끔씩  보이는 유성이 아니었다면 이곳이 우주였는지 망각했을 거야.";
            Game_TypeWriterEffect.instance.RandomText[7] = "여긴 우주야...";
            Game_TypeWriterEffect.instance.RandomText[8] = "이곳은 답답해...";
            Game_TypeWriterEffect.instance.RandomText[9] = "크게 소리를 쳐볼까? 사실 소리쳐도 문제 될 건 없을거야.";
            Game_TypeWriterEffect.instance.RandomText[10] = "나 여기 있어요!!! ... 목소리가 쉰 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[11] = "이 표지판들은 내 불안함을 알고 이곳에 세워둔 걸까?";
            Game_TypeWriterEffect.instance.RandomText[12] = "여기가 어디인지 .. 정확하게는 알 수 없네";
            Game_TypeWriterEffect.instance.RandomText[13] = "라디오가 제대로 작동하고 있어.. 노이즈도 없이.";
            Game_TypeWriterEffect.instance.RandomText[14] = "라디오가 나를 위로해 주려고 하나 봐.";
            Game_TypeWriterEffect.instance.RandomText[15] = "다들 바쁘게 하루를 살아가고 있구나, ";
            Game_TypeWriterEffect.instance.RandomText[16] = "누군가의 웃음소리를 들을 수 있어서 기뻐.\n 저 사람은 자신의 웃음소리가 나에게 위안이 되었다는 걸 알까?";
            Game_TypeWriterEffect.instance.RandomText[17] = "배고프다...";
            Game_TypeWriterEffect.instance.RandomText[18] = " 다들 즐거운 것 같아. 혹시 나는 내 삶의 기쁨을 놓치고 있는 게 아닐까?";
            Game_TypeWriterEffect.instance.RandomText[19] = "걱정하지 말자, 내 노력들은 보상받게 될 거야.";
            Game_TypeWriterEffect.instance.RandomText[20] = "라디오에 내 이야기가 나오면 좋겠어, 내 이야기가 전달되어서 말이야.";
            Game_TypeWriterEffect.instance.RandomText[21] = "내가 나오는 라디오 방송은 큰 인기를 끌 거야! 우주에서 소식을 전하는 사람은 없었을 테니 말이야.";
            Game_TypeWriterEffect.instance.RandomText[22] = "지구 여러분 제 이야기를 들어봐요!";
            Game_TypeWriterEffect.instance.RandomText[23] = "하아....";
            Game_TypeWriterEffect.instance.RandomText[24] = "무인도에서 배구공과 얘기를 하던 사람이 떠올라, 지금의 내가 그런 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[25] = "괜찮아진 줄 알았는데 괜찮지 않은 것 같아... 아프다..";
            Game_TypeWriterEffect.instance.RandomText[26] = "... 약이나 해결책이 필요해";
            Game_TypeWriterEffect.instance.RandomText[27] = "막연한 불안감이 드는 건 어쩔 수 없나 봐. 이 길을 봐.. .끝없이 이어져있잖아.";
            Game_TypeWriterEffect.instance.RandomText[28] = "우주가 팽창하듯 길이 팽창하여 늘어나고 있으면 어떻게 하지?";
            Game_TypeWriterEffect.instance.RandomText[29] = "... 지구는 오늘도 바쁘게 흘러가는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[30] = "다리가 저려...";
            Game_TypeWriterEffect.instance.RandomText[31] = "... 이곳에 누가 나와 대화를 할 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[32] = "누군가와 웃고 떠드는 일이 이렇게 먼 날이 될 줄이야.";
            Game_TypeWriterEffect.instance.RandomText[33] = "나는 이때까지 고독을 모르는 채 살아왔나 봐.";
            Game_TypeWriterEffect.instance.RandomText[34] = "주택난이 심각하다던데... 우주로 진출하면 그것도 해결될 거야.";
            Game_TypeWriterEffect.instance.RandomText[35] = "과학의 날 행사에서 항상 미래도시 그리기를 했어. 그때 도시의 배경은 우주였지.";
            Game_TypeWriterEffect.instance.RandomText[36] = "우주는 검은색일 줄만 알았는데, 다양한 색이 있는 게 신기해.";
            Game_TypeWriterEffect.instance.RandomText[37] = "…! 저 표지판 방금 두 개로 보이지 않았어?";
            Game_TypeWriterEffect.instance.RandomText[38] = "... 혹시 이 소리.. 내 배에서 난 소리야?";
            Game_TypeWriterEffect.instance.RandomText[39] = "지금 제일 원하는 건 휴식이야...";
            Game_TypeWriterEffect.instance.RandomText[40] = "우주에는 걷는 것 밖에 없는데 뭐가 힘드냐고 말하는 사람이 없어서 다행이야";
            Game_TypeWriterEffect.instance.RandomText[41] = "우리는 우주의 티끌이라고 우스갯소리처럼 말을 하곤 하는데... 사실 맞는 말이야";
            Game_TypeWriterEffect.instance.RandomText[42] = "다른 누군가 이 길에 떨어진다면 어떻게 반응할까?";
            Game_TypeWriterEffect.instance.RandomText[43] = "... 이기적일지 모르지만 정말 누군가 이 길로 떨어져 함께 걸을 수 있으면 좋겠어";
            Game_TypeWriterEffect.instance.RandomText[44] = "오늘의 내 운세는 누군가를 그리워하는 하루 일 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[45] = "라디오에서 나오는 소리들은… 지금 실시간 지구의 소리일까?";
            Game_TypeWriterEffect.instance.RandomText[46] = "이 소식들은 라디오에 녹음되었던 것들이 흘러나오는 것일지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[47] = "내가 지구를 떠났을 때 몇 월 며칠이었을까?";
            Game_TypeWriterEffect.instance.RandomText[48] = "기억이 돌아오면 좋겠어. 그럼 이 막연함도 사라질 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[49] = "멀어진 일상을 일상이라고 부를 수 있을까?";

        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "엄청 화려한 느낌이야.";
            Game_TypeWriterEffect.instance.RandomText[1] = "방금 지나온곳과 비교하니 눈이 부신것 같아.";
            Game_TypeWriterEffect.instance.RandomText[2] = "이건 식물들이겠지?";
            Game_TypeWriterEffect.instance.RandomText[3] = "식물들 사이에 우주꽃들도 있어.";
            Game_TypeWriterEffect.instance.RandomText[4] = "누가 가꾼 정원같아.";
            Game_TypeWriterEffect.instance.RandomText[5] = "라디오의 마음을 잘 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[6] = "사라진다는건 어떤걸까? 나는 상상이 안돼...";
            Game_TypeWriterEffect.instance.RandomText[7] = "우주의 식물들은 다 별가루를 먹고 사는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[8] = "아픈건 이제 나은 기분이야.";
            Game_TypeWriterEffect.instance.RandomText[9] = "이 식물의 이름은 뭘까?";
            Game_TypeWriterEffect.instance.RandomText[10] = "우리는 서로에게 이름을 지어주는데 다른 생명도 그렇게 이름을 지을지 궁금해";
            Game_TypeWriterEffect.instance.RandomText[11] = "서로를 필요 한다는건 정말 좋은 일이야.";
            Game_TypeWriterEffect.instance.RandomText[12] = "일방적인 관계는 그렇게 행복하지 않대.";
            Game_TypeWriterEffect.instance.RandomText[13] = "이 라디오는 ...우주 통역기 기능도 있나봐.";
            Game_TypeWriterEffect.instance.RandomText[14] = "우주는 비가 내리지 않으니 너희는 비가 뭔지 모를거야.";
            Game_TypeWriterEffect.instance.RandomText[15] = "지구는 비가 내려서 그런 날이면 우산이나 우비를 사용해";
            Game_TypeWriterEffect.instance.RandomText[16] = "... 그러고보니 나는 우주복을 입었으니 비가와도 우산이 필요없겠다.";
            Game_TypeWriterEffect.instance.RandomText[17] = "식물원에서 보는 식물보단 자연에서 보는 식물이 더 좋은거 같아.";
            Game_TypeWriterEffect.instance.RandomText[18] = "여긴..자연에서 보는 식물이라고 볼 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[19] = "우주꽃을 찾으려고 하니 더 안보이는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[20] = "저 꽃은 아직 피지 않았어...";
            Game_TypeWriterEffect.instance.RandomText[21] = "우주식물도 열매를 맺을까? 열매를 한번 맛 보고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[22] = "우주 꽃을 몇 송이 들고다니면 위안이 될거야. 시들지도 모르지만...";
            Game_TypeWriterEffect.instance.RandomText[23] = "너희를 지구에서 보는것보다 우주에서 보는게 더 의미있을거야.";
            Game_TypeWriterEffect.instance.RandomText[24] = "... 우주에도 계절이 있을까? 만약 있다면 주기가 엄청 길것 같아";
            Game_TypeWriterEffect.instance.RandomText[25] = "지금의 우주는 평화로워 보이니 봄일지 몰라";
            Game_TypeWriterEffect.instance.RandomText[26] = "우주를 오고싶어했던 친구...가 떠올라. 누군가와 약속했었나봐.";
            Game_TypeWriterEffect.instance.RandomText[27] = "그 친구는 나와 많은 이야기를 나누었어. 마치 서로를 다 알것 처럼 말이야.";
            Game_TypeWriterEffect.instance.RandomText[28] = "... 이 길을 걸으면서 누군가와 함께 걸으면 좋겠다고 계속 생각했어.";
            Game_TypeWriterEffect.instance.RandomText[29] = "이 길에서 느낀 가장 큰 고통은 외로움이었어.";
            Game_TypeWriterEffect.instance.RandomText[30] = "언젠가 이곳에 다시 올 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[31] = "... 길을 걸으면서 모아진 조각은 대체 뭘까?";
            Game_TypeWriterEffect.instance.RandomText[32] = "벽화에 있던 이야기들은 진짜일까?";
            Game_TypeWriterEffect.instance.RandomText[33] = "소원을 이루어지는게 달에 있다는걸 알았다면 벌써 다른사람들이 달에 도착했을꺼야";
            Game_TypeWriterEffect.instance.RandomText[34] = "사람들의 소원은 뭘까?";
            Game_TypeWriterEffect.instance.RandomText[35] = "사실 지금 내 소원이 뭔지 잘 모르겠어. 지구에 돌아가고 싶은게 맞겠지?";
            Game_TypeWriterEffect.instance.RandomText[36] = "어둠이 무서운 이유는 본능적인거래.\n어두운건 안보이는거고, 안보이는곳에선 뭐가 나올지 모르거든.";
            Game_TypeWriterEffect.instance.RandomText[37] = "너 ... 정말 예쁘게 생겼다. 옆에 너는 멋지게 생긴 식물이고 !";
            Game_TypeWriterEffect.instance.RandomText[38] = "내 기억이 제대로 돌아왔으면 좋겠어. . . 지금은 기억이 파편처럼 떠올라.";
            Game_TypeWriterEffect.instance.RandomText[39] = "산소통을 넉넉하게 사둘까? 조난당한 누군가를 만나서 많이 필요할지 모르잖아.";
            Game_TypeWriterEffect.instance.RandomText[40] = "별가루를 사용하면 몸이 가볍고 힘이 넘치는 기분이 들어.";
            Game_TypeWriterEffect.instance.RandomText[41] = "앞의 길에 또 벽화가 있을까?";
            Game_TypeWriterEffect.instance.RandomText[42] = "아마 앞길에 또 흑과 백으로 된 공간이나 포자가 날아다니는 곳이 있겠지?";
            Game_TypeWriterEffect.instance.RandomText[43] = "어쩌면 앞에 달콤한 디저트로만든 환상적인 공간이 있을지 몰라! ";
            Game_TypeWriterEffect.instance.RandomText[44] = "라디오야, 라디오야, 지구의 소식을 말해봐.";
            Game_TypeWriterEffect.instance.RandomText[45] = "흠 .. .그래그래. 앞으로 쭉 걸으면 되겠어.";
            Game_TypeWriterEffect.instance.RandomText[46] = "개나리 노오란 꽃그늘아래~ 가지런히 놓여있는 꼬까신하나~";
            Game_TypeWriterEffect.instance.RandomText[47] = "내가 우주에 갔다왔다는걸 친구들이 알면 날 부러워 할 거야.";
            Game_TypeWriterEffect.instance.RandomText[48] = "이 곳에 오래 있었어.\n그건 누군가가 날 오래 그리워했을거라는 말이지.";
            Game_TypeWriterEffect.instance.RandomText[49] = "여름저녘에 울리던 풀벌레 소리가 어렴풋하게 떠올라.";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "너와 함께 걷게 돼서 기뻐.";
            Game_TypeWriterEffect.instance.RandomText[1] = "대화를 하면서 걸어본 적이 있는데 그때 시간이 무척이나 빨리 갔어.";
            Game_TypeWriterEffect.instance.RandomText[2] = "너와 함께하는 시간도 빨리 흘러갈까 봐 겁이 나기도 든든하기도 해.";
            Game_TypeWriterEffect.instance.RandomText[3] = "또 네가 빛을 잃는다면 내가 다시 우주꽃을 너에게 줄게";
            Game_TypeWriterEffect.instance.RandomText[4] = "너는 참 예쁘게 빛나는 별이야.";
            Game_TypeWriterEffect.instance.RandomText[5] = "무언가 날아다니고 있어...";
            Game_TypeWriterEffect.instance.RandomText[6] = "우와 저기 봐!";
            Game_TypeWriterEffect.instance.RandomText[7] = "저건 뭐라고 부르는거야? 앗, 저것도!";
            Game_TypeWriterEffect.instance.RandomText[8] = "너는 우주에 대해서 많은 걸 알고 있구나";
            Game_TypeWriterEffect.instance.RandomText[9] = "너는 우주에 대해서 많은 걸 알고 있구나";
            Game_TypeWriterEffect.instance.RandomText[10] = "너도 이곳에 오기전에 있는 흑백의 공간을 알고 있니?";
            Game_TypeWriterEffect.instance.RandomText[11] = "벽화가 그려진 통로는 알고 있어?";
            Game_TypeWriterEffect.instance.RandomText[12] = "우와 우주에 그런 일들이 있었다니 .. 나도 보고 싶다!";
            Game_TypeWriterEffect.instance.RandomText[13] = "오늘은 좋은 날이야. 너에게 물어볼 것들이 잔뜩 생각났거든!";
            Game_TypeWriterEffect.instance.RandomText[14] = "별들은 어디서 태어나는 거야?";
            Game_TypeWriterEffect.instance.RandomText[15] = "네가 어떻게 태어났는지 기억해? 나? 나는.... 모르겠는걸! 뭐? 너도?";
            Game_TypeWriterEffect.instance.RandomText[16] = "너도 모르는 것들이 있구나..\n하긴 나도 지구의 모든 것을 아는 건 아니야";
            Game_TypeWriterEffect.instance.RandomText[17] = "저기 저 별들이 보여? 너와 비슷하게 생긴 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[18] = "너는 형제가 있니? 나는... 내가 생각하기론 없어!";
            Game_TypeWriterEffect.instance.RandomText[19] = "나 말고도 다른 생명을 본 적이 있구나!";
            Game_TypeWriterEffect.instance.RandomText[20] = "나처럼 생긴 사람을 봤던 기억은 없니?\n하긴 내가 흔하게 생기지 않긴 했어.";
            Game_TypeWriterEffect.instance.RandomText[21] = "오늘도 내일도 기분이 좋을 예정이야.";
            Game_TypeWriterEffect.instance.RandomText[22] = "오늘 하는 선택들이 날 좋은 길로 이끌 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[23] = "저어기 날아가는 생명은 지구의 새와 닮은 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[24] = "우주새라고 불러도 되겠지?";
            Game_TypeWriterEffect.instance.RandomText[25] = "지구의 것들이 사실 우주에 있던 것들을 본뜬 거라더니…";
            Game_TypeWriterEffect.instance.RandomText[26] = "고래도 사실 우주에 있던 친구들이 지구에 왔나 봐";
            Game_TypeWriterEffect.instance.RandomText[27] = "별들에게도 이름이 있어... 너희는 이름을 어떻게 지었니?";
            Game_TypeWriterEffect.instance.RandomText[28] = "네가 가장 행복할 때는 언제야?";
            Game_TypeWriterEffect.instance.RandomText[29] = "이 앞에 별자리들이 가득하다니 기대된다.";
            Game_TypeWriterEffect.instance.RandomText[30] = "별자리들이 우리에게 친절하면 좋겠어.";
            Game_TypeWriterEffect.instance.RandomText[31] = "지구에도 블랙홀이 있을지 몰라.\n지우개를 떨어트리면 꼭 사라지곤 했거든.";
            Game_TypeWriterEffect.instance.RandomText[32] = "너무 작은 블랙홀은 지우개만 쏙쏙 삼켜.";
            Game_TypeWriterEffect.instance.RandomText[33] = "너를 위한 우주꽃을 몇 송이 찾아올 걸 그랬어.";
            Game_TypeWriterEffect.instance.RandomText[34] = "너는 혼자 우주를 떠돌며 외로웠던 적이 없니?";
            Game_TypeWriterEffect.instance.RandomText[35] = "나는 아직 네가 말한 사라진다는 것이 어떤 기분일 지 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[36] = "앞으로의 여행은 별 탈이 없길 바라.";
            Game_TypeWriterEffect.instance.RandomText[37] = "새들아 안녕~!!!";
            Game_TypeWriterEffect.instance.RandomText[38] = "별들은 다 너처럼 친절하니?";
            Game_TypeWriterEffect.instance.RandomText[39] = "많은 별 친구를 사귀고 싶어, 맞아. 많다고 좋은 건 아니지!";
            Game_TypeWriterEffect.instance.RandomText[40] = "아이쿠! 우주 라디오를 트는 걸 까먹었어! 처음 있는 일이야.";
            Game_TypeWriterEffect.instance.RandomText[41] = "네가 말한 우주를 보려면 평생 우주를 관광해야겠다.";
            Game_TypeWriterEffect.instance.RandomText[42] = "응 그건 꼭 같이 보자, 가는 길에 있으면 더 좋고";
            Game_TypeWriterEffect.instance.RandomText[43] = "너도 나처럼 수다쟁이여서 다행이야! 나만 말하면 미안하잖아!";
            Game_TypeWriterEffect.instance.RandomText[44] = "이 앞에 보이는 달이 나의 목적지야";
            Game_TypeWriterEffect.instance.RandomText[45] = "저 달이 점점 커지는 걸 느낄 때 보람을 느껴.";
            Game_TypeWriterEffect.instance.RandomText[46] = "내가 너를 이길에 너무 잡아두는 게 아닐까 걱정돼";
            Game_TypeWriterEffect.instance.RandomText[47] = "내가 가진 별가루를 네게 뿌리면 더 빛나지 않을까? 필요 없어?";
            Game_TypeWriterEffect.instance.RandomText[48] = "그때의 오로라를 너도 보았구나!";
            Game_TypeWriterEffect.instance.RandomText[49] = "나중에 나와 오로라를 구경하자";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "이건 지구의 눈 같아...";
            Game_TypeWriterEffect.instance.RandomText[1] = "... 너와 내가 아니면 주변에 아무런 소리가 들리지 않겠지";
            Game_TypeWriterEffect.instance.RandomText[2] = "지구에선 이런 걸 고요하다고 말해";
            Game_TypeWriterEffect.instance.RandomText[3] = "연기가 자욱해서 길이 잘 보이지 않아.";
            Game_TypeWriterEffect.instance.RandomText[4] = "길가의 전구는 누가 달아둔 걸까?";
            Game_TypeWriterEffect.instance.RandomText[5] = "어라! 저건 블랙홀인가?";
            Game_TypeWriterEffect.instance.RandomText[6] = "이곳에는 너무 다양한 것들이 있어서 무얼 먼저 보아야 할지 잘 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[7] = "바닥을 잘 살펴야겠어. 길이 아닌 곳을 밟으면 어떡해.";
            Game_TypeWriterEffect.instance.RandomText[8] = "전에 봤던 달의 조각들은 너희에게 어떤 의미야?";
            Game_TypeWriterEffect.instance.RandomText[9] = "나도 감사할 때 나눠줄 수 있는 나의 마음을 담은 달의 조각이 있으면 좋겠어.";
            Game_TypeWriterEffect.instance.RandomText[10] = "만약 내가 우주에서 뭔가를 잃어버린다면 찾을 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[11] = "룬, 우주 미아가 되지 않도록 내 옆에 꼭 붙어있어!";
            Game_TypeWriterEffect.instance.RandomText[12] = "원래 블랙홀은 이론상에만 존재하던 거였대.";
            Game_TypeWriterEffect.instance.RandomText[13] = "천체망원경을 통해 지구에서 잘 볼 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "룬, 너를 내 친구들에게 소개해 주고 싶어. 친구들도 널 좋아할 거야.";
            Game_TypeWriterEffect.instance.RandomText[15] = "어쩌면 엄마가 널 우리 집에 초대해 같이 저녁을 먹을 수 있을지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[16] = "우주를 떠나 우리 행성에 올 수 있니?";
            Game_TypeWriterEffect.instance.RandomText[17] = "널 보니 생각나는 노래가 있어. '작은 별'이라는 노래야.";
            Game_TypeWriterEffect.instance.RandomText[18] = "반짝반짝 작은 별 아름답게 비치네~";
            Game_TypeWriterEffect.instance.RandomText[19] = "사실 지구에서도 최근에는 동요를 들은 적이 없어. 애들이 유치하다고 했거든!";
            Game_TypeWriterEffect.instance.RandomText[20] = "어른들이 동요를 부를 때 무언가를 떠올리듯 깊은 눈으로 먼 곳을 봤던 게 떠올라";
            Game_TypeWriterEffect.instance.RandomText[21] = "과학자들이 날 발견하고 외계 생물로 발표하면 어떻게 하지?";
            Game_TypeWriterEffect.instance.RandomText[22] = ".... 너무 말을 많이 했나 봐 입이 아픈걸...?";
            Game_TypeWriterEffect.instance.RandomText[23] = "너는 입이 아프지 않니?";
            Game_TypeWriterEffect.instance.RandomText[24] = "우주에 있으면 말이 많아지는 것 같아. 너도 공감하지?";
            Game_TypeWriterEffect.instance.RandomText[25] = "네가 말한 별자리는 아직 멀었겠지?";
            Game_TypeWriterEffect.instance.RandomText[26] = "오는 길에 곳곳에 떨어진 별가루를 주웠었어.";
            Game_TypeWriterEffect.instance.RandomText[27] = "별가루는 어떤 때에 떨어지는 거야?";
            Game_TypeWriterEffect.instance.RandomText[28] = "말이라도 하지 않으면 정말 혼자인 느낌이 들어.";
            Game_TypeWriterEffect.instance.RandomText[29] = "더 많은 우주 생명체를 만나고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[30] = "나는 다음 생에 돌로 태어나고 싶었는데, 지금 생각해 보니 취소해야겠어.\n" +
               "내가 생각하지 못했던 세상이 이렇게나 넓은데\n" +
               "아무것도 느끼지 않고 생각할 수 없는 존재가 되면 아쉬울 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[31] = "눈이다 눈. 눈이라고 생각하니 이곳이 좋아.";
            Game_TypeWriterEffect.instance.RandomText[32] = "눈이 올 때는 밖에 나가서 눈사람을 만들었는데... 이곳에서는 쌓이진 않는구나...";
            Game_TypeWriterEffect.instance.RandomText[33] = "자연이라는 건 정말 위대해.";
            Game_TypeWriterEffect.instance.RandomText[34] = "우주는 정말 끝없는 상상이 필요한 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[35] = "배고프다. 룬, 너는 배고픔을 알지 못하겠지?";
            Game_TypeWriterEffect.instance.RandomText[36] = "우주 라디오를 통해서 듣는 네 목소리가 큰 위안이 돼.";
            Game_TypeWriterEffect.instance.RandomText[37] = "이 길을 걷다 누군가를 만날 것만 같은 기분이야.";
            Game_TypeWriterEffect.instance.RandomText[38] = "룬, 직감이라는 걸 이해할 수 있어?";
            Game_TypeWriterEffect.instance.RandomText[39] = "우주에 나와 같은 사람이 또 있을까? 있다고 믿고 싶어.\n벽화 속에는 분명 사람 같아 보이는 형체가 있었어.";
            Game_TypeWriterEffect.instance.RandomText[40] = "룬, 이곳에 나 말고 다른 사람이 왔었니?";
            Game_TypeWriterEffect.instance.RandomText[41] = "넌 말하기 싫을 땐 모르는 척하더라.";
            Game_TypeWriterEffect.instance.RandomText[42] = "룬, 너와 대화할 수 있어 기뻐.";
            Game_TypeWriterEffect.instance.RandomText[43] = "달의 조각은 어떻게 만들어지는지 알려줄 수 있어?";
            Game_TypeWriterEffect.instance.RandomText[44] = "난 가끔 스트레스를 풀 때 있는 힘껏 뛰어.";
            Game_TypeWriterEffect.instance.RandomText[45] = "나는 내가 보고 있는 게 사실 전부 믿기지는 않아.";
            Game_TypeWriterEffect.instance.RandomText[46] = "룬, 너에게는 엄마가 있어?\n나는 엄마가 있었지만 지금은 없어. 지구에 계실까?";
            Game_TypeWriterEffect.instance.RandomText[47] = "한참을 걸은 것 같은데...";
            Game_TypeWriterEffect.instance.RandomText[48] = "우주를 떠도는 영혼은 다 너와 같은 죽은 별인 거야?";
            Game_TypeWriterEffect.instance.RandomText[49] = "나도 여기서 있다가 별이 될 수 있을까?";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "우와, 예쁜 별자리가 가득해.";
            Game_TypeWriterEffect.instance.RandomText[1] = "이 예쁜 별자리를 너와 함께 봐서 기뻐.";
            Game_TypeWriterEffect.instance.RandomText[2] = "저기 수많은 별들을 좀 봐.";
            Game_TypeWriterEffect.instance.RandomText[3] = "지구에서 별자리를 찾아다녔을 때가 생각나.";
            Game_TypeWriterEffect.instance.RandomText[4] = "룬, 네가 있어 너무 다행이야.";
            Game_TypeWriterEffect.instance.RandomText[5] = "네 목소리를 듣고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[6] = "왜 이러지... 우주 라디오가 말썽이네.";
            Game_TypeWriterEffect.instance.RandomText[7] = "지구에서는 태어난 날짜에 따라 사람마다 별자리가 정해져.";
            Game_TypeWriterEffect.instance.RandomText[8] = "나의 별자리는 게자리야.";
            Game_TypeWriterEffect.instance.RandomText[9] = "계속 걸으니 다리가 아파.";
            Game_TypeWriterEffect.instance.RandomText[10] = "저어기 별자리는 뭘까?";
            Game_TypeWriterEffect.instance.RandomText[11] = "별자리를 좋아해서 공부를 많이 했는데, 지금은 기억이 안 나.";
            Game_TypeWriterEffect.instance.RandomText[12] = "가장 반짝이는 별은 어디에서 왔을까?";
            Game_TypeWriterEffect.instance.RandomText[13] = "지구의 사람들은 나를 기억할까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "뭔가를 떠올려 보고 싶은데 기억이 나질 않아...";
            Game_TypeWriterEffect.instance.RandomText[15] = "우주 라디오는 어떻게 작동하는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[16] = "아 또 배가 고픈 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[17] = "우주라는 곳은 참 신기해.";
            Game_TypeWriterEffect.instance.RandomText[18] = "이 길을 끊임없이 걷고는 있지만, 살아남을 수 있을지 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[19] = "룬, 대답 좀 해줘.";
            Game_TypeWriterEffect.instance.RandomText[20] = "비상식량이 필요해. 너무 허기가 지는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[21] = "빨간색, 파란색, 초록색, 노란색... 별의 색은 참 많아.";
            Game_TypeWriterEffect.instance.RandomText[22] = "이곳에 있으면 나의 존재에 대해 고민하게 되는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[23] = "여기에선 내가 불필요한 존재일까...?";
            Game_TypeWriterEffect.instance.RandomText[24] = "사실 여기가 우주인지 내 망상 속인지 점점 구별하기가 힘들어.";
            Game_TypeWriterEffect.instance.RandomText[25] = "룬, 너의 존재만으로도 너무 큰 힘이 돼.";
            Game_TypeWriterEffect.instance.RandomText[26] = "외로움이라는 건 생각보다 훨씬 무서운 거였어.";
            Game_TypeWriterEffect.instance.RandomText[27] = "만약 지구로 돌아가게 된다면 어떤 이야기부터 해야 할까.";
            Game_TypeWriterEffect.instance.RandomText[28] = "내가 겪고 본 많은 일을 기록한 책을 써보고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[29] = "이렇게 평생 걷기만 하다 잊히기는 싫어.";
            Game_TypeWriterEffect.instance.RandomText[30] = "달의 조각은 마음이 적적할 때마다 꺼내보면 기분이 좋아.";
            Game_TypeWriterEffect.instance.RandomText[31] = "달의 이름은 왜 달일까?";
            Game_TypeWriterEffect.instance.RandomText[32] = "지구에도 엄청나게 많은 생명체가 존재하는데 이 큰 우주에는 얼마나 더 많을까?";
            Game_TypeWriterEffect.instance.RandomText[33] = "별자리는 지구에서도 볼 수 있어서인지 나에게 너무 익숙해.";
            Game_TypeWriterEffect.instance.RandomText[34] = "이건 염소자리인가...?";
            Game_TypeWriterEffect.instance.RandomText[35] = "내 친구의 별자리인 것 같아. 뭐였더라... 아! 물병자리였어.";
            Game_TypeWriterEffect.instance.RandomText[36] = "저건 오리온자리인 것 같은데...";
            Game_TypeWriterEffect.instance.RandomText[37] = "어! 저기 게자리다! 내 별자리야!";
            Game_TypeWriterEffect.instance.RandomText[38] = "한참을 걷다 보니 정말 시간이 어떻게 가는지 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[39] = "누군가의 별자리라고 생각하니 덜 외로운 것 같기도 해.";
            Game_TypeWriterEffect.instance.RandomText[40] = "낮과 밤이 있다는 건 참 행복한 거구나.";
            Game_TypeWriterEffect.instance.RandomText[41] = "우주에서 걷는 순간부터 잠이 오지 않아. 신기해.";
            Game_TypeWriterEffect.instance.RandomText[42] = "룬, 내 옆에 있어줘서 너무 고마워.";
            Game_TypeWriterEffect.instance.RandomText[43] = "별에게도 정해진 자리가 있는 게 신기해.";
            Game_TypeWriterEffect.instance.RandomText[44] = "여기까지 걸어온 것도 참 대단한 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[45] = "지구에서는 겁쟁이였던 것 같은데 여기서는 겁쟁이일 수가 없어.";
            Game_TypeWriterEffect.instance.RandomText[46] = "계속해서 걸어가자. 힘을 내자!";
            Game_TypeWriterEffect.instance.RandomText[47] = "다리가 아픈 것 같기도 하고...";
            Game_TypeWriterEffect.instance.RandomText[48] = "룬, 룬. 너의 이름을 자꾸만 부르고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[49] = "우주 휴게소에 도착하면 필요한 걸 사야겠어.";
        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {
        
            Game_TypeWriterEffect.instance.RandomText[0] = "여긴 어디지.....?";
            Game_TypeWriterEffect.instance.RandomText[1] = "우주에 있었던 건 꿈이었나?";
            Game_TypeWriterEffect.instance.RandomText[2] = "익숙하고 포근해.";
            Game_TypeWriterEffect.instance.RandomText[3] = "흐흐흥, 흐흥~♪ 기분 좋아♪";
            Game_TypeWriterEffect.instance.RandomText[4] = "어, 저건 내가 썼던 침대인 것 같은데?";
            Game_TypeWriterEffect.instance.RandomText[5] = "내가 좋아하는 아이스크림이다!";
            Game_TypeWriterEffect.instance.RandomText[6] = "여기에 멈춰있으면 지구에 사는 것처럼 살 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[7] = "가까이 다가가니 다 연기처럼 사라지네...";
            Game_TypeWriterEffect.instance.RandomText[8] = "여긴 지구가 아니야. 정신 차려야 해!";
            Game_TypeWriterEffect.instance.RandomText[9] = "정신을 못 차리겠어. 자꾸만 여기 있고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[10] = "누가 내 머릿속을 이곳에 옮겨둔 것 같아";
            Game_TypeWriterEffect.instance.RandomText[11] = "그렇게 가득한 우주를 봤지만 결국 나는 지구만 생각하고 있었나 봐";
            Game_TypeWriterEffect.instance.RandomText[12] = "그리운 물건들이야, 물건에는 사람마다의 추억이 담겨있어.";
            Game_TypeWriterEffect.instance.RandomText[13] = "이곳은 물건은 가득하지만 추억 속의 사람까지는 되살리지 못하구나...";
            Game_TypeWriterEffect.instance.RandomText[14] = "분명히 물건엔 행복한 기억이 가득 했었어.";
            Game_TypeWriterEffect.instance.RandomText[15] = "엄마와 놀러나가면 꼭 아이스크림을 사주셨어.";
            Game_TypeWriterEffect.instance.RandomText[16] = "행복의 일부는 아이스크림의 형태를 하고 있나 봐";
            Game_TypeWriterEffect.instance.RandomText[17] = "확실히 저건 내가 좋아했던 음식인것 같아.";
            Game_TypeWriterEffect.instance.RandomText[18] = "룬, 저거 봐! 어라? 룬이 언제부터 옆에 없었지?";
            Game_TypeWriterEffect.instance.RandomText[19] = "옆이 허전해, 이곳에 대해서 룬에게 설명해 주고 싶은데";
            Game_TypeWriterEffect.instance.RandomText[20] = "여기는.. 내 추억이 가득해, 날 이루고 있는 것들을 보여주는 걸까?";
            Game_TypeWriterEffect.instance.RandomText[21] = "좀 더 많은 것들로 이곳을 채우고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[22] = "이곳이 나의 내면이라면 사람들을 가득 초대해서 어울리고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[23] = "이곳은 꿈일까? 나의 소원일까? 소원이라기엔 너무 적적한걸.";
            Game_TypeWriterEffect.instance.RandomText[24] = "영원히 이곳에 있고싶어. 하지만 그러면 누가 날 찾을 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[25] = "이곳은 꼭꼭 숨겨둔 나의 보물 상자 같아.";
            Game_TypeWriterEffect.instance.RandomText[26] = "저건 사진인가?... 잘 안 보여.";
            Game_TypeWriterEffect.instance.RandomText[27] = "가까이에서 자세히 보고 싶어. 하지만 그럴 수 없네...";
            Game_TypeWriterEffect.instance.RandomText[28] = "습관적으로 달을 봤는데 ... 달이 보이지 않네.";
            Game_TypeWriterEffect.instance.RandomText[29] = "이 길의 끝에 .... 다들 날 기다리고 있지 않을까?";
            Game_TypeWriterEffect.instance.RandomText[30] = "... 누군가를 만날 수 있을 것 같은데 그러기엔 너무 적막한 공간이야...";
            Game_TypeWriterEffect.instance.RandomText[31] = "내가 정말로 원하는 건  엄마와 친구들인데....";
            Game_TypeWriterEffect.instance.RandomText[32] = "어릴 때 모았던 딱지와 단추들이 들어있는 박스야...";
            Game_TypeWriterEffect.instance.RandomText[33] = "저건 잃어버려서 한참 못 찾았던 인형인데 ...!";
            Game_TypeWriterEffect.instance.RandomText[34] = "어릴 때 내가 좋아했던 장난감이 있어.";
            Game_TypeWriterEffect.instance.RandomText[35] = "저건 .. 뭐지? 너무 옛날의 일이라서 기억이 안 나 나봐. 아기 때 쓰던 걸까?";
            Game_TypeWriterEffect.instance.RandomText[36] = "... 엄마가 쓰던 빗이야.\n나는 저 빗의 손잡이에 찍힌 무늬를 쓰다듬는 걸 좋아했었는데";
            Game_TypeWriterEffect.instance.RandomText[37] = "이곳은 사소한 습관까지 모여있어.";
            Game_TypeWriterEffect.instance.RandomText[38] = "... 솔직히 이곳을 벗어나면 또 언제 이 물건들을 보고 추억을 떠올릴 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[39] = "앨범은... 내가 잊어버린 기억도 다시 찾을 수 있는 신비한 책이야.";
            Game_TypeWriterEffect.instance.RandomText[40] = "맞아, 매일 저 침대에서 엄마가 잠을 깨웠어.";
            Game_TypeWriterEffect.instance.RandomText[41] = "저건 친구에게 받은 선물인데.. 어 저건 어린이날에 받은 거야.";
            Game_TypeWriterEffect.instance.RandomText[42] = "어릴 땐 저 액자속 그림이 나의 상상의 공간이 되곤 했어.";
            Game_TypeWriterEffect.instance.RandomText[43] = "저 컵은 어릴 때부터 내가 쓰던 거야, 나 말곤 아무도 저 컵을 쓰지 못하게 했었지!";
            Game_TypeWriterEffect.instance.RandomText[44] = "이 공간은 나의 실수나 잘못은 찾아볼 수 없어, 마치 날 잡아두려는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[45] = "아닙니다 달콤한 꿈을 꾸었습니다... 어른들이 보던 영화의 한 대사가 떠올라.";
            Game_TypeWriterEffect.instance.RandomText[46] = "중독은 사람을 행복하게 하지만 삶을 망치잖아. 행복은 모순적이야.";
            Game_TypeWriterEffect.instance.RandomText[47] = "... 이곳의 행복에 익숙해지되 중독되면 안 돼...";
            Game_TypeWriterEffect.instance.RandomText[48] = "이 공간은... 모순적이야.";
            Game_TypeWriterEffect.instance.RandomText[49] = "이곳을 벗어나지 않으면 과거의 행복에 갇혀 살게 될 거야.";
        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "달이 점점 가까이에 다가오는 게 느껴져.";
            Game_TypeWriterEffect.instance.RandomText[1] = "우주에 운석 같은 게 떠있어. 어, 운석이 아니네.";
            Game_TypeWriterEffect.instance.RandomText[2] = "둥둥 떠다는 저것을 하늘섬이라고 불러야겠어.";
            Game_TypeWriterEffect.instance.RandomText[3] = "우주꽃이 피어있네. 언제 봐도 참 예뻐.";
            Game_TypeWriterEffect.instance.RandomText[4] = "만질 수 없으니 아쉬워. 지구에서는 풀과 꽃이 많은 곳을 찾아다녔었는데...";
            Game_TypeWriterEffect.instance.RandomText[5] = "자꾸 지구를 회상하면 마음이 약해지는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[6] = "달의 조각이 많이 모였어.\n룬, 여기에 마음이 담겨 있다했지 그래서 그런지 자꾸만 열어보고 싶어.";
            Game_TypeWriterEffect.instance.RandomText[7] = "우와 저기 보이는 건 빛의 터널인 걸까? 예쁘다.";
            Game_TypeWriterEffect.instance.RandomText[8] = "지구에 이런 곳이 있었다면 다들 사진을 찍으려고 줄을 섰을 텐데.";
            Game_TypeWriterEffect.instance.RandomText[9] = "따뜻한 커피가 생각나는 날이야.";
            Game_TypeWriterEffect.instance.RandomText[10] = "가끔 몽환의 세계가 그리워져. 익숙한 것들이 주는 소중함이 있는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[11] = "룬, 꿈속에서 벗어날 수 있었던 건 네 덕분이야. 네가 없어서 깨달았어.";
            Game_TypeWriterEffect.instance.RandomText[12] = "지구에는 내가 없어도 이런저런 일들이 많이 생기는구나. 왠지 씁쓸해.";
            Game_TypeWriterEffect.instance.RandomText[13] = "룬, 나는 여기서 살아남을 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "나는 아직 나이가 어리지만, 이곳에서는 어른이 된 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[15] = "나는 지금 어른들은 상상도 할 수 없는 것들을 보고 느끼고 있잖아.\n가끔 이 사실에 우쭐해지기도 해.";
            Game_TypeWriterEffect.instance.RandomText[16] = "달은 어떻게 저렇게 예쁘게 빛날 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[17] = "지구에서는 달나라에 대한 동화가 많아. 너도 알고 있니?";
            Game_TypeWriterEffect.instance.RandomText[18] = "보름달, 초승달, 그믐... 달에 대한 표현이 생각해 보니 참 많네.";
            Game_TypeWriterEffect.instance.RandomText[19] = "달은 어두운 밤을 밝혀주는 참 고마운 존재야.";
            Game_TypeWriterEffect.instance.RandomText[20] = "난 밤이 싫었는데, 이곳에 있으니 점점 어둠이 편해지는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[21] = "지금 난 달을 향해 걸어가고 있지만, 해로 걸어가는 길도 있을까?";
            Game_TypeWriterEffect.instance.RandomText[22] = "사실 좀 답답해. 벽화만을 믿고 걸어가는 나 자신이 한심해질 때도 있어.";
            Game_TypeWriterEffect.instance.RandomText[23] = "룬, 그래도 네가 있어 나는 이 길을 끝까지 가보려고 해. 네가 옆에 있어줄 거잖아.";
            Game_TypeWriterEffect.instance.RandomText[24] = "이 길이 절망이 아닌 희망으로 물들었으면 좋겠어.";
            Game_TypeWriterEffect.instance.RandomText[25] = "뭔가 적적하니까 노래를 불러볼까? 흐흐흥~ 흐흥";
            Game_TypeWriterEffect.instance.RandomText[26] = "룬이 있어 외롭지는 않은데, 끝이 안 보이는 이 길이 막막해...";
            Game_TypeWriterEffect.instance.RandomText[27] = "좀 추운 것 같기도 하고... 나의 착각인 걸까?";
            Game_TypeWriterEffect.instance.RandomText[28] = "룬, 너의 다른 친구들은 없어?";
            Game_TypeWriterEffect.instance.RandomText[29] = "난 친구가 별로 없었어. 제일 친한 친구는 딱 1명 있었어!\n그 친구가 나에게 정말 큰 힘이 되어주었는데...\n 아, 아니야! 이제 2명이네. 룬, 너도 나의 친구니까.";
            Game_TypeWriterEffect.instance.RandomText[30] = "지구에서는 태어난 연수로 나이라는 걸 먹게 되는데 나는 13살이야.\n룬, 너에게도 나이라는 게 있을까?";
            Game_TypeWriterEffect.instance.RandomText[31] = "그래도 달이 가까워지는 게 느껴지니까 뭔가 기대돼.";
            Game_TypeWriterEffect.instance.RandomText[32] = "우주에서 아무 일도 일어나지 않았다면 오히려 더 절망적이었을 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[33] = "지구로 돌아가고 싶은데, 또 그러기 싫을 때도 있는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[34] = "중간의 우주 휴게소가 없었다면 난 벌써 여기 없었겠지...? 누가 만들어놓은 걸까.";
            Game_TypeWriterEffect.instance.RandomText[35] = "룬, 네 이름을 부르면 기분이 좋아.";
            Game_TypeWriterEffect.instance.RandomText[36] = "노래를 힘차게 불러볼까?";
            Game_TypeWriterEffect.instance.RandomText[37] = "학교에서 장래희망을 적으라고 할 때 나는 우주비행사를 적었었는데,\n벌써 그 꿈을 이룬 거야!";
            Game_TypeWriterEffect.instance.RandomText[38] = "우주를 그리라고 하면 이제 그릴 것이 너무 많아.";
            Game_TypeWriterEffect.instance.RandomText[39] = "하늘섬이 갑자기 아래로 떨어질 것 같은 느낌인데...";
            Game_TypeWriterEffect.instance.RandomText[40] = "생각이 이리 튀고 저리 튀는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[41] = "룬, 너는 어떻게 나의 말을 이해하고 대답해 주는거야?";
            Game_TypeWriterEffect.instance.RandomText[42] = "궁금한 게 너무 많아. 하지만 설명해 줄 수 없는 것들이 많겠지?";
            Game_TypeWriterEffect.instance.RandomText[43] = "누군가의 품에 안겨서 편하게 자고 싶어.\n여기에서는 편히 눈을 감은 적이 없는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[44] = "뛰어볼까? 걷는 게 지겨우면 뛰는 것도 좋더라.";
            Game_TypeWriterEffect.instance.RandomText[45] = "나 학교에서 달리기하면 늘 1등을 했는데 말이야.\n여기서도 실력 발휘를 해볼까?";
            Game_TypeWriterEffect.instance.RandomText[46] = "너무 오랫동안 우주복만 입고 있어서 지겨워.";
            Game_TypeWriterEffect.instance.RandomText[47] = "룬, 달의 신전까지 내가 갈 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[48] = "뛰어야겠어. 심장이 뛰는 걸 느껴야 살아있는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[49] = "뛰어야겠어. 심장이 뛰는 걸 느껴야 살아있는 것 같아.";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "아름답다.... 저 빛은 뭐야?";
            Game_TypeWriterEffect.instance.RandomText[1] = "분수에서 물이 아닌 빛이 떨어지고 있어....!";
            Game_TypeWriterEffect.instance.RandomText[2] = "저 빛을 만지면 어떤 느낌일까? 시원할까? 빛이니 뜨거울지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[3] = "룬... 왜 말리는 거야, 이유를 말해주면 좋잖아!";
            Game_TypeWriterEffect.instance.RandomText[4] = "오는 길에 색을 잃은 별들을 봤었어...";
            Game_TypeWriterEffect.instance.RandomText[5] = "달이 엄청 밝다, 가까워진 게 다시 한 번 실감 나.";
            Game_TypeWriterEffect.instance.RandomText[6] = "룬, 너 괜찮아? 네가 힘들면 우리 조금 천천히 가도 돼.";
            Game_TypeWriterEffect.instance.RandomText[7] = "100보 전진을 위한 1보 후퇴는 부끄러운 게 아니야.";
            Game_TypeWriterEffect.instance.RandomText[8] = "국어시간에 배운 설화에는 달을 사랑하는 사람에 빗대곤 했어.";
            Game_TypeWriterEffect.instance.RandomText[9] = "한국에서 월은 한 달을 의미하기도 해. 한 달은 시간을 세는 단위야.";
            Game_TypeWriterEffect.instance.RandomText[10] = "우주에도 지구의 나무를 심는 게 좋겠어,\n오랫동안 우주를 지켜줄 거야.";
            Game_TypeWriterEffect.instance.RandomText[11] = "누군가를 돕는다는 건 보람 있는 일이야.";
            Game_TypeWriterEffect.instance.RandomText[12] = "나를 위해서만 행동했을 때엔 알 수 없는 죄책감이 들 때가 있어.";
            Game_TypeWriterEffect.instance.RandomText[13] = "다른 사람에게 좋지 않은 행동을 하면 마음이 불편해져";
            Game_TypeWriterEffect.instance.RandomText[14] = "... 달이 가까워진다는 건 룬 너와 곧 헤어져야 한다는 거겠지?";
            Game_TypeWriterEffect.instance.RandomText[15] = "달에 누군가가 살고 있었다면 벌써 나를 발견하지 않았을까?";
            Game_TypeWriterEffect.instance.RandomText[16] = "벽화에 있던 것들이 사실 너무 오래전의 것이어서 지금은 아무것도 없는 건 아니겠지";
            Game_TypeWriterEffect.instance.RandomText[17] = "달에서도 집으로 갈 방법이 없을까 봐 무서워.";
            Game_TypeWriterEffect.instance.RandomText[18] = "오는 길에 봤던 토끼는 달에 사는 주민이었을까? 물어볼 걸 그랬어.";
            Game_TypeWriterEffect.instance.RandomText[19] = "달 토끼라는 말이 나온 이유는... 정말 달에 토끼를 발견한 사람이 만들었나 봐";
            Game_TypeWriterEffect.instance.RandomText[20] = "어쩔 수 없이 배가 고픈걸...";
            Game_TypeWriterEffect.instance.RandomText[21] = "이 여정이 얼마 남지 않은 게 느껴져.";
            Game_TypeWriterEffect.instance.RandomText[22] = "조금 빨리 나아가도 될 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[23] = "사실 목표를 위해 무언갈 이렇게까지 열심히 한 적은 이번이 처음인 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[24] = "방금 그곳을 지나와서인지 옆에 무언가 있으면 떨어질까 걱정돼.";
            Game_TypeWriterEffect.instance.RandomText[25] = "반짝반짝, 별들은 어떨 때 빛을 잃고 마는 거야?";
            Game_TypeWriterEffect.instance.RandomText[26] = "별에게 빛을 잃는다는 건 어떤 의미야?";
            Game_TypeWriterEffect.instance.RandomText[27] = "내가 읽은 책들에선 희망을 잃었다는 걸 '눈에 빛을 잃었다'라고 많이 표현했어";
            Game_TypeWriterEffect.instance.RandomText[28] = "눈에 빛을 잃은 사람들이 우주에서 빛을 얻어 가면 좋겠어.";
            Game_TypeWriterEffect.instance.RandomText[29] = "빛이 희망이라면, 우주는 희망으로 가득 찬 공간일 거야.";
            Game_TypeWriterEffect.instance.RandomText[30] = "우주의 빛들을 모은다면  그 빛은 지구보다 커다랗겠지?";
            Game_TypeWriterEffect.instance.RandomText[31] = "내가 제일 먼저 달에 도착한 소년이면 어떻게 하지?";
            Game_TypeWriterEffect.instance.RandomText[32] = "나처럼 작은 소년이 이 큰 우주에 영향을 줄 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[33] = "우주에도 영향력을 끼칠 수 있는 사람이 되고 싶어!";
            Game_TypeWriterEffect.instance.RandomText[34] = "이곳에 와선 장래희망에 대해서 크게 생각해 본 적이 없었어.";
            Game_TypeWriterEffect.instance.RandomText[35] = "지구에 가면 할 것 리스트가 벌서 384개나 돼!";
            Game_TypeWriterEffect.instance.RandomText[36] = "달에 도착하면 제일 먼저 무얼 할까? 일단 야호! 하고 소리쳐야지.";
            Game_TypeWriterEffect.instance.RandomText[37] = "어디서 엄마의 목소리를 들은 것 같아. 착각인가?";
            Game_TypeWriterEffect.instance.RandomText[38] = "이 길 덕분에 무사히 이곳까지 올 수 있었어.";
            Game_TypeWriterEffect.instance.RandomText[39] = "룬, 너는 나와 헤어지면 뭘 할 거야?";
            Game_TypeWriterEffect.instance.RandomText[40] = "너와 함께 지구에 가고 싶어, 너에게 정이 들었거든.";
            Game_TypeWriterEffect.instance.RandomText[41] = "너도 나와 헤어지기 싫은 기분을 느끼니?";
            Game_TypeWriterEffect.instance.RandomText[42] = "우리가 약속을 하면 그 약속으로 서로를 추억할 수 있을 거야.";
            Game_TypeWriterEffect.instance.RandomText[43] = "어린왕자 속 여우의 기분이 어떤 기분인지 알겠어.";
            Game_TypeWriterEffect.instance.RandomText[44] = "엄마가 좋아하는 만화 중엔 우주에 기차가 다니는 만화가 있었어.";
            Game_TypeWriterEffect.instance.RandomText[45] = "달콤한 우유 사탕을 먹고 싶어, 룬 너에게도 나눠주고 싶다.";
            Game_TypeWriterEffect.instance.RandomText[46] = "달에 올 수 있는 길은 여기 하나뿐일까?";
            Game_TypeWriterEffect.instance.RandomText[47] = "언젠가 과학이 발전되어 이곳을 모두와 함께 걸을 수 있으면 좋겠다.";
            Game_TypeWriterEffect.instance.RandomText[48] = "이곳은 평생 잊지 못할 기억이 될 거야.";
            Game_TypeWriterEffect.instance.RandomText[49] = "룬, 나 말고도 너의 영혼에 빛을 찾아줄 친구들을 찾길 바라.";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "이 기둥들은 뭘까? 특이하게 생겼어.";
            Game_TypeWriterEffect.instance.RandomText[1] = "이건 운석의 파편을 맞고 부서진 걸까?";
            Game_TypeWriterEffect.instance.RandomText[2] = "... 이건 달의 벽화에 새겨져있던 건물과 기둥이야!";
            Game_TypeWriterEffect.instance.RandomText[3] = "누가 이곳에 이런 걸 만든 거지?";
            Game_TypeWriterEffect.instance.RandomText[4] = "달에 누군가가 살고 있었나 봐, 지금은... 모르겠어.";
            Game_TypeWriterEffect.instance.RandomText[5] = "어쩌다가 이곳에 살던 원주민들이 사라진 걸까?";
            Game_TypeWriterEffect.instance.RandomText[6] = "... 특이한 곳이야 지구는 달에 이런 게 있다는 걸 알까?";
            Game_TypeWriterEffect.instance.RandomText[7] = "드디어 달에 왔어......! 이곳을 위해 이렇게 걸은 거야.";
            Game_TypeWriterEffect.instance.RandomText[8] = "벽화의 소원을 이루어준다는 돌이 이곳에 있을까?";
            Game_TypeWriterEffect.instance.RandomText[9] = "저기요! 누구 없어요?";
            Game_TypeWriterEffect.instance.RandomText[10] = "그렇게 바라던 달에 왔는데 막상 도착하니 두려워.";
            Game_TypeWriterEffect.instance.RandomText[11] = "달에 사는 토끼들은 다들 어디 갔을까?";
            Game_TypeWriterEffect.instance.RandomText[12] = "이곳에 도달한 왕이나 대통령이 있었겠지";
            Game_TypeWriterEffect.instance.RandomText[13] = "전에 만났던 파란빛 영혼은 언제부터 영혼을 만드는 일을 했을까?";
            Game_TypeWriterEffect.instance.RandomText[14] = "달토끼는 어디로 갔을까?";
            Game_TypeWriterEffect.instance.RandomText[15] = "나도 이곳에 살 수 있을까?... 아니지! 이곳엔 내가 원하는 삶은 없어!";
            Game_TypeWriterEffect.instance.RandomText[16] = "천천히 둘러봐야겠어... TV에서 보던 유적지 모습을 보는 느낌이야.";
            Game_TypeWriterEffect.instance.RandomText[17] = "이 기둥에 새겨진 무늬는 무슨 뜻이 있을까?";
            Game_TypeWriterEffect.instance.RandomText[18] = "예쁜 걸 좋아했나 봐, 기둥에 무늬를 새긴걸 보니 말이야.";
            Game_TypeWriterEffect.instance.RandomText[19] = "이곳에 살던 주민이 이 길을 만들었을까?";
            Game_TypeWriterEffect.instance.RandomText[20] = "이 길을 만들었기 때문에 벽화를 만들었을 거야.";
            Game_TypeWriterEffect.instance.RandomText[21] = "어쩌면 달주민은 달과 지구를 교류했을지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[22] = "이 길은 달과 지구를 이어주는 실크로드 같은 거 였을 거 같아!";
            Game_TypeWriterEffect.instance.RandomText[23] = "여기는 산소도, 물도 없어. 어떻게 살았던 걸까?";
            Game_TypeWriterEffect.instance.RandomText[24] = "모든 생명체가 지구처럼 물과 산소를 필요로 하지 않는다는 게 신기해.";
            Game_TypeWriterEffect.instance.RandomText[25] = "용감한 소년, 지금 종착지에 도착했습니다!";
            Game_TypeWriterEffect.instance.RandomText[26] = "이곳에서 엄마를 만날 수 있는 방법을 찾을 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[27] = "엄마~! 제가 왔어요! ... 엄마는 이곳에 없겠지.";
            Game_TypeWriterEffect.instance.RandomText[28] = "다들 내가 달에 도착했다는 걸 믿을까?";
            Game_TypeWriterEffect.instance.RandomText[29] = "사람들에게 달에 왔던 사실을 말하는 걸 생각하니 마음이 들떠!";
            Game_TypeWriterEffect.instance.RandomText[30] = "사진을 찍을 수 있으면 좋을 텐데, 내가 달에 도착했다는 걸 실시간으로 올리고 말이야!";
            Game_TypeWriterEffect.instance.RandomText[31] = "알 수 없는 풍경들에... 불안하고.. 떨려, 그런데 막 방방 뛰고 싶을 정도로 가슴이 간지러워.";
            Game_TypeWriterEffect.instance.RandomText[32] = "이곳을 엄마와 또 올 수 있을까?";
            Game_TypeWriterEffect.instance.RandomText[33] = "벽화에 있던 소원석은 어디에 있는걸까?";
            Game_TypeWriterEffect.instance.RandomText[34] = "이 건물들은 언제부터 있었던 걸까?";
            Game_TypeWriterEffect.instance.RandomText[35] = "이곳의 주민들이 처음 달에 정착했을 때 어떤 마음이었을까?\n달에 비친 태양의 빛에 마음이 홀렸을지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[36] = "여기서의 삶이 만족스러웠을까?";
            Game_TypeWriterEffect.instance.RandomText[37] = "이곳은 지구가 보이고 우주가 보여, 사색에 잠기기 좋은 곳이야.";
            Game_TypeWriterEffect.instance.RandomText[38] = "이 장소의 주민들은 소원석을 지키는 수호자였을지도 몰라.";
            Game_TypeWriterEffect.instance.RandomText[39] = "다들 어디로 가버린 걸까?";
            Game_TypeWriterEffect.instance.RandomText[40] = "달주민들이 지구에 휴가를 떠났을지도 몰라, 갈림길이 달라 휴가길에 마주치지 못한 거지.";
            Game_TypeWriterEffect.instance.RandomText[41] = "집에 들어가서 그들이 어떤 삶을 사는지 보고 싶어 실례겠지?";
            Game_TypeWriterEffect.instance.RandomText[42] = "달주민들은 무엇을 먹고 살까?";
            Game_TypeWriterEffect.instance.RandomText[43] = "소원석을 훔치러 온 줄 알고 날 적대시하면 어떻게 하지?";
            Game_TypeWriterEffect.instance.RandomText[44] = "달주민 여러분 저는 적이 아니에요... 그저 집에 가고 싶은 작은 소년이에요!";
            Game_TypeWriterEffect.instance.RandomText[45] = "... 적막해, 나 말고는 아무 소리도 나지 않는 것 같아.";
            Game_TypeWriterEffect.instance.RandomText[46] = "부지런하게 걸어야겠어, 분명 벽화에 있던 제단이 있을거야.";
            Game_TypeWriterEffect.instance.RandomText[47] = "내가 우주에 갔다왔다는걸 친구들이 알면 날 부러워 할 거야.";
            Game_TypeWriterEffect.instance.RandomText[48] = "이곳이 지구에서 보던 작고 빛나는 점이야";
            Game_TypeWriterEffect.instance.RandomText[49] = "... 달에서 보는 지구는, 지구에서 보는 달만큼이나 예쁘구나.";
        }
    }
    //필수 라디오 나레이션
    public void EventTextRadio(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = " 응답 하라 - 취지직-";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "그 유일한 소년이 치지직 안타까운 치지직 다음 내용은";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "△△기업 취지지직- 자재비 갈취 혐의- 취지직  극구 부인 취지직-";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {

        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- 어떻게 됐니? -";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "이 사태를 통해 치지직 △△기업과 결탁 치지직 ";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "취지직 보고 치이이익 너를 그리워해 치이이익";

        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "발표를 취이익 연락이 되지 치이익 사실상 사망을 취지직";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "치이익 오늘 검찰청은 ☆☆사건의 아동에 대해 치이이익 세간에선 우려의 시선 치이익";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이이익 널 치이익 기다리고 있어 치이익";
        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "아동에 대한 치이익 인륜적인 문제 치이이익";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "현재 △△과 치지직 연류된 것 치이익 알려 치이익";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이이이이이이이이익 언제 치이이익 옆에 있어 치이이익";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- 응답 치지지직 아직 연락 안돼요, 치지직";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "치지직 인터뷰 -칙- 치직- 이용하여 무력한 아동을 치지직- 실험을 무리 치지직-";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이익 하지만, 치이이익 천천히 와, 날 치이익";
        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- 치지지직 - 57일 응답 - 치직 없- 치지직";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "치지지직- 참석 - 칙,치이이익,1심에서 치이익-칙, 권리를 치익";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이이익 - 야 사랑 치이이이익 언제나 치이익";
        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- 계속 움직이고 치지직- 치이익- 칙- 부서지지 않고 치지직 폭발의 힘으로 - 치지직";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "출석하지 않아 치이이익- 거부는 치익- 으로 미뤄져 칙, 치이이익-";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이이익 오늘도 칙, 하루야 치이익, 돌아간 칙- 머리를 빗겨줄게 , 치이익";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "치이익- 칙- 하지만 속도를 치이이익- 수색해야하지 치지직- 칙";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "치지지직 - 그때 그랬으면 안됐어 치지직- 일만 없었어도 칙- 네가 먼저 꼬드겼잖아 치지지지직-";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "아마 치이이익- 기억이- 치이이익- 실망하겠지 치이이익 - 안하구나 치이이이익";

        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "아니, 치지지지직- 위에서 허락이  치지지지직, 칙, 치지직- 봐둬... 알아볼게. 칙";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "치지직, 치익, 판결을 벗어나지 못했습니다 치이익- 나만 그랬을 것 같 치이이익 - ";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "치이익, 아가 치익, 우리 아가, 치이익 치익 칙- 안아주고 칙- 치지지 칙,지직직-";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "-99일 생존 보고 들어갑니다. 응답 없음입니다. 내일을 마지막으로 ...";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "△△기업은 쇠락의 길을 걷게 되었습니다. 실형 선고를 받은 □□회장은 해당 우주선에 대한...";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "언제나 너를 사랑하고 있다는 걸 알아줬으면 해.";
        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지 지직- 100일간 치지직- 더 이상의 수신이 치지직- 달의 방향으로 - 치이 이이 이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "지직 앞으로 치지직 좋습니다 칙, 시늉만 해요 치지지직 곧 나올 수  치지직 ";
            Game_TypeWriterEffect.instance.RadioText[2] = "어릴때 치지지직 네 칙, 좋아했던 치지지직 몇 번이고 반복해 읽 치지지직";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "치직- 고아가 된지 치지지직 얼마 안 된 치익-를 치지지직 - 희망을 이용해 ... 치지직-";
            Game_TypeWriterEffect.instance.RadioText[1] = "너희 월급이 치지지직 디서 나왔는 치지지지직 실업자가 치지지직- 감형-";
            Game_TypeWriterEffect.instance.RadioText[2] = "지켜주지 치지직- 해서 치지지직- 안해... - 치지직 아가 치지지직치이이이익-";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "- 어, 반응이 있 치지지직- 다들 치이익 모여봐- ";
            Game_TypeWriterEffect.instance.RadioText[1] = "넌 뭐야 치지지직- 찍지 마세요 - 치이이이익- △△기업 회장이 -칙\n 보석금을 치이익- 이 악마! 칙,치익";
            Game_TypeWriterEffect.instance.RadioText[2] = "성장할 수 -치지지직- 네 치지지지지직-치이이익- 뭐든지 될 수 치이이익-";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {
        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {
        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "- 들리니? 치지직 들리면 칙 대답 치이이이이이익 - 칙-";
            Game_TypeWriterEffect.instance.RadioText[1] = "살아있다는 소식을 - 칙 치이이익- 소감이 -치이익- 대답을 하시죠! 치이이익- 검찰은 칙, 치이이이익-";
            Game_TypeWriterEffect.instance.RadioText[2] = "얼마남지 않 - 치이이이익 - 네가 치이이익 의 목소리를 치이이익 곧 마지칙, 치이익 자랑스럽단다 -";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지지직- 산소가 부족할텐 치지지지직- 어떻- 치지지직 기적이야 치지지직 렇게 어린소 치익-";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지지직 악몽을 꿔 치이이익 그 아이가 치지지직 잘못했어 치지지직 전부  치지지직";
            Game_TypeWriterEffect.instance.RadioText[2] = "기억해 치이이익 아의 추억을 칙-, 들리지 않ㅇ 치이이익- 항상 곁에 치이이이익 ";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "- 치이익- 달에 도착 -치이익 이건 기적이야 - \n치익 칙, 확인해봐 치이이익- 아직은 -치익";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지지직- 자신의 비리 혐의를 치지지직 인정하면서 칙---익-\n △△기업은 다른 치이익 인수합병에 치이이익-";
            Game_TypeWriterEffect.instance.RadioText[2] = "치이이익 - 치지지지직 치지지직\n" +
                "치지지직 치칙  사랑해 언제나, 치이이이이익 치칙 - 칙, 치이이이익 , 치지지직\n" +
                "치이이이익 나의 아가. 혼자서도 잘 헤쳐나갈 수 있어.\n 치이이익 - 치이이익 엄마는 치이이익\n" +
                "별이되어 치이이익 항상 널 -치직 - 칙 - ";
        }
    }
    //랜덤 라디오 나레이션
    public void EventRandomTextRadio(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = " 취직- 치이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지직 - 오늘 서울은 - 치지직 - ";
            Game_TypeWriterEffect.instance.RadioText[2] = "5차선 도로 교통체증이 - 치지직 예상 됩니다 - ";
            Game_TypeWriterEffect.instance.RadioText[3] = "하하하! 치지직 ";
            Game_TypeWriterEffect.instance.RadioText[4] = "취지직 - 어제 친구랑 찜질방에서 취지직- 했거든요!";
            Game_TypeWriterEffect.instance.RadioText[5] = "don't  -  cry, don't취지지직 cry 취지직 -";
            Game_TypeWriterEffect.instance.RadioText[6] = "치지직 행을 하면서 치지직 지켜야하는 예절이 치지직";
            Game_TypeWriterEffect.instance.RadioText[7] = "치지직 출근길 오늘은 치지직 신청곡 치지직";
            Game_TypeWriterEffect.instance.RadioText[8] = "네 시청자분 취지지직 그랬군요 치지직 신청곡 틀어 취지직";
            Game_TypeWriterEffect.instance.RadioText[9] = "별빛이 내린치지지직 샤랄랄랄랄랄라 치지지직";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {

        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "취직- 치이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지직 - The Moon is Earth's  - 치지직 -  only natural satellite";
            Game_TypeWriterEffect.instance.RadioText[2] = "오늘은 치친 치지지직 위한 치직";
            Game_TypeWriterEffect.instance.RadioText[3] = "치직 반짝반짝 치지직 아름답게 치지직";
            Game_TypeWriterEffect.instance.RadioText[4] = "취지지직 치이이익";
            Game_TypeWriterEffect.instance.RadioText[5] = "취지지직";
            Game_TypeWriterEffect.instance.RadioText[6] = "박○○씨는 치이익 올해의 시민상을 치지직";
            Game_TypeWriterEffect.instance.RadioText[7] = "치이이익 - ";
            Game_TypeWriterEffect.instance.RadioText[8] = "원 투쓰리 베이비~ 치이이익 - 너의 마음속 진실~ 치이익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치이익 흐린뒤 맑아져 치이익 습도는 다소 높아 치이익-";
        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치이이이이이이이이이이이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "아! 1번 선수 치이익 한국이 치이이익";
            Game_TypeWriterEffect.instance.RadioText[2] = "----- 첫 번째!! 첫 번째!!! 일등으로 들어옵니다! 치이이이이익";
            Game_TypeWriterEffect.instance.RadioText[3] = "치이익 출근길 다들 치이익 힘내세요 치이익";
            Game_TypeWriterEffect.instance.RadioText[4] = "고슴도치는 치이익 무척 단단한 마음을 치지직";
            Game_TypeWriterEffect.instance.RadioText[5] = "치이이이익 혼자여도 치이익 무섭지 않았죠 - 칙";
            Game_TypeWriterEffect.instance.RadioText[6] = "치이익 오늘 경기는 치이이익 모든 국민이 치이익";
            Game_TypeWriterEffect.instance.RadioText[7] = "치이익 하지만 치익 단점은 치이익 옆에 아무도 없었 치이익";
            Game_TypeWriterEffect.instance.RadioText[8] = "치익 개봉한 치이이익 평점을 치이익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치이익 책 읽어주는 치이익 고슴도치 이야기 치이익";

        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치칙... 오늘 날씨는 화창하네요..치치...칙";
            Game_TypeWriterEffect.instance.RadioText[1] = "치치...치칙, 가끔 비가 오는 곳도 있겠어요.. 치치직.";
            Game_TypeWriterEffect.instance.RadioText[2] = "삐리링 삐리링- 즐거운 월요일 치칙..";
            Game_TypeWriterEffect.instance.RadioText[3] = "치지지직 홍대역에서 큰 사고가 났다고 합니다 지금 현장의 치칙";
            Game_TypeWriterEffect.instance.RadioText[4] = "요즘 유튜브에서 핫한 치지직 꾸덕꾸덕한 그릭 요거트를 먹어볼께요 치칙";
            Game_TypeWriterEffect.instance.RadioText[5] = "치지직...치직...";
            Game_TypeWriterEffect.instance.RadioText[6] = "칙... 아픈 치지직 마음이 약해져 치지지직";
            Game_TypeWriterEffect.instance.RadioText[7] = "지직...칙....직...지직...";
            Game_TypeWriterEffect.instance.RadioText[8] = "누군가가 있기를 치지지직 간절히 바랬어요 치지직";
            Game_TypeWriterEffect.instance.RadioText[9] = "치지직 파노라마 치지직 치지직 찾아온 작은 치지직 기억 ";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
        

        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지직 - 치직";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지지직 삐리뽀빠리뽀 치지직 삐삐빠뽀";
            Game_TypeWriterEffect.instance.RadioText[2] = "치지직 얘기를 치지지직 듣고있어 치지직";
            Game_TypeWriterEffect.instance.RadioText[3] = "치직 있잖아 저아이 치지직 귀엽지? 치지직";
            Game_TypeWriterEffect.instance.RadioText[4] = "치익 치이익 -";
            Game_TypeWriterEffect.instance.RadioText[5] = "마음을 가라앉히고 치이익 숨을 크게, 치이익";
            Game_TypeWriterEffect.instance.RadioText[6] = "치이이이이이이익 어라? 치이이이익";
            Game_TypeWriterEffect.instance.RadioText[7] = "치익 태양은 치이익 폭발로 인해 치이익 변환 치익";
            Game_TypeWriterEffect.instance.RadioText[8] = "치이이이이이익 그리운 소식을 치이익 상봉하여 치직";
            Game_TypeWriterEffect.instance.RadioText[8] = "치지직- ";

        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치이이익 청소는 치이익 여기에 붓고 닦으면 치이익 끝! ";
            Game_TypeWriterEffect.instance.RadioText[1] = "쉬운일 치이이익 하지만 치이익 하면 되어있어 칙익 -";
            Game_TypeWriterEffect.instance.RadioText[2] = "치이익 여기는 R포인트 치이익";
            Game_TypeWriterEffect.instance.RadioText[3] = "치이이이이익 A포털은 검색어를 치이익";
            Game_TypeWriterEffect.instance.RadioText[4] = "칙 -";
            Game_TypeWriterEffect.instance.RadioText[5] = "치이익 국민 치이익 사전 투표를 치이익";
            Game_TypeWriterEffect.instance.RadioText[6] = "치이이이이이이익- 치지직-";
            Game_TypeWriterEffect.instance.RadioText[7] = "치지직 - 오늘 메트로폴리탄은 구름 한 점 치이익 -  ";
            Game_TypeWriterEffect.instance.RadioText[8] = "치이이익 여기 플러그 세트 치이이익 더이상 안만든 치이익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치직 널 가질 수 없다면 치지지지직 거나 마찬가지. 치지직";

        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지직 불안해 하지만 치지지직 나도 사실 불안해 치지직-";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지지지직 흑동고래는 치지지직 -";
            Game_TypeWriterEffect.instance.RadioText[2] = "치이익 실종 아동은 치이익 현재 45세 치지직 예상 모습은 치지직";
            Game_TypeWriterEffect.instance.RadioText[3] = "치이익- 득표율이 - 치이이익 오오오! - 치이익 네 앞서고 있습니다 치이이익";
            Game_TypeWriterEffect.instance.RadioText[4] = "치직 루시드 드림! 치지지직 - 꿈을 믿으시나요? 치지직- 자각몽과는 달라요 치지직 -";
            Game_TypeWriterEffect.instance.RadioText[5] = "치지직 달은 여러 이름으로 불리는데요  치지지직 상현달 치지직 초승달 치직";
            Game_TypeWriterEffect.instance.RadioText[6] = "치지지직 검찰의 치지지직 이 요구되고 있 치이이이이이이이익-";
            Game_TypeWriterEffect.instance.RadioText[7] = "치이이이이이이이이익- ";
            Game_TypeWriterEffect.instance.RadioText[8] = "치이이익, 치직, 고개를 치지지직 나를 버리고오 치지직 님이~";
            Game_TypeWriterEffect.instance.RadioText[9] = "치, 치지직, 칙";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지직 컬렉션이란 치지직 모으거나 수집 치지직 일 또는 치직";
            Game_TypeWriterEffect.instance.RadioText[1] = "치직 고슴도치는 치지직 친구를 치지지지직 지쳐서 치지직";
            Game_TypeWriterEffect.instance.RadioText[2] = "치지직 가끔은 치지직 마음을 가볍게 치지지직 친구가 치지직";
            Game_TypeWriterEffect.instance.RadioText[3] = "생겼어요 치지직 더이상 치지지지지직 롭지 않았어요 치직";
            Game_TypeWriterEffect.instance.RadioText[4] = "치이이익- 치지직- 치익";
            Game_TypeWriterEffect.instance.RadioText[5] = "...치이이익 ..... 치지직 ... 응 치직 알겠어 치지지직 헤어져 치직";
            Game_TypeWriterEffect.instance.RadioText[6] = "치이이이이이이이이이이이이이이이이이익";
            Game_TypeWriterEffect.instance.RadioText[7] = "치지지직 오늘 행성 BF-2치이이익 공표합니 치이익";
            Game_TypeWriterEffect.instance.RadioText[8] = "여러 생물의 공생을 치이이익 위해선 치이익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치이이익 사연은 치이이익 화장실로 달려가는 치이익 하하하!! 치익";

        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치이이익 내년에 치이이익 앞뒀는데 칙- 소감을 치이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "치익 3월 치이이익 벚꽃이 절경 치이이이익 었는 치이익";
            Game_TypeWriterEffect.instance.RadioText[2] = "치이이익 축제가 치이이익 멋진 광경을 치이익";
            Game_TypeWriterEffect.instance.RadioText[3] = "치익- 치익- 플루엔자가 치익 - 조류의 집단 폐사 치익- ";
            Game_TypeWriterEffect.instance.RadioText[4] = "치이이익 오늘의 운세 치이익 흉입니다. 치이익 조심 치이익";
            Game_TypeWriterEffect.instance.RadioText[5] = "치익 치익 비온뒤에 치지지직 맑게 개어 치이익";
            Game_TypeWriterEffect.instance.RadioText[6] = "오늘의 치지지직 사자성어는 치지직 청풍명월";
            Game_TypeWriterEffect.instance.RadioText[7] = "치이익-";
            Game_TypeWriterEffect.instance.RadioText[8] = "아 맞아 치이이익- 당신 치이익- 저번에 응응 그래 치이익 사랑해 응 치익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치이이이익 영재를 찾아 치이이익 부모님이 아이를 치이익 행복한 가정이네요 치익";

        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "하하하! 맞아요 그때 제가 얼마나 난처했는지!";
            Game_TypeWriterEffect.instance.RadioText[1] = "오늘 부산에서 소식 전해드립니다. 이번 국제 영화제에 관심이 많은데요.";
            Game_TypeWriterEffect.instance.RadioText[2] = "I'm Missing You··· bye.";
            Game_TypeWriterEffect.instance.RadioText[3] = "過去の過ちに向き合う時、私たちは選択しなければなりません。";
            Game_TypeWriterEffect.instance.RadioText[4] = "과학계의 큰 충격이 휩쓸고 지나갔습니다. 우주를 향한 열망은 아직도 지속된 채 ...";
            Game_TypeWriterEffect.instance.RadioText[5] = "어제 ○○드라마가 최고의 시청률을 기록했습니다.";
            Game_TypeWriterEffect.instance.RadioText[6] = "요즘 아이들의 유행어가 빠르게 진화하고 있습니다. 이는 대부분 인터넷으로 확산되며...";
            Game_TypeWriterEffect.instance.RadioText[7] = "외국은 현재 k-pop 열풍이 불고 있습니다 □□아이돌 빌보드차트에 오르며 이를 증빙하고 ...";
            Game_TypeWriterEffect.instance.RadioText[8] = "새로운 맛 출시! 지금 구매하면 QR코드를 통해 이벤트 참여 가능해요! 1등은 ...";
            Game_TypeWriterEffect.instance.RadioText[9] = "9월 28일 ◇◇운세. 운명이든 우연의 일치든 부르고 싶은 대로 불러도 좋다. 당신의 삶은 막 바뀌려던 참이니까";
        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치익 어린이 상식 치이익 오늘은 식물원 인데요 치이익 인간에게 유용한 치이익";
            Game_TypeWriterEffect.instance.RadioText[1] = "치지지직 우리 ○○이 많이 자랐 치지직 아 그럼 ○○이는 △△이랑 같이 갈거 치지직";
            Game_TypeWriterEffect.instance.RadioText[2] = "치익 치이이익 -Both the Moon's 치익- in the earthly sk치지지지지지직";
            Game_TypeWriterEffect.instance.RadioText[3] = "치지직- 키스해줘 치지직 웃을 수 없을 치지직- 야이야이 야아아 칙-";
            Game_TypeWriterEffect.instance.RadioText[4] = "연예인인 ○○씨가 치지지지직 □□프로그램에서 치지지지지 눈물을";
            Game_TypeWriterEffect.instance.RadioText[5] = "치직 오늘 세계에서는 치지지지직 무너져 인명피해가 치지직";
            Game_TypeWriterEffect.instance.RadioText[6] = "- 치지직 하하하하 그래서 어떻게 됐 치이이익";
            Game_TypeWriterEffect.instance.RadioText[7] = "치익- 저기봐 아직 치지직- ";
            Game_TypeWriterEffect.instance.RadioText[8] = "치지지직 그만 연락해 치직- 난 아직 화가 치지직";
            Game_TypeWriterEffect.instance.RadioText[9] = "싱해요 치지직 산지에서 직접 치지직 합리적인 치직";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지직 오이의 치지직 효능은 치지지 블루베리가 칙";
            Game_TypeWriterEffect.instance.RadioText[1] = "치이이익 ----- 팡! 치지직";
            Game_TypeWriterEffect.instance.RadioText[2] = "칙- 치지직 - 치직 -";
            Game_TypeWriterEffect.instance.RadioText[3] = "치지지지지직 -";
            Game_TypeWriterEffect.instance.RadioText[4] = "어머~ 에쁘네요 치이이이이익-";
            Game_TypeWriterEffect.instance.RadioText[5] = "치직 우리는 치지직 생각했다 칙,치직 별이 되기로 치직";
            Game_TypeWriterEffect.instance.RadioText[6] = "치지지직 치지직 칙 기차의 발명은 치지직 그렇게 치직";
            Game_TypeWriterEffect.instance.RadioText[7] = "함께하는 누군가가 치지직 큰 위안이 치지직 오늘의 퇴근길 치칙";
            Game_TypeWriterEffect.instance.RadioText[8] = "치지직 뉴스 이만 마칩니다 치지직 빰빠빠 빠빠빰빠.";
            Game_TypeWriterEffect.instance.RadioText[9] = "치지지직 삐리리 뽀 치직, 삐릭 치이이익 삐리뽀뽀 칙 빠뽀 칙";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지지직 이 맘도 치지직 넌 내게 반했어 치지직 녹아버렸어 치지직";
            Game_TypeWriterEffect.instance.RadioText[1] = "치직 - 치지직 모스부호는 치지지지직- 스카우트 출신의 치직-";
            Game_TypeWriterEffect.instance.RadioText[2] = "치익 치지직, 햇빛이 강렬해 치지직 자외선 수치는 치이이익 -";
            Game_TypeWriterEffect.instance.RadioText[3] = "반짝반짝 작은 친구야 치이이익 따뜻해- 치이익 날아가지마 칙- 내곁에 - 서 칙-";
            Game_TypeWriterEffect.instance.RadioText[4] = "치지지직 지금 이순간 치지직 숨겨왔던 치지직 맞서싸우리라 칙-";
            Game_TypeWriterEffect.instance.RadioText[5] = "광활한 광야를 치지직 인생아 치지직 칙- 치지직 이 세상에 나 치직 그만일까!";
            Game_TypeWriterEffect.instance.RadioText[6] = "치직- 피융.... 치지직- 칙 피슝 - 칙";
            Game_TypeWriterEffect.instance.RadioText[7] = "우주에는 소리가 치지지지직 매개체가 없어 치치직-";
            Game_TypeWriterEffect.instance.RadioText[8] = "치이이익";
            Game_TypeWriterEffect.instance.RadioText[9] = "치지지지직 - 칙 , 칙, 치직, △△선수가 은퇴를 치이익 부상으로 칙- 사실상";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {

        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {

        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "예상했던 대로 치지직- 가진 않아도 치직- 언제나, 최선을 치지직";
            Game_TypeWriterEffect.instance.RadioText[1] = "치이이이익 치이익- 네, 이 불판은 고기 굽는 소리도 치이이이익- 다릅니다.";
            Game_TypeWriterEffect.instance.RadioText[2] = "치익  60초 후 치이이이이익 - 니다.";
            Game_TypeWriterEffect.instance.RadioText[3] = "보장받고 싶으시 치이이익 평생 치이익 보장이 됩니다 치이익 약관을 치익";
            Game_TypeWriterEffect.instance.RadioText[4] = "치직- 보이스피싱이 치지지직 - 아들을 사칭하여 치지직-";
            Game_TypeWriterEffect.instance.RadioText[5] = "어머 얘 좀 봐 치이이익 ○○이 △△ 딸이에 치이이이익-";
            Game_TypeWriterEffect.instance.RadioText[6] = "치이익 - 아무것도 아니지 치직- 시간은 치지직 치직- 마지막 저금통장에 치지직-";
            Game_TypeWriterEffect.instance.RadioText[7] = "치이이익 - 치직- 칙이익-";
            Game_TypeWriterEffect.instance.RadioText[8] = "사랑을 받는다는 게 치이이이익 항상 궁금했어 치익-";
            Game_TypeWriterEffect.instance.RadioText[9] = "칙 - 오늘의 사건사고 치이이익 싱크홀이 치이이익 화재가 발생 치이이익 강도가 치익";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "4인조 치이익 로 영원히 남을 치이익- 다음 내한은 - 치이이익- ";
            Game_TypeWriterEffect.instance.RadioText[1] = "치이익- 전쟁이 주는 나쁜 -치이익-칙, 치이익, 혼자 남은 치이익. 고아는.칙";
            Game_TypeWriterEffect.instance.RadioText[2] = "많은 도움 치이익 필요합니다. 치이익 칙 , 호프세프 치익 후원문의 칙- ";
            Game_TypeWriterEffect.instance.RadioText[3] = "치이익 - 별 볼 일 없 칙- 나의 별헤는 밤 치이익- 이 별에서 치이익- 이별은 칙.";
            Game_TypeWriterEffect.instance.RadioText[4] = "칙- 달은 치이이이이이이익 - 루나, 룬, 문, 다양한 칙- 어로 치이익-";
            Game_TypeWriterEffect.instance.RadioText[5] = "치직, 치지지지직, 삐리뽀 치지직, 이제, 치지직, 거의, 치지직 네 치칙";
            Game_TypeWriterEffect.instance.RadioText[6] = "치지지지직, 치직, 칙, 치지지직-";
            Game_TypeWriterEffect.instance.RadioText[7] = "치지직, 최초의 만남은 칙- 치지지지직 이런 변화까지는 치지직 하지 못했을 겁니다 치칙-";
            Game_TypeWriterEffect.instance.RadioText[8] = "종말론자들은 치지지직- 했지만 치지직- 느끼기 위해 찾는 치지지직-칙- 심리를 이용한 칙-";
            Game_TypeWriterEffect.instance.RadioText[9] = "노는 게 제일 좋 치지지직- 언제나 즐거워 -칙 - 내일은 또 무슨 일이 치지지직-";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[5];
            Game_TypeWriterEffect.instance.RadioText[0] = "치지직 - 이별이란 무엇일까요? 치직- 헤어지는것은 칙- 끝이라 할 수 없죠 치익- ";
            Game_TypeWriterEffect.instance.RadioText[1] = "칙- 잘자요 - 치익 - 이제 그만 치익 또 만나요 칙";
            Game_TypeWriterEffect.instance.RadioText[2] = "최선을 치이이이익 치이이이이이이익 치이이익 그리운 치이이이익";
            Game_TypeWriterEffect.instance.RadioText[3] = "치이익 오늘의 치이익 맑음 입니다 치이익 - 차도 밀리지않아 - 칙";
            Game_TypeWriterEffect.instance.RadioText[4] = "칙- 헤어짐이 있을 운세 치이익- 칙 행운의 아이템은 치익-  추억입니다. 칙-";
        }
    }

}