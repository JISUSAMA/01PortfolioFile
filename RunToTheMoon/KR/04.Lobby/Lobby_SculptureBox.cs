using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Lobby_SculptureBox : MonoBehaviour
{
    public Image LightLine_1;
    public Image LightLine_2;
    public Image LightLine_3;

    public GameObject[] PiceKinds;

    public Button DrawSpeedUpBtn;

    float leftTime = 10.0f;
    float coolTime = 10.0f;
    float speed = 0.3f;
    public Volume Sculture_Volume;
    public VolumeProfile _Sculture_Profile;
    Bloom Sculture;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Initialization();
        //서버에 저장되어야함
        Lobby_UIManager.instance.PiceCount = PlayerPrefs.GetInt("MoonPieceCount"); //획득한 조각 갯수 
        Set_ScultureCollection();
    }
    //초기화 해주기
    void Initialization()
    {
        speed = 0.3f;
        for (int i = 0; i < Lobby_UIManager.instance.PiceCount; i++)
        {
            PiceKinds[i].SetActive(false);
            PiceKinds[i].transform.GetChild(0).gameObject.SetActive(false);
            LightLine_1.fillAmount = 0;
            LightLine_2.fillAmount = 0;
            LightLine_3.fillAmount = 0;
        }
    }
    public void Set_ScultureCollection()
    {
        if (Lobby_UIManager.instance.PiceCount < 10)
        {
            DrawSpeedUpBtn.interactable = false; //버튼 비활성화
            StartCoroutine(_Collection());
        }
        else
        {
            DrawSpeedUpBtn.interactable = true; //버튼 활성화
            DrawSpeedUpBtn.onClick.AddListener(OnClick_DrawSpeedUp); //원그리는 속도 증가
            StartCoroutine(All_Collection());
        }
    }

    IEnumerator _Collection()
    {
        for (int i = 0; i < Lobby_UIManager.instance.PiceCount; i++)
        {
            PiceKinds[i].SetActive(true); //보석활성화
            if (!i.Equals(0))
            {
                //8조각보다 작으면
                if (i < 8)
                {
                    //  Debug.Log("i : " + i);
                    float ratio = 1.0f / 8.0f * (i);
                    LightLine_1.fillAmount = ratio;
                    //  Debug.Log("ratio : " + ratio);
                }
                //9조각 이상이면,
                else
                {
                    LightLine_2.fillAmount = 1f; //두번째 줄 채움
                }
            }
        }
        yield return null;
    }


    IEnumerator All_Collection()
    {

        //보석의 갯수만큼 보석 활성화
        for (int i = 0; i < Lobby_UIManager.instance.PiceCount; i++)
        {
            //2,3,4,5,6,7,8 / 9,10
            if (i < 8)
            {
                PiceKinds[i].SetActive(true);
                PiceKinds[i].transform.GetChild(0).gameObject.SetActive(true);
                float ratio = 1.0f / 8.0f * (i + 1);
                while (LightLine_1.fillAmount < ratio)
                {
                    LightLine_1.fillAmount += Time.deltaTime * speed;
                    yield return null;
                }
                Debug.Log("LightLine_2.fillAmount : " + LightLine_2.fillAmount);
                if (i == 7)
                {
                    Debug.Log("LightLine_2.fillAmount : " + LightLine_2.fillAmount);
                    while (!LightLine_2.fillAmount.Equals(1))
                    {
                        LightLine_2.fillAmount += Time.deltaTime * speed;
                        yield return null;
                    }
                }
            }
            else
            {
                PiceKinds[i].SetActive(true);
                PiceKinds[i].transform.GetChild(0).gameObject.SetActive(true);
                if (i == 9)
                {
                    yield return new WaitForSeconds(0.1f);
                    Debug.Log("LightLine_3.fillAmount : " + LightLine_3.fillAmount);
                    while (!LightLine_3.fillAmount.Equals(1))
                    {
                        LightLine_3.fillAmount += Time.deltaTime * speed;
                        yield return null;
                    }
                    All_PiceCollect();
                }
            }
        }
        yield return null;

    }
    public void All_PiceCollect()
    {
        StartCoroutine(_All_PiceCollect());
        Debug.Log("calll: ");
    }
    IEnumerator _All_PiceCollect()
    {
        Sculture = (Bloom)Sculture_Volume.profile.components[0];
        while (Sculture.intensity.value < 1f)
        {
            Sculture.intensity.value += Time.deltaTime * speed;
            Debug.Log(" 1 :" + Sculture.intensity.value);
            yield return null;
        }
    }
    public void OnClick_DrawSpeedUp()
    {
        speed += 0.5f;
    }

}
