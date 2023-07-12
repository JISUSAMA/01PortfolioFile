using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Narration : MonoBehaviour
{
    public Button SkipButton;

    void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("HallofFame"))
        {
            SkipButton.onClick.AddListener(() => Game_TypeWriterEffect.instance.TypingBtn_speedUP());
        }
    }
    //필수 나레이션
    public void EventTextList(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.EventText = new string[11];
            //우주휴게소 암시
            //Game_TypeWriterEffect.instance.EventText[0] = "어? 저 푸른빛은 뭐지?";
            Game_TypeWriterEffect.instance.EventText[0] = " Huh? What’s that blue glow?";

            //산소통 줍기(획득 전)
            //game_typewritereffect.instance.eventtext[1] = "어? 이건 뭐지? 산소통이야, 누가 놔둔 걸까 ?\n" +
            //    "나 같은 사람이 이 길을 걷게 될 거라는 걸 알았던 걸까? \n" +
            //    "마침 잘 됐어!산소가 떨어지고 있었는데..\n 산소가 떨어지면 쓰러질 거야.\n"
            //    + "오른쪽 위에 있는 가방에서 산소통을 꺼내 사용하자.";

            Game_TypeWriterEffect.instance.EventText[1] = "Huh? What’s this? An oxygen tank...Wonder who left it here?\n" +
             "Did somebody know that I’d come along this path? \n" +
             "Perfect timing! I was running low on oxygen...\n I’ll pass out if I’m out of oxygen.\n"
             + "Let’s use the oxygen tank from our bag in the top right corner.";

            //별가루 줍기(획득 전)
            //Game_TypeWriterEffect.instance.EventText[2] = "이 반짝이는 건 뭐지?\n" +
            //       "두 개나 있어...둘 다 별가루인 것 같아, 별처럼 반짝거려!\n" +
            //       "이건 어디에 쓰는 물건일까? 한번 열어볼까 ?\n" +
            //       "오른쪽 위에 있는 가방에서 별가루를 꺼내 사용해보자!";

            Game_TypeWriterEffect.instance.EventText[2] = "I wonder what this shiny object is?\n" +
                 "There’s two of them... Seeing how shiny they are, both appear to be stardust!\n" +
                 "What is this used for? Let’s try opening it?\n" +
                 "Let’s use the stardust from our bag in the top right corner.";

            //별가루 줍기(사용 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "어..! 별가루를 여니까 발걸음이 가벼워...\n" +
            //    " 평소보다 두배로 빠르게 걸어지는 것 같아!";
            Game_TypeWriterEffect.instance.EventText[3] = "Whoa...! My footsteps feel lighter now...\n" +
    " I think I’m walking twice as fast as I normally do!";

            //우주 라디오 켜기(획득 전)
            //Game_TypeWriterEffect.instance.EventText[4] = "/라디오: 치이익- 치익- 여기는 - 치익/\n 이게 무슨 소리지? ... 어라, 라디오가 있어!";
            Game_TypeWriterEffect.instance.EventText[4] = "/Radio: (static) This... (static)/ \n What’s this sound...?  What the... there’s a radio here!";

            //Game_TypeWriterEffect.instance.EventText[5] = "무슨 문제가 있나봐 라디오에서 아무런 소리가 나지 않아.\n" +
            //    "설마 연료 같은 게 필요할까? ...!\n 이 라디오에 묻은 가루가 달가루와 비슷해!\n" +
            //    "달가루를 뿌리면 라디오를 들을 수 있을지 몰라\n 가방에서 달가루를 꺼내 라디오에 뿌려봐야겠어.";

            Game_TypeWriterEffect.instance.EventText[5] = "I think there’s a problem with the radio. I can’t hear anything.\n" +
           "Does it need some kind of battery? ...!\n The dust on the radio looks similar to moondust!\n" +
           "Maybe I should try to scatter some moondust on it \n Let’s try throwing some moondust on the radio.";

            //(3-2) 우주 라디오 켜기 (행동 이후)
            //Game_TypeWriterEffect.instance.EventText[6] = "/치이익 - 서울은 오늘도 치익- 다음 소식은 해운대의 치지지직 -\n/ 지구의 소식이잖아!";
            Game_TypeWriterEffect.instance.EventText[6] = "/(static) Seoul is still... (static) Next up, at Haeundae... (static)\n / Sounds like news from Earth!";

            //(4) 갈림길 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[7] = "이런 곳에 갈림길이라니...\n 달까지 가려면 어디로 가야 하지?...선택을 해야 해.\n 에잇,,!이쪽이다.잘못 선택했으면 돌아오면 되지.";
            Game_TypeWriterEffect.instance.EventText[7] = "Can’t believe there’s a crossroad here...\n Which way to the moon?... I need to choose.\n Whatever...! This way should be right. \n I can always come back if it’s a dead end.";

            //길 사라짐으로 인한 뛰기(전)
            //Game_TypeWriterEffect.instance.EventText[8] = "... 이상한 느낌이 들어, 소름이 끼치기도 해...\n 어라 ? 길이...점점 투명해지고 있어 ...설마...!길이 사라지고 있잖아!\n" +
            //    "빨리 뛰어야겠어. 길이 사라지기 전에 벗어나야 할 것 같아 느낌이 안 좋아";
            Game_TypeWriterEffect.instance.EventText[8] = "... Something feels off. I’m getting goosebumps...\n Huh? The path... it’s becoming more and more transparent... \n Wait a minute...! The path is disappearing!\n" +
               "I got to hurry. I have a bad feeling so I should get out of here before the path disappears.";

            //길 사라짐으로 인한 뛰기(후)
            //   Game_TypeWriterEffect.instance.EventText[9] = "이럴 때 별가루가 있으면 좋을텐데!\n 부지런히 달리자...!";
            Game_TypeWriterEffect.instance.EventText[9] = "Now would be the perfect time to have some stardust!\n Let’s keep running...!";
            //(6) 비상식량 발견
            //Game_TypeWriterEffect.instance.EventText[10] = "어라 이건 지구의 통조림과 비슷하게 생겼어....\n 사람이 먹을 수 있다고 적혀있는 것 같은데..." +
            //    "\n 사람이 먹지 못하는 것도 있을 수 있겠다 잘 확인하고 먹어야겠어.\n 이곳에 얼마나 오래 있었던 걸까? 먹어도 되겠지?";
            Game_TypeWriterEffect.instance.EventText[10] = "This looks very much like canned food from Earth...\n It does say that it’s edible..." +
         "\n Pretty sure that some are not edible. I should watch what I eat.\n I wonder how long it’s been out here? Can I eat this?";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 누군가의 쪽지 (획득 후)
            // Game_TypeWriterEffect.instance.EventText[0] = "알아볼 수 없는 글자들이 가득해. 나에게 무엇을 전하고 싶은 걸까.";
            Game_TypeWriterEffect.instance.EventText[0] = "Just a lot of unrecognizable characters. I wonder what it reads.";

            //(2) 외로움 쫓기(미션 전)
            // Game_TypeWriterEffect.instance.EventText[1] = "자꾸만 안 좋은 생각을 하게 돼.\n 외로움을 쫓아야 해. 나쁜 생각을 없애기 위해 있는 힘껏 달려야겠어.";
            Game_TypeWriterEffect.instance.EventText[1] = "I keep getting these discouraging thoughts\n I need to fight off the loneliness. \n I should run as fast as I can to keep these negative thoughts at bay.";

            //(3) 갈림길 (갈림길 도착 시)
            //Game_TypeWriterEffect.instance.EventText[2] = "갈림길에 표지판이 있어서 정말 다행이야.";
            Game_TypeWriterEffect.instance.EventText[2] = "So glad that there’s a signpost at the crossroad.";
            //(4) 의문의 조각함 (획득 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "어? 이 상자 같은 건 뭐지? 조각함이네...?\n 10개의 조각을 끼울 수 있는 조각판이 있구나. 혹시 모르니 챙겨가야겠어.";
            Game_TypeWriterEffect.instance.EventText[3] = "Huh? What is this? A shard box...?\n There are 10 engravings that can fit each shard. I’d better take this.";
            //(5) 별가루 줍기 (획득 후)
            // Game_TypeWriterEffect.instance.EventText[4] = "이야호, 별가루를 주웠어! 분명 여기저기 필요한 곳이 많을거야.";
            Game_TypeWriterEffect.instance.EventText[4] = "Hooray, I found some stardust! I’m sure it’ll be useful here and there.";
            //(6)비상식량 발견 (획득 후)
            // Game_TypeWriterEffect.instance.EventText[5] = "이건 통조림이잖아. 나는 운이 좋은 거 같아! 아주 맛있겠어!";
            Game_TypeWriterEffect.instance.EventText[5] = "Look, some canned food. I think I’m feeling lucky! Looks delicious, too!";
        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.EventText = new string[5];
            //(1) 비상 식량 발견 (발견 후)
            // Game_TypeWriterEffect.instance.EventText[0] = "이건 번개 문양이 그려진 통조림이야.\n 사람이 먹을 수 있다고 그림이 그려져있어.\n 이런 게 어떻게 여기 있는 걸까?";
            Game_TypeWriterEffect.instance.EventText[0] = "This canned food has a lightning symbol on it.\n The illustration on it shows that it’s edible.\n How did this end up here?";

            //(2) 갈림길 (도달 전)
            // Game_TypeWriterEffect.instance.EventText[1] = "또 갈림길이다...";
            Game_TypeWriterEffect.instance.EventText[1] = "Crossroad again...";

            //(3) 사람의 왕래 벽화 (발견 후)
            //Game_TypeWriterEffect.instance.EventText[2] = "달을 왔다 갔다 했던 사람이 있었던 것 같아. 벽화가 말해주고 있잖아.\n " +
            //    "사람 말고 다른 존재도 있었던 것 같아.\n " +
            //    "이 길을 통해서 달을 왕래했던 걸까?";
            Game_TypeWriterEffect.instance.EventText[2] = "It appears as if somebody must have traversed back and forth to the moon.\n The murals seem to depict this.\n" +
                "Something other than humans have been here as well.\n " +
                "Did they come and go through this path?";
            //(4) 조각함의 존재 (발견 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "벽화에 의하면 얼마 전에 주웠던 조각함에 대해 적혀있어.\n" +
            //    "달의 가운데 그려져있는데...어떤 조각을 담아둘 수 있나 봐.\n " +
            //    "꼭 필요한 것 같은데... 이 조각을 다 모아야 하는 것 같아.";
            Game_TypeWriterEffect.instance.EventText[3] = "This mural describes the shard box I picked up a while ago.\n" +
                "Something is drawn around the middle of the moon...Maybe it can hold certain shards.\n " +
                "I think I should keep this... Perhaps I should collect all the shards.";
            //(5) 통로에서 1.5배 빨리 닳음 (시작부터)
            //Game_TypeWriterEffect.instance.EventText[4] = "뭔가 숨이 부족한 것 같아. 맙소사. 산소가 왜 이렇게 빨리 닳는 거지...!!\n" +
            //    " 부족하기 전에 얼른 달려가야겠어.\n 여기만 지나가면 괜찮아지겠지...?";
            Game_TypeWriterEffect.instance.EventText[4] = "Feels like I’m short of breath. Oh no, why is the oxygen running out so fast...!!\n" +
          "I should pick up the pace before it runs out.\n Will things get better once I pass this section?";
        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 비상 식량 발견 (발견 후)
            //Game_TypeWriterEffect.instance.EventText[0] = "이번에는 해가 그려져있는 스낵이야.\n 역시나 사람이 먹을 수 있는 표시가 되어있어.\n 대체 누가 만들어놓은 걸까?";
            Game_TypeWriterEffect.instance.EventText[0] = "This time it’s a snack with a picture of the sun.\n Says it’s edible for humans.\n Who could have put it here?";

            //(2) 갈림길 (도달 전)
            // Game_TypeWriterEffect.instance.EventText[1] = "하나는 해, 하나는 달이 그려져있네. 나는 달로 가야지.";
            Game_TypeWriterEffect.instance.EventText[1] = "One leads to the sun, the other to the moon. I should head to the moon.";

            //(3) 소형 산소통 줍기 (주운 후)
            //Game_TypeWriterEffect.instance.EventText[2] = "이건 소형 산소통이야. 필요할 때 적절히 사용할 수 있을 것 같아! ";
            Game_TypeWriterEffect.instance.EventText[2] = "This is a small oxygen tank. I should use it when it’s necessary!";

            //(4) 길 사라짐으로 인한 뛰기(구간 빨리 달리기 전)
            //Game_TypeWriterEffect.instance.EventText[3] = "점점 길이 사라지는 것 같아... 그냥 느낌 탓인가?\n" +
            //    " 으앗...!!!길이 정말 사라지고 있잖아 ? !!!\n " +
            //    "빨리 달려야 해!! 안 그러면 나도 함께 사라질지도 몰라!!!";
            Game_TypeWriterEffect.instance.EventText[3] = "Why do I get the feeling that the path is slowly vanishing...?\n" +
        "Argh...!!! The path is really gone?!!! \n " +
        "I need to hurry!! I might disappear as well!!!";

            //(5) 징검다리
            //Game_TypeWriterEffect.instance.EventText[4] = "어라.. 길의 간격이 너무 넓어... 그냥 걸어가면 길에서 멀어져 버릴 것 같아.\n " +
            //    "달려서 징검다리를 뛰어넘어가자!";
            Game_TypeWriterEffect.instance.EventText[4] = "That’s odd... The paths are too far apart... I might disappear if I just walk across.\n " +
                "Let’s jump across the stepping stones at full speed!";
            // (6) 조각난 우주선의 파편(발견 후)
            //Game_TypeWriterEffect.instance.EventText[5] = "우주선의 파편인 건가...? 누군가 여기 우주선을 타고 온 걸까?\n 아니면 내가 착륙했을 때 여기까지 날아온 걸까?\n 이건 필요 없으니 얼른 지나가자.";
            Game_TypeWriterEffect.instance.EventText[5] = "TFragment from a space shuttle...? Did someone come here on a space shuttle?\n Or is this the place that I originally landed? \n I won’t be needing this, so let’s leave it.";
        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //아기별 만나기 전
            //Game_TypeWriterEffect.instance.EventText[0] = "방금 이상한 소리가 들린 것 같아. \n" +
            //    "/라디오 : 도...와...줘 치익 \n/ ...또... 들렸어!";
            Game_TypeWriterEffect.instance.EventText[0] = "I think I just heard something. \n" +
                "/Radio: H-Help... me (static) \n/  ...There it is again! ";

            //아기별 만난 후
            //Game_TypeWriterEffect.instance.EventText[1] = "아기 별님아 엄마를 잃어버렸니?\n " +
            //    "/라디오 : 응... 흑흑..나를 도와줘.\n/" +
            //    "너는 이름이 뭐야?\n " +
            //    "/라디오 : 미..츄야.\n/ 미츄구나, 지구의 meet you와 닮은 이름이야\n" +
            //    "/라디오 : 엄마를 잃어버렸어... 엄마를 찾는 걸 도와줄 수 있어?\n/ " +
            //    "물론이지, 내가 너의 엄마를 찾아줄게! 나와 함께 가자.";

            Game_TypeWriterEffect.instance.EventText[1] = "Baby star, did you lose your mother?\n " +
                "/Radio: Yeah... (sob) Please help me find her.\n/" +
                "What’s your name?\n " +
                "/Radio: I’m... Michu.\n/ Hi, Michu. Kind of sounds like “meet you” back on Earth \n" +
                "/Radio: I lost my mother... Will you help me find her? \n/ " +
                "Of course, I will! Come with me.";
            //갈림길 (전)
            //Game_TypeWriterEffect.instance.EventText[2] = "미츄, 갈림길에서는 당황하지 말고 표지판을 따라가면 돼.\n " +
            //    "/라디오 : 우리도 달을 향해 가는 중이었어. 같은 방향이어서 다행이야.\n/ " +
            //    "그래? 정말 잘 됐다!";
            Game_TypeWriterEffect.instance.EventText[2] = "Michu, don’t get confused at the crossroads and just follow the signposts.\n " +
        "/Radio: We were heading for the moon as well. I’m glad we’re going to the same place.\n/ " +
        "Is that so? That’s a relief!";
            ////////////////////////////////////////////////////////////
            //비상식량, 발견전
            //Game_TypeWriterEffect.instance.EventText[3] = "어, 이건 행성이 그려진 통조림이네.\n " +
            //    "이걸 먹으니 갈증과 배고픔이 순식간에 사라졌어.";
            Game_TypeWriterEffect.instance.EventText[3] = "Huh, this canned food has a picture of a planet\n " +
                "I don’t feel hungry or thirsty at all after eating this.";

            //엄마별 만나기 (전)
            //Game_TypeWriterEffect.instance.EventText[4] = "어..! 저 별이 점점 이곳에 다가오는 것 같아\n " +
            //  "/라디오 : ...어?\n/" +
            //  "미츄 너랑 똑같이 생겼어!\n" + "저기요!! 미츄를 찾고 있나요?\n" +
            //  "여기에요!";
            Game_TypeWriterEffect.instance.EventText[4] = "Look...! A star seems to be approaching us\n " +
              "/Radio: ...Really?\n/" +
              "It looks just like you, Michu!\n" + "Excuse me!! Are you looking for Michu?\n" +
              "Over here!";

            //엄마별 만나기 (후)
            //Game_TypeWriterEffect.instance.EventText[5] = "미츄 너의 엄마야 !\n " +
            //    "/라디오 : 맞아! 우리 엄마야! 엄마아! 흑흑.\n/" +
            //    "나도 엄마가 보고 싶어지네... 미츄, 엄마를 찾아서 정말 다행이야.\n " +
            //    "/라디오 : 아기별을 찾아줘서 고마워요. 보답으로 이 달의 조각을 줄게요.\n/ " +
            //    "/라디오 : 너랑 걷는 동안 즐거웠어! \n/ " +
            //    "나도 덕분에 외롭지 않았어. 넌 우주에서 사귄 첫 친구야! 절대 잊지 않을게.\n 잘 가!";
            Game_TypeWriterEffect.instance.EventText[5] = "Michu, it’s your mother!\n " +
       "/Radio: You’re right! That’s my mom! Mommy!! (sob)\n/" +
        "I really miss my mother, too... Michu, I’m really glad you found yours.\n " +
          "/Radio: Thank you for finding my baby. I want you to have this moon shard in return.\n/ " +
        "/Radio: It was fun walking with you! \n/ " +
        "Thanks for keeping me company.\n You’re the first friend I made in space! \n I’ll never forget about you.\n Goodbye!";
            //별가루 사용!
            //Game_TypeWriterEffect.instance.EventText[6] = "기분이 너무 좋아.\n " +
            //  "별가루를 사용해서 조금 빨리 달려볼까?\n";
            Game_TypeWriterEffect.instance.EventText[6] = "This feels good.\n " +
              "Should I pick up the pace a little by using moondust?\n";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[0] = "갈림길이 도대체 몇 개일까...? 저기는 길이 없는 것 같으니 이쪽으로 가야겠어.";
            Game_TypeWriterEffect.instance.EventText[0] = "Just how many crossroads are there...? I think I’ll head down this way.";

            //(2) 길 사라짐으로 인한 뛰기 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[1] = "어? 뭔가 불길한데...??\n 역시...!!길이 사라지는 것 같아.뛰어야겠어...!";
            Game_TypeWriterEffect.instance.EventText[1] = "Huh? Something is off...?? I knew it...!! The path is vanishing. I should run...";
            //(3) 손전등 (발견 전)
            //Game_TypeWriterEffect.instance.EventText[2] = "어 발에 뭔가가 걸리는 것 같아.\n" +
            //    "달 무늬가 새겨진 손전등이야..!!이게 켜지면 조금 더 밝아질 것 같아.\n" +
            //    "엇! 켜졌다! 훨씬 더 나은 걸?";
            Game_TypeWriterEffect.instance.EventText[2] = "I felt something around my foot.\n" +
               "A flashlight with moon decorations...!! This will make things brighter.\n" +
               "Ah, it came on! Now that’s much better.";
            ////////////////////////////////////////////////////////////
            //(4) 비상식량 (발견 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "달과 별이 그려져있네.\n  무슨 맛인지 설명할 수 없지만, 맛있는 건 분명해...!\n 덕분에 허기가 사라지는 것 같아.";
            Game_TypeWriterEffect.instance.EventText[3] = "This one is inscribed with a moon and a star.\n No idea what it means, but tastes good nonetheless...!\n I’m not hungry anymore.";

            //(5) 누군가의 쪽지 (발견 후)
            // Game_TypeWriterEffect.instance.EventText[4] = "어? 쪽지다. 여기엔 글이 적혀있어!!\n 런 투더 문.... ?";
            Game_TypeWriterEffect.instance.EventText[4] = "Huh? A note. There’s something written on it!!\n Run. To. The. Moon...?";

            //(6) 산소통 구멍 (구멍 난 후)
            //Game_TypeWriterEffect.instance.EventText[5] = "취이이익- 취이이이익-\n어라...? 이게 무슨 소리지 ?\n" +
            //    "산소통에 구멍이 난 것 같아...!! 산소가 빨리 닳기 시작했어!\n" +
            //    "산소가 부족해지기 전에 달려서 다음 휴게소에 도착해야겠어!";

            Game_TypeWriterEffect.instance.EventText[5] = "(hiss) (hiss) Hmm...? What’s this sound?\n" +
                "I think there’s a hole in the oxygen tank...!!\n Oxygen level is going down rapidly!\n" +
                "I should hurry up and make it to the next rest area before the oxygen runs out.";

            //(7) 징검다리
            //Game_TypeWriterEffect.instance.EventText[6] = "어라.. 길의 간격이 너무 넓어... \n " +
            //    "달려서 징검다리를 뛰어넘어가자!";

            Game_TypeWriterEffect.instance.EventText[6] = "That’s odd... The paths are too far apart...\n " +
                         "Let’s jump across the stepping stones at full speed!";
        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[0] = "달 그림이 그려진 표지판이 보여. 이쪽으로 가는 게 맞겠지?";
            Game_TypeWriterEffect.instance.EventText[0] = "I see a signpost with a picture of the moon. I should head this way, right?";

            //(2) 비상식량 (후)
            //Game_TypeWriterEffect.instance.EventText[1] = "얏호! 비상식량이다! 덕분에 배가 부르겠어.";
            Game_TypeWriterEffect.instance.EventText[1] = "Hooray! Food! Thanks to this I won’t be hungry.";
            //(3) 별가루 줍기 (전)
            //Game_TypeWriterEffect.instance.EventText[2] = "어?! 별가루다! 아이템 함에 넣어둬야겠어.";
            Game_TypeWriterEffect.instance.EventText[2] = "Eh?! More stardust! Better stash this in my inventory.";

            //(4) 고래 꼬리 쫓아 달리기 (전)
            //Game_TypeWriterEffect.instance.EventText[3] = " 저 멀리서 무언가가 다가오고 있어...\n" +
            //    "... 저건... 고래...?\n 고래와 무척 닮은 다른 존재이겠지?\n" +
            //    "어...? 고래의 끝에 무엇인가가 달려있어...! 달의 조각이다!\n " +
            //    "달에 도달할 때 필요해보였는데...! 저 고래를 쫓아가봐야겠어!";

            Game_TypeWriterEffect.instance.EventText[3] = "Something is coming towards me from far away...\n" +
                "...Is that... a whale...?\n Surely it’s something else that looks like a whale?\n" +
                "What...? There’s something on the whale’s tail...! A moon shard!\n " +
                "I’ll be needing it to reach the moon...!\n I should go after that whale!";

            //(4-1) 고래 꼬리 쫓아 달리기 (후)
            //Game_TypeWriterEffect.instance.EventText[4] = "색이 다른 달의 조각이야. 예쁘다...\n조각함에 잘 보관해야겠어.";
            Game_TypeWriterEffect.instance.EventText[4] = "This moon shard has a different color. It’s beautiful...\n I should put this in the shard box.";

            //(5) 다 쓴 대형 산소통 줍기 (후)
            //Game_TypeWriterEffect.instance.EventText[5] = "앗, 이건 구멍이 나있잖아? 산소가 하나도 없어.\n 다 써버린 대형 산소통이어서 아쉽다...";
            Game_TypeWriterEffect.instance.EventText[5] = "Argh, it has a hole in it. No a drop of oxygen left.\n Aww, an empty large oxygen tank...";

            //(6) 징검다리 구간(전)
            //Game_TypeWriterEffect.instance.EventText[6] = "여기는 징검다리 구간이야! 달가루를 써서 가야겠어!";
            Game_TypeWriterEffect.instance.EventText[6] = "I should use some moondust to cross the stepping stones!";

        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 대형산소통 줍기
            // Game_TypeWriterEffect.instance.EventText[0] = "우와! 대형 산소통을 주웠다! 산소통에 문제가 생겼을 때 사용하면 되겠어!";
            Game_TypeWriterEffect.instance.EventText[0] = "Wow! A large oxygen tank!\n I should use it if I have any problems with my current oxygen tank.";

            //(2) 달의 포자에서 살아남기 (전)
            //Game_TypeWriterEffect.instance.EventText[1] = "어...? 예쁘다.. 이건 뭐지...?\n" +
            //    "포자같이 생겼네? 달의 포자라고 불러야겠어!\n" +
            //    "근데 숨이 좀 부족해지는 것 같은데, 뭐지 ?\n 포자가 있는 구간은 빨리 지나쳐가야겠어!!";
            Game_TypeWriterEffect.instance.EventText[1] = "Huh...? So pretty... What is this....?\n" +
                "Looks like some type of spore? I’ll call it moon spore!\n" +
                "But it feels like I’m slowly running out of breath? \n I should just run past areas with moon spores.";
            //(3) 갈림길 (후)
            //Game_TypeWriterEffect.instance.EventText[2] = "표지판을 따라 걷기는 하는데, 갑자기 길이 없어지거나 하지는 않겠지?";
            Game_TypeWriterEffect.instance.EventText[2] = "I’m following the signposts,\n but the path won’t vanish on me, right?";

            //(4) 비상식량 발견 (후)
            //Game_TypeWriterEffect.instance.EventText[3] = "식량이다...!! 흐음~ 좋은 냄새. 이 길에서 찾은 비상식량은 전부 다 맛있어...!\n" +
            //    "먹을 수 있는 게 없었다면 난 여기까지 오지 못했겠지...?";
            Game_TypeWriterEffect.instance.EventText[3] = "Food...! Mmm, that smells so good. All the food I find here tastes so good...!\n" +
                "I wouldn’t have made it here without these, right...?";

            //(5) 달의 포자 구간 (전)
            //Game_TypeWriterEffect.instance.EventText[4] = "뭐야...! 또 달의 포자가 있잖아.\n 후읍. 숨을 아껴 써야 해.\n 숨쉬기 어려워지기 전에 얼른 뛰어가자!!!";
            Game_TypeWriterEffect.instance.EventText[4] = "What...! Another moon spore.\n Phew. I should control my breaths.\n Let’s get past this area before I run out of breath!!!";

            //(6) 비어있는 별가루 줍기 (후)
            //Game_TypeWriterEffect.instance.EventText[5] = "별가루통이다! 후... 근데 텅 비어있잖아?\n 이건 필요 없을 것 같아.";
            Game_TypeWriterEffect.instance.EventText[5] = "A stardust jar! Aww... It’s all empty though.\n I won’t be needing this";

        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 비상식량 (후)
            //Game_TypeWriterEffect.instance.EventText[0] = " 이건 조금 낯선 맛인 것 같아. 그래도 지구의 음식보다 훨씬 더 든든한 것 같아.\n" +
            //    "지구의 다른 음식과 달리 먹으면 배고픔이 늦게 오는 것 같아.";
            Game_TypeWriterEffect.instance.EventText[0] = " This tastes kind of strange.\n Still it’s much more nourishing than food on Earth.\n" +
               "Feels like the food here slows down my metabolism.";

            //(2) 우주 식물도감 (후)
            //Game_TypeWriterEffect.instance.EventText[1] = " 이건 백과사전 같은 건가...?\n" +
            //    "우주의 생명체에 대해 기록해놓은 것 같아.\n" +
            //    "지구어로도 번역되어 있는 것들이 있네...?!\n" +
            //    "꽤 도움이 될 것 같아.";
            Game_TypeWriterEffect.instance.EventText[1] = "Is this an encyclopedia...?\n" +
                "It has records of extraterrestrial lifeforms.\n" +
                "Some entries are even translated into earth language...?!\n" +
                "This might come in handy.";

            //(3) 별가루 줍기 (전)
            //Game_TypeWriterEffect.instance.EventText[2] = "어, 저건 별가루인 것 같아! 얼른 줍자!";
            Game_TypeWriterEffect.instance.EventText[2] = "Huh, that looks like stardust! Better pick it up quick!";


            //(4) 우주꽃 흐느낌 듣기
            //Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 치..치직...나를 도와줘\n/ " +
            //    "어 ? 이건 무슨 소리지...?\n /라디오 : 여기야..여기...\n/ 앗, 시들어가는 꽃이잖아...! 네가 나를 불렀니?\n" +
            //    "/라디오 : 나는 달가루가 필요해... 별가루가 없으면 예쁘게 피어있을 수가 없어.\n"
            //    + "나를 위해 달가루를 뿌려줄 수 있니...?\n/ " +
            //    "마침 오는 길에 달가루를 주웠어!\n 너를 도와주기 위해서였나봐!\n 기다려봐, 지금 바로 뿌려줄게!";

            Game_TypeWriterEffect.instance.EventText[3] = "/Radio: (static)... Help me \n/ " +
                "Huh? What’s that...?\n /Radio: Over... here...\n/ Ah, a withering flower...! Did you call me?!\n" +
                "/Radio: I need  moondust... I can’t fully bloom without it.\n"
                + "Will you sprinkle some moondust over me...?\n/ " +
                "It just so happens that I found some on the way here!\n Perhaps I was destined to help you!\n Hold on, I’ll give you some right away!\n";
            //갈림길
            //Game_TypeWriterEffect.instance.EventText[4] = "달에도 이런 안내판이 있다는 게 신기해.\n" +
            //    "지구와 달을 자유롭게 다녔을 때 세워둔 거겠지...?";
            Game_TypeWriterEffect.instance.EventText[4] = "I’m surprised that signposts like this exist on the moon.\n" +
                "Perhaps they were placed here\n when people could freely roam between the Earth and the Moon...?";
            //고장난 산소통
            //Game_TypeWriterEffect.instance.EventText[5] = "에잇, 이건 고장난 산소통이야...";
            Game_TypeWriterEffect.instance.EventText[5] = "Dang it, a damaged oxygen tank...";
            // (4-1) 우주꽃 흐느낌 듣기
            // Game_TypeWriterEffect.instance.EventText[6] = "/라디오 : 도와줘서 고마워, 나도 너를 도와주고 싶어.\n 내가 가지고 있는 달의 조각을 가져가렴./ ";
            Game_TypeWriterEffect.instance.EventText[6] = "/Radio: Thank you so much. I’d be glad to help you, too.\n Please take this moon shard with you./ ";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 갈림길 (도달전)
            //   Game_TypeWriterEffect.instance.EventText[0] = "이번 갈림길은.... 이쪽이다. 누가 표지판을 반대로 바꾸거나 하지 않았겠지?";
            Game_TypeWriterEffect.instance.EventText[0] = "This time it should be... This way.\n I hope nobody switched up the signs.";

            //(2) 우주 식물도감 (후)
            //Game_TypeWriterEffect.instance.EventText[1] = "저 불빛들은 뭐지?....\n" +
            //    "마치 불꽃놀이를 보는 것 같아. 보기만 해도 마음이 따스해져\n" +
            //    "누가 터트리고 있는지 당장 확인하러 가야겠어!\n" +
            //    "어라... 자세히 보니..저 불빛은 누군가 터트리는 게 아니야.\n" +
            //    "운석들이 부딪혀서 나는 빛이야...\n " +
            //    "아름답다...";
            Game_TypeWriterEffect.instance.EventText[1] = "What are those lights...?\n" +
             "They seem like fireworks. Just looking at it is making me feel cozy\n" +
             "I should go check who’s lighting them up!\n" +
             "Wait a second... Actually... Those lights aren’t caused by fireworks.\n" +
             "They’re asteroids colliding on the surface.\n " +
             "That’s spectacular...";

            //(3) 횟불대 줍기 (후)
            //Game_TypeWriterEffect.instance.EventText[2] = "이건 뭐지? 달이 그려진 횃불대 인것 같아...\n" +
            //    "이게 왜 여기 있던 걸까?\n" +
            //    "혹시 모르니까 챙겨가야겠어.";
            Game_TypeWriterEffect.instance.EventText[2] = "What do we have here? A torch with moon engravings...\n" +
      "Why would this be here?\n" +
      "I should take it just in case.";

            //(4) 길 사라짐으로 인한 뛰기(전)
            //Game_TypeWriterEffect.instance.EventText[3] = "길이 또 사라지기 시작했어! 뛰어야 해!";
            Game_TypeWriterEffect.instance.EventText[3] = "The path is disappearing again! Run!";
            //(5) 비상식량 발견(획득후)
            // Game_TypeWriterEffect.instance.EventText[4] = "우주 통조림을 발견했어! 배고팠는데 잘됐다!";
            Game_TypeWriterEffect.instance.EventText[4] = "Canned food! Thank goodness. I was getting hungry!";
            //(6) 산소 부족(전)
            //Game_TypeWriterEffect.instance.EventText[5] = "몸이 좋지 않아서인지 숨이 잘 쉬어지지 않아...\n" +
            //    "숨을 너무 가쁘게 쉬었나 봐..산소가 빨리 달기 시작했어..\n" +
            //    "산소가 바닥나기 전에 빨리 휴게소에 가야겠어...!";
            Game_TypeWriterEffect.instance.EventText[5] = "I don’t feel too good. I’m having a hard time breathing...\n" +
                "I’ve been breathing too hard... The oxygen is draining fast...\n" +
                "I need to find a rest area before the oxygen runs out...!";
            //(7) 쓰러지기 (전)
            //Game_TypeWriterEffect.instance.EventText[6] = "너무 추워... 으슬으슬해... 열도 나는 거 같아..\n" +
            //    "앞이 점..점 안보...이.....네.....";
            Game_TypeWriterEffect.instance.EventText[6] = "So cold... I’m shivering from head to toe...\n I think I feel a fever coming...\n" +
                "I think I’m going... to pass out... soon...";
        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 정신을 차리기 (전)
            //Game_TypeWriterEffect.instance.EventText[0] = "분명 너무 추웠는데, 지금은 따뜻한 것 같아...\n 잠시 정신을 잃은 것 같아... 여긴 어디지.. ?\n " +
            //    "엄마의 품일까...? 따뜻해....";
            Game_TypeWriterEffect.instance.EventText[0] = "It was so cold a while ago, but now I feel warm...\n I think I was unconscious for a bit... Where am I...?\n " +
                "Am I being hugged by my mother...? So warm....";

            //(2) 산소 닳음 (전)
            //Game_TypeWriterEffect.instance.EventText[1] = "여기는 달의 통로를 지날 때처럼 산소통의 공기가 빨리 닳고 있어.\n" +
            //    "휴게소에 들릴 때마다 반드시 산소통을 구매해야 쓰러지지 않겠어!";
            Game_TypeWriterEffect.instance.EventText[1] = "Similar to running through the moon path,\n it feels like oxygen is draining faster.\n" +
                "I’ll need to purchase oxygen tanks at every rest area to prevent passing out!";
            //(3) 정신을 차린 (후)
            //Game_TypeWriterEffect.instance.EventText[2] = "달빛이 내 주위를 맴도는 것 같아.\n 이 빛 때문에 몸이 따뜻했구나.\n" +
            //    "아, 맞아.가방 안에 횃불대가 있었어...!\n 혹시 이 빛을 담아 갈 수 있으려나?\n" +
            //    "달빛이 옮겨붙었어! 어두운 통로를 갈 때 도움이 될 것 같아!";
            Game_TypeWriterEffect.instance.EventText[2] = "The moonlight seems to be circling around me.\n So this is why I felt warm.\n" +
                "Ah, right. There’s a torch in my bag...!\n Is it possible to take this light with me?\n" +
                "The torch was lit by the moonlight!\n This might come in handy in dark tunnels!";

            //(4) 달의 포자(전)
            //Game_TypeWriterEffect.instance.EventText[3] = " 어.. 저기 흩날리는 건 달의 포자인 것 같아...!\n" +
            //    "산소가 부족해지기 전에 달려서 저 구간을 지나야겠어!";
            Game_TypeWriterEffect.instance.EventText[3] = "Huh... That appears to be a cluster of moon spores...!\n" +
                "I should run past this area before the oxygen runs out";
            //(5) 달의 포자 (후)
            //Game_TypeWriterEffect.instance.EventText[4] = "헉.. 헉... 심장이 너무 빨리 뛰어. 산소가 부족했어서 더 그런가 봐.\n" +
            //    "앞으로도 달의 포자는 조심해야겠어.";
            Game_TypeWriterEffect.instance.EventText[4] = "(Pant)... (pant)... My heart is racing.\n Most likely because there wasn’t enough oxygen.\n" +
                "I need to be careful around those moon spores.";
            //(6) 소원석과 관련된 벽화1 (후)
            //Game_TypeWriterEffect.instance.EventText[5] = "달의 신전에는 소원석이 있는 것 같아.\n" +
            //    "소원석에 소원을 빌면 이뤄주는 걸까...?\n" +
            //    "그곳에 도착하면, 지구로 돌아가게 해달라고 해야 할까 ? ";
            Game_TypeWriterEffect.instance.EventText[5] = "I think the wishing stone is at the Temple of the Moon.\n" +
                "Will the wishing stone make my wish come true?\n" +
                "When I get there, should I make a wish to go back to Earth? ";
            //(7) 소원석과 관련된 벽화2 (후)
            //Game_TypeWriterEffect.instance.EventText[6] = "소원석과 관련된 벽화가 또 있네? \n" +
            //    "이름을 새기면 소원을 빌 수 있는 것 같은데...";
            Game_TypeWriterEffect.instance.EventText[6] = "Here’s another mural depicting the wishing stone. \n" +
                "I think I can make a wish if I carve my name on it.";
            //(8) 소원석 통로 지나가기 (전)
            //Game_TypeWriterEffect.instance.EventText[7] = " 어..? 이 길의 끝에 있는 저건 뭐지...???";
            Game_TypeWriterEffect.instance.EventText[7] = "Huh...? What’s that at the end of this path?";
            //(9) 달의 조각 발견
            //   Game_TypeWriterEffect.instance.EventText[8] = "달의 조각이야!!!\n" + "또 다른 색을 가졌네.정말 예쁘다.\n " +
            //"이 통로로 나가면 또 어떤 곳이 나타날까...?";
            Game_TypeWriterEffect.instance.EventText[8] = "A moon shard!!!\n" + "This one has a different color. It’s so beautiful.\n " +
         "I wonder who put this here?";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //(1) 표지판 등장(발견 후)
            //Game_TypeWriterEffect.instance.EventText[0] = "저기 뭐가 보여... 표지판이다!\n" +
            //    "이 표지판이 없었다면 이 길을 걷는다는 느낌이 들지 않았을 거야...\n" +
            //    "누가 세워둔 표지판일까?";
            Game_TypeWriterEffect.instance.EventText[0] = "I see something up ahead... A signpost!\n" +
                "Without these signposts,\n I might as well be walking in the middle of where...\n" +
                "I wonder who put this here?";

            //(2) 달의 실타래 줍기(획득 후)
            //Game_TypeWriterEffect.instance.EventText[1] = "이건 실타래야...\n" +
            //    "반짝반짝한 빛이 흘러나오는 게 너무 아름다워,\n" +
            //    "곧 필요하게 될지도 몰라, 가져가야겠어.";
            Game_TypeWriterEffect.instance.EventText[1] = "A skein...\n" +
                "The glowing light emanating from it is so alluring.\n" +
                "I might be needing this soon, let’s take it.";

            //(3) 비상식량 줍기( 획득 후)
            // Game_TypeWriterEffect.instance.EventText[2] = "간간이 비상식량이 떨어져 있어서 다행이야.\n" +
            //"너무 배고팠어... 급하게 먹으면 체할지도 모르니 천천히 먹어야겠어.";
            Game_TypeWriterEffect.instance.EventText[2] = "What a relief to find food every now and then.\n" +
                "I was so hungry...\n I should take my time eating this.";


            //(4) 대형 산소통 줍기(획득후)
            //Game_TypeWriterEffect.instance.EventText[3] = "줍기 전에는 전처럼 구멍 난 대형 산소통일까 했는데...\n 이건 산소가 가득한 산소통이야!\n" +
            //    "지금 건강이 좋지 않지만... 오늘 운은 정말 좋은 편인 것 같아";
            Game_TypeWriterEffect.instance.EventText[3] = "Before I picked it up\n it looked like the large oxygen tank from before...\n But this one is filled with oxygen!\n" +
                "Although I’m not feeling good right now...\n My luck today has been quite amazing";
            //(5) 갈림길(도달 전)
            //Game_TypeWriterEffect.instance.EventText[4] = " 또 갈림길이야... 이젠 적응이 된 것 같아. 이쪽으로 가야 해.";
            Game_TypeWriterEffect.instance.EventText[4] = "Another crossroad... I starting to get the hang of this. It should be this way.";
            //(6) 별가루 줍기(획득 후)
            //Game_TypeWriterEffect.instance.EventText[5] = "반짝거려... 계속 이 빛만 바라보고 싶어.\n 챙겨둬야겠어. 유용하게 쓰일 거야.";
            Game_TypeWriterEffect.instance.EventText[5] = "Shiny... I just want to stare at it forever.\n I’ll keep it. Put it to good use later.";

            //답답함 떨치기 2022.02.15
            //Game_TypeWriterEffect.instance.EventText[6] = "이곳은 답답해...";
            Game_TypeWriterEffect.instance.EventText[6] = "This place is stuffy...";
        }
        //룬과의 만남
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 영혼 등장 (전)
            //Game_TypeWriterEffect.instance.EventText[0] = " /라디오: 치직 - 안녕...? 치지직 / \n" +
            //    "/라디오: 나는 빛을 잃은 영혼이야.\n 우주꽃 열 송이를 모아서 나에게 주면, 나는 다시 빛을 찾을 수 있어.\n" +
            //    "하지만, 난 더 이상 꽃을 찾을 힘이 없어.\n 이러다가 곧 사라지고 말거야. 너무 무서워./\n" +
            //    "내가 모아줄게, 나와 함께 가자.\n" +
            //    "/라디오: 하지만 나는 더 이상 움직일 힘이 없어.../ \n";
            Game_TypeWriterEffect.instance.EventText[0] = " /Radio: (static) Hello...? (static)/ \n" +
                "/Radio: I am a soul without light.\n If you could find me ten space flowers,\n I’ll be able to reclaim the light.\n" +
                "I no longer have the strength to find these flowers.\n Soon enough I will disappear and this scares me so much./\n" +
                "Don’t worry, I’ll help. Come with me.\n" +
                "/Radio: But I can’t move on my own.../ ";

            //(1-2) 영혼 등장 (후)
            //Game_TypeWriterEffect.instance.EventText[1] = "그건 걱정하지마... 좋은 생각이 났어.";
            Game_TypeWriterEffect.instance.EventText[1] = "Don't worry about that... I got an idea.";

            //Game_TypeWriterEffect.instance.EventText[2] = "너와 나의 팔에 실을 묶어서 연결했어.자 이제 됐지?";
            Game_TypeWriterEffect.instance.EventText[2] = "I tied a string to your arm and connected it.\n It's okay now, right?";


            //(2) 비상식량
            //Game_TypeWriterEffect.instance.EventText[3] = "이번 통조림은... 어떤 맛일까?\n" +
            //    "매번 어떤 비상식량을 찾게 될지 기대하게 되는 것 같아.\n" +
            //    "이렇게 기대를 하다 보면... 실망도 커질 텐데 말이야.";
            Game_TypeWriterEffect.instance.EventText[3] = "This canned food...\n I wonder what it’ll taste like?\n" +
                "I get excited when I think about the kind of food\n I’ll find next time.\n" +
                "I shouldn’t set my expectations too high though...\n I might get more disappointed.";

            //(3) 우주꽃을 모으기(전)
            //Game_TypeWriterEffect.instance.EventText[4] = "우주 꽃 열 송이를 다 모았어... 여기 꽃을 받아.\n" +
            //    "/라디오: 고마워!/\n";
            Game_TypeWriterEffect.instance.EventText[4] = "I’ve collected all ten space flowers....\n Here, they’re for you.\n" +
                "/Radio: Thank you!/ ";
            //(3-1) 우주꽃을 모으기(후)
            //Game_TypeWriterEffect.instance.EventText[5] = "네게서 빛이 나기 시작했어...! 눈부셔! \n" + "/라디오: 너로 인해 새롭게 태어났어. 고마워!/ \n ...! 이게 네 원래 모습이구나!\n" +
            //    "/라디오: 응! 네 덕분에 원래 모습을 되찾았어. \n 라디오: 감사의 인사로 내가 가지고 있던 달의 조각을 줄게.\n 라디오: 내가 도와줄 수 있는 건 없을까?/ \n" +
            //    "사실... 혼자 가는 길이 너무 외로워. 네가 괜찮다면 나와 함께 걸어줄래 ?\n" +
            //    "/라디오 : 좋아, 너의 도착지까지 함께 동행할게.";
            Game_TypeWriterEffect.instance.EventText[5] = "You’re starting to glow...! Gosh, that’s dazzling! \n" + "/Radio: I’ve been reborn, thanks to you!/ \n ...! So this is your original form!\n" +
                "/Radio: That’s right! I owe it all to you. \n Radio: As a token of gratitude, I want you to have this moon shard.\n Radio: Is there anything else I can help you with?/ \n" +
                "Actually... It does get awful lonely out there.\n If it’s not much to ask, will you join me?\n" +
                "/Radio: Sure, I’ll follow you to your destination./ ";
            //(4) 갈림길 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[6] = "갈림길이야... 이쪽으로 가도 되겠지?";
            Game_TypeWriterEffect.instance.EventText[6] = "A crossroad... Should we go down this path?";
            //(5) 별과의 동행 (후)
            //Game_TypeWriterEffect.instance.EventText[7] = ".. 있잖아. 너와 함께하게 되어서 너무 기뻐.\n 나는 오랫동안 이 길에서 혼잣말을 했거든.\n" +
            //    "/라디오: 내가 너의 말을 들어줄께/ \n" +
            //    "이렇게 대답이 돌아오는 대화도 오랜만이야. 든든한걸!\n" +
            //    "/라디오: 너는 외로움을 많이 타는구나? 어떻게 외로움을 견뎠니 ?/ \n" +
            //    "엄마와 친구와 함께했던 추억을 생각하고 함께할 미래를 상상 했어!\n" +
            //    "/라디오: 너는 주변 사람들을 사랑하는구나,\n 너의 말 한마디에 그들에 대한 애정이 묻어나./ ";

            Game_TypeWriterEffect.instance.EventText[7] = "...Hey. I’m really glad that you joined me.\n I used to talk to myself for the longest time.\n" +
             "/Radio: I’m here to listen to you./ \n" +
             "It’s been such a long time since I had a conversation.\n Thanks for having my back! \n" +
             "/Radio: You’re quite the lonely type, aren’t you?\n How did you bear the loneliness?/ \n" +
             "I looked back on the memories I shared with mother and friends and imagined what our future would look like!\n" +
             "/Radio: You must really love your close ones.\n Your love for them seems to resonate with every word./ ";

            //(6) 달의 포자 길 건너기(발견 후)
            //Game_TypeWriterEffect.instance.EventText[8] = "... 빛나는 포자야. 여기에 오기 전에 이런 곳을 지나왔었어.\n" +
            //    "예쁘게 빛나지만 산소통의 산소를 빨아들여서 오래 있을 수 없어...\n" +
            //    "이곳을 빨리 지나가야겠어.";

            //(6) 달의 포자 길 건너기(발견 후)
            Game_TypeWriterEffect.instance.EventText[8] = "... A shiny spore.\n I passed through this kind of place on my way here.\n" +
                "It’s fascinating to behold but we can’t stay too long \n because it drains the oxygen.\n" +
                "Let’s hurry past this place.";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1)갈림길(도달 전)
            //Game_TypeWriterEffect.instance.EventText[0] = "네가.... 그러고 보니 영혼아 네게도 이름이 있니?\n" +
            //    "/라디오: 응, 나의 이름은 룬이야/ \n 예쁜 이름이구나, 룬 내가 이쪽으로 가는 걸 어떻게 생각해 ?\n /라디오 : 좋은 선택인걸./ ";
            Game_TypeWriterEffect.instance.EventText[0] = "Hey... By the way, do you have a name?\n" +
                "/Radio: Yes, my name is Lune./ \n That’s a pretty name.\n Lune, what do you think about going this way?\n /Radio: An excellent choice./ ";
            //(2)비상식량(획득 후)
            //Game_TypeWriterEffect.instance.EventText[1] = "그러고 보니 룬은 이런 비상식량을 먹어본적이 있니?\n" +
            //    "/라디오: 아니, 네가 먹어보고 어떤 건지 설명해줘./ \n" +
            //    "음 어떻게 설명을 해줘야 할까... 고민할 시간을 줘!";
            Game_TypeWriterEffect.instance.EventText[1] = "Lune, have you ever tried this food?\n" +
                "/Radio: No, but please tell me what they’re like./ \n" +
                "Hmm, how should I describe it... \n Give me some time to think!";

            //(3)징검다리 구간(도달 전)
            //Game_TypeWriterEffect.instance.EventText[2] = "이크, 징검다리 구간이야. 달가루를 사용해야겠다.";
            Game_TypeWriterEffect.instance.EventText[2] = "Eek! Stepping stones. I should use stardust.";

            //(4)빛새(발견후)
            //Game_TypeWriterEffect.instance.EventText[3] = "빛나는 새야! ...어라? 새가 아닌가? 동그랗게 생겼네.\n" +
            //    "빛이 날아다니는 것 같아!";
            Game_TypeWriterEffect.instance.EventText[3] = "A shining bird!... Or is it? It looks round.\n" +
                "It’s a floating ball of light!";
            //(5)크리스탈 결정체(도달 후)
            //Game_TypeWriterEffect.instance.EventText[4] = "우와... 크리스탈이가득해... 그러고 보니 달의 조각을 닮았어\n" +
            //    "이 광석들 혹시 달의 조각 아니야 ?\n" +
            //    "/라디오 : 맞아, 누군가의 진실한 마음이 담겨야만 이 광석이 달의 조각이 될 수 있어./ \n" +
            //    "그럼 내가 모은 조각들은 모두, 마음이 담긴 조각들이었구나..마음이 든든해졌어.";
            Game_TypeWriterEffect.instance.EventText[4] = "Wow... Look at all these crystals...\n They look just like the moon shards\n" +
                "Could they have originated from these crystals?\n" +
                "/Radio: That’s right. Only those that embody true feelings can become moon shards./ \n" +
                "So each of the moon shards I’ve collected so far has its own feelings...\n I feel more determined now.";
            //(6)길의 차가움(전)
            //Game_TypeWriterEffect.instance.EventText[5] = "이곳에 계속 있으니까 점점 몸이 시려와...\n 온도가 많이 낮은 곳인가 봐\n" +
            //    "/라디오: 어떻게 할 거야 ?/ \n 달릴 거야, 이곳에 오래 있으면 안 될 것 같은 기분이 들어...\n 날 놓치지 말고 잘 따라와!";
            Game_TypeWriterEffect.instance.EventText[5] = "It’s painfully cold in this area...\n It’s as if I were inside a freezer\n" +
                "/Radio: What will you do?/ \nI’m going to run.\n It feels like the longer I stay here, the worse will happen.\n Hang tight. Don’t let go of me!";

        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.EventText = new string[6];
            //(1) 맵 진입 후 바로
            //Game_TypeWriterEffect.instance.EventText[0] = "눈처럼 하얀 입자가 날리는 곳이네. 예쁘다.";
            Game_TypeWriterEffect.instance.EventText[0] = "White, snowy particles are floating around here. It’s beautiful.";
            //(2) 갈림길 (전)
            //Game_TypeWriterEffect.instance.EventText[1] = "갈림길이네. 룬, 그림이 좀 희미하지만 이쪽 방향이 맞는 것 같지?\n" +
            //    "/라디오 : 응.달이 그려져있잖아!/\n 이 머나먼 길도 너와 함께하니 즐거워\n /라디오 : 나도 그래, 특별한 추억이 될 것 같아./ ";
            Game_TypeWriterEffect.instance.EventText[1] = "It’s a crossroad. Lune,\n the sign looks faded, but do you reckon it’s this way?\n" +
                "/Radio: Yes. That one has a picture of the moon!/\n I’m so glad that you’re here with me.\n /Radio: So am I. This is going to be a special memory./ ";
            //(3) 비상 식량 (후)
            //Game_TypeWriterEffect.instance.EventText[2] = "별 그림이 많은 비상식량이야.\n 톡톡 튀는 맛이 아주 신기하네? 지구에는 없는 맛이야.\n " +
            //    "/라디오 : 나도 지구라는 곳이 궁금해. 늘 멀리서만 봐왔거든./ \n" +
            //    "너와 함께 지구로 돌아가면 좋겠어. 내가 보여주고 싶은 게 너무 많아. ";
            Game_TypeWriterEffect.instance.EventText[2] = "This one has a bunch of stars on it.\n . I’m surprised at how zesty it is.\n Can’t find this type of flavor on Earth.\n " +
                "/Radio: I wonder what Earth is like.\n I’ve only observed it from afar./ \n" +
                "I hope we could go back together.\n There is so much I want to show you. ";
            //(4) 별가루 줍기 (후)
            //Game_TypeWriterEffect.instance.EventText[3] = "별가루통이야. 반짝반짝 빛나는 성분이 들어있나 봐.\n " +
            //    "/라디오 : 이 별가루를 지구로 가져가면 재로 변해버린데.\n 우주에 있을 때만 빛날 수 있어./ \n " +
            //    "정말 ? 지구의 사람들에게 보여주고 싶었는데... 아쉽다.";
            Game_TypeWriterEffect.instance.EventText[3] = "A stardust jar.\n Looks like it has something sparkling inside.\n " +
            "/Radio: I heard stardust turns to ash if taken to Earth. \n It can only shine on the moon./ \n " +
            "Really? I wanted to show this to others... Bummer.";
            //(5) 목걸이 발견 (전)
            //Game_TypeWriterEffect.instance.EventText[4] = "저기 유독 빛나는 게 있는데... 저게 뭐지?";
            Game_TypeWriterEffect.instance.EventText[4] = "Something over there is shining awful bright...What is that?";
            //(6) 목걸이 발견 (후)
            //Game_TypeWriterEffect.instance.EventText[5] = "어? 이건 목걸이잖아...? 달의 무늬가 새겨져있네.\n" +
            //    "/라디오 : 누군가가 이 길을 걸어가다 잃어버렸나 봐./ ";
            Game_TypeWriterEffect.instance.EventText[5] = "Huh? It’s a necklace...?\n It has moon patterns on it.\n" +
                "/Radio: Somebody must’ve lost it on the way./ ";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {
            Game_TypeWriterEffect.instance.EventText = new string[8];
            //(1) 맵 진입 후 바로
            //Game_TypeWriterEffect.instance.EventText[0] = "저기 보이는 거 산소통 맞지? 역시!\n 룬은 산소가 필요 없으니 편할 것 같아.\n" +
            //    "/라디오 : 나는 너와 달리 이곳에서 태어나고 자랐으니까/ ";
            Game_TypeWriterEffect.instance.EventText[0] = "Is that an oxygen tank over there? Knew it!\n Lune, it must be convenient that you don’t need oxygen.\n" +
                "/Radio: Yes, unlike you I was born and raised here./ ";
            //(2) 갈림길 (전)
            //Game_TypeWriterEffect.instance.EventText[1] = "룬, 이번에는 표지판이 거의 보이지 않아... 같이 길을 선택해 줘\n" +
            //    "/라디오: 저기 바닥에 희미하게 달 그림이 보이는 것 같아./ \n" +
            //    "그럼 저기다! 저기로 가야겠어!";
            Game_TypeWriterEffect.instance.EventText[1] = "Lune, I don’t see a signpost this time...\n Help me choose which way to go.\n" +
                "/Radio: If you look closely,\n there’s a picture of the moon on the ground./ \n" +
                "That way it is, then! Let’s go!";
            //(3) 징검다리 길 (도달 전)
            //Game_TypeWriterEffect.instance.EventText[2] = "징검다리야, 이곳은 그냥 지나가기 힘들겠어... 별가루를 사용하자.\n" +
            //    "룬 너는 필요 없어 ?\n" +
            //    "/라디오 : ....나는 치이이이이익 - 픽 -/ ";
            Game_TypeWriterEffect.instance.EventText[2] = "Stepping stones, again.\n It’ll be tough crossroad it...\n Let’s use some moondust.\n" +
                "Lune, do you need some?\n" +
                "/Radio: ...I (static) (zip)/ ";
            //(4) 우주라디오 켜기(전)
            //Game_TypeWriterEffect.instance.EventText[3] = "룬? ..... 라디오가 고장 났나!?\n" +
            //    "뭐 ? 아니라고 ? 아! 라디오의 달가루가 다 소모됐구나!";
            Game_TypeWriterEffect.instance.EventText[3] = "Lune?... Did the radio break down!?\n" +
                "Huh? Wait a minute! The radio used up all the moondust!";
            //(4-1) 우주라디오 켜기(후)
            //Game_TypeWriterEffect.instance.EventText[4] = "너와 다시 대화를 못하게 될까 봐 놀랐었어.\n" +
            //    "/라디오 : 그래도 침착하게 잘 대처했어./ \n" +
            //    "맞아! 그거 알아 룬?\n  언제부턴가 라디오에서 지구의 소식이 아닌\n 너와의 대화에만 집중한 거 있지!\n" +
            //    "/라디오 : 영광인걸, 자 다시 힘내서 가자.";
            Game_TypeWriterEffect.instance.EventText[4] = "I was afraid that I wouldn’t be able to talk to you again.\n" +
                "/Radio: Still, good thinking on your part./ \n" +
                "Oh, right! You know what, Lune?\n  Ever since we met,\n I began to ignore news from Earth.\n" +
                "/Radio: I’m flattered. C’mon let’s get going.";
            //(5) 별자리 소개(발견 후)
            //Game_TypeWriterEffect.instance.EventText[5] = " ! 저기 별자리들이 있어.\n 우리별에서 물병자리라고 부르는 별자리야.\n" +
            //    "/라디오 : 지구에서도 우주를 제대로 보고 있구나./ \n" +
            //    "당연하지! 사람들은 우주를 사랑하는걸!\n" +
            //    "/라디오 : 부끄럽지만 저 푸른 별은 볼게 너무 많아서\n 우주를 볼 시간이 없을 거라 생각했어./ \n" +
            //    "하하 그럴 리 없지! 우주가 훨씬 넓은걸?\n 저기 봐, 저 별자리는 오리온자리야!\n" +
            //    "/라디오 : 들떠 보이네./ \n" +
            //    "당연하지, 이렇게 가까이서 별자리를 보게 될 줄 몰랐어.\n어라 저 별자리는 처음 보는 별자리야.\n" +
            //    "/라디오 : 아, 저 별자리는 작아서 우주에서만 볼 수 있을 거야.\n 네가 괜찮다면 내가 소개해줄께/ \n" +
            //    "당연히 괜찮지! 하나씩 다 소개해 줘!";
            Game_TypeWriterEffect.instance.EventText[5] = "Look, there are stars over there.\n Back on Earth, we call them Aquarius.\n" +
                "/Radio: I guess people down on Earth are observing the galaxy./ \n" +
                "You bet! People love everything about it!\n" +
                "/Radio: I always thought people on Earth had too much on their hands.\n I didn’t think they would have the time to observe the galaxy./ \n" +
                "Haha, no way! The galaxy is much bigger, you know?\n Look over there, those stars are called the Orion!\n" +
                "/Radio: You seem excited./ \n" +
                "Of course,\n I never thought I’d get the chance to observe them this close.\n Hmm, I haven’t seen that one before.\n" +
                "/Radio: Oh, that one should only be visible from space.\n I could introduce you, if you’d like./ \n" +
                "Sounds awesome!\n Take your time and tell me about each one!";
            //(6) 맹독의 달의 포자(포자 길 걷기 전)
            //Game_TypeWriterEffect.instance.EventText[6] = "또 포자야... 산소를 빼앗기기 전에 빨리 벗어나야겠어.\n" +
            //    "여기는 다른 포자와 다르게 붉은색을 띠고있어..\n뭔가 다른걸까? 괜히 더 불길한걸..";
            Game_TypeWriterEffect.instance.EventText[6] = "Spores, again...\n I should hurry before I lose more oxygen.\n" +
                "The spores here are red...\nI wonder what’s different about them?\n They look more menacing...";
            //(7) 맹독의 달의 포자(걷는 중) + 쓰러짐
            //Game_TypeWriterEffect.instance.EventText[7] = "걷고 있는데... 몸에 힘이 잘 안 들어가....\n" +
            //    "... 이런... 왜 자꾸 눈이 감기지... 안되는데....\n 이 포자에... 다른 포자랑 다른게 있나 봐.....\n" +
            //    "...참아야해... 빨리 벗어나야 하는데........눈이 감겨... 졸린 것 같아...\n" +
            //    "........";
            Game_TypeWriterEffect.instance.EventText[7] = "I can’t... seem to move...\n" +
                "... Why... are my eyes closing...\n Not good...\n Something...\n is different about these spores....\n" +
                "...I need to stay awake...\n Must get out of here quick...\n But my eyes... are so tired...\n" +
                "........";
        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {
            Game_TypeWriterEffect.instance.EventText = new string[7];
            //((1) 눈 뜸
            //Game_TypeWriterEffect.instance.EventText[0] = "! 여기는 ....어디지? 룬? ...어디 있어? \n ... 여기는 우주가 아닌 것 같아.별도 달도 보이지 않아..\n " +
            //    "지구의 물건들이야...여긴 지구인가?..\n 분명....우주의 길을 걷다가 마지막에 포자 길을 걸은 뒤로 기억이 없어...\n" +
            //    "언제부터 이곳에 있었던 거지? ..일단...걸어볼까 ? ";
            Game_TypeWriterEffect.instance.EventText[0] = "! Where... am I? Lune?... Where are you? \n ... This doesn’t look like space.\n Can’t see the moon or stars...\n " +
                "These are stuff from Earth...\n Am I back on Earth?\n I can’t remember anything...\n past the point of walking on that path \n and running into those spores.\n" +
                "How long was I here for?\n...Let’s...try walking?";
            //(2) 내가  쓰던 침대( 발견 후)
            //Game_TypeWriterEffect.instance.EventText[1] = "어! 저건 내가 썼던 침대야...\n 왜 이런 곳에 있지? 누가 가져다 둔 걸까?\n" +
            //    "누워자고 싶어....\n가까이 다가가니 침대가 사라지잖아...\n 실제로 있는 게 아니야...\n" +
            //    "... 매일 밤 동화를 읽어주던 엄마가 그리워,\n 그 날들이 가장 행복한 날들이었던 것 같아.\n" +
            //    "저 침대는 내가 아플 때에도, 잠을 잘 때에도 늘 있었는데...\n" +
            //    "엄마가 이불빨래를 하는 날이면\n 침대에서도 뽀송한 햇빛 향이 가득했었어.\n 하지만 누울 수 없네...";
            Game_TypeWriterEffect.instance.EventText[1] = "Ah! This is the best I used to sleep on...\n Why is it here? Who could’ve put it here?\n" +
                "I just want to lie down and sleep...\n The bed vanishes when I get close...\n This is not real...\n" +
                "... I miss the nights when mother used to read me books.\n Those days were the happiest time of my life.\n" +
                "That bed was always there for me\n when I was sick, or even tired...\n" +
                "When mother used to wash the covers,\n it had that cozy fragrance after being dried in the sun.\n But I can’t lie down...";

            //(3) 즐겨먹던 아이스크림( 발견 후)
            //Game_TypeWriterEffect.instance.EventText[2] = "저건 내가 제일 좋아하는 아이스크림이잖아!\n" +
            //    "아마... 저것도 다가가면 사라지겠지?\n ...희망고문이라는 단어는 이럴 때 쓰는 말인가 봐.\n" +
            //    "저 아이스크림에도 추억이 있어,\n 놀이터에서 주운 동전을 모아 사 먹었던 기억도 있어\n" +
            //    "하지만 무엇보다 엄마랑 외출했다\n 사주신 아이스크림이 가장 맛있었지.\n" +
            //    "사자마자 떨어트렸을 때는 너무 서러워서 울었지만...\n 지금 생각하니 그렇게 울 일은 아니었던 것 같아.\n" +
            //    "어렸을 때 친구랑 돈을 모아 한 개를 사곤\n 한입씩 나눠먹었던 적도 있어.";
            Game_TypeWriterEffect.instance.EventText[2] = "That’s my favorite ice cream!\n" +
                "Will that... vanish if I get too close?\n ...A pie in the sky would best describe my situation.\n" +
                "I have fond memories of that ice cream.\n I remember collecting spare changes at the playground to buy one.\n" +
                "But above all else, the ice cream\n that my mother bought me on our outings tasted the greatest.\n" +
                "Even though I cried my heart out\n when I dropped it on the ground...\n Looking back,\n it was such a trivial thing to cry about.\n" +
                "There was even a time when a friend and I shared one,\n and took one bite at a time.";

            //(4) 가장 좋아하는 책( 발견 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "이건... 내가 좋아한 책이잖아,\n 나는 이 책을 몇 번이고 읽었어.\n" +
            //    "친구들은 같은 책을 계속 읽으면\n 부모님들이 좋아하지 않는다 했지만\n 우리 엄마는 별말씀을 하지 않으셨어\n" +
            //    "이 책은... 작은 아이의 이야기야,\n 그 아이가 회색 신사들로부터 마을을 구하지!\n" +
            //    "엄마도 이 책을 좋아했어,\n 내가 몰라 접어둔 페이지가 가끔 펴져있는 것을 보았을 때\n 엄마도 이 책을 읽고 있구나 싶었지!\n" +
            //    "아껴서 소중하게 읽었지만 책의 모퉁이가 닳아버렸어.\n" +
            //    "앗 이건 이 책도 좋아했어.\n 고슴도치의 이야기야,\n 혼자 있던 고슴도치가 친구를 가득 사귀는 책이었지...\n" +
            //    "다시 읽어보고 싶은데...\n 이곳에 오기 전에 많이 읽을 걸 그랬어.";
            Game_TypeWriterEffect.instance.EventText[3] = "This... is my favorite book.\n I can’t remember how many times I’ve read this.\n" +
                "My friends used to tell me that their\n parents didn’t approve of them reading\n the same book over and over.\n But my mother was different.\n" +
                "This book... is about a young boy\n The boy ends up saving the town from the grey gentlemen!\n" +
                "Mother really liked this book, too.\n I would secretly fold the pages\n but sometimes they were opened.\n That’s when I figured she had read this book, too!\n" +
                "I was careful with the book\n but the corners are all worn out.\n" +
                "Ah, I loved this other book, as well.\n It’s about a porcupine.\n The lonely porcupine who makes a ton of friends.\n" +
                "I would like to read it again...\n I should’ve read it more before I came here.";
            //(5) 사진앨범(발견 후)
            //Game_TypeWriterEffect.instance.EventText[4] = "앨범이야! 그것도 우리 집 앨범!\n 엄마의 사진을 볼 수 있을지 몰라!\n" +
            //    "... 손을 대면 사라져...\n 안의 사진은 볼 수 없는 걸까?...\n 어! 저절로 펼쳐지고 있어.\n" +
            //    "아, 나의 아기 때 모습이야..\n 엄마의 얼굴은 나오지 않았지만 날 안고 있는 저 팔은 엄마야.\n" +
            //    "이건 처음으로 유치원에서 상을 받았을 때야.\n뿌듯해서 상장을 들고 엄마에게 사진을 찍어달라 했었어.\n" +
            //    "엄마야! 앗 너무 빨리 페이지가 넘어가버렸어...\n" +
            //    "엄마의 모습을 보니..더욱 보고 싶어,\n 가슴이 답답하고 슬픈 기분이 들어...\n 많이 그리운가 봐.\n" +
            //    "엄마와 어떻게 이별을 했었는지 떠오르지 않아,\n 내가 여기 잘 있다는 걸 엄마가 알아야 하는데.\n" +
            //    "여긴 꿈의 공간인가봐...\n 나의 기억들로 가득하잖아.\n" +
            //    "... 뒤로 갈 수 록... 혼자찍은 사진이 많네,\n 엄마가 사진을 찍어주고 있겠지?\n" +
            //    "병원 침대는 왜 찍은거지? \n...이부분에 대한 기억은 없어. 분명 몇번이고 봤었는데...\n" +
            //    "..앨범이 끝났어. ";
            Game_TypeWriterEffect.instance.EventText[4] = "An album! My family album!\nMaybe there’s a picture of my mother!\n" +
                "... It vanishes when I go near it...\n Is there no way to see the pictures?...\n Ah! It’s opening by itself.\n" +
                "Aww, that’s me when I was a baby...\n You can’t see her face,\n but those arms carrying me are my mother’s.\n" +
                "This is when I won a prize at kindergarten for the first time.\nI was so proud of myself that I asked\n for a picture holding the trophy.\n" +
                "This is my mother!\n Huh, the page went by too fast...\n" +
                "After seeing her picture... I miss her more.\n My chest feels heavy and I’m getting depressed...\n I really do miss her.\n" +
                "I can’t even remember how we parted.\n I just hope she knows that I’m doing fine.\n" +
                "This appears to be a dream.\n It’s brimming with my memories.\n" +
                "... Further down the road...\n it’s mostly pictures of myself.\n I bet mother took those for me?\n" +
                "What’s that picture of a hospital bed for? \n... I can’t remember anything about this.\n After all this time...\n" +
                "... That’s the end of the album. ";
            //(6) 꿈에서 깨기 위해 달려라 (달리기 전)
            //Game_TypeWriterEffect.instance.EventText[5] = "... 역시 이곳은 꿈이야, 가도 가도 똑같은 길이 이어져있어...\n" +
            //    "무엇보다 아무리 걸어도 다리가 아프지 않아\n" +
            //    "이곳에서 나가고 싶지 않지만... 깨어나야 할 것 같아... 그런 예감이 들어.\n" +
            //    "벗어나려면 어떻게 해야 하지? 여기는 어떻게 들어온거지?\n" +
            //    "... 이때까지 위기를 벗어난 방법을 생각하면 답은 한 개야.\n" +
            //    "달려야겠어";
            Game_TypeWriterEffect.instance.EventText[5] = "... This is a dream.\n Everywhere I go the same paths are all connected...\n" +
                "Also my legs aren’t getting tired\n no matter how much I walk.\n" +
                "I don’t want to leave this place...\n But I need to wake up...\n Just a hunch.\n" +
                "What do I need to do to get out of here?\n How did I end up here anyway?\n" +
                "... There’s only one solution\n to get out of this predicament.\n" +
                "I have to run.";
            // (5) 바람개비(발견 후)
            //    Game_TypeWriterEffect.instance.EventText[6] = "바람개비야! \n 공원에서 발견하고 엄마한테 사달라고 떼를 썼었어. \n" +
            //"한동안 어딜가든 이 바람개비와 함께였지!\n" + "하하, 이름이 없는게 흠이구나! \n 바람개비에 이렇게 그리운 기분이 들줄 몰랐어. \n" +
            //"열심히 가지고 놀았는데 언제부턴가 사라져버렸어, \n 왜 그렇게 금방 잊어버렸을까 ? \n" +
            // "난 이 바람개비가 내 생각보다 훨씬 마음에 들었나봐. \n 이렇게 기억들이 나는걸 보면 말이야.\n" +
            //"몇몇의 어른은 별 것 아닌것에 추억하는데 시간을 쏟지 말라고 해. \n 그건 어떻게 구별하는 거야?";
            Game_TypeWriterEffect.instance.EventText[6] = "A pinwheel! \n I remember begging my mom\n to buy me one at the local park.\n" +
        "This pinwheel and I were inseparable for a while!\n" + "Haha, I forgot to name it!\n Didn’t know how much I’d miss a pinwheel.\n" +
        "I used to play with it for the longest time,\n but then one day it disappeared. \nWhy did I forget about it so soon?\n" +
         "I think I was much more attached to the pinwheel than I thought\n Judging by all the memories I have of it.\n" +
        "Some adults told me not to dwell on or cherish trivial matters.\n How do I tell them apart?";
        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.EventText = new string[8];
            //(0) 깨어남
            //Game_TypeWriterEffect.instance.EventText[0] = "/라디오 : 괜찮아? 꿈을 많이 꾼 것 같았어./\n" +
            //    "... 누구... 아! 룬! 나 깨어났어. 잠들어 버렸구나.\n" +
            //    "/라디오 : 응, 이제 포자들은 없어졌어.기운을 차리면 다시 걷자/\n" +
            //    "너무 그리운 꿈을 꾸었던 것 같아.어라 달의 조각이야.\n" +
            //    "/라디오 : 꿈에서 얻었나 봐, 달의 조각은 어디에도 있을 수 있거든!/ ";
            Game_TypeWriterEffect.instance.EventText[0] = "/ Radio: You ok? You were asleep for a long time./\n" +
                "... Who... Ah, Lune! I’m awake now.\n I must’ve fallen asleep.\n" +
                "/Radio: Yeah, but the spores are gone now.\n Let’s start walking again when you can./\n" +
                "I had such a nostalgic dream.\n Look, a moon shard.\n" +
                "/Radio: You must’ve gotten it from the dream.\n Moon shards can be anywhere!/ ";
            //(1) 갈림길(도달 전)
            //Game_TypeWriterEffect.instance.EventText[1] = "갈림길이야, 꿈속에서는 갈림길이 없었는데. 돌아온 게 실감 나.\n" +
            //    "이쪽 길인 것 같아.";
            Game_TypeWriterEffect.instance.EventText[1] = "Hey, it’s a crossroad.\n There weren’t any in my dream.\n Now I feel like I’ve returned to reality.\n" +
                "I think it’s this way.";
            //(2) 비상식량(획득 후)
            //Game_TypeWriterEffect.instance.EventText[2] = "통조림이네!, 사실 꿈을 꾸면서 지구에서 먹었던 아이스크림을 보았어.\n" +
            //    "/라디오 : 아이스크림이 뭐야?/\n" +
            //    "디저트라고 하는데 음..밥으로 먹지 않고 간식으로 먹어!\n 아마 이곳에선 먹을 수 없겠지?\n" +
            //    "/라디오 : 아마도, 통조림은 많이 봤지만\n 이외의 다른 생명체들이 먹을 수 있는 건 본 적 없는 것 같아./ ";
            Game_TypeWriterEffect.instance.EventText[2] = "Hey, some canned food!\n In my dreams I saw the ice cream\n that I used to have on Earth.\n" +
                "/Radio: What’s an ice cream?/\n" +
                "We call it a dessert.\n It’s sort of a snack!\n It’s not possible to eat it here though, right?\n" +
                "/Radio: Probably not, although I saw many canned food.\n I haven’t seen anything else that other lifeforms could eat./ ";
            //(3) 징검다리(도달 전)
            //Game_TypeWriterEffect.instance.EventText[3] = "징검다리야, 달가루를 사용해서 건너야겠어!\n" +
            //    "룬 가방에 있는 달가루를 꺼내줘!";
            Game_TypeWriterEffect.instance.EventText[3] = "Stepping stones, again. We should use stardust to cross!\n Lune, hand me some moondust from the bag, please!\n" +
                "룬 가방에 있는 달가루를 꺼내줘!";
            //(4) 대형 산소통 줍기(획득 후)
            //Game_TypeWriterEffect.instance.EventText[4] = "산소통이야! 그것도 큰 산소통. 지금 내게 가장 필요한 거야.\n" +
            //    "룬 내가 이 길을 걸으면서 가장 필요한 요소가 세 가지가 있는데 뭔지 알아?\n" +
            //    "/라디오 : 흠..너무 어려운데? 하나는 산소야, 다른 하나는... 통조림 ?/\n" +
            //    "맞아! 똑똑한걸, 그리고 남은 하나는 룬, 바로 너야!";
            Game_TypeWriterEffect.instance.EventText[4] = "An oxygen tank! A large one at that.\n I needed this so much.\n" +
                "Lune, guess what three essential thing\n I need for running this path?\n" +
                "/Radio: Hmm... That’s a hard question.\n One is oxygen, the other is... canned food?/\n" +
                "Bingo! You’re a clever one.\n The last one is you, Lune!";
            //((5) 하늘섬(전)
            //Game_TypeWriterEffect.instance.EventText[5] = "저기 ...하늘섬에 무슨 일이 일어나고 있는 것 같아.\n" +
            //    "이 소리는 뭐지? 이런 파편들이 떨어진다!!\n" +
            //    "/라디오 : 섬에 문제가 생겼나 봐./\n" +
            //    "응 그런 것 같아...\n 이 정도면 섬의 파편이 길에도 떨어질 것 같아...\n이곳을 벗어나야겠어.\n" +
            //    "파편에 맞을 뻔했어...! 조심해야 해!";
            Game_TypeWriterEffect.instance.EventText[5] = "Look...\n There’s something happening on sky island.\n" +
                "What’s this noise?\n Watch out, there’s falling debris!!\n" +
                "/Radio: Something must be wrong with the island./\n" +
                "Yeah, I think so, too...\n At this rate,\n the debris from the island will land on the path...\nLet’s get out of here.\n" +
                "I almost got hit by the debris...!\n Be careful!";
            //(5-1) 하늘섬(후) ---사용안함
            //Game_TypeWriterEffect.instance.EventText[6] = "응 그런 것 같아... 이 정도면 섬의 파편이 길에도 떨어질 것 같아... 이곳을 벗어나야겠어.\n" +
            //    "파편에 맞을 뻔했어...!조심해야 해!";
            Game_TypeWriterEffect.instance.EventText[6] = "Yeah, I think so, too...\nAt this rate,\n the debris from the island will land on the path...\n" +
                "Let’s get out of here.\n I almost got hit by the debris...! Be careful!";
            //(6) 산소통 구멍
            //Game_TypeWriterEffect.instance.EventText[7] = "앗! 산소통에 섬에서 떨어진 파편에 맞아서 구멍이 났어.\n" +
            //    "/라디오 : 괜찮아? 버틸 수 있겠어?/\n" +
            //    "산소를 채워도 채워도 금방 빠져나가... 산소가 부족할 것 같아.\n" +
            //    "최대한 빨리 달려야 해!";
            Game_TypeWriterEffect.instance.EventText[7] = "Oh no!\n My oxygen tank was damaged\n from the debris of the island.\n" +
                "/Radio: Will you be alright?\n Can you hang in there?/\n" +
                "No matter how many times\n I refill the oxygen, it keeps seeping out...\n I’m not going to have enough.\n" +
                "We need to start sprinting!";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //룬의 몸이 깜빡임(시작시)
            //Game_TypeWriterEffect.instance.EventText[0] = "... ! 룬 네 몸이 깜빡이고 있어!... 무슨 일이지..?\n" +
            //    "꽃이 더 필요한 거야 ? ..이럴 때만 대답을 안 하는구나.";
            Game_TypeWriterEffect.instance.EventText[0] = "...! Lune, your body is twinkling!...\n What’s happening...?\n" +
                "Do you need more flowers?...\n You’re awful quiet when I need an answer.";
            //(1) 비상식량)
            //Game_TypeWriterEffect.instance.EventText[1] = "길에서 만나서 반가운걸? 룬, 산소통, 비상식량! 마침 배고팠는데 잘 됐어.\n" +
            //    "/라디오 : 배고픔을 몰라서 네 기분을 다 이해할 수 없어서 아쉽다./ ";
            Game_TypeWriterEffect.instance.EventText[1] = "I’m glad I found you here.\n Lune, food! Perfect, I was starting to get hungry.\n" +
                "/Radio: I wish I could understand what it means to be hungry./ ";
            //(2) 갈림길
            //Game_TypeWriterEffect.instance.EventText[2] = "이제는 갈림길을 선택하는 게 많이 무섭지 않아.\n" +
            //    "여전히 불안함은 있지만... 벌써 달이 이렇게 가까워졌는걸.";
            Game_TypeWriterEffect.instance.EventText[2] = "Now I’m not afraid of making choices at crossroads.\n" +
                "Although, I still get nervous...\n The moon is so close now.";
            //(3) 파란빛 영혼 만나기(발견 후)
            //Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 저기, 누군가 있어/\n" +
            //    "... ! 저 영혼은 푸른빛을 띠고 있어. 망은.. 꽃을 구하는 중인가?\n" +
            //    "/라디오 : 아마도 별을 구하고 있을 거야.\n 파란빛의 영혼은 색을 잃은 별들을 거두어 영혼을 만들어. \n 그를 도와주면 좋은 일이 생길 거야./ ";
            Game_TypeWriterEffect.instance.EventText[3] = "/Radio: Look, there’s somebody over there./\n" +
                "...! That soul has a blue tinge to it.\n Is it looking for flowers?\n" +
                "/Radio: It’s probably looking for stars.\n Blue souls harvest colorless stars to create a soul.\n Something good might happen if we help it./ ";
            //((4) 잠자리망(만난 후)
            //Game_TypeWriterEffect.instance.EventText[4] = " 파란빛 영혼아, 내가 도와줄까?\n" +
            //    " /라디오 : 성수기였는데 네가 도와준다니 다행이야.\n 라디오 : 이 망으로 색을 잃어 떨어지는 별을 담아줘/ ";
            Game_TypeWriterEffect.instance.EventText[4] = "Hello blue soul, can I help you?\n" +
                " /Radio: Perfect timing,\n it just so happens to be peak season.\n Use this net to collect the falling stars./ ";
            //(5) 별가루 줍기(획득 후)
            //Game_TypeWriterEffect.instance.EventText[5] = "별을 모으려고 했더니 별가루가 떨어져 있네!\n" +
            //    "이 별가루는 남은 길에 유용하게 쓰일 거야, 룬도 그렇게 생각하지?\n " +
            //    "/라디오: ......치지직 - / ";
            Game_TypeWriterEffect.instance.EventText[5] = "I came to collect stars\n but there’s stardust everywhere!\n" +
                "The stardust will be helpful for the remaining trip.\n What do you think, Lune?\n " +
                "/Radio: ... (static)/ ";
            //(6) 별 모으기(모은 후)
            //Game_TypeWriterEffect.instance.EventText[6] = "우와, 바쁘게 별들을 잡다 보니 벌써 30개를 모두 모았어!\n" +
            //    "/라디오: 고마워, 네 덕분에 일이 훨씬 줄어줄었어./\n" +
            //    "네게 도움이 되어서 다행이야,\n 덕분에 이 길을 지루하지 않게 걸을 수 있었어.\n" +
            //    "/라디오: 흠, 혹시 달가루 좀 있어?/\n" +
            //    "필요하니 ? 방금 주운 달가루가 있을 거야\n" +
            //    "/라디오: 그럼 그 달가루를 네가 모은 별들에 뿌려봐,\n 특별한 일이 일어날 거야./\n" +
            //    "그럼...! 뿌려볼게!";
            Game_TypeWriterEffect.instance.EventText[6] = "Wow, I’ve collected all 30 stars!\n" +
                "/Radio: Thank you.\n You saved me from so much trouble./\n" +
                "I was glad to be of help.\n Thanks to you I was able to run without getting bored.\n" +
                "/Radio: Hmm, do you happen to have moondust?/\n" +
                "Do you need any? I found some recently.\n" +
                "/Radio: Try scattering the moondust onto the stars you’ve collected.\n Something special will happen./\n" +
                "Ok, then...! Here I go!";
            //((7) 별 모으기(달가루를 뿌린 후)
            //Game_TypeWriterEffect.instance.EventText[7] = "...! 별들이 빛나고있어 !  달의 조각이야!";
            Game_TypeWriterEffect.instance.EventText[7] = "...! The stars are shining! It’s a moon shard!";
            //((8) 달의 조각 획득
            //Game_TypeWriterEffect.instance.EventText[8] = "/라디오: 이 달의 조각은 네가 가지도록 해. 감사의 선물이야.\n  이제 얼마 남지 않았어. 힘내/ ";
            Game_TypeWriterEffect.instance.EventText[8] = "/Radio: Keep this moon shard, as a token of appreciation.\n You’re almost there. Stay strong./ ";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.EventText = new string[9];
            //(1) 비상식량(후)
            //Game_TypeWriterEffect.instance.EventText[0] = "달이 그려져 있는 식량이야. 식량 중에 제일 맛있는 것 같아!";
            Game_TypeWriterEffect.instance.EventText[0] = "An emergency ration with a moon picture. It’s the best one I’ve had so far!";
            //(2) 갈림길 (전)
            //Game_TypeWriterEffect.instance.EventText[1] = "이쪽으로 가면 되겠다.\n 표지판이 없었다면, 정말 어쩔 뻔했을까. 아찔해.\n" +
            //    "룬, 달에 거의 다 와가는 것 같아.\n " +
            //    "/라디오 : 너와 함께 여기까지 올 수 있어서 기뻐!/ \n " +
            //    "이 길의 끝에서 우리는 헤어져야 하는 거야?\n " +
            //    "/라디오 : 헤어짐의 끝에는 또 다른 만남이 있잖아. 너무 슬퍼하지 마./ ";
            Game_TypeWriterEffect.instance.EventText[1] = "This way it is.\n What would I have done without these signposts?\n Just imagining that makes me woozy.\n" +
                "Lune, I think we’re getting closer to the moon.\n " +
                "/Radio: I’m really happy that I made it all the way here with you!/ \n " +
                "Will we say goodbye at the end of this path?\n " +
                "/Radio: It will be the beginning of the end. Don’t be too sad./ ";
            //(3) 조각의 문 (전)
            //Game_TypeWriterEffect.instance.EventText[2] = "이건 처음 보는 문인데...? \n우주 휴게소 게이트와는 달라!\n" +
            //    "/라디오 : 가지고 있는 달의 조각을 끼워봐./\n" +
            //    "룬, 달의 조각 10개가 있어야 열리는 것 같아.\n그런데 난 한 조각이 부족해...\n" +
            //    "/라디오 : 치칙...치지이이익/\n" +
            //    "룬, 내 말 듣고 있어? 문을 열 수가 없어.";
            Game_TypeWriterEffect.instance.EventText[2] = "This is my first time seeing this door...\n It’s different from the space rest area gate!\n" +
                "/Radio: Try to put in the moon shards./\n" +
                "Lune, I think it will only open if I have all 10 moon shards.\n But I’m missing one shard...\n" +
                "/Radio: (static) .../ \n" +
                "Lune, can you hear me? I can’t open the gate.";

            //(3) 우주라디오 부서짐 (전)
            //Game_TypeWriterEffect.instance.EventText[3] = "/라디오 : 치..치지직....치지직 치지이이익 -/ \n" +
            //    "뭐야, 갑자기 라디오가 왜이러지?\n 룬의 목소리를 들을 수가 없어.\n" +
            //    "/라디오 : 치...치지지 - 이이익 -/\n" +
            //    "탁탁!! 이 중요한 시점에 고장 나버리면 어떡해...!!";
            Game_TypeWriterEffect.instance.EventText[3] = "/Radio: (static)... (static).../ \n" +
                "Huh, what’s wrong with the radio?\n I can’t hear Lune’s voice.\n" +
                "/Radio: (static) .../ \n" +
                "(hitting the radio) How can you break down at such a crucial moment...!!";

            //(4) 우주라디오 부서짐 (후)
            //Game_TypeWriterEffect.instance.EventText[4] = "제발!! 탁탁!!! 엇 ? !라디오를 떨어뜨렸어....!!! 콰지직\n" +
            //    "어.. ? 이건 ?! 달의 조각이야! 룬!! 보고 있어? \n" +
            //    "라디오가 고장 나서 너의 목소리를 들을 수 없어.";
            Game_TypeWriterEffect.instance.EventText[4] = " Please!! (hitting the radio)\n Oh no! I dropped the radio...!!!\n(crash)\n" +
                "What...? This is?! A moon shard!\n Lune! Were you watching over me?\n" +
                "The radio isn’t working so I can’t hear you.";
            //조각의 문 앞에서 
            //(4) 룬과 이별
            //"(룬이 핑그르르 내 주위를 돈다)"
            //Game_TypeWriterEffect.instance.EventText[5] = "룬, 나에게 작별 인사하는거야...? 흑.\n" +
            //    "나와 여기까지 함께해 줘서 고마워. 절대 잊지 못할 거야.";
            Game_TypeWriterEffect.instance.EventText[5] = "Lune, is this where we say goodbye...? (sob)\n" +
           "Thank you for joining me on my journey.\n I’ll never forget about you.";

            // "(룬이 제자리에서 핑그르르 돌며 사라진다.)";
            //(5) 조각 끼우기
            //Game_TypeWriterEffect.instance.EventText[6] = "조각함에서 버튼을 누르면 문이 열리나 봐...!!\n" +
            //    "아앗...! 눈부셔!";
            Game_TypeWriterEffect.instance.EventText[6] = "Pressing the button on the shard box opens the gate...!!\n" +
                "Argh...! Too bright!";

            // 명예의 전당에서 사용되는 나레이션
            //(5) 조각 끼우기
            //Game_TypeWriterEffect.instance.EventText[7] = "조각함에서 버튼을 누르면 문이 열리나 봐...!!\n" +
            //    "아앗...!눈부셔!";
            Game_TypeWriterEffect.instance.EventText[7] = "Pressing the button on the shard box opens the gate...!!\n" +
                "Argh...! Too bright!";
            //(6) 산소통 구멍 (구멍 난 후)
            //Game_TypeWriterEffect.instance.EventText[8] = "취이이익- 취이이이익-\n어라...? 이게 무슨 소리지 ?\n" +
            //    "산소통에 구멍이 난 것 같아...!! 산소가 빨리 닳기 시작했어!\n" +
            //    "산소가 부족해지기 전에 달려서 다음 휴게소에 도착해야겠어!";
            Game_TypeWriterEffect.instance.EventText[8] = "(hiss) (hiss) Hmm...? What’s this sound?\n" +
                "I think there’s a hole in the oxygen tank...!!\n Oxygen level is going down rapidly!\n" +
                "I should hurry up and make it to the next rest area before the oxygen runs out.";

            /*
             명예의 전당 대사 
             [21] Hall of Fame

            //(1) Arriving at wishing stone
            Is this the moon...? 
            I’m finally here! I think I can make a wish if I carve my name on the wishing stone.

            */
        }
    }
    //랜덤 나레이션
    public void EventRandomTextList(int wayPoint)
    {
        Game_TypeWriterEffect.instance.RandomText = new string[50];
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Where am I...";
            Game_TypeWriterEffect.instance.RandomText[1] = "What will be at the end of this road?";
            Game_TypeWriterEffect.instance.RandomText[2] = "What are those blinking lights?";
            Game_TypeWriterEffect.instance.RandomText[3] = "Wow, they’re pretty...";
            Game_TypeWriterEffect.instance.RandomText[4] = "…";
            Game_TypeWriterEffect.instance.RandomText[5] = "What happens when oxygen runs out?";
            Game_TypeWriterEffect.instance.RandomText[6] = "Is anyone here...";
            Game_TypeWriterEffect.instance.RandomText[7] = "Sigh... It’s a little cold...";
            Game_TypeWriterEffect.instance.RandomText[8] = "Oh, what’s this?";
            Game_TypeWriterEffect.instance.RandomText[9] = "I’m hungry.";
            Game_TypeWriterEffect.instance.RandomText[10] = "What time is it?";
            Game_TypeWriterEffect.instance.RandomText[11] = "What’s the date today?";
            Game_TypeWriterEffect.instance.RandomText[12] = "All I can do is walk.";
            Game_TypeWriterEffect.instance.RandomText[13] = "There are many strange things in the universe.";
            Game_TypeWriterEffect.instance.RandomText[14] = "How did I get here?";
            Game_TypeWriterEffect.instance.RandomText[15] = "... I don’t remember much.";
            Game_TypeWriterEffect.instance.RandomText[16] = "My memories are faint...";
            Game_TypeWriterEffect.instance.RandomText[17] = "I guess quiet is a word that describes outer space.";
            Game_TypeWriterEffect.instance.RandomText[18] = "There is no sound.";
            Game_TypeWriterEffect.instance.RandomText[19] = "I’m talking to myself more.";
            Game_TypeWriterEffect.instance.RandomText[20] = "I keep thinking about things on Earth.";
            Game_TypeWriterEffect.instance.RandomText[21] = "There's something I'll probably forget if I don't think about it.";
            Game_TypeWriterEffect.instance.RandomText[22] = "Can I ever see the rainbow I saw back then?";
            Game_TypeWriterEffect.instance.RandomText[23] = "I want some noodles.";
            Game_TypeWriterEffect.instance.RandomText[24] = "Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune...";
            Game_TypeWriterEffect.instance.RandomText[25] = "Could that be Mercury?";
            Game_TypeWriterEffect.instance.RandomText[26] = "That one’s called Venus in English.";
            Game_TypeWriterEffect.instance.RandomText[27] = "If it rained in space...";
            Game_TypeWriterEffect.instance.RandomText[28] = "It would be full of life.";
            Game_TypeWriterEffect.instance.RandomText[29] = "I can’t see it.";
            Game_TypeWriterEffect.instance.RandomText[30] = "Humming~♪";
            Game_TypeWriterEffect.instance.RandomText[31] = "Sometimes I miss the song my friend hummed.";
            Game_TypeWriterEffect.instance.RandomText[32] = "What happened when you were happy is remembered for a long time.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I think of things that seem to be everywhere.";
            Game_TypeWriterEffect.instance.RandomText[34] = "It is surprising that there are no pigeons in space.";
            Game_TypeWriterEffect.instance.RandomText[35] = "I'd like to plant street lamps and trees on both sides of the road.";
            Game_TypeWriterEffect.instance.RandomText[36] = "If you have a car, you can get through this road in no time.";
            Game_TypeWriterEffect.instance.RandomText[37] = "Usually I didn't know what I was thinking.";
            Game_TypeWriterEffect.instance.RandomText[38] = "Even in this short amount of time, I think a lot.";
            Game_TypeWriterEffect.instance.RandomText[39] = "This is how the Little Prince must have felt.";
            Game_TypeWriterEffect.instance.RandomText[40] = "Where is my rose?";
            Game_TypeWriterEffect.instance.RandomText[41] = "Will tomorrow be like today?";
            Game_TypeWriterEffect.instance.RandomText[42] = "The universe never seems to change.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I'm sure it's still changing.";
            Game_TypeWriterEffect.instance.RandomText[44] = "A lot of things must be happening on Earth today.";
            Game_TypeWriterEffect.instance.RandomText[45] = "If this is actually a dream... I should show off my vivid dream!";
            Game_TypeWriterEffect.instance.RandomText[46] = "It’s not a dream...";
            Game_TypeWriterEffect.instance.RandomText[47] = "I don't think this is the worst situation.";
            Game_TypeWriterEffect.instance.RandomText[48] = "It’s as if I’m traveling.";
            Game_TypeWriterEffect.instance.RandomText[49] = "This step marks the start of the journey.";

        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Is anyone there?";
            Game_TypeWriterEffect.instance.RandomText[1] = "....";
            Game_TypeWriterEffect.instance.RandomText[2] = "Strange things are floating around...";
            Game_TypeWriterEffect.instance.RandomText[3] = "...I’m sure they won't shoot a laser beam or something all of a sudden.";
            Game_TypeWriterEffect.instance.RandomText[4] = "Excuse me...";
            Game_TypeWriterEffect.instance.RandomText[5] = " I’m just a boy... Don’t shoot...";
            Game_TypeWriterEffect.instance.RandomText[6] = "I’m hungry... If there’s food...";
            Game_TypeWriterEffect.instance.RandomText[7] = "I miss the smell of savory bread from the bakery.";
            Game_TypeWriterEffect.instance.RandomText[8] = "I shouldn’t have talked to my friend like that then.";
            Game_TypeWriterEffect.instance.RandomText[9] = "I’m sorry...";
            Game_TypeWriterEffect.instance.RandomText[10] = "...The radio stopped playing.";
            Game_TypeWriterEffect.instance.RandomText[11] = "What if I don't meet anyone?";
            Game_TypeWriterEffect.instance.RandomText[12] = "Is there no end to this road?";
            Game_TypeWriterEffect.instance.RandomText[13] = "What if I just walk like this for the rest of my life?";
            Game_TypeWriterEffect.instance.RandomText[14] = "Somebody help me!";
            Game_TypeWriterEffect.instance.RandomText[15] = "It’s too cold here...";
            Game_TypeWriterEffect.instance.RandomText[16] = "I feel like nothing in outer space...";
            Game_TypeWriterEffect.instance.RandomText[17] = "I can’t talk about this fear to anyone...";
            Game_TypeWriterEffect.instance.RandomText[18] = "I have nobody...";
            Game_TypeWriterEffect.instance.RandomText[19] = "I wish I had a picture of me and my mom.";
            Game_TypeWriterEffect.instance.RandomText[20] = "... What was Mom like?";
            Game_TypeWriterEffect.instance.RandomText[21] = "I can barely remember.";
            Game_TypeWriterEffect.instance.RandomText[22] = "Perhaps I’ve been here forever.";
            Game_TypeWriterEffect.instance.RandomText[23] = "I misunderstood you and hurt you.";
            Game_TypeWriterEffect.instance.RandomText[24] = "... Will tomorrow come?";
            Game_TypeWriterEffect.instance.RandomText[25] = "No one tells me if this is the right path...";
            Game_TypeWriterEffect.instance.RandomText[26] = "If I’m lost in a maze...";
            Game_TypeWriterEffect.instance.RandomText[27] = "Radio, talk to me. Tell me the news of the Earth.";
            Game_TypeWriterEffect.instance.RandomText[28] = "I think it’s broken...";
            Game_TypeWriterEffect.instance.RandomText[29] = "It’s cold and I’m hungry...";
            Game_TypeWriterEffect.instance.RandomText[30] = "I think someone is following me...";
            Game_TypeWriterEffect.instance.RandomText[31] = "... Don’t follow me!";
            Game_TypeWriterEffect.instance.RandomText[32] = "I have to run away...";
            Game_TypeWriterEffect.instance.RandomText[33] = "I’m scared...";
            Game_TypeWriterEffect.instance.RandomText[34] = "I’m lonely... So lonely...";
            Game_TypeWriterEffect.instance.RandomText[35] = "I wish I had someone to talk to. Just one person.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I can’t do this alone...";
            Game_TypeWriterEffect.instance.RandomText[37] = "Could there be another radio?";
            Game_TypeWriterEffect.instance.RandomText[38] = "You must’ve been abandoned because you were defective!";
            Game_TypeWriterEffect.instance.RandomText[39] = "I’m tired... When will this end...";
            Game_TypeWriterEffect.instance.RandomText[40] = "Still... I have to go.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Quickly... More quickly...";
            Game_TypeWriterEffect.instance.RandomText[42] = "I want to take a hot shower.";
            Game_TypeWriterEffect.instance.RandomText[43] = " I want to lie down in bed.";
            Game_TypeWriterEffect.instance.RandomText[44] = "I’m tired... I want to sleep.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I want to go home...";
            Game_TypeWriterEffect.instance.RandomText[46] = "Someone please send me home!";
            Game_TypeWriterEffect.instance.RandomText[47] = "... I have to go.";
            Game_TypeWriterEffect.instance.RandomText[48] = "If I don't go, I'll have to stay here forever.";

        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            //필수 랜덤 나레이션
            Game_TypeWriterEffect.instance.RandomText[0] = "I’m glad the radio is working again.";
            Game_TypeWriterEffect.instance.RandomText[1] = "It was a strange place...";
            Game_TypeWriterEffect.instance.RandomText[2] = "Who would’ve thought that there were murals in outer space?";
            Game_TypeWriterEffect.instance.RandomText[3] = "If we let the Earth know, it’ll be a great discovery.";
            Game_TypeWriterEffect.instance.RandomText[4] = "This might be the only clue about going home.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I... believe in my choice.";
            Game_TypeWriterEffect.instance.RandomText[6] = "Sometimes I make choices that aren't right,";
            Game_TypeWriterEffect.instance.RandomText[7] = "but those choices brought me here.";
            Game_TypeWriterEffect.instance.RandomText[8] = "It’s impossible to have no regrets all the time.";
            Game_TypeWriterEffect.instance.RandomText[9] = "I wish I could do both without choosing.";
            Game_TypeWriterEffect.instance.RandomText[10] = "Radio! What’s the weather like today?";
            Game_TypeWriterEffect.instance.RandomText[11] = "Sometimes I’m startled because the radio seems to talk to me.";
            Game_TypeWriterEffect.instance.RandomText[12] = "Jeez! I almost fell down.";
            Game_TypeWriterEffect.instance.RandomText[13] = "They said it's important to be healthy, so let's walk carefully.";
            Game_TypeWriterEffect.instance.RandomText[14] = "Maybe I can meet someone here.";
            Game_TypeWriterEffect.instance.RandomText[15] = "This mural... It’s painted so nicely!";
            Game_TypeWriterEffect.instance.RandomText[16] = "What language is written here?";
            Game_TypeWriterEffect.instance.RandomText[17] = "I don't think it's the language of the Earth.";
            Game_TypeWriterEffect.instance.RandomText[18] = "It's a word that looks similar to \"hope\" in Korean.";
            Game_TypeWriterEffect.instance.RandomText[19] = "I think I have hope.";
            Game_TypeWriterEffect.instance.RandomText[20] = "I think I’m somewhat carefree now.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Let’s just be like this!";
            Game_TypeWriterEffect.instance.RandomText[22] = "Just a little more to go!";
            Game_TypeWriterEffect.instance.RandomText[23] = "When did the moon start to exist?";
            Game_TypeWriterEffect.instance.RandomText[24] = "It feels weird that I'm going to the moon.";
            Game_TypeWriterEffect.instance.RandomText[25] = "If I go by spaceship, it won’t take long!";
            Game_TypeWriterEffect.instance.RandomText[26] = "Why was I in a spaceship?";
            Game_TypeWriterEffect.instance.RandomText[27] = "I think Earth is the prettiest planet.";
            Game_TypeWriterEffect.instance.RandomText[28] = "This wall... I’m sure it’s not made of regular stones.";
            Game_TypeWriterEffect.instance.RandomText[29] = "It probably wasn't made by one person.";
            //갈림길 나레이션 
            Game_TypeWriterEffect.instance.RandomText[30] = "This road will be the way home.";
            Game_TypeWriterEffect.instance.RandomText[31] = "The previous place was so scary.";
            Game_TypeWriterEffect.instance.RandomText[32] = "I think it's fine here except that it’s quiet.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I wish I had a friend to talk to.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Maybe someone is listening to me!";
            Game_TypeWriterEffect.instance.RandomText[35] = "Aah! I’m listening.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I thought there were aliens in space.";
            Game_TypeWriterEffect.instance.RandomText[37] = "If you look at this wall, you might think there really are aliens.";
            Game_TypeWriterEffect.instance.RandomText[38] = "How should I talk to an alien if I meet one?";
            Game_TypeWriterEffect.instance.RandomText[39] = "Can aliens talk?";
            Game_TypeWriterEffect.instance.RandomText[40] = "In the movies... they communicated with their fingers...";
            Game_TypeWriterEffect.instance.RandomText[41] = "...Hmm";
            Game_TypeWriterEffect.instance.RandomText[42] = "My legs hurt.";
            Game_TypeWriterEffect.instance.RandomText[43] = "Shall I take a break?";
            Game_TypeWriterEffect.instance.RandomText[44] = "(Groaning)";
            Game_TypeWriterEffect.instance.RandomText[45] = "I had a dream last night... I forgot what it was about.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Why do I quickly forget the things I was thinking about?";
            Game_TypeWriterEffect.instance.RandomText[47] = "Some say forgetfulness is a blessing.";
            Game_TypeWriterEffect.instance.RandomText[48] = "I want to remember.";
            Game_TypeWriterEffect.instance.RandomText[49] = "I should have made a record of it like these murals!";

        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Let’s go!";
            Game_TypeWriterEffect.instance.RandomText[1] = "I might be the first person to walk in space.";
            Game_TypeWriterEffect.instance.RandomText[2] = "I looked for four-leaf clovers when I was young.";
            Game_TypeWriterEffect.instance.RandomText[3] = "I finally found a four-leaf clover.";
            Game_TypeWriterEffect.instance.RandomText[4] = "As always, I will overcome!";
            Game_TypeWriterEffect.instance.RandomText[5] = "I want to find the Little Prince's star.";
            Game_TypeWriterEffect.instance.RandomText[6] = "I must’ve taken for granted a lot more than I thought.";
            Game_TypeWriterEffect.instance.RandomText[7] = "There's nothing that should be taken for granted.";
            Game_TypeWriterEffect.instance.RandomText[8] = "I must’ve made a lot of choices.";
            Game_TypeWriterEffect.instance.RandomText[9] = "Sometimes it’s nice to not have any plans.";
            Game_TypeWriterEffect.instance.RandomText[10] = "I want to see my future.";
            Game_TypeWriterEffect.instance.RandomText[11] = "My future self has safely arrived at the destination.";
            Game_TypeWriterEffect.instance.RandomText[12] = "I can have a new start today.";
            Game_TypeWriterEffect.instance.RandomText[13] = "Will I be able to meet someone?";
            Game_TypeWriterEffect.instance.RandomText[14] = "Is anyone there?";
            Game_TypeWriterEffect.instance.RandomText[15] = "Space seems like the only place where common sense doesn't work.";
            Game_TypeWriterEffect.instance.RandomText[16] = "I used to compare the infinite to the universe, and I think it was a fitting metaphor.";
            Game_TypeWriterEffect.instance.RandomText[17] = "I know there can't always be only good things.";
            Game_TypeWriterEffect.instance.RandomText[18] = "I know the magic of making good things.";
            Game_TypeWriterEffect.instance.RandomText[19] = "Good things happen when you have a good heart.";
            Game_TypeWriterEffect.instance.RandomText[20] = "The flowers that did not bloom then must have bloomed now.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Who was I with on my last day on Earth?";
            Game_TypeWriterEffect.instance.RandomText[22] = "I want to find my memories.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Is it true that rabbits live on the moon?";
            Game_TypeWriterEffect.instance.RandomText[24] = "Hope is better than fear!";
            Game_TypeWriterEffect.instance.RandomText[25] = "Maybe the rabbits can jump because of stardust.";
            Game_TypeWriterEffect.instance.RandomText[26] = "It's not always the worst situation.";
            Game_TypeWriterEffect.instance.RandomText[27] = "You can rest when you want to.";
            Game_TypeWriterEffect.instance.RandomText[28] = "Because arriving quickly is not the only answer.";
            Game_TypeWriterEffect.instance.RandomText[29] = "I’m in space again.";
            Game_TypeWriterEffect.instance.RandomText[30] = "It's like I just saw a mural in a dream.";
            Game_TypeWriterEffect.instance.RandomText[31] = "But it wasn’t a dream!";
            Game_TypeWriterEffect.instance.RandomText[32] = "It’s aurora. I've never seen it on Earth!";
            Game_TypeWriterEffect.instance.RandomText[33] = " It’s pretty... It’s like the universe is rippling.";
            Game_TypeWriterEffect.instance.RandomText[34] = "If I go to Earth, I'll go see the aurora with someone.";
            Game_TypeWriterEffect.instance.RandomText[35] = "It’s pretty...";
            Game_TypeWriterEffect.instance.RandomText[36] = "I want to carry this aurora around in my pocket.";
            Game_TypeWriterEffect.instance.RandomText[37] = "I wish I could take it out every time I want to see it.";
            Game_TypeWriterEffect.instance.RandomText[38] = "I’m sure it’ll give comfort to those who need it.";
            Game_TypeWriterEffect.instance.RandomText[39] = "Like I was comforted by it.";
            Game_TypeWriterEffect.instance.RandomText[40] = "I hope everyone is happy.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Radio, are you looking at this aurora too?";
            Game_TypeWriterEffect.instance.RandomText[42] = "If so, please tell me the news of the Earth.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I used to draw the universe when I was young.";
            Game_TypeWriterEffect.instance.RandomText[44] = "10 years from now, people might be living in space.";
            Game_TypeWriterEffect.instance.RandomText[45] = "Hmm, the one who discovered this path was a dignified boy!\n Hmm, I'll have to think of another comment.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Does Mom know that I’m here?";
            Game_TypeWriterEffect.instance.RandomText[47] = "I’ll tell her everything when I return home!";
            Game_TypeWriterEffect.instance.RandomText[48] = "I don't know how many days have passed\n because the days haven't changed.";
            Game_TypeWriterEffect.instance.RandomText[49] = "Shining bright, shining beautifully.";


        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "I made a bucket list for when I go back to Earth.";
            Game_TypeWriterEffect.instance.RandomText[1] = "Go to a buffet and eat as much delicious food as possible.";
            Game_TypeWriterEffect.instance.RandomText[2] = "Watch all the recently released movies.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Say to Mom “I love you.”";
            Game_TypeWriterEffect.instance.RandomText[4] = "Become the author of \"How to Survive on the Moon.\"";
            Game_TypeWriterEffect.instance.RandomText[5] = "When I think like this, I forget that I’m lonely.";
            Game_TypeWriterEffect.instance.RandomText[6] = "It’s good to see someone.";
            Game_TypeWriterEffect.instance.RandomText[7] = "I thought I wouldn’t see anyone here.";
            Game_TypeWriterEffect.instance.RandomText[8] = "I have a feeling that more and more good things are about to happen.";
            Game_TypeWriterEffect.instance.RandomText[9] = "I think there is an end to this road too.";
            Game_TypeWriterEffect.instance.RandomText[10] = "I could have a fresh start somewhere else.";
            Game_TypeWriterEffect.instance.RandomText[11] = "In fact, in a place I never thought I would meet anyone...\n I was scared because I heard a noise!";
            Game_TypeWriterEffect.instance.RandomText[12] = "It’s full of asteroids.";
            Game_TypeWriterEffect.instance.RandomText[13] = "Keep reminiscing about your memories with your mom.";
            Game_TypeWriterEffect.instance.RandomText[14] = "The memories will take to you your mom.";
            Game_TypeWriterEffect.instance.RandomText[15] = "Losing my mother means that she also lost me.";
            Game_TypeWriterEffect.instance.RandomText[16] = "She’s probably sad...";
            Game_TypeWriterEffect.instance.RandomText[17] = "I thought that if anyone lost their mother, I would take them to her.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Mmm... Let’s think.";
            Game_TypeWriterEffect.instance.RandomText[19] = "How did he lose his mom?";
            Game_TypeWriterEffect.instance.RandomText[20] = "I hope there aren’t any baobab trees on that asteroid.";
            Game_TypeWriterEffect.instance.RandomText[21] = "I... Actually I don’t remember my mom clearly.";
            Game_TypeWriterEffect.instance.RandomText[22] = "Because I had an accident when I arrived here...";
            Game_TypeWriterEffect.instance.RandomText[23] = "I think I lost my memories because of the accident.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I know that my mom was a very warm person!";
            Game_TypeWriterEffect.instance.RandomText[25] = "Without good memories, I wouldn't have come this far.";
            Game_TypeWriterEffect.instance.RandomText[26] = "...I have a lot of thoughts as I walk down this road.";
            Game_TypeWriterEffect.instance.RandomText[27] = "I've always thought that it would be nice to have a baby star to talk to.";
            Game_TypeWriterEffect.instance.RandomText[28] = "Maybe my wish brought Michu here.";
            Game_TypeWriterEffect.instance.RandomText[29] = "What will I say first when I find my mom?";
            Game_TypeWriterEffect.instance.RandomText[30] = "I have a lot of things I want to say,\n so I thought about it for a long time.";
            Game_TypeWriterEffect.instance.RandomText[31] = "I have a lot of things I want to say...";
            Game_TypeWriterEffect.instance.RandomText[32] = "When I meet someone I miss, I'll say I love you first.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I should think about it more.";
            Game_TypeWriterEffect.instance.RandomText[34] = "What are other people thinking right now?";
            Game_TypeWriterEffect.instance.RandomText[35] = "Radio sometimes tells stories about the Earth.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I can’t hear it clearly because it's broken.";
            Game_TypeWriterEffect.instance.RandomText[37] = "but sometimes the radio seems to talk to me.";
            Game_TypeWriterEffect.instance.RandomText[38] = "I guess I've been alone longer than I thought.";
            Game_TypeWriterEffect.instance.RandomText[39] = "I'll feel empty if I part with the life I find when I'm lonely.";
            Game_TypeWriterEffect.instance.RandomText[40] = "I’ll be able to move on with the memories of us.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Join me on the adventure of finding my mom.";
            Game_TypeWriterEffect.instance.RandomText[42] = "They say you shouldn’t hold back on saying thank you.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I feel warm in my heart. I know the name of this feeling.";
            Game_TypeWriterEffect.instance.RandomText[44] = "It’s called joy or happiness.";
            Game_TypeWriterEffect.instance.RandomText[45] = "It must’ve been Mom who told me about this feeling.";
            Game_TypeWriterEffect.instance.RandomText[46] = "People in my memories... They must’ve taught me a lot, whether I know it or not.";
            Game_TypeWriterEffect.instance.RandomText[47] = "I think I hear whispers.";
            Game_TypeWriterEffect.instance.RandomText[48] = "There will be a sunset on that asteroid too.";
            Game_TypeWriterEffect.instance.RandomText[49] = "Even though we have lived different lives,\n it's amazing how we become more like each other when we're together.";

        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "This is a strange place.";
            Game_TypeWriterEffect.instance.RandomText[1] = "I feel like I'm in a black hole.";
            Game_TypeWriterEffect.instance.RandomText[2] = "It’s too dark.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Is something wrong with my eyes?";
            Game_TypeWriterEffect.instance.RandomText[4] = "I can’t breathe because it’s too dark.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I think my palms are sweating.";
            Game_TypeWriterEffect.instance.RandomText[6] = "If I fall off this path, I think I'll be one of the countless darknesses.";
            Game_TypeWriterEffect.instance.RandomText[7] = "I’m shaking because I’m scared.";
            Game_TypeWriterEffect.instance.RandomText[8] = "Will I be able to get out of the darkness?";
            Game_TypeWriterEffect.instance.RandomText[9] = "Everything looks black and white.";
            Game_TypeWriterEffect.instance.RandomText[10] = "Am I dead?";
            Game_TypeWriterEffect.instance.RandomText[11] = "No... This must be part of space...";
            Game_TypeWriterEffect.instance.RandomText[12] = "What if space is a realm after death?";
            Game_TypeWriterEffect.instance.RandomText[13] = "It’s nonsense.";
            Game_TypeWriterEffect.instance.RandomText[14] = "I hope it’s over soon.";
            Game_TypeWriterEffect.instance.RandomText[15] = "I should be careful at a time like this.";
            Game_TypeWriterEffect.instance.RandomText[16] = "What happened here?";
            Game_TypeWriterEffect.instance.RandomText[17] = "It’s like someone cursed it.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Is it a space of ordeal given to me?";
            Game_TypeWriterEffect.instance.RandomText[19] = "Gasp, that was dangerous.";
            Game_TypeWriterEffect.instance.RandomText[20] = "Be careful... Careful...";
            Game_TypeWriterEffect.instance.RandomText[21] = "... Did Michu go safely with Mom?";
            Game_TypeWriterEffect.instance.RandomText[22] = "I miss Michu more because I’m in a place like this.";
            Game_TypeWriterEffect.instance.RandomText[23] = "If I was with someone, I wouldn’t be this scared.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I guess I’m scared because I’m alone.";
            Game_TypeWriterEffect.instance.RandomText[25] = "I don’t want to get used to a situation like this.";
            Game_TypeWriterEffect.instance.RandomText[26] = "When I become used to it,\nit becomes a habit that’s hard to get rid of.";
            Game_TypeWriterEffect.instance.RandomText[27] = "I want to be used to being together.";
            Game_TypeWriterEffect.instance.RandomText[28] = "I guess the best thing right now is just getting used to it.";
            Game_TypeWriterEffect.instance.RandomText[29] = "I recently saw a comment like this. It's the worst situation when the word \"best\" comes out.";
            Game_TypeWriterEffect.instance.RandomText[30] = "This isn’t the worst situation, is it?";
            Game_TypeWriterEffect.instance.RandomText[31] = "I just felt a chill.";
            Game_TypeWriterEffect.instance.RandomText[32] = "I want to sleep in a warm bed.";
            Game_TypeWriterEffect.instance.RandomText[33] = "It’s still dark.";
            Game_TypeWriterEffect.instance.RandomText[34] = "When can I get out of here?";
            Game_TypeWriterEffect.instance.RandomText[35] = "I want to see the aurora again.";
            Game_TypeWriterEffect.instance.RandomText[36] = "Is anyone there?";
            Game_TypeWriterEffect.instance.RandomText[37] = "It's especially cold in places like this.";
            Game_TypeWriterEffect.instance.RandomText[38] = "Is this the right way?";
            Game_TypeWriterEffect.instance.RandomText[39] = "Am I going in the opposite direction?";
            Game_TypeWriterEffect.instance.RandomText[40] = "I started to have doubts a while ago.";
            Game_TypeWriterEffect.instance.RandomText[41] = "This is a crossroad I faced before...\nDid I make a wrong choice?";
            Game_TypeWriterEffect.instance.RandomText[42] = "I am not confident about the crossroads\nI will meet in the future.";
            Game_TypeWriterEffect.instance.RandomText[43] = "Am I making the right choice?";
            Game_TypeWriterEffect.instance.RandomText[44] = "It's silly to doubt my choice.";
            Game_TypeWriterEffect.instance.RandomText[45] = "You can rectify what you have already chosen, but you can't undo it.";
            Game_TypeWriterEffect.instance.RandomText[46] = "It’s too tiring.";
            Game_TypeWriterEffect.instance.RandomText[47] = "I won’t doubt myself! This space too!";
            Game_TypeWriterEffect.instance.RandomText[48] = "If you go the wrong way, you can go back.";
            Game_TypeWriterEffect.instance.RandomText[49] = "Let’s go. I can’t be here forever.";

        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Now that I'm out of that place, there's a space like this!";
            Game_TypeWriterEffect.instance.RandomText[1] = "Maybe I've been through scary places to get here.";
            Game_TypeWriterEffect.instance.RandomText[2] = "It’s pretty... It’s like the universe is rippling.";
            Game_TypeWriterEffect.instance.RandomText[3] = "I didn't know whales could live in space...";
            Game_TypeWriterEffect.instance.RandomText[4] = "Is it correct to call it a whale?";
            Game_TypeWriterEffect.instance.RandomText[5] = "I’m not in space, I might be in the sea.";
            Game_TypeWriterEffect.instance.RandomText[6] = "When did I get into the sea? Haha!";
            Game_TypeWriterEffect.instance.RandomText[7] = "Michu should’ve seen this!";
            Game_TypeWriterEffect.instance.RandomText[8] = "Michu may have already seen this.";
            Game_TypeWriterEffect.instance.RandomText[9] = "I want to be a whale...";
            Game_TypeWriterEffect.instance.RandomText[10] = "I hope this path continues.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Their movements give me comfort.";
            Game_TypeWriterEffect.instance.RandomText[12] = "I think the whales are saying, \"Good job coming all the way here.\"";
            Game_TypeWriterEffect.instance.RandomText[13] = "Do they eat stars?";
            Game_TypeWriterEffect.instance.RandomText[14] = "I’m hungry.";
            Game_TypeWriterEffect.instance.RandomText[15] = "It's nice to see the sparkle.";
            Game_TypeWriterEffect.instance.RandomText[16] = "Crows have a habit of collecting sparkling objects, so crows will like this place!";
            Game_TypeWriterEffect.instance.RandomText[17] = "Seeing whales makes me feel cool.";
            Game_TypeWriterEffect.instance.RandomText[18] = "People will be busy when they come here!\n In order not to miss any of this scenery!";
            Game_TypeWriterEffect.instance.RandomText[19] = "Whale! Hi!";
            Game_TypeWriterEffect.instance.RandomText[20] = "Oh! I think that whale understood me.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Haha! I think I made eye contact with a whale.\nI guess it was a coincidence!";
            Game_TypeWriterEffect.instance.RandomText[22] = "If every day is full of these events, everyone will be happy.";
            Game_TypeWriterEffect.instance.RandomText[23] = "What are they thinking about while swimming in space?";
            Game_TypeWriterEffect.instance.RandomText[24] = "If it weren't for the oxygen,\nI would have stood here and looked around...";
            Game_TypeWriterEffect.instance.RandomText[25] = "Maybe it’s a dream. All of it!";
            Game_TypeWriterEffect.instance.RandomText[26] = "Can I ride a whale?";
            Game_TypeWriterEffect.instance.RandomText[27] = "It’ll be as fast as a spaceship.";
            Game_TypeWriterEffect.instance.RandomText[28] = "Whale! Can you give me a ride?";
            Game_TypeWriterEffect.instance.RandomText[29] = "Hmm. Whales must be busy, or maybe they didn’t see me.";
            Game_TypeWriterEffect.instance.RandomText[30] = "Why can't you see small things well when you’re big?";
            Game_TypeWriterEffect.instance.RandomText[31] = "Those whales won't be visible at the end of the road.";
            Game_TypeWriterEffect.instance.RandomText[32] = "(Groaning)";
            Game_TypeWriterEffect.instance.RandomText[33] = "Today I dreamed of swimming in the sea with whales.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Do whales dream? What kind of dream would they have?";
            Game_TypeWriterEffect.instance.RandomText[35] = "They look happy swimming freely.";
            Game_TypeWriterEffect.instance.RandomText[36] = "A whale that swims freely off the road!";
            Game_TypeWriterEffect.instance.RandomText[37] = "Do they have a destination?";
            Game_TypeWriterEffect.instance.RandomText[38] = "It would have been more fun if I could go wherever I want.";
            Game_TypeWriterEffect.instance.RandomText[39] = "I should be light-hearted.";
            Game_TypeWriterEffect.instance.RandomText[40] = "If I think about other things, I’ll arrive soon.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Gasp! Stardust poured out of that whale's back.";
            Game_TypeWriterEffect.instance.RandomText[42] = "A whale can breathe in 2,000 tons of air in 2 seconds.";
            Game_TypeWriterEffect.instance.RandomText[43] = "Ah, This is the space radio.";
            Game_TypeWriterEffect.instance.RandomText[44] = "Today's space is clear and endless, and a group of shining whales will pass by.";
            Game_TypeWriterEffect.instance.RandomText[45] = "If there are whales in space, there might be cats and dogs too!";
            Game_TypeWriterEffect.instance.RandomText[46] = "Maybe all living things on Earth are aliens from space!";
            Game_TypeWriterEffect.instance.RandomText[47] = "I want to be big like a whale.";
            Game_TypeWriterEffect.instance.RandomText[48] = "Then, they’ll be able to see me from Earth!";
            Game_TypeWriterEffect.instance.RandomText[49] = "People on Earth! I’m here!";
        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "What are these...\n These spores are like dandelion seeds...";
            Game_TypeWriterEffect.instance.RandomText[1] = "I feel like I'm getting short of breath...";
            Game_TypeWriterEffect.instance.RandomText[2] = "I think the shortness of breath is due to the spores here.";
            Game_TypeWriterEffect.instance.RandomText[3] = "They look pretty.";
            Game_TypeWriterEffect.instance.RandomText[4] = "I need to get out of here quickly.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I have to run. Let’s run.";
            Game_TypeWriterEffect.instance.RandomText[6] = "I’ll be fine when I get out of here.";
            Game_TypeWriterEffect.instance.RandomText[7] = "Where are these spores coming from?";
            Game_TypeWriterEffect.instance.RandomText[8] = "There are too many... It’s too dangerous.";
            Game_TypeWriterEffect.instance.RandomText[9] = "Did someone spray them on purpose?";
            Game_TypeWriterEffect.instance.RandomText[10] = "It’d be nice to know the source of the spores!";
            Game_TypeWriterEffect.instance.RandomText[11] = "I feel the light rippling under my feet.";
            Game_TypeWriterEffect.instance.RandomText[12] = "Isn't there anyone who made notes about this situation... like by making a mural?";
            Game_TypeWriterEffect.instance.RandomText[13] = "(Coughing)";
            Game_TypeWriterEffect.instance.RandomText[14] = "Spores won't come into the spacesuit.";
            Game_TypeWriterEffect.instance.RandomText[15] = "Spores didn't come into the spacesuit, but it's so uncomfortable.";
            Game_TypeWriterEffect.instance.RandomText[16] = "I need a new oxygen tank.";
            Game_TypeWriterEffect.instance.RandomText[17] = "....(Coughing)";
            Game_TypeWriterEffect.instance.RandomText[18] = "Are there dandelions around?";
            Game_TypeWriterEffect.instance.RandomText[19] = "If you step on the spores, the light will come up.";
            Game_TypeWriterEffect.instance.RandomText[20] = "It's hard to breathe.";
            Game_TypeWriterEffect.instance.RandomText[21] = "I wish I were a creature that didn't breathe!";
            Game_TypeWriterEffect.instance.RandomText[22] = "I always got 1st place in breath holding contests...";
            Game_TypeWriterEffect.instance.RandomText[23] = "I guess life sometimes enjoys giving me trials like this.";
            Game_TypeWriterEffect.instance.RandomText[24] = "How would you feel if every day was full of trials?";
            Game_TypeWriterEffect.instance.RandomText[25] = "There are quite a few good days. I like to enjoy such days.";
            Game_TypeWriterEffect.instance.RandomText[26] = "I think I saw these spores\nwhen I was playing in the mountains with my friend.";
            Game_TypeWriterEffect.instance.RandomText[27] = "Those white crystals won't fall on the road, right?";
            Game_TypeWriterEffect.instance.RandomText[28] = "Is it far to get out of here?";
            Game_TypeWriterEffect.instance.RandomText[29] = "I don't think I walked much, but I'm already tired.";
            Game_TypeWriterEffect.instance.RandomText[30] = "That white crystal looks sharp.";
            Game_TypeWriterEffect.instance.RandomText[31] = "... How many days have I walked?";
            Game_TypeWriterEffect.instance.RandomText[32] = "I want someone to find me.";
            Game_TypeWriterEffect.instance.RandomText[33] = "Sometimes I just want to stand here and do nothing.";
            Game_TypeWriterEffect.instance.RandomText[34] = "The fact that you don't want to do anything is proof that you're tired.";
            Game_TypeWriterEffect.instance.RandomText[35] = "I don't want gloomy days to become a habit.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I have to walk again, I have to get out of here now.";
            Game_TypeWriterEffect.instance.RandomText[37] = "I think my mom told me that I can do anything.\nI can be anything.";
            Game_TypeWriterEffect.instance.RandomText[38] = "The lights seen from afar are pretty,\nbut sometimes they are threatening when they come close.";
            Game_TypeWriterEffect.instance.RandomText[39] = "Now I'm on my way to do what I can.";
            Game_TypeWriterEffect.instance.RandomText[40] = "I think I know why adults often compare life to a road.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Which one is important? The road... or me as I walk that road?";
            Game_TypeWriterEffect.instance.RandomText[42] = "Will oxygen disappear quickly if I yawn?";
            Game_TypeWriterEffect.instance.RandomText[43] = "I can't choose the road right now, so I’ll care for myself.";
            Game_TypeWriterEffect.instance.RandomText[44] = "I have to get a hold of myself. I can do it.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I have to go where there are no spores.";
            Game_TypeWriterEffect.instance.RandomText[46] = "I've been walking this path for a long time,\nand I've found good things.";
            Game_TypeWriterEffect.instance.RandomText[47] = "Others, like me,\nwill find the good things about this path.";
            Game_TypeWriterEffect.instance.RandomText[48] = "It takes a long time to understand it.";
            Game_TypeWriterEffect.instance.RandomText[49] = "What is this icicle-like light?";
        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "It's a bit easier to breathe now.";
            Game_TypeWriterEffect.instance.RandomText[1] = "Mmm, around me, this is... a flower.";
            Game_TypeWriterEffect.instance.RandomText[2] = "Why do I find flowers pretty?\nBecause they’re small and precious?";
            Game_TypeWriterEffect.instance.RandomText[3] = "Who planted the flowers here first?";
            Game_TypeWriterEffect.instance.RandomText[4] = "The flowers may have come here on their own,\nmaking a choice at a crossroads like me.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I miss the flowers on Earth.";
            Game_TypeWriterEffect.instance.RandomText[6] = "How do plants feel...\nchoosing a place to grow for the rest of their lives?";
            Game_TypeWriterEffect.instance.RandomText[7] = "A self-help book said you should buy flowers for yourself.";
            Game_TypeWriterEffect.instance.RandomText[8] = "If flowers are all around, will the value of flowers decrease?";
            Game_TypeWriterEffect.instance.RandomText[9] = "I smell a scent.";
            Game_TypeWriterEffect.instance.RandomText[10] = "It smells warm and comfy and it reminds me of stars and the moon.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Are those stars and flowers from space?";
            Game_TypeWriterEffect.instance.RandomText[12] = "I have a feeling I'm going to be lucky today.";
            Game_TypeWriterEffect.instance.RandomText[13] = "I think the more flowers there are, the more valuable the flowers are.";
            Game_TypeWriterEffect.instance.RandomText[14] = "When I return to Earth, I will buy flowers for me.";
            Game_TypeWriterEffect.instance.RandomText[15] = "A flower garden looks more beautiful than the flowers blooming alone.";
            Game_TypeWriterEffect.instance.RandomText[16] = "I’ve been to a botanical garden full of flowers.\nThe hot dog there was really good!";
            Game_TypeWriterEffect.instance.RandomText[17] = "I’m a lucky boy.\n Who else would have experienced this?";
            Game_TypeWriterEffect.instance.RandomText[18] = "The flower of the Little Prince used to be lonely.";
            Game_TypeWriterEffect.instance.RandomText[19] = "Flowers, you’re together,\nso I guess you aren’t lonely.";
            Game_TypeWriterEffect.instance.RandomText[20] = "You’re all beautiful, so don’t fight.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Flowers, have you seen anyone passing by?";
            Game_TypeWriterEffect.instance.RandomText[22] = "What does it feel like to be here all the time?";
            Game_TypeWriterEffect.instance.RandomText[23] = "If I pass this place on my way back, the flowers will greet me.";
            Game_TypeWriterEffect.instance.RandomText[24] = "Flowers don’t do anything, but they calm me down.";
            Game_TypeWriterEffect.instance.RandomText[25] = "I also want to be a person who can make those around me feel at ease.";
            Game_TypeWriterEffect.instance.RandomText[26] = "You’re lucky flowers.\nYou’re flowers of hope to me.";
            Game_TypeWriterEffect.instance.RandomText[27] = "I know why flowers are good,\nbecause people give flowers as gifts when there’s good news.\nFlowers are in the center of happiness.";
            Game_TypeWriterEffect.instance.RandomText[28] = "What do you symbolize?\nIf you don’t symbolize anything, I’ll make something up.";
            Game_TypeWriterEffect.instance.RandomText[29] = "I helped the star who just lost its mom.\nI felt warm and happy then.";
            Game_TypeWriterEffect.instance.RandomText[30] = "If you're alone,\nyou won't be able to feel the satisfaction of helping anyone.";
            Game_TypeWriterEffect.instance.RandomText[31] = "... I’ll enjoy this scenery for a moment.";
            Game_TypeWriterEffect.instance.RandomText[32] = "I think I can endure and walk only when I get used to positive emotions.\nNot only on this road, but also on the path of my life.";
            Game_TypeWriterEffect.instance.RandomText[33] = "There was a story behind the mythical flowers. Do you have one, too?";
            Game_TypeWriterEffect.instance.RandomText[34] = "I won't be able to see you again at the end of this road.";
            Game_TypeWriterEffect.instance.RandomText[35] = "Even if I go, wonderful people will come to help you.";
            Game_TypeWriterEffect.instance.RandomText[36] = "... (Humming)";
            Game_TypeWriterEffect.instance.RandomText[37] = "When a song plays on the radio, sometimes it's fun to guess the lyrics.";
            Game_TypeWriterEffect.instance.RandomText[38] = "Flowers sing too.";
            Game_TypeWriterEffect.instance.RandomText[39] = "I think each petal would have a syllable.";
            Game_TypeWriterEffect.instance.RandomText[40] = "I'm sure your voices are all different.";
            Game_TypeWriterEffect.instance.RandomText[41] = "I hope I can hear everyone's voice next time.";
            Game_TypeWriterEffect.instance.RandomText[42] = "I’ve been walking like this for a long time.";
            Game_TypeWriterEffect.instance.RandomText[43] = "There is an endless road ahead and I may have to keep walking.";
            Game_TypeWriterEffect.instance.RandomText[44] = "I think it's relatively warm here.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I'm glad I don't have a pollen allergy!";
            Game_TypeWriterEffect.instance.RandomText[46] = "Actually,\nwalking in space like this is not much different from living on Earth.\nIn terms of emotions.";
            Game_TypeWriterEffect.instance.RandomText[47] = "My friend is allergic to pollen so he always carries a tissue in the spring.";
            Game_TypeWriterEffect.instance.RandomText[48] = "Is there a botanical garden in space?\nThere were various flowers in the botanical garden,\nbut you were not there.";
            Game_TypeWriterEffect.instance.RandomText[49] = "You’re my new acquaintances.";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "....? That’s... Fireworks...";
            Game_TypeWriterEffect.instance.RandomText[1] = "I didn't expect to see fireworks in space!";
            Game_TypeWriterEffect.instance.RandomText[2] = "Who is setting off firecrackers?";
            Game_TypeWriterEffect.instance.RandomText[3] = "The meteorites hit and sparkled!";
            Game_TypeWriterEffect.instance.RandomText[4] = "From a distance,\nit looks like a light that welcomes me.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I think my mom and I set off firecrackers at the beach a long time ago.";
            Game_TypeWriterEffect.instance.RandomText[6] = "I used to crouch down and watch the sparkler for a while.";
            Game_TypeWriterEffect.instance.RandomText[7] = "The debris of the meteorite won't fall here.";
            Game_TypeWriterEffect.instance.RandomText[8] = "Although they’re pretty,\nI think I'm worried about the inevitably\nfalling fragments of the meteorite.";
            Game_TypeWriterEffect.instance.RandomText[9] = "Where did these meteorites come from?";
            Game_TypeWriterEffect.instance.RandomText[10] = "Fireworks are just for watching, but why did I like it so much?";
            Game_TypeWriterEffect.instance.RandomText[11] = " I think everyone who was enjoying fireworks looked happy.";
            Game_TypeWriterEffect.instance.RandomText[12] = "I think this kind of spark expresses excitement.";
            Game_TypeWriterEffect.instance.RandomText[13] = "I feel dizzy...";
            Game_TypeWriterEffect.instance.RandomText[14] = "All the games I know are fun, and fireworks are exactly that.";
            Game_TypeWriterEffect.instance.RandomText[15] = "When you see these floating meteorites, you realize that space is free of gravity.";
            Game_TypeWriterEffect.instance.RandomText[16] = "The meteorite that stayed still looked like a stone,\nbut it's bigger than I thought.";
            Game_TypeWriterEffect.instance.RandomText[17] = "The universe is full of things that I can't imagine.";
            Game_TypeWriterEffect.instance.RandomText[18] = "The flame looks warm.";
            Game_TypeWriterEffect.instance.RandomText[19] = "If I could float this little light in a black and white space,\nI wouldn't feel so cold.";
            Game_TypeWriterEffect.instance.RandomText[20] = "How are Earth's firecrackers made?";
            Game_TypeWriterEffect.instance.RandomText[21] = "The teacher said, “Mistakes can be corrected, so don't be afraid.”";
            Game_TypeWriterEffect.instance.RandomText[22] = "In the East,\nfireworks were believed to have the power to drive away evil spirits.";
            Game_TypeWriterEffect.instance.RandomText[23] = "How long is this road?";
            Game_TypeWriterEffect.instance.RandomText[24] = "It's a pity that the light disappears so quickly.";
            Game_TypeWriterEffect.instance.RandomText[25] = "... Wait, it looks like there are two flames.";
            Game_TypeWriterEffect.instance.RandomText[26] = "I think I feel hot.";
            Game_TypeWriterEffect.instance.RandomText[27] = "..... I have to admit it.\nI think I'm sick right now.";
            Game_TypeWriterEffect.instance.RandomText[28] = "I want to know how far I've come.";
            Game_TypeWriterEffect.instance.RandomText[29] = "If you're sick, no matter how hard\nand strong you are, you'll collapse.";
            Game_TypeWriterEffect.instance.RandomText[30] = "The reason why I want to be pampered\ntoday is not because I'm weak, but because I'm sick.";
            Game_TypeWriterEffect.instance.RandomText[31] = "They say it's sad to be alone when you're sick,\nmaybe it's because you want to depend on someone,\nbut you don’t have anyone.";
            Game_TypeWriterEffect.instance.RandomText[32] = "When I'm sick, I want to be pampered for some reason,\nand I want people to worry about me.";
            Game_TypeWriterEffect.instance.RandomText[33] = "If we go to the moon, will there be people living on the moon?";
            Game_TypeWriterEffect.instance.RandomText[34] = "Even if... I can't get to the moon, I don't think it's such a bad trip.";
            Game_TypeWriterEffect.instance.RandomText[35] = "I miss Mom every day, but I especially miss her today.";
            Game_TypeWriterEffect.instance.RandomText[36] = "What is that blinking light over there?";
            Game_TypeWriterEffect.instance.RandomText[37] = "It looks like a black hole...";
            Game_TypeWriterEffect.instance.RandomText[38] = "It's hard to breathe... Am I just feeling that way?";
            Game_TypeWriterEffect.instance.RandomText[39] = "I think I can rest a little today...";
            Game_TypeWriterEffect.instance.RandomText[40] = "I feel chilly. I think I used too much energy.";
            Game_TypeWriterEffect.instance.RandomText[41] = "I want to have warm egg porridge.";
            Game_TypeWriterEffect.instance.RandomText[42] = ".....I’m sick.....";
            Game_TypeWriterEffect.instance.RandomText[43] = "The sun has no oxygen, but it is constantly burning...";
            Game_TypeWriterEffect.instance.RandomText[44] = "The common sense we know seems useless in space.";
            Game_TypeWriterEffect.instance.RandomText[45] = "Let's do our best, then we can take a break at the space rest area.";
            Game_TypeWriterEffect.instance.RandomText[46] = "I’ll have a good rest when I get to the rest area.";
            Game_TypeWriterEffect.instance.RandomText[47] = "I'm glad I didn't get sick in the black and white space.";
            Game_TypeWriterEffect.instance.RandomText[48] = "Don’t give up. Cheer up!";
            Game_TypeWriterEffect.instance.RandomText[49] = "When I was sick, my mom gave me warm milk with a little sugar.";
        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "... I feel warm... No, hot.";
            Game_TypeWriterEffect.instance.RandomText[1] = "... Mom ...";
            Game_TypeWriterEffect.instance.RandomText[2] = "Wouldn't it be great if there was medicine on the way...";
            Game_TypeWriterEffect.instance.RandomText[3] = "Lights are hovering around.";
            Game_TypeWriterEffect.instance.RandomText[4] = "I must have lost my mind for a while.";
            Game_TypeWriterEffect.instance.RandomText[5] = "What is this place? The surrounding landscape has changed.";
            Game_TypeWriterEffect.instance.RandomText[6] = "... I think there’s a painting.";
            Game_TypeWriterEffect.instance.RandomText[7] = "Is this a continuation of the mural I saw last time?";
            Game_TypeWriterEffect.instance.RandomText[8] = "...This spore is similar to the one I saw last time.";
            Game_TypeWriterEffect.instance.RandomText[9] = "... I think it's hard to breathe. I'm not used to this.";
            Game_TypeWriterEffect.instance.RandomText[10] = "When I return to Earth,\nI think I’ll be the person who can hold their breath the longest.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Does this road not welcome me for coming this far?";
            Game_TypeWriterEffect.instance.RandomText[12] = "I feel like my feet are burning...";
            Game_TypeWriterEffect.instance.RandomText[13] = "Is this the way to the sun? It is hot..";
            Game_TypeWriterEffect.instance.RandomText[14] = "I feel like my whole body is sweaty...";
            Game_TypeWriterEffect.instance.RandomText[15] = "This mural is about...There's a moon on this mural, too.";
            Game_TypeWriterEffect.instance.RandomText[16] = "I think this painting is a stone or a shard or something important...";
            Game_TypeWriterEffect.instance.RandomText[17] = "I need to get out of here quickly.";
            Game_TypeWriterEffect.instance.RandomText[18] = "It's hot and it's hard to breathe... I think it's harder to bear because of the pain...";
            Game_TypeWriterEffect.instance.RandomText[19] = "...Somebody help me...";
            Game_TypeWriterEffect.instance.RandomText[20] = ".... Is there any life that can help me?";
            Game_TypeWriterEffect.instance.RandomText[21] = "It’s hard for me to talk...";
            Game_TypeWriterEffect.instance.RandomText[22] = "I'm thirsty and my throat feels scratchy.";
            Game_TypeWriterEffect.instance.RandomText[23] = "I think there are more spores...";
            Game_TypeWriterEffect.instance.RandomText[24] = "The temperature seems to keep rising because all sides are blocked...";
            Game_TypeWriterEffect.instance.RandomText[25] = "I want to escape...";
            Game_TypeWriterEffect.instance.RandomText[26] = "... I’ve already come so far that I can't go back.";
            Game_TypeWriterEffect.instance.RandomText[27] = "They say pleasure follows pain. There will be a good day.";
            Game_TypeWriterEffect.instance.RandomText[28] = "Will I be able to grow a little if I endure these things?";
            Game_TypeWriterEffect.instance.RandomText[29] = "If I were a flower, I wouldn't be here.";
            Game_TypeWriterEffect.instance.RandomText[30] = "... I want to see stars.\nThe cosmic sky must have been my pleasure.";
            Game_TypeWriterEffect.instance.RandomText[31] = "I dream a lot these days.\n What do I want to see in my dreams?";
            Game_TypeWriterEffect.instance.RandomText[32] = "Today, I saw my mom in my dream, and she hugged me tightly.\nIt felt so warm.";
            Game_TypeWriterEffect.instance.RandomText[33] = "... I didn't want to leave my mother's arms, and I woke up from my dream.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Dreams can remind you of the past and show you what you want.";
            Game_TypeWriterEffect.instance.RandomText[35] = "Nightmares show me the situation I'm afraid of.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I've had a lot of nightmares here.";
            Game_TypeWriterEffect.instance.RandomText[37] = "The nightmare I had the most was... just walking without saying anything.";
            Game_TypeWriterEffect.instance.RandomText[38] = "Grandma used to sing Arirang when she was having fun.\nI suddenly remembered it.";
            Game_TypeWriterEffect.instance.RandomText[39] = "I like to wake up late in the morning on holidays and eat cereal.";
            Game_TypeWriterEffect.instance.RandomText[40] = "The reason I eat cereal is because I want to drink sweet milk.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Whales cannot enter this passage.\nIt's both hot and narrow.";
            Game_TypeWriterEffect.instance.RandomText[42] = "Can stars feel hot? I should have asked Michu.";
            Game_TypeWriterEffect.instance.RandomText[43] = "In a movie I liked, the protagonist sang a song, and the title was “Remember Me”.";
            Game_TypeWriterEffect.instance.RandomText[44] = "Why do we want to remain in someone's memory?";
            Game_TypeWriterEffect.instance.RandomText[45] = "... I feel like I'm out of breath.";
            Game_TypeWriterEffect.instance.RandomText[46] = "I felt like I was in a sauna with my mom when I was young.";
            Game_TypeWriterEffect.instance.RandomText[47] = "Had there been water, the vapor would have been crazy.";
            Game_TypeWriterEffect.instance.RandomText[48] = "If I put an egg on this road, will it get cooked?\nIt would’ve been boiled before it was cooked.";
            Game_TypeWriterEffect.instance.RandomText[49] = "I have to walk diligently.";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = ".... Where am I?";
            Game_TypeWriterEffect.instance.RandomText[1] = ".... I can’t move my legs.";
            Game_TypeWriterEffect.instance.RandomText[2] = "Maybe I can’t control anything on this road.";
            Game_TypeWriterEffect.instance.RandomText[3] = "There’s nothing...";
            Game_TypeWriterEffect.instance.RandomText[4] = "Be quiet...";
            Game_TypeWriterEffect.instance.RandomText[5] = "Am I going forward?";
            Game_TypeWriterEffect.instance.RandomText[6] = "If it weren't for the occasional meteors,\nI would’ve forgotten that I was in space.";
            Game_TypeWriterEffect.instance.RandomText[7] = "I’m in space...";
            Game_TypeWriterEffect.instance.RandomText[8] = "I feel frustrated...";
            Game_TypeWriterEffect.instance.RandomText[9] = "Shall I shout? It wouldn’t be a problem.";
            Game_TypeWriterEffect.instance.RandomText[10] = "I’m here! I think my voice is hoarse.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Were these signs put up here knowing my anxiety?";
            Game_TypeWriterEffect.instance.RandomText[12] = "Where am I? I can’t be sure.";
            Game_TypeWriterEffect.instance.RandomText[13] = "Radio is working properly. There is no noise.";
            Game_TypeWriterEffect.instance.RandomText[14] = "Radio is trying to comfort me.";
            Game_TypeWriterEffect.instance.RandomText[15] = "Everyone is having a busy day.";
            Game_TypeWriterEffect.instance.RandomText[16] = "It's nice to hear someone's laughter.\nDoes that person know that the sound of his laughter was comforting to me?";
            Game_TypeWriterEffect.instance.RandomText[17] = " I’m hungry...";
            Game_TypeWriterEffect.instance.RandomText[18] = "I think everyone is having fun.\nCould it be that I’m missing out on the joy of my life?";
            Game_TypeWriterEffect.instance.RandomText[19] = "Let’s not worry. I’ll be rewarded for my effort.";
            Game_TypeWriterEffect.instance.RandomText[20] = " I want my story to be delivered and to be played on the radio.";
            Game_TypeWriterEffect.instance.RandomText[21] = "My radio show will be a huge hit!\n There will be no one in space to deliver the news.";
            Game_TypeWriterEffect.instance.RandomText[22] = "People on Earth, listen to my story!";
            Game_TypeWriterEffect.instance.RandomText[23] = "Sigh...";
            Game_TypeWriterEffect.instance.RandomText[24] = "It reminds me of someone talking to a volleyball on a deserted island,\nI think that's what I am now.";
            Game_TypeWriterEffect.instance.RandomText[25] = " I thought I was okay, but I don't think I was... It hurts...";
            Game_TypeWriterEffect.instance.RandomText[26] = "... I need medicine or a solution.";
            Game_TypeWriterEffect.instance.RandomText[27] = "I can't help but feel vaguely anxious.\nLook at this road... It's endless.";
            Game_TypeWriterEffect.instance.RandomText[28] = "What if the road expands and expands just like the universe does?";
            Game_TypeWriterEffect.instance.RandomText[29] = "The Earth seems to be turning busily today as usual.";
            Game_TypeWriterEffect.instance.RandomText[30] = "My leg is asleep.";
            Game_TypeWriterEffect.instance.RandomText[31] = "...Who can talk with me here?";
            Game_TypeWriterEffect.instance.RandomText[32] = "I didn't know I’d spend days without laughing or chatting with someone.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I guess I've lived without knowing solitude until now.";
            Game_TypeWriterEffect.instance.RandomText[34] = "I heard that the housing crisis is serious...\nIf we go to space, that will be resolved.";
            Game_TypeWriterEffect.instance.RandomText[35] = "At Science Day events, I always drew a future city.\nAt that time, the background of the city was space.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I thought space was black, but it's amazing that there are so many different colors.";
            Game_TypeWriterEffect.instance.RandomText[37] = "...! Didn't it look like two signs just now?";
            Game_TypeWriterEffect.instance.RandomText[38] = "... This sound... It's from my stomach.";
            Game_TypeWriterEffect.instance.RandomText[39] = "What I want the most right now is a break...";
            Game_TypeWriterEffect.instance.RandomText[40] = " I'm glad there's no one to say, \"What's the hardship when there's only walking in space ?\"";
            Game_TypeWriterEffect.instance.RandomText[41] = "We often joke that we're the dust of the universe and that's true.";
            Game_TypeWriterEffect.instance.RandomText[42] = "How would you react if someone else fell on this road?";
            Game_TypeWriterEffect.instance.RandomText[43] = "... It may be selfish,\nbut I really wish someone would fall on this road and walk with me.";
            Game_TypeWriterEffect.instance.RandomText[44] = "Today's fortune says that I’ll miss someone.";
            Game_TypeWriterEffect.instance.RandomText[45] = "Are the sounds coming out of the radio the real-time sound of the Earth?";
            Game_TypeWriterEffect.instance.RandomText[46] = "This news may be from what was recorded on the radio.";
            Game_TypeWriterEffect.instance.RandomText[47] = "What date was it when I left Earth?";
            Game_TypeWriterEffect.instance.RandomText[48] = "I want my memories back.\nThen this vague feeling will disappear.";
            Game_TypeWriterEffect.instance.RandomText[49] = " Can you call a distant daily life a daily life?";

        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "It looks really splendid.";
            Game_TypeWriterEffect.instance.RandomText[1] = "Compared to the place I just passed by, it's dazzling.";
            Game_TypeWriterEffect.instance.RandomText[2] = "These must be plants.";
            Game_TypeWriterEffect.instance.RandomText[3] = "There are space flowers among the plants.";
            Game_TypeWriterEffect.instance.RandomText[4] = "It looks like someone's garden.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I don’t know what Radio is thinking.";
            Game_TypeWriterEffect.instance.RandomText[6] = "I can't imagine what it's like to disappear...";
            Game_TypeWriterEffect.instance.RandomText[7] = "Do all the plants in space eat moondust?";
            Game_TypeWriterEffect.instance.RandomText[8] = "I feel like I’m all better.";
            Game_TypeWriterEffect.instance.RandomText[9] = "What’s the name of this plant?";
            Game_TypeWriterEffect.instance.RandomText[10] = "We give each other names,\nand I wonder if other living things name themselves like that.";
            Game_TypeWriterEffect.instance.RandomText[11] = "It's really nice to need each other.";
            Game_TypeWriterEffect.instance.RandomText[12] = "One-sided relationships are not that happy.";
            Game_TypeWriterEffect.instance.RandomText[13] = "This radio has a space interpreter function.";
            Game_TypeWriterEffect.instance.RandomText[14] = "It doesn’t rain in space, so you probably don't know what rain is.";
            Game_TypeWriterEffect.instance.RandomText[15] = " It rains on Earth, so on those days, we use an umbrella or raincoat.";
            Game_TypeWriterEffect.instance.RandomText[16] = "... Come to think of it, I'm wearing a spacesuit,\nso even if it rains, I won't need an umbrella.";
            Game_TypeWriterEffect.instance.RandomText[17] = "The plants you see in nature are better than\nthe plants you see in the botanical garden.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Can you call this a plant you see in nature?";
            Game_TypeWriterEffect.instance.RandomText[19] = "When I try to find the cosmic flower, I can't seem to find it anymore.";
            Game_TypeWriterEffect.instance.RandomText[20] = "That flower hasn’t bloomed yet...";
            Game_TypeWriterEffect.instance.RandomText[21] = "Do space plants bear fruit? I want to taste the fruit.";
            Game_TypeWriterEffect.instance.RandomText[22] = "Carrying a few cosmic flowers would be comforting.\nThey may wither though...";
            Game_TypeWriterEffect.instance.RandomText[23] = "Seeing you from space is more meaningful than seeing you from Earth.";
            Game_TypeWriterEffect.instance.RandomText[24] = "... Are there seasons in space?\nIf there are, I think the cycle will be very long.";
            Game_TypeWriterEffect.instance.RandomText[25] = "Space seems peaceful now, so it might be spring.";
            Game_TypeWriterEffect.instance.RandomText[26] = "Reminds me of a friend who wanted to come to space.\nI must have promised someone.";
            Game_TypeWriterEffect.instance.RandomText[27] = "The friend talked a lot with me.\nIt's like we knew everything about each other.";
            Game_TypeWriterEffect.instance.RandomText[28] = "... I kept thinking that it would be nice to walk with someone on this road.";
            Game_TypeWriterEffect.instance.RandomText[29] = "The biggest pain I felt on this road was loneliness.";
            Game_TypeWriterEffect.instance.RandomText[30] = "Will I be able to come back here someday?";
            Game_TypeWriterEffect.instance.RandomText[31] = "... What are these shards I gathered along the way?";
            Game_TypeWriterEffect.instance.RandomText[32] = "Are the stories on the murals real?";
            Game_TypeWriterEffect.instance.RandomText[33] = "If they had known that wishes come true on the moon, they would’ve already come to the moon.";
            Game_TypeWriterEffect.instance.RandomText[34] = "What are people’s wishes?";
            Game_TypeWriterEffect.instance.RandomText[35] = "Actually, I don't know what my wish is.\nI guess I want to go back to Earth.";
            Game_TypeWriterEffect.instance.RandomText[36] = "People instinctively fear the dark.\nBecause you can't see anything in the dark,\nand you don't know what's going to come out.";
            Game_TypeWriterEffect.instance.RandomText[37] = "You’re so pretty! Beside you is a cool-looking plant!";
            Game_TypeWriterEffect.instance.RandomText[38] = "I hope my memory comes back fully. Now it’s fragmented.";
            Game_TypeWriterEffect.instance.RandomText[39] = "Should I buy plenty of oxygen tanks?\nI might need a lot of them when I meet someone in distress.";
            Game_TypeWriterEffect.instance.RandomText[40] = "When you use stardust, you feel light and energetic.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Are there any other murals on the road ahead?";
            Game_TypeWriterEffect.instance.RandomText[42] = "Probably there is another black and white space or a place with floating spores on the road ahead.";
            Game_TypeWriterEffect.instance.RandomText[43] = "Maybe there is a fantastic space made of sweet desserts ahead!";
            Game_TypeWriterEffect.instance.RandomText[44] = "Radio, tell me the news of the Earth.";
            Game_TypeWriterEffect.instance.RandomText[45] = "Hmm... Okay. I’ll just have to walk straight.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Twinkle twinkle little star.\n How I wonder what you are.";
            Game_TypeWriterEffect.instance.RandomText[47] = "My friends would envy me if they found out that I've been to space.";
            Game_TypeWriterEffect.instance.RandomText[48] = "I’ve been here a long time.\nThat means someone must’ve been missing me for a long time.";
            Game_TypeWriterEffect.instance.RandomText[49] = "I vaguely recall the sound of crickets chirping on a summer evening.";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "I’m happy to walk with you.";
            Game_TypeWriterEffect.instance.RandomText[1] = "I've walked while talking with someone. Time flew so fast then.";
            Game_TypeWriterEffect.instance.RandomText[2] = "I'm reassured, but also afraid that the time I spend with you will pass quickly.";
            Game_TypeWriterEffect.instance.RandomText[3] = "If you lose light again, I will give you the space flower once more.";
            Game_TypeWriterEffect.instance.RandomText[4] = "You’re a pretty shining star.";
            Game_TypeWriterEffect.instance.RandomText[5] = "Something is flying...";
            Game_TypeWriterEffect.instance.RandomText[6] = "Wow, look!";
            Game_TypeWriterEffect.instance.RandomText[7] = "What’s that called? What about that?";
            Game_TypeWriterEffect.instance.RandomText[8] = "You know a lot about space.";
            Game_TypeWriterEffect.instance.RandomText[9] = "You know a lot about space.";
            Game_TypeWriterEffect.instance.RandomText[10] = "Do you know about the black and white space that existed before you came here?";
            Game_TypeWriterEffect.instance.RandomText[11] = "I know the passage with murals on it.";
            Game_TypeWriterEffect.instance.RandomText[12] = "Wow, I can't believe that happened in space.\nI want to see them too!";
            Game_TypeWriterEffect.instance.RandomText[13] = "Today’s a good day.\nI have a lot of questions to ask you!";
            Game_TypeWriterEffect.instance.RandomText[14] = "Where do stars come from?";
            Game_TypeWriterEffect.instance.RandomText[15] = "Do you remember how you were born? Me?\nI... don’t know! What? You too?";
            Game_TypeWriterEffect.instance.RandomText[16] = "There are things you don't know either.\nWell, I don't know everything about the Earth.";
            Game_TypeWriterEffect.instance.RandomText[17] = "I see stars over there. They look like you.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Do you have siblings? I... don’t think I do!";
            Game_TypeWriterEffect.instance.RandomText[19] = "You’ve never seen a living being besides me!";
            Game_TypeWriterEffect.instance.RandomText[20] = "I don't remember seeing a person who looked like me.\nI don’t have a common look.";
            Game_TypeWriterEffect.instance.RandomText[21] = "I feel good today. I’ll feel good tomorrow.";
            Game_TypeWriterEffect.instance.RandomText[22] = "I think the choices I make today will lead me to a good path.";
            Game_TypeWriterEffect.instance.RandomText[23] = "The being flying by looks like a bird on Earth.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I guess it’s okay to call it space bird.";
            Game_TypeWriterEffect.instance.RandomText[25] = "I can't believe the things on Earth are modeled after the things in space...";
            Game_TypeWriterEffect.instance.RandomText[26] = "Whales must have come to Earth with their friends from space.";
            Game_TypeWriterEffect.instance.RandomText[27] = "Stars have names too... How did you name yourselves?";
            Game_TypeWriterEffect.instance.RandomText[28] = "When are you the most happy?";
            Game_TypeWriterEffect.instance.RandomText[29] = "I can't wait to see all the constellations in front of me.";
            Game_TypeWriterEffect.instance.RandomText[30] = "I hope the constellations are kind to us.";
            Game_TypeWriterEffect.instance.RandomText[31] = "There might be a black hole on Earth.\nMy eraser used to disappear when I dropped it.";
            Game_TypeWriterEffect.instance.RandomText[32] = "Black holes that are too small swallow only erasers.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I should have got some space flowers for you.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Have you ever been lonely wandering in space alone?";
            Game_TypeWriterEffect.instance.RandomText[35] = "I still don't know what you mean by disappearing... or how it feels.";
            Game_TypeWriterEffect.instance.RandomText[36] = "Good luck with your future trip.";
            Game_TypeWriterEffect.instance.RandomText[37] = "Hi, birds!";
            Game_TypeWriterEffect.instance.RandomText[38] = "Is every star kind like you?";
            Game_TypeWriterEffect.instance.RandomText[39] = "I want to make a lot of star friends.\nRight. It’s not always good to have many friends!";
            Game_TypeWriterEffect.instance.RandomText[40] = "Oops! I forgot to play the space radio!\nIt never happened before.";
            Game_TypeWriterEffect.instance.RandomText[41] = "To see the outer space you mentioned,\nI should travel for the rest of my life.";
            Game_TypeWriterEffect.instance.RandomText[42] = "Yes, let’s check it out together.\nIt’d be nice if it’s on the way.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I'm glad that you're a chatterbox like me!\nI would’ve felt sorry if I was the only one talking!";
            Game_TypeWriterEffect.instance.RandomText[44] = "The moon in front of me is my destination.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I feel rewarded when I feel that moon is getting bigger and bigger.";
            Game_TypeWriterEffect.instance.RandomText[46] = "I'm worried that I'm keeping you on this path for too long.";
            Game_TypeWriterEffect.instance.RandomText[47] = "Would you shine more if I scattered my stardust on you? No need.";
            Game_TypeWriterEffect.instance.RandomText[48] = "You saw the aurora too!";
            Game_TypeWriterEffect.instance.RandomText[49] = "Check out the aurora with me later.";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "It’s like Earth’s snow...";
            Game_TypeWriterEffect.instance.RandomText[1] = "... If it wasn't for you and me, it’d be silent around us.";
            Game_TypeWriterEffect.instance.RandomText[2] = "On Earth, we call this silence.";
            Game_TypeWriterEffect.instance.RandomText[3] = "It's hard to see the road because of the smoke.";
            Game_TypeWriterEffect.instance.RandomText[4] = "Who put light bulbs on the side of the street?";
            Game_TypeWriterEffect.instance.RandomText[5] = "Oh! Is that a black hole?";
            Game_TypeWriterEffect.instance.RandomText[6] = "There are so many different things here.\nI'm not sure what to look at first.";
            Game_TypeWriterEffect.instance.RandomText[7] = "I need to look at the floor carefully.\nWhat if I step on something that's not a road?";
            Game_TypeWriterEffect.instance.RandomText[8] = "What do the moon shards you saw before mean to you?";
            Game_TypeWriterEffect.instance.RandomText[9] = "I wish I had a moon shard that I could give out when I’m grateful.";
            Game_TypeWriterEffect.instance.RandomText[10] = "If I lose something in space, can I find it?";
            Game_TypeWriterEffect.instance.RandomText[11] = "Lune, stay close to me so that you don't get lost in space!";
            Game_TypeWriterEffect.instance.RandomText[12] = "Originally, black holes existed only in theory.";
            Game_TypeWriterEffect.instance.RandomText[13] = "Can we see well from Earth through an astronomical telescope?";
            Game_TypeWriterEffect.instance.RandomText[14] = "Lune, I want to introduce you to my friends.\nMy friends will love you too.";
            Game_TypeWriterEffect.instance.RandomText[15] = "Maybe my mom can invite you to my house and we have dinner together.";
            Game_TypeWriterEffect.instance.RandomText[16] = "Can you leave space and come to our planet?";
            Game_TypeWriterEffect.instance.RandomText[17] = "There's a song that reminds me of you. It's called \"Little Star\".";
            Game_TypeWriterEffect.instance.RandomText[18] = "Twinkle twinkle little star. How I wonder what you are.";
            Game_TypeWriterEffect.instance.RandomText[19] = "Actually I haven't heard children's songs on Earth recently.\nKids said it was childish!";
            Game_TypeWriterEffect.instance.RandomText[20] = "When adults sang children's songs,\nthey looked like they were reminiscing about something";
            Game_TypeWriterEffect.instance.RandomText[21] = "What if scientists find me and announce that I’m an alien?";
            Game_TypeWriterEffect.instance.RandomText[22] = ".... I think I talked too much. My mouth hurts.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Doesn’t your mouth hurt?";
            Game_TypeWriterEffect.instance.RandomText[24] = "I feel like I talk a lot when I'm in space. Do you agree?";
            Game_TypeWriterEffect.instance.RandomText[25] = "The constellation you mentioned is still far away, isn't it?";
            Game_TypeWriterEffect.instance.RandomText[26] = "On the way here, I picked up stardust everywhere.";
            Game_TypeWriterEffect.instance.RandomText[27] = "When does the stardust fall?";
            Game_TypeWriterEffect.instance.RandomText[28] = "If I don't say anything, I really feel alone.";
            Game_TypeWriterEffect.instance.RandomText[29] = " I want to meet more space creatures.";
            Game_TypeWriterEffect.instance.RandomText[30] = " I wanted to be reborn as a stone in my next life,\nbut now that I think about it, I want to take that back.\n" +
                "It’d be a pity if I couldn’t feel anything or think...\nwhen the world is so big.";
            Game_TypeWriterEffect.instance.RandomText[31] = "It’s snow. I like this place when I consider that it’s snow.";
            Game_TypeWriterEffect.instance.RandomText[32] = "When it snowed, I went out and made a snowman...\nbut it doesn't pile up here...";
            Game_TypeWriterEffect.instance.RandomText[33] = "Nature is really great.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Space really needs endless imagination.";
            Game_TypeWriterEffect.instance.RandomText[35] = "I’m hungry. Lune, you don’t know what hunger is.";
            Game_TypeWriterEffect.instance.RandomText[36] = "Your voice on the space radio is a great comfort.";
            Game_TypeWriterEffect.instance.RandomText[37] = "I feel like I’ll meet someone while walking down this road.";
            Game_TypeWriterEffect.instance.RandomText[38] = "Lune, can you understand intuition?";
            Game_TypeWriterEffect.instance.RandomText[39] = "Is there anyone else like me in space? I want to believe there is.\nThere was definitely a figure that looked like a human being in the mural.";
            Game_TypeWriterEffect.instance.RandomText[40] = "Lune, did anyone other than me come here?";
            Game_TypeWriterEffect.instance.RandomText[41] = "You ignore me when you don’t want to talk.";
            Game_TypeWriterEffect.instance.RandomText[42] = "Lune, I’m happy when I can talk to you.";
            Game_TypeWriterEffect.instance.RandomText[43] = "Can you tell me how the moon shards are made?";
            Game_TypeWriterEffect.instance.RandomText[44] = "I sometimes run as hard as I can to relieve stress.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I can't really believe everything I'm seeing.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Lune, do you have a mom?\nI used to have a mom, but now I don't.\nCould she be on Earth?";
            Game_TypeWriterEffect.instance.RandomText[47] = "I feel like I’ve walked a lot...";
            Game_TypeWriterEffect.instance.RandomText[48] = "Are all the souls wandering in space dead stars like you?";
            Game_TypeWriterEffect.instance.RandomText[49] = "Can I stay here and become a star?";
        }
        //16. 안녕, 별자리,800
        else if (wayPoint.Equals(16))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Wow, it's full of pretty constellations.";
            Game_TypeWriterEffect.instance.RandomText[1] = "I'm glad to see this pretty constellation with you.";
            Game_TypeWriterEffect.instance.RandomText[2] = "Look at all the stars.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Reminds me of when I was looking for constellations on Earth.";
            Game_TypeWriterEffect.instance.RandomText[4] = "Lune, I'm so glad you're here.";
            Game_TypeWriterEffect.instance.RandomText[5] = "I want to hear your voice.";
            Game_TypeWriterEffect.instance.RandomText[6] = "What’s wrong with this? The space radio isn’t working.";
            Game_TypeWriterEffect.instance.RandomText[7] = "On Earth, each person's constellation is determined by the date of their birth.";
            Game_TypeWriterEffect.instance.RandomText[8] = "My zodiac sign is Cancer.";
            Game_TypeWriterEffect.instance.RandomText[9] = "My legs hurt from walking.";
            Game_TypeWriterEffect.instance.RandomText[10] = "What’s that constellation?";
            Game_TypeWriterEffect.instance.RandomText[11] = "I studied a lot about constellations\nbecause I liked them, but now I can't remember.";
            Game_TypeWriterEffect.instance.RandomText[12] = "Where did the brightest star come from?";
            Game_TypeWriterEffect.instance.RandomText[13] = "Will the people of Earth remember me?";
            Game_TypeWriterEffect.instance.RandomText[14] = "I want to remember something, but I can't...";
            Game_TypeWriterEffect.instance.RandomText[15] = "How does space radio work?";
            Game_TypeWriterEffect.instance.RandomText[16] = "I’m hungry again.";
            Game_TypeWriterEffect.instance.RandomText[17] = "Space is so interesting.";
            Game_TypeWriterEffect.instance.RandomText[18] = "I'm constantly walking on this road,\nbut I'm scared about whether I can survive.";
            Game_TypeWriterEffect.instance.RandomText[19] = "Lune, answer me.";
            Game_TypeWriterEffect.instance.RandomText[20] = "I need emergency food. I'm getting so hungry.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Red, blue, green, yellow... There are so many colors.";
            Game_TypeWriterEffect.instance.RandomText[22] = "Being here makes me think about my existence.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Am I unnecessary here?";
            Game_TypeWriterEffect.instance.RandomText[24] = "Actually, it's getting harder and harder to tell if this is space or my delusion.";
            Game_TypeWriterEffect.instance.RandomText[25] = "Lune, your existence alone gives me so much strength.";
            Game_TypeWriterEffect.instance.RandomText[26] = "Loneliness was a lot scarier than I thought.";
            Game_TypeWriterEffect.instance.RandomText[27] = "If I were to return to Earth, what would I say first?";
            Game_TypeWriterEffect.instance.RandomText[28] = "I want to write a book about the many things I've been through.";
            Game_TypeWriterEffect.instance.RandomText[29] = "I don't want to be forgotten after walking like this for the rest of my life.";
            Game_TypeWriterEffect.instance.RandomText[30] = "It feels good to take out the moon shard whenever I’m lonely.";
            Game_TypeWriterEffect.instance.RandomText[31] = "Why is the moon called the moon?";
            Game_TypeWriterEffect.instance.RandomText[32] = "There are so many living things on Earth.\nHow many more creatures are there in this vast space?";
            Game_TypeWriterEffect.instance.RandomText[33] = "Constellations are so familiar to me,\nperhaps because I can see them on Earth.";
            Game_TypeWriterEffect.instance.RandomText[34] = "Is this Capricorn?";
            Game_TypeWriterEffect.instance.RandomText[35] = "I think it's my friend's zodiac sign.\nWhat was it? Ah! It was Aquarius.";
            Game_TypeWriterEffect.instance.RandomText[36] = "I think that's Orion...";
            Game_TypeWriterEffect.instance.RandomText[37] = "Oh! That's Cancer! My zodiac sign!";
            Game_TypeWriterEffect.instance.RandomText[38] = "When I’m walking for a long time,\nI really don't notice time passing.";
            Game_TypeWriterEffect.instance.RandomText[39] = "When I think it's someone's zodiac sign, I'm less lonely.";
            Game_TypeWriterEffect.instance.RandomText[40] = "It's a very happy thing to have a day and night.";
            Game_TypeWriterEffect.instance.RandomText[41] = "I can't sleep from the moment I walk in space. Interesting.";
            Game_TypeWriterEffect.instance.RandomText[42] = "Lune, thank you so much for being next to me.";
            Game_TypeWriterEffect.instance.RandomText[43] = "It's amazing that stars have a set spot.";
            Game_TypeWriterEffect.instance.RandomText[44] = "It's amazing that I walked all the way here.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I was a coward on Earth, but I can't be one here.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Let’s keep walking. Let’s cheer up!";
            Game_TypeWriterEffect.instance.RandomText[47] = "I think my legs hurt...";
            Game_TypeWriterEffect.instance.RandomText[48] = "Lune, Lune. I want to keep calling your name.";
            Game_TypeWriterEffect.instance.RandomText[49] = "I'll buy what I need when I get to the space rest area.";
        }
        //17. 몽환의 세계,850
        else if (wayPoint.Equals(17))
        {

            Game_TypeWriterEffect.instance.RandomText[0] = "Where am I?";
            Game_TypeWriterEffect.instance.RandomText[1] = "Was being in space a dream?";
            Game_TypeWriterEffect.instance.RandomText[2] = "This feels familiar and comfy.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Humming~♪ It feels good♪";
            Game_TypeWriterEffect.instance.RandomText[4] = "Oh, I think that's my bed.";
            Game_TypeWriterEffect.instance.RandomText[5] = "It’s my favorite ice cream!";
            Game_TypeWriterEffect.instance.RandomText[6] = "If I stay here, can I live as if I’m on Earth?";
            Game_TypeWriterEffect.instance.RandomText[7] = "As I get closer, it disappears like smoke...";
            Game_TypeWriterEffect.instance.RandomText[8] = "This is not the Earth. Get a hold of yourself!";
            Game_TypeWriterEffect.instance.RandomText[9] = "I can’t. I want to stay here.";
            Game_TypeWriterEffect.instance.RandomText[10] = "I think someone moved what’s in my head to here.";
            Game_TypeWriterEffect.instance.RandomText[11] = "I saw so much of the universe,\nbut I guess I was only thinking about the Earth.";
            Game_TypeWriterEffect.instance.RandomText[12] = "These are the things I miss.\nEach person's memories are contained in the object.";
            Game_TypeWriterEffect.instance.RandomText[13] = "This place is full of things,\nbut they can't bring back the people in my memories...";
            Game_TypeWriterEffect.instance.RandomText[14] = "Surely, things were full of happy memories.";
            Game_TypeWriterEffect.instance.RandomText[15] = "When I went out with my mom,\nshe always bought me ice cream.";
            Game_TypeWriterEffect.instance.RandomText[16] = "Some of the happiness must be in the form of ice cream.";
            Game_TypeWriterEffect.instance.RandomText[17] = "That's definitely my favorite food.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Lune, look at that! Since when was Lune not next to me?";
            Game_TypeWriterEffect.instance.RandomText[19] = "I feel lonely. I want to talk to Lune about this place";
            Game_TypeWriterEffect.instance.RandomText[20] = "This place is... full of memories.\nDoes it show what I’m made of?";
            Game_TypeWriterEffect.instance.RandomText[21] = "I want to fill this place with more stuff.";
            Game_TypeWriterEffect.instance.RandomText[22] = "If this is my inner world,\nI want to invite a lot of people to hang out.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Is this a dream?\nIs it my wish? It's too lonely to be a wish.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I want to be here forever.\nBut then who could find me?";
            Game_TypeWriterEffect.instance.RandomText[25] = "This place is like my treasure box that I hid.";
            Game_TypeWriterEffect.instance.RandomText[26] = "Is that a picture? I can’t see it well.";
            Game_TypeWriterEffect.instance.RandomText[27] = "I want to see it up close, but I can’t...";
            Game_TypeWriterEffect.instance.RandomText[28] = "I used to look at the moon habitually... I can't see it.";
            Game_TypeWriterEffect.instance.RandomText[29] = "At the end of this road... Won't everyone be waiting for me?";
            Game_TypeWriterEffect.instance.RandomText[30] = "I think I'll be able to meet someone,\nbut it's too quiet a place for that...";
            Game_TypeWriterEffect.instance.RandomText[31] = "All I really want is my mom and friends...";
            Game_TypeWriterEffect.instance.RandomText[32] = "It's a box of ddakji and buttons that I collected when I was a kid.";
            Game_TypeWriterEffect.instance.RandomText[33] = "That's a doll that I lost and couldn't find for a long time...!";
            Game_TypeWriterEffect.instance.RandomText[34] = "There’s a toy that I loved when I was little.";
            Game_TypeWriterEffect.instance.RandomText[35] = "What is that? It was so long ago I can't remember.\nDid I use it when I was a baby?";
            Game_TypeWriterEffect.instance.RandomText[36] = "... This is the comb my mom used.\nI loved stroking the print on the handle of that comb.";
            Game_TypeWriterEffect.instance.RandomText[37] = "Even the smallest habits are gathered here.";
            Game_TypeWriterEffect.instance.RandomText[38] = "... Honestly, when will I be able to see these objects \nand reminisce about my memories if I leave this place?";
            Game_TypeWriterEffect.instance.RandomText[39] = "The album is... a mysterious book\nwhere I can find my forgotten memories again.";
            Game_TypeWriterEffect.instance.RandomText[40] = "That's right.\nMy mom woke me up in that bed every day.";
            Game_TypeWriterEffect.instance.RandomText[41] = "That's a gift from a friend... \nThat's what I got for Children's Day.";
            Game_TypeWriterEffect.instance.RandomText[42] = "When I was a child,\nthe picture in that frame used to become a space of my imagination.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I've been using that cup since I was young.\nNo one else could use that cup!";
            Game_TypeWriterEffect.instance.RandomText[44] = "This space can't find my mistakes or faults,\nit's like they're trying to hold me.";
            Game_TypeWriterEffect.instance.RandomText[45] = "No, I had a sweet dream.\nI remember a line from a movie that adults used to watch.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Addiction makes people happy, but it ruins their lives.\nHappiness is contradictory.";
            Game_TypeWriterEffect.instance.RandomText[47] = "...Get used to the happiness here, but don't get addicted...";
            Game_TypeWriterEffect.instance.RandomText[48] = "This place is contradictory.";
            Game_TypeWriterEffect.instance.RandomText[49] = "If I don't leave this place,\nI'll be trapped in the happiness of the past.";
        }
        //18. 점점 더 가까이,900
        else if (wayPoint.Equals(18))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "I can feel the moon approaching closer and closer.";
            Game_TypeWriterEffect.instance.RandomText[1] = "There's a meteorite in space.\nOh, it's not a meteorite.";
            Game_TypeWriterEffect.instance.RandomText[2] = "That floating thing should be called Sky Island.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Space flowers are blooming. They're always pretty.";
            Game_TypeWriterEffect.instance.RandomText[4] = "It's too bad that I can't touch it.\nI used to search for places with lots of grass and flowers on Earth...";
            Game_TypeWriterEffect.instance.RandomText[5] = "I become softhearted when I keep thinking about the Earth.";
            Game_TypeWriterEffect.instance.RandomText[6] = "I gathered a lot of moon shards.\nLune, I told you there was a heart here,\nso I want to keep opening it.";
            Game_TypeWriterEffect.instance.RandomText[7] = "Is that a tunnel of light I see over there? Beautiful.";
            Game_TypeWriterEffect.instance.RandomText[8] = "If there was a place like this on Earth,\neveryone would line up to take pictures.";
            Game_TypeWriterEffect.instance.RandomText[9] = "I feel like having a cup of hot coffee.";
            Game_TypeWriterEffect.instance.RandomText[10] = "Sometimes I miss the dream world.\nFamiliar things are precious.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Lune, it was thanks to you that I was able to escape from my dream.\nI realized it when you were gone.";
            Game_TypeWriterEffect.instance.RandomText[12] = "Many things happen on Earth without me.\nIt's kind of sad.";
            Game_TypeWriterEffect.instance.RandomText[13] = "Lune, will I be able to survive here?";
            Game_TypeWriterEffect.instance.RandomText[14] = "I’m still young, but I feel like I’m an adult here.";
            Game_TypeWriterEffect.instance.RandomText[15] = "I'm seeing and feeling things that adults can't even imagine. Sometimes I feel proud of this fact.";
            Game_TypeWriterEffect.instance.RandomText[16] = "How can the moon shine so beautifully?";
            Game_TypeWriterEffect.instance.RandomText[17] = "There are many fairy tales about the moon on Earth.\nDo you know that?";
            Game_TypeWriterEffect.instance.RandomText[18] = "A full moon, a crescent moon... Come to think of it,\nthere are so many expressions about the moon.";
            Game_TypeWriterEffect.instance.RandomText[19] = "The moon is a very precious thing that lights up the dark night.";
            Game_TypeWriterEffect.instance.RandomText[20] = "I didn't like the night, but now that I'm here,\nI’m getting more comfortable in the dark.";
            Game_TypeWriterEffect.instance.RandomText[21] = "If there is a way to the moon,\nwill there be a way to the sun?";
            Game_TypeWriterEffect.instance.RandomText[22] = "Actually, it's a bit frustrating.\nThere are times when I feel pathetic as I walk by...\ntrusting only the murals.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Lune, you're still here,\nso I'm going to go all the way down this road.\nYou'll be by my side.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I want this road to be colored with hope rather than despair.";
            Game_TypeWriterEffect.instance.RandomText[25] = "Since it's quiet, should I sing? (Humming)";
            Game_TypeWriterEffect.instance.RandomText[26] = "I'm not lonely, because of Lune,\nbut this road with no end in sight frustrates me...";
            Game_TypeWriterEffect.instance.RandomText[27] = "It seems a bit cold... Maybe I'm mistaken?";
            Game_TypeWriterEffect.instance.RandomText[28] = "Lune, do you have any other friends?";
            Game_TypeWriterEffect.instance.RandomText[29] = "I didn't have many friends.\nI only had 1 best friend! He's been a great help to me... No, now there are 2.\nLune, you are my friend too.";
            Game_TypeWriterEffect.instance.RandomText[30] = "On Earth, your age is the number of years you have lived.\nI'm 13 years old. Lune, how old are you?";
            Game_TypeWriterEffect.instance.RandomText[31] = "I can feel the moon getting closer, so I'm getting excited.";
            Game_TypeWriterEffect.instance.RandomText[32] = "It would’ve been more hopeless if nothing had happened in space.";
            Game_TypeWriterEffect.instance.RandomText[33] = "I want to go back to Earth.\nSometimes I don’t want to though.";
            Game_TypeWriterEffect.instance.RandomText[34] = "If there wasn't a space rest area in the middle,\nI wouldn't be here. Who made it?";
            Game_TypeWriterEffect.instance.RandomText[35] = "Lune, I feel good when I call your name.";
            Game_TypeWriterEffect.instance.RandomText[36] = "Shall I sing loudly?";
            Game_TypeWriterEffect.instance.RandomText[37] = "When I was asked to write down my dream at school,\nI wrote down astronaut.\nI already achieved that dream!";
            Game_TypeWriterEffect.instance.RandomText[38] = "When asked to draw the universe, there are so many things to draw.";
            Game_TypeWriterEffect.instance.RandomText[39] = "It looks like Sky Island is going to fall...";
            Game_TypeWriterEffect.instance.RandomText[40] = "My thoughts are jumping about.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Lune, how do you understand what I'm saying and answer me?";
            Game_TypeWriterEffect.instance.RandomText[42] = "I have so many questions.\nBut there must be a lot of things that cannot be explained.";
            Game_TypeWriterEffect.instance.RandomText[43] = " I want to sleep comfortably in someone's arms.\nI don't think I've ever slept comfortably here.";
            Game_TypeWriterEffect.instance.RandomText[44] = "Shall I run? It's good to run if I’m tired of walking.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I always won 1st place when I ran at school.\nShould I show my skills here?";
            Game_TypeWriterEffect.instance.RandomText[46] = "I'm tired of wearing my spacesuit for so long.";
            Game_TypeWriterEffect.instance.RandomText[47] = "Lune, will I be able to reach the Temple of the Moon?";
            Game_TypeWriterEffect.instance.RandomText[48] = "I need to run. I feel alive when my heart is racing.";
            Game_TypeWriterEffect.instance.RandomText[49] = "One, two, one, two. Let me count my steps.";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "Beautiful... What is that light?";
            Game_TypeWriterEffect.instance.RandomText[1] = "Light, not water, is falling from the fountain...!";
            Game_TypeWriterEffect.instance.RandomText[2] = "How would it feel if I touched that light? Would it be cool?\nIt might be hot because it's light.";
            Game_TypeWriterEffect.instance.RandomText[3] = "Lune, why are you stopping me?\nIt'd be nice if you told me the reason!";
            Game_TypeWriterEffect.instance.RandomText[4] = "On my way here, I saw stars that had lost their color...";
            Game_TypeWriterEffect.instance.RandomText[5] = "The moon is very bright,\nand I realize once again that we are close.";
            Game_TypeWriterEffect.instance.RandomText[6] = "Lune, are you okay?\nIf you’re tired, we can go slowly.";
            Game_TypeWriterEffect.instance.RandomText[7] = "It's no shame to take 1 step back to go 100 steps forward.";
            Game_TypeWriterEffect.instance.RandomText[8] = "The folktales I learned in Korean class used to compare the moon to a loved one.";
            Game_TypeWriterEffect.instance.RandomText[9] = "A month means thirty days. “Month” is a unit of counting time.";
            Game_TypeWriterEffect.instance.RandomText[10] = "I better plant trees in space.\nThey will protect space for a long time.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Helping someone is rewarding.";
            Game_TypeWriterEffect.instance.RandomText[12] = "When I act selfish I feel guilty.";
            Game_TypeWriterEffect.instance.RandomText[13] = "When I wrong people, I feel uncomfortable.";
            Game_TypeWriterEffect.instance.RandomText[14] = "... The moon is getting closer.\nIt means that I will have to part with you soon, Lune.";
            Game_TypeWriterEffect.instance.RandomText[15] = "If someone had been living on the moon,\nthey would’ve discovered me already.";
            Game_TypeWriterEffect.instance.RandomText[16] = "The things in the murals are actually from a long time ago,\nso maybe they don't exist now?";
            Game_TypeWriterEffect.instance.RandomText[17] = "I'm afraid there's no way to get home from the moon.";
            Game_TypeWriterEffect.instance.RandomText[18] = "Was the rabbit I saw on the way a rabbit living on the moon?\nI should've asked.";
            Game_TypeWriterEffect.instance.RandomText[19] = "“Moon rabbit” must have been made by \nthe person who found the rabbit on the moon.";
            Game_TypeWriterEffect.instance.RandomText[20] = "I’m hungry. I can’t help it...";
            Game_TypeWriterEffect.instance.RandomText[21] = "I can feel that this journey is almost over.";
            Game_TypeWriterEffect.instance.RandomText[22] = "I think I can move a little faster.";
            Game_TypeWriterEffect.instance.RandomText[23] = "Actually this is the first time I've worked so hard on achieving a goal.";
            Game_TypeWriterEffect.instance.RandomText[24] = "I'm worried that if there's something next to me, I'll fall.\nMaybe it’s because I just passed by there.";
            Game_TypeWriterEffect.instance.RandomText[25] = "When do the twinkling stars lose their light?";
            Game_TypeWriterEffect.instance.RandomText[26] = "What does it mean for a star to lose light?";
            Game_TypeWriterEffect.instance.RandomText[27] = "In the books I read, losing hope is expressed as \"losing light in one’s eyes.\"";
            Game_TypeWriterEffect.instance.RandomText[28] = "I hope people who have lost light in their eyes can get light from space.";
            Game_TypeWriterEffect.instance.RandomText[29] = "If light is hope, the universe will be a space full of hope.";
            Game_TypeWriterEffect.instance.RandomText[30] = "If we collect the light from the universe,\nthe amount of light will be bigger than the earth.";
            Game_TypeWriterEffect.instance.RandomText[31] = "What if I'm the first boy to arrive on the moon?";
            Game_TypeWriterEffect.instance.RandomText[32] = "Can a small boy like me affect this big universe?";
            Game_TypeWriterEffect.instance.RandomText[33] = "I want to be a person who influences space!";
            Game_TypeWriterEffect.instance.RandomText[34] = "I've never thought much about my future dreams when I came here.";
            Game_TypeWriterEffect.instance.RandomText[35] = "I already have 384 things on my to do list on Earth!";
            Game_TypeWriterEffect.instance.RandomText[36] = "What should I do first when I arrive at the moon?\nFirst, I’ll shout \"Yahoo!\"";
            Game_TypeWriterEffect.instance.RandomText[37] = "I think I heard my mom's voice. Am I mistaken?";
            Game_TypeWriterEffect.instance.RandomText[38] = "Thanks to this road, I was able to come here safely.";
            Game_TypeWriterEffect.instance.RandomText[39] = "Lune, what will you do if we part?";
            Game_TypeWriterEffect.instance.RandomText[40] = "I want to go to Earth with you,\nbecause I'm attached to you.";
            Game_TypeWriterEffect.instance.RandomText[41] = "Do you also feel like you don't want to part?";
            Game_TypeWriterEffect.instance.RandomText[42] = "If we make a promise, we’ll be able to remember each other.";
            Game_TypeWriterEffect.instance.RandomText[43] = "I know how the Little Prince’s fox felt.";
            Game_TypeWriterEffect.instance.RandomText[44] = "Among my mom's favorite cartoons,\nthere was a cartoon with a train in space.";
            Game_TypeWriterEffect.instance.RandomText[45] = "I want to eat sweet milk candy, and I want to share it with you, Lune.";
            Game_TypeWriterEffect.instance.RandomText[46] = "Is this the only way to get to the moon?";
            Game_TypeWriterEffect.instance.RandomText[47] = "I hope that science will advance someday\nso that I can walk here with everyone.";
            Game_TypeWriterEffect.instance.RandomText[48] = "This will be a memory\nI’ll never forget for the rest of my life.";
            Game_TypeWriterEffect.instance.RandomText[49] = "Lune, I hope you make friends besides me\nwho will find light in your soul.";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RandomText[0] = "What are these pillars?\nThey look weird.";
            Game_TypeWriterEffect.instance.RandomText[1] = "Did it break after being hit by a piece of meteorite?";
            Game_TypeWriterEffect.instance.RandomText[2] = "... These are buildings and pillars engraved on the moon's murals!";
            Game_TypeWriterEffect.instance.RandomText[3] = "Who made them here?";
            Game_TypeWriterEffect.instance.RandomText[4] = "I guess someone lived on the moon.\nNow... I don't know.";
            Game_TypeWriterEffect.instance.RandomText[5] = "How did the natives who lived here disappear?";
            Game_TypeWriterEffect.instance.RandomText[6] = "... It's a unique place. Would the people on Earth know \nthat there's something like this on the moon?";
            Game_TypeWriterEffect.instance.RandomText[7] = "Finally I’m on the moon!\n I walked so much to get here.";
            Game_TypeWriterEffect.instance.RandomText[8] = "Is there a stone here that makes the wishes of murals come true?";
            Game_TypeWriterEffect.instance.RandomText[9] = "Hello? Is anyone there?";
            Game_TypeWriterEffect.instance.RandomText[10] = "I really hoped to come to the moon.\nBut now that I’m here, I'm afraid.";
            Game_TypeWriterEffect.instance.RandomText[11] = "Where did all the moon rabbits go?";
            Game_TypeWriterEffect.instance.RandomText[12] = "There must have been a king or a president who reached here.";
            Game_TypeWriterEffect.instance.RandomText[13] = "I wonder when the blue soul started making souls?";
            Game_TypeWriterEffect.instance.RandomText[14] = "Where did the moon rabbit go?";
            Game_TypeWriterEffect.instance.RandomText[15] = "Can I live here?\nNo! The life I want isn’t here!";
            Game_TypeWriterEffect.instance.RandomText[16] = "I'll have to look around slowly...\nIt's like seeing the ruins you see on TV.";
            Game_TypeWriterEffect.instance.RandomText[17] = "What does the engraved pattern on this pillar mean?";
            Game_TypeWriterEffect.instance.RandomText[18] = "I guess they liked pretty things,\nseeing that there was a pattern on the pillar.";
            Game_TypeWriterEffect.instance.RandomText[19] = "Did the residents who lived here make this road?";
            Game_TypeWriterEffect.instance.RandomText[20] = "They must’ve made a mural because they made this road.";
            Game_TypeWriterEffect.instance.RandomText[21] = "Perhaps the moon inhabitants went back\nand forth between the moon and the Earth.";
            Game_TypeWriterEffect.instance.RandomText[22] = "This road must have been like a silk road\nconnecting the moon and the Earth!";
            Game_TypeWriterEffect.instance.RandomText[23] = "There is no oxygen or water here. How did people live?";
            Game_TypeWriterEffect.instance.RandomText[24] = "It's amazing that all living things don't need water\nand oxygen like you do on Earth.";
            Game_TypeWriterEffect.instance.RandomText[25] = "Brave boy,\nyou have reached your destination!";
            Game_TypeWriterEffect.instance.RandomText[26] = "Can I find a way to meet my mom here?";
            Game_TypeWriterEffect.instance.RandomText[27] = "Mom! I’m here!\nI guess Mom isn’t here.";
            Game_TypeWriterEffect.instance.RandomText[28] = "Will everyone believe I'm on the moon?";
            Game_TypeWriterEffect.instance.RandomText[29] = "I'm excited to tell people that I came to the moon!";
            Game_TypeWriterEffect.instance.RandomText[30] = "I wish I could take a picture.\nThen I could post it and let people know that I arrived on the moon!";
            Game_TypeWriterEffect.instance.RandomText[31] = "The mysterious scenery... I'm nervous...\nBut I’m excited enough to want to jump around.";
            Game_TypeWriterEffect.instance.RandomText[32] = "Can I come back here with Mom?";
            Game_TypeWriterEffect.instance.RandomText[33] = "Where is the wishing stone in the mural?";
            Game_TypeWriterEffect.instance.RandomText[34] = "Since when did these buildings exist?";
            Game_TypeWriterEffect.instance.RandomText[35] = "How did the residents feel when they first settled on the moon?\nThey may have been fascinated by the light of the sun reflected on the moon.";
            Game_TypeWriterEffect.instance.RandomText[36] = "Were they satisfied with there lives here?";
            Game_TypeWriterEffect.instance.RandomText[37] = "This is a good place to think\nbecause you can see the Earth and space.";
            Game_TypeWriterEffect.instance.RandomText[38] = "The residents of this place may have been guardians of the wishing stone.";
            Game_TypeWriterEffect.instance.RandomText[39] = "Where did everyone go?";
            Game_TypeWriterEffect.instance.RandomText[40] = "Moon residents may have gone on vacation to Earth.\nI might have missed them because we took different crossroads.";
            Game_TypeWriterEffect.instance.RandomText[41] = "I want to go to their houses and see what kind of life they live.\nBut that’d be rude.";
            Game_TypeWriterEffect.instance.RandomText[42] = "What do moon residents eat?";
            Game_TypeWriterEffect.instance.RandomText[43] = "What do I do if they think I'm here to steal the wishing stone\nand they become hostile to me?";
            Game_TypeWriterEffect.instance.RandomText[44] = "Moon residents, I’m not your enemy.\nI’m just a boy who wants to go home!";
            Game_TypeWriterEffect.instance.RandomText[45] = "... It’s quiet. I don’t hear anything except myself.";
            Game_TypeWriterEffect.instance.RandomText[46] = "I must walk diligently,\nthere must be an altar that was in the mural.";
            Game_TypeWriterEffect.instance.RandomText[47] = "My friends would envy me if they found out that I've been to space.";
            Game_TypeWriterEffect.instance.RandomText[48] = "This is the little shiny dot I've seen on Earth.";
            Game_TypeWriterEffect.instance.RandomText[49] = "... The Earth seen from the moon is\njust as beautiful as the moon seen from the Earth.";
        }
    }
    //필수 라디오 나레이션
    public void EventTextRadio(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "Do you copy- (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "The only boy- (static) regrettable- (static) Next-";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "△△ Corporation- (static) allegation of extortion- (static) denial- (static)";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {

        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- What happened? -";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "Through this incident- (static) colluding with △△ corporation- (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) I miss- (static) you- (static)";

        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "Announce- (static) lost contact- (static) dead- (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "(static) Today the Prosecutors' Office- about the child in the ☆☆ case- (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) We’re- (static) waiting for you (static)";
        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "Humanity issue (static) with children (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "Currently it is (static) involved (static) known to (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) Always (static) next to you (static)";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {
        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- Reply (static) Still can’t get a hold of (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "(static) Interview (static) using a helpless child (static) experiment (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) Take your time (static) Me (static noise)";
        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "- (static) 57 days (static) without any(static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "(static) take place (static) 1st trial (static) rights (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) Love (static) always (static)";
        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "Keep moving (static) don’t break (static) power of explosion (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "did not appear (static) refusal (static) postponed (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) today (static) turned (static) comb your hair (static)";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "(static) but speed (static) must investigate (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "(static) you shouldn’t have done (static)\nif it wasn’t for (static) you talked me into it first (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = " Probably (static) remember (static) disappoint (static) no (static)";

        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "No, (static) their permission (static)\nlook after (static) I’ll look into (static)";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "(static) couldn't deviate from the verdict (static) I think I was the only (static)";
            Game_TypeWriterEffect.instance.EventRadioText[2] = "(static) Baby (static) my baby (static) hug (static)";
        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.EventRadioText = new string[3];
            Game_TypeWriterEffect.instance.EventRadioText[0] = "It's a 99-day survival report. No response. Tomorrow is the last...";
            Game_TypeWriterEffect.instance.EventRadioText[1] = "△△ Corporation is facing decline.\nCEO □□, who was sentenced to imprisonment, said about the spacecraft...";
            Game_TypeWriterEffect.instance.EventRadioText[2] = " I want you to know that I’ll always love you.";
        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) for 100 days (static) no more reception (static) direction of the moon (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) from now on (static) good (static) pretend (static) released soon (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Childhood (static) yes (static) like (static) many times (static) repeatedly read (static)";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) recently (static) orphaned (static) using hope (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "Your paycheck (static) where it comes from (static) unemployed (static) reduced sentence-";
            Game_TypeWriterEffect.instance.RadioText[2] = "Protect (static) can’t (static) baby (static)";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "- Oh, response (static) everyone (static) gather-";
            Game_TypeWriterEffect.instance.RadioText[1] = "What (static) don’t take pic- (static) CEO of △△ Corporation- (static)\nPost bail (static) evil! (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "able to grow (static) yes (static) whatever is possi- (static)";
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
            Game_TypeWriterEffect.instance.RadioText[0] = "- Can you hear me? (static) If you can (static) answer (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "news of being alive (static) how do you feel- (static) answer us!\n(static) the prosecution- (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Not much left (static) you (static) voice (static) soon (static) I’m proud of you-";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) not enough oxygen (static) how- (static) it’s a miracle (static) a young boy (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) have nightmare (static) that boy (static) wrong (static) everything (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Remember (static) memories (static) can’t hear (static) always by your side (static)";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[3];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) arrive on the moon (static) it’s a miracle (static) check (static) not yet (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) admitted to (static) alleged corruption (static)\n△△ Corporation (static) other mergers and acquisitions- (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) I’ll love you always (static) My baby,\nyou can do it on your own.\n(static) Mommy will (static) be a star and\n(static) you always- (static) ";
        }
    }
    //랜덤 라디오 나레이션
    public void EventRandomTextRadio(int wayPoint)
    {
        //   1. 여정의 시작,50
        if (wayPoint.Equals(1))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) Today Seoul- (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "5-lane road traffic congestion (static) expected- ";
            Game_TypeWriterEffect.instance.RadioText[2] = "Hahaha! (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) Yesterday I went to a sauna (static) a friend!";
            Game_TypeWriterEffect.instance.RadioText[4] = "don’t - cry, don’t (static) cry (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) when traveling (static) etiquette (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) on the way to work (static) song request (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "Yes, I see. (static) I’ll play the requested (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "Stars (static) falling (static)";
        }
        //2. 목적없는 발걸음,100
        else if (wayPoint.Equals(2))
        {

        }
        //3. 달의 비밀,150
        else if (wayPoint.Equals(3))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[6];

            Game_TypeWriterEffect.instance.RadioText[0] = "(static) The moon is Earth's - (static)- only natural satellite";
            Game_TypeWriterEffect.instance.RadioText[1] = "For (static) today (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) Twinkle twinkle (static) beautiful (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "Mr. Park received (static) this year’s good citizen (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "One, two, three, baby (static) The truth in your heart (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) sunny after overcast (static) rather high humidity (static)";
        }
        //4. 희망의 끈,200
        else if (wayPoint.Equals(4))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "Oh! Number 1 player (static) Korea (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "----------- Number one! First! He comes in first! (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) Cheer up on your (static) way to work (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "Hedgehogs have (static) a very hard heart (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) Even though I was alone (static) I wasn’t scared- (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) Today’s game (static) everyone (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) but (static) the drawback (static) nobody was with (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "(static) released (static) viewers’ rating (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "(static) a story of (static) a hedgehog reading (static)";

        }
        //5. 길을 잃은 아기별,250
        else if (wayPoint.Equals(5))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[8];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) It’s sunny today... (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) In some places it’ll rain... (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Ring, ring- Happy Monday (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) There has been a terrible accident in Hongdae Station. At the scene (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "I’ll try Greek yogurt which is popular on YouTube- (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) painful (static) faint heart (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "I desperately hoped (static) for someone to (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = " (static) panorama (static) small (static) memory";
        }
        //6. 맴도는 공허함,300
        else if (wayPoint.Equals(6))
        {


        }
        //7. 빛의 무리,350
        else if (wayPoint.Equals(7))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[7];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) boom bop (static) beep beep";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Listening (static) to (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) Well (static) he’s (static) cute (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "Calm down (static) deep breath (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) What (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) Sun (static) explosion (static) change-";
            Game_TypeWriterEffect.instance.RadioText[6] = "longing (static) reunite";


        }
        //8. 수상한 빛,400
        else if (wayPoint.Equals(8))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[8];
            Game_TypeWriterEffect.instance.RadioText[0] = "cleaning (static) pour here and mop (static) done!";
            Game_TypeWriterEffect.instance.RadioText[1] = "easy (static) but (static) you can do";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) This is R point. (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) A portal search word (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) vote (static) in advance (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) Today metropolitan (static) not a single cloud (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) This plug set (static) not manufactured anymore (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "(static) If I can’t have (static) you (static)";

        }
        //9. 나를 도와줘,450
        else if (wayPoint.Equals(9))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[8];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) nervous but (static) I’m actually nervous too (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) humpback whale (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) the missing child (static) 45 years old now (static) expected to look (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) the percentage of vote (static) Oh! (static) yes, he’s ahead (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) lucid dream!\n(static) do you believe in a dream (static)\nit’s different from a lucid dream (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) The Moon has many names (static) crescent moon (static) waxing moon (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) the prosecution (static) demand (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "over a hill (static) without me (static)";
        }
        //10. 불꽃놀이,500
        else if (wayPoint.Equals(10))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) Collection is (static) bringing or gathering (static) objects or- (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) hedgehog (static) friend (static) tired (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) sometimes (static) light hearted (static) friend (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "have (static) no more (static) I wasn’t lonely (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "Today's fortune is bad. (static) be careful (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) Okay (static) I get it.\n(static) Let’s break up. (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) Today planet BF-2 (static) announce (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "For the symbiosis of (static) many creatures (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "(static) the story (static) running to the bathroom (static) hahaha! (static)";

        }
        //11. 소원석,550
        else if (wayPoint.Equals(11))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) Next year (static) your thoughts on (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "March (static) cherry blossoms (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Festival (static) amazing scenery (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) influenza mass stranding of fowls (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "Today's fortune is bad. (static) be careful (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = " (static) after rain (static) sunny (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "The quote (static) of the day (static) a fresh wind and a bright moon";
            Game_TypeWriterEffect.instance.RadioText[7] = "Yes, right. (static) you (static) last time (static) Yes (static) I love you (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "Find Young-jae  parents (static) it’s a happy family (static)";

        }
        //12. 발버둥 치는 마음,600
        else if (wayPoint.Equals(12))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = " Hahaha! Right. That was very awkward!";
            Game_TypeWriterEffect.instance.RadioText[1] = "Here's the news from Busan today.\nPeople are very interested in this international film festival.";
            Game_TypeWriterEffect.instance.RadioText[2] = "I'm Missing You··· bye.";
            Game_TypeWriterEffect.instance.RadioText[3] = "過去の過ちに向き合う時、私たちは選択しなければなりません。";
            Game_TypeWriterEffect.instance.RadioText[4] = "It was a great shock for the scientific community.\nThe desire for the universe still persists...";
            Game_TypeWriterEffect.instance.RadioText[5] = "Yesterday drama ○○ had the highest ratings.";
            Game_TypeWriterEffect.instance.RadioText[6] = "Children's buzzwords are evolving rapidly these days.\nThese are mostly spread through the Internet...";
            Game_TypeWriterEffect.instance.RadioText[7] = "Currently there’s a K-pop craze in foreign countries.\n□□ Idol has proved it by topping the Billboard chart...";
            Game_TypeWriterEffect.instance.RadioText[8] = "New flavor released!\nIf you purchase now, you can participate in the event using QR code!\n1st place...";
            Game_TypeWriterEffect.instance.RadioText[9] = "September 28 ◇◇ horoscope.\nYou can call it whatever you like, whether it be fate or coincidence.\nBecause your life is about to change.";
        }
        //13. 우주를 떠도는 영혼,650
        else if (wayPoint.Equals(13))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[10];
            Game_TypeWriterEffect.instance.RadioText[0] = "children (static) Today is the botanical garden (static) useful for human";
            Game_TypeWriterEffect.instance.RadioText[1] = "○○ has grown so much (static) Oh, then ○○ will go with (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) Both the moon’s (static) in the earthly sk- (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "Kiss me (static) cannot smile (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "Celebrity ○○ (static) on TV show □□ (static) tears";
            Game_TypeWriterEffect.instance.RadioText[5] = "(static) International news today collapsed (static) casualty (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "- (static) hahaha so what happened (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "Look over there (static)- ";
            Game_TypeWriterEffect.instance.RadioText[8] = "Stop calling (static) I’m still angry (static)";
            Game_TypeWriterEffect.instance.RadioText[9] = "It’s fresh (static) local produce (static) reasonable price (static)";
        }
        //14. 함께하는 여정,700
        else if (wayPoint.Equals(14))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[8];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) blueberry (static) good for (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Boom! (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Goodness. It’s pretty (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "We thought (static) to be a star (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) the invention of train (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "Together (static) someone (static) big comfort (static) today’s traffic (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) that’s all the news for (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "(static) beep (static) bop (static) bee beep (static)";
        }
        //15. 목걸이의 주인,750
        else if (wayPoint.Equals(15))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) heart (static) you’re into me (static) melted-";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Morse code (static) ex-scout (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) strong sun (static) the UV rate (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "Twinkle twinkle little friend (static) warm (static) don’t fly away stay with me- (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) this is the moment (static) this is the one (static) become one";
            Game_TypeWriterEffect.instance.RadioText[5] = "Vast wilderness (static) life (static) this world (static) stop!";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) Bang (static) Whoosh (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "In space (static) no medium (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "(static) Athlete △△ will retire (static) due to injury (static)";
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
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "As expected (static) even if I don’t go (static) always do the best (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Yes, this grill (static) barbecue meat (static) make different noise (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "(static) after 60 seconds (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "guarantee (static) life time interest guarantee (static) terms and (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "(static) voice phishing (static) pretending to be the son (static)";
            Game_TypeWriterEffect.instance.RadioText[5] = "Goodness, look at her. (static) ○○ is △△’s daughter (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "(static) It’s nothing (static) time is (static) last bank account (static)";
            Game_TypeWriterEffect.instance.RadioText[7] = "I always wondered (static) being loved (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "(static) Today’s accidents and incidents (static) sinkhole (static) fire (static) robbery (static)";
        }
        //19. 색을 잃은 별,950
        else if (wayPoint.Equals(19))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[9];
            Game_TypeWriterEffect.instance.RadioText[0] = "4-member (static) remembered forever (static) next concert in Korea is scheduled (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Terrible aftermath (static) war (static) orphans (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "Need a lot of help (static) hope (static) sponsor (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) nothing (static) starry night (static) break up (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "Moon is (static) luna, lune, moon, various names (static)-";
            Game_TypeWriterEffect.instance.RadioText[5] = "Beep (static) now (static) almost (static)";
            Game_TypeWriterEffect.instance.RadioText[6] = "The first encounter (static) such a change apocalyptist (static) unable";
            Game_TypeWriterEffect.instance.RadioText[7] = "Apocalyptists (static) said (static) to feel (static) using psychologi- (static)";
            Game_TypeWriterEffect.instance.RadioText[8] = "Playing is the most fun (static) always fun (static)\nwhat will happen tomorrow (static)";
        }
        //20. 달의 신전,1000
        else if (wayPoint.Equals(20))
        {
            Game_TypeWriterEffect.instance.RadioText = new string[5];
            Game_TypeWriterEffect.instance.RadioText[0] = "(static) What is a breakup?\n(static) Breaking up (static) isn’t the end (static)";
            Game_TypeWriterEffect.instance.RadioText[1] = "(static) Good night (static) So long see you again (static)";
            Game_TypeWriterEffect.instance.RadioText[2] = "The best (static) longing (static)";
            Game_TypeWriterEffect.instance.RadioText[3] = "(static) today’s (static) sunny no traffic (static)";
            Game_TypeWriterEffect.instance.RadioText[4] = "Likely going through breakup (static)\ntoday’s lucky item is (static) memories. (static)";
        }
    }

}