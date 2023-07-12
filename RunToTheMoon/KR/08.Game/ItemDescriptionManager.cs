using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionManager : MonoBehaviour
{
    //블루, 오렌지, 그린, 퍼플, 옐로,
    public GameObject ItemDescription_ob;//비상식량
    public GameObject[] Snack;
    public GameObject[] Can;
    public GameObject[] OxygenTank_small;
    public GameObject[] OxygenTank_big;
    public GameObject[] StarDust;  //별가루
    public GameObject MoonPowder; //달가루
    [Header("이벤트 대한 세부 설명")]
    public GameObject SpaceRadio;  //우주라디오
    public GameObject Torch; //횟불
    public GameObject LightBird; //빛새
    public GameObject MoonSpore; //달의 포자

    public GameObject Thread; //달의 실타래
    //벽화
    public GameObject Mural3_1; //달과 지구의 왕래
    public GameObject Mural3_2; //조각함
    public GameObject Mural11_1; //달의 신전
    public GameObject Mural11_2; //소원석
    //
    public GameObject SpacecraftDamage; //부서진 우주선
    public GameObject Necklace; //목걸이
    public GameObject SpaceFlower; // 달의 꽃
    public GameObject Net;//빛의 망
    public GameObject Flashlight; //손전등
    public GameObject SpacePlantBook; // 우주 식물도감
    public GameObject Letter; //누군가의 편지
    //
    public GameObject MyFavorite_bed; // 나의 침대
    public GameObject MyFavorite_Icecream; // 아이스크림
    public GameObject MyFavorite_book; // 가장 좋아했던 책
    public GameObject MyFavorite_pinwheel; // 바람개비
    public GameObject MyAlbum;


    [Header("달의 조각")]
    public GameObject[] MoonPices; //조각의 색깔 
    public Text count_txt;
    void Start()
    {
        //모든 스낵관련
        for (int i = 0; i < Snack.Length; i++)
        {
            Snack[i].SetActive(false);
        }
        for (int j = 0; j < Can.Length; j++)
        {
            Can[j].SetActive(false);
        }
        for (int j = 0; j < OxygenTank_small.Length; j++)
        {
            OxygenTank_small[j].SetActive(false);
        }
        for (int j = 0; j < OxygenTank_big.Length; j++)
        {
            OxygenTank_big[j].SetActive(false);
        }
        for (int j = 0; j < StarDust.Length; j++)
        {
            StarDust[j].SetActive(false);
        }
        if (Game_TypeWriterEffect.instance.wayPoint.Equals(1))
        {
            if (SpaceRadio.activeSelf.Equals(true)) { SpaceRadio.SetActive(false); }
        }
    }
    public void Show_ItemDescription(string DescriptionName)
    {
        Debug.Log("Show_ItemDescription: " + DescriptionName);
        StartCoroutine(_Show_ItemDescription(DescriptionName));

    }
    IEnumerator _Show_ItemDescription(string DescriptionName)
    {
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_TypeWriterEffect.instance.Event_ing = true; //설명중에 나레이션 나오는것을 막기 위해서 넣음
        Debug.Log("descriptionName: " + DescriptionName);
        yield return new WaitForSeconds(1f);
        ItemDescription_ob.SetActive(true);

        if (DescriptionName.Equals("SpaceRadio")) { SpaceRadio.SetActive(true); }
        else if (DescriptionName.Equals("Torch")) { Torch.SetActive(true); }
        else if (DescriptionName.Equals("LightBird")) { LightBird.SetActive(true); }
        else if (DescriptionName.Equals("MoonSpore")) { MoonSpore.SetActive(true); }
        else if (DescriptionName.Equals("Thread")) { Thread.SetActive(true); }
        else if (DescriptionName.Equals("Mural3_1")) { Mural3_1.SetActive(true); }
        else if (DescriptionName.Equals("Mural3_2")) { Mural3_2.SetActive(true); }
        else if (DescriptionName.Equals("Mural11_1")) { Mural11_1.SetActive(true); }
        else if (DescriptionName.Equals("Mural11_2")) { Mural11_2.SetActive(true); }
        else if (DescriptionName.Equals("SpacecraftDamage")) { SpacecraftDamage.SetActive(true); }
        else if (DescriptionName.Equals("Necklace")) { Necklace.SetActive(true); }
        else if (DescriptionName.Equals("SpaceFlower")) { SpaceFlower.SetActive(true); }
        else if (DescriptionName.Equals("Net")) { Net.SetActive(true); }
        else if (DescriptionName.Equals("Flashlight")) { Flashlight.SetActive(true); }
        else if (DescriptionName.Equals("SpacePlantBook")) { SpacePlantBook.SetActive(true); }
        else if (DescriptionName.Equals("Letter")) { Letter.SetActive(true); }

        else if (DescriptionName.Equals("MyFavorite_bed")) { MyFavorite_bed.SetActive(true); }
        else if (DescriptionName.Equals("MyFavorite_Icecream")) { MyFavorite_Icecream.SetActive(true); }
        else if (DescriptionName.Equals("MyFavorite_book")) { MyFavorite_book.SetActive(true); }
        else if (DescriptionName.Equals("MyFavorite_pinwheel")) { MyFavorite_pinwheel.SetActive(true); }
        else if (DescriptionName.Equals("MyAlbum")) { MyFavorite_pinwheel.SetActive(true); }

        //달의 조각
        else if (DescriptionName.Equals("MoonPices_Sky")) { MoonPices[0].SetActive(true); }// 3 달의 비밀
        else if (DescriptionName.Equals("MoonPices_LightGreen")) { MoonPices[1].SetActive(true); } //5 길 잃은 아기별
        else if (DescriptionName.Equals("MoonPices_Green")) { MoonPices[2].SetActive(true); } //7 빛의 무리
        else if (DescriptionName.Equals("MoonPices_Yellow")) { MoonPices[3].SetActive(true); }  //9 우주 꽃 살리기
        else if (DescriptionName.Equals("MoonPices_Red")) { MoonPices[4].SetActive(true); }//11 소원석
        else if (DescriptionName.Equals("MoonPices_Pink")) { MoonPices[5].SetActive(true); }//13 우주를 떠도는 영혼
        else if (DescriptionName.Equals("MoonPices_Purple")) { MoonPices[6].SetActive(true); }//15 목걸이 주인
        else if (DescriptionName.Equals("MoonPices_Blue")) { MoonPices[7].SetActive(true); }//18 점점 더 가까이
        else if (DescriptionName.Equals("MoonPices_White")) { MoonPices[8].SetActive(true); }//19색을 잃은 빛
        else if (DescriptionName.Equals("MoonPices_Black")) { MoonPices[9].SetActive(true); }//20 달의 신전
        else if (DescriptionName.Equals("MoonPowder")) { MoonPowder.SetActive(true); }//달가루 

        //산소통
        else if (DescriptionName.Equals("OxygenTank_small_New")) { OxygenTank_small[0].SetActive(true); }
        else if (DescriptionName.Equals("OxygenTank_small_Old")) { OxygenTank_small[1].SetActive(true); }
        else if (DescriptionName.Equals("OxygenTank_big_New")) { OxygenTank_big[0].SetActive(true); }
        else if (DescriptionName.Equals("OxygenTank_big_Old")) { OxygenTank_big[1].SetActive(true); }
        //비상식량
        else if (DescriptionName.Equals("Snack_blue")) { Snack[0].SetActive(true); }
        else if (DescriptionName.Equals("Snack_orange")) { Snack[1].SetActive(true); }
        else if (DescriptionName.Equals("Snack_green")) { Snack[2].SetActive(true); }
        else if (DescriptionName.Equals("Snack_purple")) { Snack[3].SetActive(true); }
        else if (DescriptionName.Equals("Snack_yellow")) { Snack[4].SetActive(true); }
        //통조림
        else if (DescriptionName.Equals("Can_blue")) { Can[0].SetActive(true); }
        else if (DescriptionName.Equals("Can_orange")) { Can[1].SetActive(true); }
        else if (DescriptionName.Equals("Can_green")) { Can[2].SetActive(true); }
        else if (DescriptionName.Equals("Can_purple")) { Can[3].SetActive(true); }
        else if (DescriptionName.Equals("Can_yellow")) { Can[4].SetActive(true); }
        //별가루
        else if (DescriptionName.Equals("StarDust_New")) { StarDust[0].SetActive(true); }
        else if (DescriptionName.Equals("StarDust_Empty")) { StarDust[1].SetActive(true); }
        count_txt.gameObject.SetActive(true);
        SoundFunction.Instance.ItemDescription_Sound();
        for (int i = 5; i > 0; i--)
        {
            count_txt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        ItemDescription_ob.SetActive(false);
        if (DescriptionName.Equals("SpaceRadio")) { SpaceRadio.SetActive(false); }
        else if (DescriptionName.Equals("Torch")) { Torch.SetActive(false); }
        else if (DescriptionName.Equals("LightBird")) { LightBird.SetActive(false); }
        else if (DescriptionName.Equals("MoonSpore")) { MoonSpore.SetActive(false); }
        else if (DescriptionName.Equals("Thread")) { Thread.SetActive(false); }
        else if (DescriptionName.Equals("Mural3_1")) { Mural3_1.SetActive(false); }
        else if (DescriptionName.Equals("Mural3_2")) { Mural3_2.SetActive(false); }
        else if (DescriptionName.Equals("Mural11_1")) { Mural11_1.SetActive(false); }
        else if (DescriptionName.Equals("Mural11_2")) { Mural11_2.SetActive(false); }
        else if (DescriptionName.Equals("SpacecraftDamage")) { SpacecraftDamage.SetActive(false); }
        else if (DescriptionName.Equals("Necklace")) { Necklace.SetActive(false); }
        else if (DescriptionName.Equals("SpaceFlower")) { SpaceFlower.SetActive(false); }
        else if (DescriptionName.Equals("Net")) { Net.SetActive(false); }
        else if (DescriptionName.Equals("Flashlight")) { Flashlight.SetActive(false); }
        else if (DescriptionName.Equals("SpacePlantBook")) { SpacePlantBook.SetActive(false); }
        else if (DescriptionName.Equals("Letter")) { Letter.SetActive(false); }

        else if (DescriptionName.Equals("MyFavorite_bed")) { MyFavorite_bed.SetActive(false); }
        else if (DescriptionName.Equals("MyFavorite_Icecream")) { MyFavorite_Icecream.SetActive(false); }
        else if (DescriptionName.Equals("MyFavorite_book")) { MyFavorite_book.SetActive(false); }
        else if (DescriptionName.Equals("MyAlbum")) { MyFavorite_book.SetActive(false); }
        else if (DescriptionName.Equals("MyFavorite_pinwheel")) { MyFavorite_pinwheel.SetActive(false); }
        //달의 조각
        else if (DescriptionName.Equals("MoonPices_Sky")) { MoonPices[0].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_LightGreen")) { MoonPices[1].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Green")) { MoonPices[2].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Yellow")) { MoonPices[3].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Red")) { MoonPices[4].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Pink")) { MoonPices[5].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Purple")) { MoonPices[6].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Blue")) { MoonPices[7].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_White")) { MoonPices[8].SetActive(false); }
        else if (DescriptionName.Equals("MoonPices_Black")) { MoonPices[9].SetActive(false); }
        else if (DescriptionName.Equals("MoonPowder")) { MoonPowder.SetActive(false); }//달가루 

        //산소통
        else if (DescriptionName.Equals("OxygenTank_small_New")) { OxygenTank_small[0].SetActive(false); }
        else if (DescriptionName.Equals("OxygenTank_small_Old")) { OxygenTank_small[1].SetActive(false); }
        else if (DescriptionName.Equals("OxygenTank_big_New")) { OxygenTank_big[0].SetActive(false); }
        else if (DescriptionName.Equals("OxygenTank_big_Old")) { OxygenTank_big[1].SetActive(false); }
        //비상식량
        else if (DescriptionName.Equals("Snack_blue")) { Snack[0].SetActive(false); }
        else if (DescriptionName.Equals("Snack_orange")) { Snack[1].SetActive(false); }
        else if (DescriptionName.Equals("Snack_green")) { Snack[2].SetActive(false); }
        else if (DescriptionName.Equals("Snack_purple")) { Snack[3].SetActive(false); }
        else if (DescriptionName.Equals("Snack_yellow")) { Snack[4].SetActive(false); }
        //통조림
        else if (DescriptionName.Equals("Can_blue")) { Can[0].SetActive(false); }
        else if (DescriptionName.Equals("Can_orange")) { Can[1].SetActive(false); }
        else if (DescriptionName.Equals("Can_green")) { Can[2].SetActive(false); }
        else if (DescriptionName.Equals("Can_purple")) { Can[3].SetActive(false); }
        else if (DescriptionName.Equals("Can_yellow")) { Can[4].SetActive(false); }
        //별가루
        else if (DescriptionName.Equals("StarDust_New")) { StarDust[0].SetActive(false); }
        else if (DescriptionName.Equals("StarDust_Empty")) { StarDust[1].SetActive(false); }
        Game_TypeWriterEffect.instance.Event_ing = false;
        yield return null;
    }
}
