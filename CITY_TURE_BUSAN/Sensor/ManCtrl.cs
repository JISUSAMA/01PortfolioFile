using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCtrl : MonoBehaviour
{
    public Material skinMat;
    public Material hairMat;

    public Renderer man_Body_Rend;    //남자 몸
    public Renderer[] man_Skin_Rend;    //남자 피부
    public Renderer[] man_Hair_Rend;    //남자 머리
    MaterialPropertyBlock propertyBlock;

    public GameObject[] hairObj;
    public GameObject[] faceObj;
    public GameObject[] jacketObj;
    public GameObject[] pantsObj;
    public GameObject[] shoesObj;
    public GameObject[] helmelObj;
    public GameObject[] glovesObj;
    public GameObject bicycle;

    [Header("애니메이션")]
    public Animator man_BicycleAnim;  //자전거 애니
    public Animator man_bodyAnim;   //몸 애니
    public Animator[] man_HairAnim;   //동서흑 순서 머리 애니(서양 애니는 기본이라서 기본으로 깔고 가야함)
    public Animator[] man_FaceAnim;   //동서흑 순서 얼굴 애니(서양 애니는 기본이라서 기본으로 깔고 가야함)
    public Animator[] man_JacketAnim;   //상의
    public Animator[] man_PantsAnim; //하의
    public Animator[] man_ShoesAnim; //신발
    public Animator[] man_HelmetAnim;  //헬맷
    public Animator[] man_GlovesAnim;   //장갑

    [Header("오브젝트 텍스쳐")]
    public SkinnedMeshRenderer[] man_JacketTextrue;   //상의 
    public SkinnedMeshRenderer[] man_PantsTextrue;    //하의
    public SkinnedMeshRenderer[] man_ShoesTextrue;    //신발
    public SkinnedMeshRenderer[] man_HelmetTextrue;   //헬맷
    public SkinnedMeshRenderer[] man_glovesTextrue;   //장갑 
    public MeshRenderer[] man_BikeTexture;   //자전거

  



    public void Animator_Initialization(int _hair, int _face, int _jacket, int _pants, int _shoes)
    {
        //Debug.Log("_shoes " + _shoes);
        man_BicycleAnim.Rebind(); man_bodyAnim.Rebind(); man_HairAnim[_hair].Rebind(); man_FaceAnim[_face].Rebind();
        man_JacketAnim[_jacket].Rebind(); man_PantsAnim[_pants].Rebind();
        man_ShoesAnim[_shoes].Rebind(); man_HelmetAnim[0].Rebind(); man_GlovesAnim[0].Rebind();
    }


    //속도 빠른 애니메이션
    public void Animator_Speed(bool _normalState, bool _speedState, int _hair, int _face, int _jacket, int _pants, int _shoes, float _speed)
    {
        man_BicycleAnim.SetFloat("Speed", _speed);
        man_BicycleAnim.SetBool("NormalStart", _normalState);
        man_BicycleAnim.SetBool("SpeedStart", _speedState);

        man_bodyAnim.SetFloat("Speed", _speed);
        man_bodyAnim.SetBool("NormalStart", _normalState);
        man_bodyAnim.SetBool("SpeedStart", _speedState);

        man_HairAnim[_hair].SetFloat("Speed", _speed);
        man_HairAnim[_hair].SetBool("NormalStart", _normalState);
        man_HairAnim[_hair].SetBool("SpeedStart", _speedState);

        man_FaceAnim[_face].SetFloat("Speed", _speed);
        man_FaceAnim[_face].SetBool("NormalStart", _normalState);
        man_FaceAnim[_face].SetBool("SpeedStart", _speedState);

        man_JacketAnim[_jacket].SetFloat("Speed", _speed);
        man_JacketAnim[_jacket].SetBool("NormalStart", _normalState);
        man_JacketAnim[_jacket].SetBool("SpeedStart", _speedState);

        man_PantsAnim[_pants].SetFloat("Speed", _speed);
        man_PantsAnim[_pants].SetBool("NormalStart", _normalState);
        man_PantsAnim[_pants].SetBool("SpeedStart", _speedState);

        man_ShoesAnim[_shoes].SetFloat("Speed", _speed);
        man_ShoesAnim[_shoes].SetBool("NormalStart", _normalState);
        man_ShoesAnim[_shoes].SetBool("SpeedStart", _speedState);

        man_HelmetAnim[0].SetFloat("Speed", _speed);
        man_HelmetAnim[0].SetBool("NormalStart", _normalState);
        man_HelmetAnim[0].SetBool("SpeedStart", _speedState);

        man_GlovesAnim[0].SetFloat("Speed", _speed);
        man_GlovesAnim[0].SetBool("NormalStart", _normalState);
        man_GlovesAnim[0].SetBool("SpeedStart", _speedState); 
    }


    public void AnimClear_Fail(float _speed, bool _normalState, bool _speedState, bool _clearState, bool _failState, int _hair, int _face, int _jacket, int _pants, int _shoes)
    {
        man_BicycleAnim.SetFloat("Speed", _speed);
        man_BicycleAnim.SetBool("NormalStart", _normalState);
        man_BicycleAnim.SetBool("SpeedStart", _speedState);
        man_BicycleAnim.SetBool("ClearStart", _clearState);
        man_BicycleAnim.SetBool("FailStart", _failState);

        man_bodyAnim.SetFloat("Speed", _speed);
        man_bodyAnim.SetBool("NormalStart", _normalState);
        man_bodyAnim.SetBool("SpeedStart", _speedState);
        man_bodyAnim.SetBool("ClearStart", _clearState);
        man_bodyAnim.SetBool("FailStart", _failState);

        man_HairAnim[_hair].SetFloat("Speed", _speed);
        man_HairAnim[_hair].SetBool("NormalStart", _normalState);
        man_HairAnim[_hair].SetBool("SpeedStart", _speedState);
        man_HairAnim[_hair].SetBool("ClearStart", _clearState);
        man_HairAnim[_hair].SetBool("FailStart", _failState);

        man_FaceAnim[_face].SetFloat("Speed", _speed);
        man_FaceAnim[_face].SetBool("NormalStart", _normalState);
        man_FaceAnim[_face].SetBool("SpeedStart", _speedState);
        man_FaceAnim[_face].SetBool("ClearStart", _clearState);
        man_FaceAnim[_face].SetBool("FailStart", _failState);

        man_JacketAnim[_jacket].SetFloat("Speed", _speed);
        man_JacketAnim[_jacket].SetBool("NormalStart", _normalState);
        man_JacketAnim[_jacket].SetBool("SpeedStart", _speedState);
        man_JacketAnim[_jacket].SetBool("ClearStart", _clearState);
        man_JacketAnim[_jacket].SetBool("FailStart", _failState);

        man_PantsAnim[_pants].SetFloat("Speed", _speed);
        man_PantsAnim[_pants].SetBool("NormalStart", _normalState);
        man_PantsAnim[_pants].SetBool("SpeedStart", _speedState);
        man_PantsAnim[_pants].SetBool("ClearStart", _clearState);
        man_PantsAnim[_pants].SetBool("FailStart", _failState);

        man_ShoesAnim[_shoes].SetFloat("Speed", _speed);
        man_ShoesAnim[_shoes].SetBool("NormalStart", _normalState);
        man_ShoesAnim[_shoes].SetBool("SpeedStart", _speedState);
        man_ShoesAnim[_shoes].SetBool("ClearStart", _clearState);
        man_ShoesAnim[_shoes].SetBool("FailStart", _failState);

        man_HelmetAnim[0].SetFloat("Speed", _speed);
        man_HelmetAnim[0].SetBool("NormalStart", _normalState);
        man_HelmetAnim[0].SetBool("SpeedStart", _speedState);
        man_HelmetAnim[0].SetBool("ClearStart", _clearState);
        man_HelmetAnim[0].SetBool("FailStart", _failState);

        man_GlovesAnim[0].SetFloat("Speed", _speed);
        man_GlovesAnim[0].SetBool("NormalStart", _normalState);
        man_GlovesAnim[0].SetBool("SpeedStart", _speedState);
        man_GlovesAnim[0].SetBool("ClearStart", _clearState);
        man_GlovesAnim[0].SetBool("FailStart", _failState);
    }


    public void Animator_Test(int _jacket)
    {
        man_JacketAnim[_jacket].Rebind();
    }

    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        Initialization();
        //Animator_Declaration(); //선언
    }


    void Initialization()
    {
        //머리 세팅
        if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair1")  //동양
        {
            HairSetting(true, false, false, false);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair2") //서양
        {
            HairSetting(false, true, false, false);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair3") //흑인
        {
            HairSetting(false, false, true, false);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Hair") == "HelmetHair")
        {
            HairSetting(false, false, false, true); //헬맷착용 시 머리
        }

        //얼굴 세팅
        if (PlayerPrefs.GetString("Busan_Player_Face") == "Asian")
        {
            FaceSetting(true, false, false);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Face") == "White")
        {
            FaceSetting(false, true, false);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Face") == "Black")
        {
            FaceSetting(false, false, true);
        }

        ////아이템 세팅
        //장갑
        if (PlayerPrefs.GetString("Busan_Wear_GlovesKind") == "No")
        {
            glovesObj[0].SetActive(false);    //장갑없음
        }
        else if (PlayerPrefs.GetString("Busan_Wear_GlovesKind") == "ItemID_Gloves")
        {
            glovesObj[0].SetActive(true); //장값 있음
        }
        //헬맷
        if (PlayerPrefs.GetString("Busan_Wear_HelmetKind") == "No")
        {
            helmelObj[0].SetActive(false);    //헬맷없음
        }
        else if (PlayerPrefs.GetString("Busan_Wear_HelmetKind") == "ItemID_Helmet")
        {
            helmelObj[0].SetActive(true); //헬맷잇음
        }
        //상의
        if (PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_Nais")
        {
            JacketSetting(true, false, false);  //기본나시
        }
        else if (PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_Tshirt")
        {
            JacketSetting(false, true, false);  //반팔
        }
        else if (PlayerPrefs.GetString("Busan_Wear_JacketKind") == "ItemID_LongShirt")
        {
            JacketSetting(false, false, true);  //긴팔
        }
        //하의
        if (PlayerPrefs.GetString("Busan_Wear_PantsKind") == "ItemID_Short")
        {
            PantsSetting(true, false);  //반바지
        }
        else if (PlayerPrefs.GetString("Busan_Wear_PantsKind") == "ItemID_LongPants")
        {
            PantsSetting(false, true);  //긴바지
        }
        //신발
        if (PlayerPrefs.GetString("Busan_Wear_ShoesKind") == "ItemID_Sandal")
        {
            ShoesSetting(true, false);  //샌들
        }
        else if (PlayerPrefs.GetString("Busan_Wear_ShoesKind") == "ItemID_Shoes")
        {
            ShoesSetting(false, true);  //신발
        }

    }


    //선택한 해당 머리 활성화
    public void HairSetting(bool _asian, bool _westerner, bool _african, bool _helmetHair)
    {
        hairObj[0].SetActive(_asian);   //Hair1
        hairObj[1].SetActive(_westerner);   //Hair2
        hairObj[2].SetActive(_african); //Hair3
        hairObj[3].SetActive(_helmetHair);  //HelmetHair 헬맷머리
    }


    public void FaceSetting(bool _asian, bool _westerner, bool _african)
    {
        faceObj[0].SetActive(_asian);   //황색
        faceObj[1].SetActive(_westerner);   //백색
        faceObj[2].SetActive(_african); //흑색
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
        //Debug.Log("머리 스타일 = " + PlayerPrefs.GetString("Busan_Player_Hair"));
        if(_helmet == true)
        {
            if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair2") //서양인 머리 일때
            {
                helmelObj[0].SetActive(_helmet);
                //서양머리 비활성화 - 헬맷쓴머리 활성화
                hairObj[1].SetActive(false); hairObj[3].SetActive(true);
                PlayerPrefs.SetString("Busan_Player_Hair", "HelmetHair"); //플레이어 머리 변경
                PlayerPrefs.SetString("Busan_Wear_HairStyleName", "HelmetHair");  //헤어스타일 변경
                PlayerPrefs.SetInt("Busan_HairNumber", 3);    //헤어넘버 변경
                PlayerPrefs.SetString("Busan_Wear_HelmetKind", "ItemID_Helmet");  //헬맷종류
            }
            else
            {
                helmelObj[0].SetActive(_helmet);
            }
        }
        else
        {
            if (PlayerPrefs.GetString("Busan_Player_Hair") == "Hair2") //서양인 머리 일때
            {
                helmelObj[0].SetActive(_helmet);
                //서양머리 비활성화 - 헬맷쓴머리 활성화
                hairObj[1].SetActive(true); hairObj[3].SetActive(false);
                PlayerPrefs.SetString("Busan_Player_Hair", "Hair2"); //플레이어 머리 변경
                PlayerPrefs.SetString("Busan_Wear_HairStyleName", "Hair2");  //헤어스타일 변경
                PlayerPrefs.SetInt("Busan_HairNumber", 1);    //헤어넘버 변경
                PlayerPrefs.SetString("Busan_Wear_HelmetKind", "No"); //헬맷종류
            }
            else
            {
                helmelObj[0].SetActive(_helmet);
            }
        }
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
        Texture textrue = Resources.Load<Texture>("Item Texture/Jacket/Man/" + _style);
        man_JacketTextrue[_jacketIndex].material.color = Color.white;
        man_JacketTextrue[_jacketIndex].material.mainTexture = textrue;

        man_JacketTextrue[_jacketIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        man_JacketTextrue[_jacketIndex].material.SetTexture("_BaseMap", textrue);
    }

    //하의 스타일에 따른 텍스쳐 변경
    public void PantsTextrueSetting(string _style, int _pantsIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Pants/Man/" + _style);
        man_PantsTextrue[_pantsIndex].material.color = Color.white;
        man_PantsTextrue[_pantsIndex].material.mainTexture = textrue;

        man_PantsTextrue[_pantsIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        man_PantsTextrue[_pantsIndex].material.SetTexture("_BaseMap", textrue);
    }

    //신발 스타일에 따른 텍스쳐 변경
    public void ShoesTextrueSetting(string _style, int _shoesIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Shoes/Man/" + _style);

        
        if (_style == "BasicSandal")
        {
            man_ShoesTextrue[_shoesIndex].material.color = Color.white;
            man_ShoesTextrue[_shoesIndex].material.mainTexture = textrue;
            man_ShoesTextrue[_shoesIndex+1].material.color = Color.white;
            man_ShoesTextrue[_shoesIndex+1].material.mainTexture = textrue;

            man_ShoesTextrue[_shoesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
            man_ShoesTextrue[_shoesIndex].material.SetTexture("_BaseMap", textrue);
            man_ShoesTextrue[_shoesIndex + 1].material.SetColor("_BaseColor", new Color(1, 1, 1));
            man_ShoesTextrue[_shoesIndex + 1].material.SetTexture("_BaseMap", textrue);
        }
        else
        {
            man_ShoesTextrue[_shoesIndex].material.color = Color.white;
            man_ShoesTextrue[_shoesIndex].material.mainTexture = textrue;

            man_ShoesTextrue[_shoesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
            man_ShoesTextrue[_shoesIndex].material.SetTexture("_BaseMap", textrue);
        }
        
    }

    //헬맷 스타일에 따른 텍스쳐 변경
    public void HelmetTextrueSetting(string _style, int _helemtIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Helmet/Man/" + _style);
        man_HelmetTextrue[_helemtIndex].material.color = Color.white;
        man_HelmetTextrue[_helemtIndex].material.mainTexture = textrue;

        man_HelmetTextrue[_helemtIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        man_HelmetTextrue[_helemtIndex].material.SetTexture("_BaseMap", textrue);
    }

    public void GlovesTextrueSetting(string _style, int _glovesIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Gloves/Man/" + _style);
        man_glovesTextrue[_glovesIndex].material.color = Color.white;
        man_glovesTextrue[_glovesIndex].material.mainTexture = textrue;

        man_glovesTextrue[_glovesIndex].material.SetColor("_BaseColor", new Color(1, 1, 1));
        man_glovesTextrue[_glovesIndex].material.SetTexture("_BaseMap", textrue);
    }

    public void BicycleTextrueSetting(string _style, int _bikeIndex)
    {
        Texture textrue = Resources.Load<Texture>("Item Texture/Bicycle/Man/" + _style);

        for(int i =0; i < man_BikeTexture.Length; i++)
        {
            man_BikeTexture[i].material.color = Color.white;
            man_BikeTexture[i].material.mainTexture = textrue;

            man_BikeTexture[i].material.SetColor("_BaseColor", new Color(1, 1, 1));
            man_BikeTexture[i].material.SetTexture("_BaseMap", textrue);
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
            man_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        } 
        else if (_skinColor == "FaceBrown")
        {
            skinMat.color = new Color(0.43f, 0.31f, 0.24f);  //갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.43f, 0.31f, 0.24f));
            man_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        } 
        else if (_skinColor == "FacePaleBrown")
        {
            skinMat.color = new Color(0.71f, 0.47f, 0.37f);  //연갈색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(0.71f, 0.47f, 0.37f));
            man_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        } 
        else if (_skinColor == "FaceApricot")
        {
            skinMat.color = new Color(1f, 0.6f, 0.45f);  //살색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
            man_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        }
        else if (_skinColor == "FaceWhite")
        {
            skinMat.color = new Color(1f, 0.7f, 0.6f);   //백색

            //유니벌스렌더러 색상 변경 하기 위함
            propertyBlock.SetColor("_BaseColor", new Color(1f, 0.7f, 0.6f));
            man_Skin_Rend[PlayerPrefs.GetInt("Busan_BodyNumber")].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        }
    }

    //플레이어 머리색 초기화
    public void PlayerHairColorInit(string _hairColor)
    {
        if (_hairColor == "HairBlack")
        {
            hairMat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색

            propertyBlock.SetColor("_BaseColor", new Color(0.07f, 0.07f, 0.07f));
            man_Hair_Rend[PlayerPrefs.GetInt("Busan_HairNumber")].SetPropertyBlock(propertyBlock);
        }
        else if (_hairColor == "HairRedBrown")
        {
            hairMat.color = new Color(0.46f, 0.06f, 0.08f);   //붉은갈색

            propertyBlock.SetColor("_BaseColor", new Color(0.46f, 0.06f, 0.08f));
            man_Hair_Rend[PlayerPrefs.GetInt("Busan_HairNumber")].SetPropertyBlock(propertyBlock);
        }  
        else if (_hairColor == "HairBrown")
        {
            hairMat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

            propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));
            man_Hair_Rend[PlayerPrefs.GetInt("Busan_HairNumber")].SetPropertyBlock(propertyBlock);
        }  
        else if (_hairColor == "HairYellowBrown")
        {
            hairMat.color = new Color(0.39f, 0.27f, 0.14f);  //똥색

            propertyBlock.SetColor("_BaseColor", new Color(0.39f, 0.27f, 0.14f));
            man_Hair_Rend[PlayerPrefs.GetInt("Busan_HairNumber")].SetPropertyBlock(propertyBlock);
        }  
        else if (_hairColor == "HairYellow")
        {
            hairMat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

            propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.65f, 0.26f));
            man_Hair_Rend[PlayerPrefs.GetInt("Busan_HairNumber")].SetPropertyBlock(propertyBlock);
        }
    }
}
