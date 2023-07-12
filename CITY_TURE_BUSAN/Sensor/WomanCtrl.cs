using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanCtrl : MonoBehaviour
{
    public Material skinMat;
    public Material hairMat;

    MaterialPropertyBlock propertyBlock;
    MaterialPropertyBlock jacketProperty;

    public Renderer woman_Jacket_Rend;

    public Renderer[] woman_Body_Rend;  //여자 몸
    public Renderer[] woman_Skin_Rend;  //여자 피부
    public Renderer[] woman_Hair_Rend;  //여자 머리 //0,1:아시아 / 2,3:서양 / 4:흑인


    public GameObject[] hairObj;
    public GameObject[] bodyObj;
    public GameObject[] jacketObj;
    public GameObject[] pantsObj;
    public GameObject[] shoesObj;
    public GameObject[] helmelObj;
    public GameObject[] glovesObj;
    public GameObject bicycle;



    [Header("애니메이션")]
    public Animator[] woman_HairAnim;   //동서흑 순서 머리 애니(서양 애니는 기본이라서 기본으로 깔고 가야함)
    public Animator[] woman_BodyAnim;   //동서흑 순서 몸 애니(서양 애니는 기본이라서 기본으로 깔고 가야함)
    public Animator woman_BicycleAnim;  //자전거 애니
    public Animator[] woman_JacketAnim;   //상의
    public Animator[] woman_PantsAnim; //하의
    public Animator[] woman_ShoesAnim; //신발
    public Animator[] woman_HelmetAnim;  //헬맷
    public Animator[] woman_GlovesAnim;   //장갑

    [Header("오브젝트 텍스쳐")]
    public SkinnedMeshRenderer[] woman_JacketTextrue;   //상의 
    public SkinnedMeshRenderer[] woman_PantsTextrue;    //하의
    public SkinnedMeshRenderer[] woman_ShoesTextrue;    //신발
    public SkinnedMeshRenderer[] woman_HelmetTextrue;   //헬맷
    public SkinnedMeshRenderer[] woman_glovesTextrue;   //장갑 
    public MeshRenderer[] woman_BikeTexture;    //자전거




    public void Animator_Initialization(int _hair, int _body, int _jacket, int _pants, int _shoes)
    {
        //Debug.Log("_shoes " + _shoes);
        woman_BicycleAnim.Rebind(); woman_HairAnim[_hair].Rebind(); woman_BodyAnim[_body].Rebind();
        woman_JacketAnim[_jacket].Rebind(); woman_PantsAnim[_pants].Rebind();
        woman_ShoesAnim[_shoes].Rebind(); woman_HelmetAnim[0].Rebind(); woman_GlovesAnim[0].Rebind();
    }

    //속도 빠른 애니메이션
    public void Animator_Speed(bool _normalState, bool _speedStat, int _hair, int _body, int _jacket, int _pants, int _shoes, float _speed)
    {
        woman_BicycleAnim.SetFloat("Speed", _speed);
        woman_BicycleAnim.SetBool("NormalStart", _normalState);
        woman_BicycleAnim.SetBool("SpeedStart", _speedStat);

        woman_HairAnim[_hair].SetFloat("Speed", _speed);
        woman_HairAnim[_hair].SetBool("NormalStart", _normalState);
        woman_HairAnim[_hair].SetBool("SpeedStart", _speedStat);

        woman_BodyAnim[_body].SetFloat("Speed", _speed);
        woman_BodyAnim[_body].SetBool("NormalStart", _normalState);
        woman_BodyAnim[_body].SetBool("SpeedStart", _speedStat);

        woman_JacketAnim[_jacket].SetFloat("Speed", _speed);
        woman_JacketAnim[_jacket].SetBool("NormalStart", _normalState);
        woman_JacketAnim[_jacket].SetBool("SpeedStart", _speedStat);

        woman_PantsAnim[_pants].SetFloat("Speed", _speed);
        woman_PantsAnim[_pants].SetBool("NormalStart", _normalState);
        woman_PantsAnim[_pants].SetBool("SpeedStart", _speedStat);

        woman_ShoesAnim[_shoes].SetFloat("Speed", _speed);
        woman_ShoesAnim[_shoes].SetBool("NormalStart", _normalState);
        woman_ShoesAnim[_shoes].SetBool("SpeedStart", _speedStat);

        woman_HelmetAnim[0].SetFloat("Speed", _speed);
        woman_HelmetAnim[0].SetBool("NormalStart", _normalState);
        woman_HelmetAnim[0].SetBool("SpeedStart", _speedStat);

        woman_GlovesAnim[0].SetFloat("Speed", _speed);
        woman_GlovesAnim[0].SetBool("NormalStart", _normalState);
        woman_GlovesAnim[0].SetBool("SpeedStart", _speedStat);
    }

    public void AnimClear_Fail(float _speed, bool _normalState, bool _speedState, bool _clearState, bool _failState, int _hair, int _body, int _jacket, int _pants, int _shoes)
    {
        woman_BicycleAnim.SetFloat("Speed", _speed);
        woman_BicycleAnim.SetBool("NormalStart", _normalState);
        woman_BicycleAnim.SetBool("SpeedStart", _speedState);
        woman_BicycleAnim.SetBool("ClearStart", _clearState);
        woman_BicycleAnim.SetBool("FailStart", _failState);

        woman_HairAnim[_hair].SetFloat("Speed", _speed);
        woman_HairAnim[_hair].SetBool("NormalStart", _normalState);
        woman_HairAnim[_hair].SetBool("SpeedStart", _speedState);
        woman_HairAnim[_hair].SetBool("ClearStart", _clearState);
        woman_HairAnim[_hair].SetBool("FailStart", _failState);

        woman_BodyAnim[_body].SetFloat("Speed", _speed);
        woman_BodyAnim[_body].SetBool("NormalStart", _normalState);
        woman_BodyAnim[_body].SetBool("SpeedStart", _speedState);
        woman_BodyAnim[_body].SetBool("ClearStart", _clearState);
        woman_BodyAnim[_body].SetBool("FailStart", _failState);

        woman_JacketAnim[_jacket].SetFloat("Speed", _speed);
        woman_JacketAnim[_jacket].SetBool("NormalStart", _normalState);
        woman_JacketAnim[_jacket].SetBool("SpeedStart", _speedState);
        woman_JacketAnim[_jacket].SetBool("ClearStart", _clearState);
        woman_JacketAnim[_jacket].SetBool("FailStart", _failState);

        woman_PantsAnim[_pants].SetFloat("Speed", _speed);
        woman_PantsAnim[_pants].SetBool("NormalStart", _normalState);
        woman_PantsAnim[_pants].SetBool("SpeedStart", _speedState);
        woman_PantsAnim[_pants].SetBool("ClearStart", _clearState);
        woman_PantsAnim[_pants].SetBool("FailStart", _failState);

        woman_ShoesAnim[_shoes].SetFloat("Speed", _speed);
        woman_ShoesAnim[_shoes].SetBool("NormalStart", _normalState);
        woman_ShoesAnim[_shoes].SetBool("SpeedStart", _speedState);
        woman_ShoesAnim[_shoes].SetBool("ClearStart", _clearState);
        woman_ShoesAnim[_shoes].SetBool("FailStart", _failState);

        woman_HelmetAnim[0].SetFloat("Speed", _speed);
        woman_HelmetAnim[0].SetBool("NormalStart", _normalState);
        woman_HelmetAnim[0].SetBool("SpeedStart", _speedState);
        woman_HelmetAnim[0].SetBool("ClearStart", _clearState);
        woman_HelmetAnim[0].SetBool("FailStart", _failState);

        woman_GlovesAnim[0].SetFloat("Speed", _speed);
        woman_GlovesAnim[0].SetBool("NormalStart", _normalState);
        woman_GlovesAnim[0].SetBool("SpeedStart", _speedState);
        woman_GlovesAnim[0].SetBool("ClearStart", _clearState);
        woman_GlovesAnim[0].SetBool("FailStart", _failState);
    }
    

    public void Animator_Test(int _jacket)
    {
        woman_JacketAnim[_jacket].Rebind();
    }

    void Start()
    {
        Initialization();
        propertyBlock = new MaterialPropertyBlock();
        jacketProperty = new MaterialPropertyBlock();
        //Animator_Declaration(); //선언
    }

    void Initialization()
    {
        //머리 세팅
        if(PlayerPrefs.GetString("Busan_Player_Hair") == "Hair1")
        {
            HairSetting(true, false, false);
        }
        else if(PlayerPrefs.GetString("Busan_Player_Hair") == "Hair2")
        {
            HairSetting(false, true, false);
        }
        else if(PlayerPrefs.GetString("Busan_Player_Hair") == "Hair3")
        {
            HairSetting(false, false, true);
        }

        //몸 세팅
        if(PlayerPrefs.GetString("Busan_Player_Face") == "Asian")
        {
            BodySetting(true, false, false);
        }
        else if(PlayerPrefs.GetString("Busan_Player_Face") == "White")
        {
            BodySetting(false, true, false);
        }
        else if(PlayerPrefs.GetString("Busan_Player_Face") == "Black")
        {
            BodySetting(false, false, true);
        }

        ////아이템 세팅
        //장갑
        if(PlayerPrefs.GetString("Busan_Wear_GlovesKind") == "No")
        {
            glovesObj[0].SetActive(false);    //장갑없음
        }
        else if(PlayerPrefs.GetString("Busan_Wear_GlovesKind") == "ItemID_Gloves")
        {
            glovesObj[0].SetActive(true); //장값 있음
        }
        //헬맷
        if(PlayerPrefs.GetString("Busan_Wear_HelmetKind") == "No")
        {
            helmelObj[0].SetActive(false);    //헬맷없음
        }
        else if(PlayerPrefs.GetString("Busan_Wear_HelmetKind") == "ItemID_Helmet")
        {
            helmelObj[0].SetActive(true); //헬맷잇음
        }
        //상의
        if(PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_Nais")
        {
            JacketSetting(true, false, false);  //기본나시
        }
        else if(PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_Tshirt")
        {
            JacketSetting(false, true, false);  //반팔
        }
        else if(PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_LongShirt")
        {
            JacketSetting(false, false, true);  //긴팔
        }
        //하의
        if(PlayerPrefs.GetString("Busan_Wear_PantsKind") == "ItemID_Short")
        {
            PantsSetting(true, false);  //반바지
        }
        else if(PlayerPrefs.GetString("Busan_Wear_PantsKind") == "ItemID_LongPants")
        {
            PantsSetting(false, true);  //긴바지
        }
        //신발
        if(PlayerPrefs.GetString("Busan_Wear_ShoesKind") == "ItemID_Sandal")
        {
            ShoesSetting(true, false);  //샌들
        }
        else if(PlayerPrefs.GetString("Busan_Wear_ShoesKind") == "ItemID_Shoes")
        {
            ShoesSetting(false, true);  //신발
        }

    }

    
    //선택한 해당 머리 활성화
    public void HairSetting(bool _asian, bool _westerner, bool _african)
    {
        hairObj[0].SetActive(_asian);   //묶음
        hairObj[1].SetActive(_westerner);   //올림
        hairObj[2].SetActive(_african); //단발
    }

    
    public void BodySetting(bool _asian, bool _westerner, bool _african)
    {
        Debug.Log("_asian " + _asian + " _westerner " + _westerner + " _african " + _african);
        bodyObj[0].SetActive(_asian);   //황색
        bodyObj[1].SetActive(_westerner);   //백색
        bodyObj[2].SetActive(_african); //흑색
    }

    //선택한 해당 상의 활성화
    public void JacketSetting(bool _basic, bool _short, bool _long)
    {
        jacketObj[0].SetActive(_basic); //나시
        jacketObj[1].SetActive(_short); //반팔
        jacketObj[2].SetActive(_long);  //긴팔
    }

    //선택한 해당 바지 활성화
    public void PantsSetting(bool _basic, bool _long)
    {
        pantsObj[0].SetActive(_basic);  //반바지
        pantsObj[1].SetActive(_long);   //긴바지
    }

    //선택한 해당 신발 활성화
    public void ShoesSetting(bool _sandal, bool _shoes)
    {
        shoesObj[0].SetActive(_sandal); //샌들
        shoesObj[1].SetActive(_shoes);  //신발
    }

    //선택한 해당 헬맷 활성화
    public void HelmetSetting(bool _helmet)
    {
        helmelObj[0].SetActive(_helmet);
    }

    //선택한 해당 장갑 활성화
    public void GlovesSetting(bool _gloves)
    {
        glovesObj[0].SetActive(_gloves);
    }


    //상의 스타일에 따른 텍스쳐 변경
    public void JacketTextrueSetting(string _style, int _jacketIndex)
    {
        //Texture text = Resources.Load<Texture>("Item Texture/"+ _style);
        Texture textrue = Resources.Load<Texture>("Item Texture/Jacket/Woman/" + _style);
        woman_JacketTextrue[_jacketIndex].material.color = Color.white;
        woman_JacketTextrue[_jacketIndex].material.mainTexture = textrue;

        woman_JacketTextrue[_jacketIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        woman_JacketTextrue[_jacketIndex].material.SetTexture("_BaseMap", textrue);
    }

    //하의 스타일에 따른 텍스쳐 변경
    public void PantsTextrueSetting(string _style, int _pantsIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Pants/Woman/" + _style);
        woman_PantsTextrue[_pantsIndex].material.color = Color.white;
        woman_PantsTextrue[_pantsIndex].material.mainTexture = textrue;

        woman_PantsTextrue[_pantsIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        woman_PantsTextrue[_pantsIndex].material.SetTexture("_BaseMap", textrue);
    }

    //신발 스타일에 따른 텍스쳐 변경
    public void ShoesTextrueSetting(string _style, int _shoesIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Shoes/Woman/" + _style);
        

        if (_style == "BasicSandal")
        {
            woman_ShoesTextrue[_shoesIndex].material.color = Color.white;
            woman_ShoesTextrue[_shoesIndex].material.mainTexture = textrue;
            woman_ShoesTextrue[_shoesIndex + 1].material.color = Color.white;
            woman_ShoesTextrue[_shoesIndex + 1].material.mainTexture = textrue;


            woman_ShoesTextrue[_shoesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
            woman_ShoesTextrue[_shoesIndex].material.SetTexture("_BaseMap", textrue);
            woman_ShoesTextrue[_shoesIndex + 1].material.SetColor("_BaseColor", new Color(1, 1, 1));
            woman_ShoesTextrue[_shoesIndex + 1].material.SetTexture("_BaseMap", textrue);
        }
        else
        {
            woman_ShoesTextrue[_shoesIndex].material.color = Color.white;
            woman_ShoesTextrue[_shoesIndex].material.mainTexture = textrue;

            woman_ShoesTextrue[_shoesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
            woman_ShoesTextrue[_shoesIndex].material.SetTexture("_BaseMap", textrue);
        }
    }

    //헬맷 스타일에 따른 텍스쳐 변경
    public void HelmetTextrueSetting(string _style, int _helemtIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Helmet/Woman/" + _style);
        woman_HelmetTextrue[_helemtIndex].material.color = Color.white;
        woman_HelmetTextrue[_helemtIndex].material.mainTexture = textrue;

        woman_HelmetTextrue[_helemtIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        //propertyBlock.SetTexture("_BaseMap", textrue);
        woman_HelmetTextrue[_helemtIndex].material.SetTexture("_BaseMap", textrue);
    }

    public void GlovesTextrueSetting(string _style, int _glovesIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Gloves/Woman/" + _style);
        woman_glovesTextrue[_glovesIndex].material.color = Color.white;
        woman_glovesTextrue[_glovesIndex].material.mainTexture = textrue;

        woman_glovesTextrue[_glovesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        //propertyBlock.SetTexture("_BaseMap", textrue);
        woman_glovesTextrue[_glovesIndex].material.SetTexture("_BaseMap", textrue);
    }

    public void BicycleTextrueSetting(string _style, int _bikeIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Bicycle/Woman/" + _style);

        for(int i = 0; i < woman_BikeTexture.Length; i++)
        {
            woman_BikeTexture[i].material.color = Color.white;
            woman_BikeTexture[i].material.mainTexture = textrue;

            woman_BikeTexture[i].material.SetColor("_BaseColor", new Color(1, 1, 1));
            //propertyBlock.SetTexture("_BaseMap", textrue);
            woman_BikeTexture[i].material.SetTexture("_BaseMap", textrue);
        }
        
    }


    //플레이어 피부색 초기화
    public void PlayerSkinColorInit(string _skinColor)
    {
        if (_skinColor == "FaceBlack")
        {
            skinMat.color = new Color(0.2f, 0.1f, 0.09f);    //흑색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.09f));
            woman_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
        }
        else if (_skinColor == "FaceBrown")
        {
            skinMat.color = new Color(0.43f, 0.31f, 0.24f);  //갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.43f, 0.31f, 0.24f));
            woman_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
        } 
        else if (_skinColor == "FacePaleBrown")
        {
            skinMat.color = new Color(0.71f, 0.47f, 0.37f);  //연갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.71f, 0.47f, 0.37f));
            woman_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
        } 
        else if (_skinColor == "FaceApricot")
        {
            skinMat.color = new Color(1f, 0.6f, 0.45f);  //살색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
            woman_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
        }  
        else if (_skinColor == "FaceWhite")
        {
            skinMat.color = new Color(1f, 0.7f, 0.6f);   //백색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(1f, 0.7f, 0.6f));
            woman_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
        }
    }

    //플레이어 머리색 초기화
    public void PlayerHairColorInit(string _hairColor)
    {
        if (_hairColor == "HairBlack")
        {
            hairMat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.07f, 0.07f, 0.07f));

            PropertyBlackHairRendColorChange(); //머리색 변경
        }  
        else if (_hairColor == "HairRedBrown")
        {
            hairMat.color = new Color(0.46f, 0.06f, 0.08f);   //붉은갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.46f, 0.06f, 0.08f));

            PropertyBlackHairRendColorChange(); //머리색 변경
        }
        else if (_hairColor == "HairBrown")
        {
            hairMat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));

            PropertyBlackHairRendColorChange(); //머리색 변경
        } 
        else if (_hairColor == "HairYellowBrown")
        {
            hairMat.color = new Color(0.39f, 0.27f, 0.14f);  //똥색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.39f, 0.27f, 0.14f));

            PropertyBlackHairRendColorChange(); //머리색 변경
        }
        else if (_hairColor == "HairYellow")
        {
            hairMat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.65f, 0.26f));

            PropertyBlackHairRendColorChange(); //머리색 변경
        }
    }

    void PropertyBlackHairRendColorChange()
    {
        if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair1")
        {
            woman_Hair_Rend[0].SetPropertyBlock(propertyBlock);
            woman_Hair_Rend[1].SetPropertyBlock(propertyBlock);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair2")
        {
            woman_Hair_Rend[2].SetPropertyBlock(propertyBlock);
            woman_Hair_Rend[3].SetPropertyBlock(propertyBlock);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair3")
        {
            woman_Hair_Rend[4].SetPropertyBlock(propertyBlock);
        }
    }
}
