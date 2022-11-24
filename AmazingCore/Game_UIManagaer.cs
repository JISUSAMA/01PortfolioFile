using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_UIManagaer : MonoBehaviour
{
    [Header("In Game Timer")]
    public float playTime;
    public TextMeshProUGUI PlayTime_tmp;
    public GameObject CountDownPopup_ob;
    public Sprite[] CountNum_sp;

    [Header("RoundLevel UI")]

    public int Stage_i;
    public Image StageImg;
    public Sprite[] StageImg_sp;
    public GameObject[] StageGrup;
    
    public GameObject ClearPopup;
    public Text ClearText;
    public GameObject[] clear_nextStageBtn;
    
    public GameObject FailPopup;
    public GameObject ExitPopup;

    public Image Clear_Fevertime_img;
    public Sprite[] Clear_FeverTime_sp;
    public GameObject ClearParticle;
    public Button GameExitBtn;

    public GameObject TutorialPopup; //Ʃ�θ���
    public GameObject[] Tutorial_obs; //7��

    [Range(0, 1f)]
    public float AnimationMoveValue; //blend ���� ���Ǵ� ��

    [Header("Cinemachine")]
    public CinemachineDollyCart[] cDollyCart;
    public CinemachineSmoothPath[] cSmoothPath;

    [Header("3. TowerMaker")]
    public GameObject[] Tik_tok_prefebs;
    public Vector3 PosVector;
    public Camera cam;
    public Transform createTrs;
    public List<GameObject> ClimbCounts; //ƽ�� �������� ���� Ȯ��
    public int ClimbCounts_i; // �� ����ߴ��� Ȯ��
    public Text ClimbCounts_text;

    public GameObject[] TikTok_FeverObGrup;
    public Slider TowerCountSlider; //Ÿ���� ���� ����
    bool FeverTime; //�ǹ�Ÿ��
    bool isResetPosition = false;
    public GameObject[] Stage_Clear_Fairy;

    //public CinemachineVirtualCamera TowerClear_Cam;

    [System.Serializable]

    public struct WireWalking
    {
        //�������� ��, ī�޶� ��ȯ�� �ʿ��� ī�޶� 
        public CinemachineVirtualCamera[] Stage1_Cam;
        public CinemachineVirtualCamera[] Stage2_Cam;
        public CinemachineVirtualCamera[] Stage3_Cam;
        public CinemachineVirtualCamera[] Stage4_Cam;
        public CinemachineVirtualCamera Fail_Cam;

        public Animator[] TikTok_stage1_ani;
        public Animator[] TikTok_stage2_ani;
        public Animator[] TikTok_stage3_ani;
        public Animator[] TikTok_stage4_ani;

        public GameObject[] ClearParticleGrup;
        public bool CmMotionFinish;
        public GameObject FailCameraMotion_ob;
        public Vector3 FailCameraMotion_vec;

    }
    [Header("3. Wire Walking")]
    public WireWalking WireWalk;


    [Header("3. Delivery Man")]
    public Animator DeliveryManAni;
    public GameObject[] Mandu_left_ob;
    public GameObject[] Mandu_right_ob;
    public float pos;
    public static Game_UIManagaer instance { get; private set; }
    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        //UI_Initialization();
        
    }


    public void UI_Initialization()
    {
        GameExitBtn.onClick.AddListener(() => OnClicK_PopupOpen(ExitPopup)); //������ ��ư 

        if (GameManager.instance.CoreGame_Stage_i.Equals(1))
        {
            StageImg.sprite = StageImg_sp[0]; //�������� �ѹ�
            clear_nextStageBtn[0].SetActive(true);
            clear_nextStageBtn[1].SetActive(false);
            ClearText.text = "�����մϴ�! ���� 1�� Ŭ�����ϼ̾��. ���� �ܰ�� �ٷ� �Ѿ���?";
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(2))
        {
            StageImg.sprite = StageImg_sp[1];//�������� �ѹ�
            clear_nextStageBtn[0].SetActive(true);
            clear_nextStageBtn[1].SetActive(false);
            ClearText.text = "�����մϴ�! ���� 2�� Ŭ�����ϼ̾��. ���� �ܰ�� �ٷ� �Ѿ���?";
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(3))
        {
            StageImg.sprite = StageImg_sp[2];//�������� �ѹ�
            clear_nextStageBtn[0].SetActive(true);
            clear_nextStageBtn[1].SetActive(false);
            ClearText.text = "�����մϴ�! ���� 3�� Ŭ�����ϼ̾��. ���� �ܰ�� �ٷ� �Ѿ���?";
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(4))
        {
            StageImg.sprite = StageImg_sp[3];//�������� �ѹ�
            clear_nextStageBtn[0].SetActive(false);
            clear_nextStageBtn[1].SetActive(true);
            ClearText.text = "�����մϴ�!\n��� ���������� Ŭ�����ϼ̾��.";
        }

    }
    //���� �÷��� �ð� ����
    public void Timer()
    {
        StopCoroutine(_Timer());
        StartCoroutine(_Timer());
    }
    IEnumerator _Timer()
    {
      
        while (Game_AppManager.instance.PlayStart)
        {
            playTime += Time.deltaTime;
            PlayTime_tmp.text = getParseTime(Game_DataManager.instance.level_playTime_step - playTime);
            if (playTime >= Game_DataManager.instance.level_playTime_step)
            {
                //Debug.Log("cDollyCart : " + cDollyCart[0].m_Position);
                Game_AppManager.instance.PlayStart = false;
            }
            yield return null;
        }

        yield return null;
    }
    public string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + ':' + tokens[1];
    }
    //Ʃ�丮�� ����
    public void Tutorial_start()
    {
        StopCoroutine(_Tutorial_start());
        StartCoroutine(_Tutorial_start());
    }
    IEnumerator _Tutorial_start()
    {
        if (GameManager.instance.CoreGame_Name_str.Equals("DeliveryMan"))
        {
            if (PlayerPrefs.HasKey("DeliveryMan_tutorial")) { Game_AppManager.instance.Tutorial = true; }
            else
            {
                PlayerPrefs.SetString("DeliveryMan_tutorial", "true");
                TutorialPopup.SetActive(true);
            }
        }
        else if (GameManager.instance.CoreGame_Name_str.Equals("TowerMaker"))
        {
            if (PlayerPrefs.HasKey("TowerMaker_tutorial")){Game_AppManager.instance.Tutorial = true; }
            else 
            {
                PlayerPrefs.SetString("TowerMaker_tutorial", "true");
                TutorialPopup.SetActive(true);
            }
        }
        else if (GameManager.instance.CoreGame_Name_str.Equals("WireWalking"))
        {
            if (PlayerPrefs.HasKey("WireWalking_tutorial")) { Game_AppManager.instance.Tutorial = true; }
            else
            {
                PlayerPrefs.SetString("WireWalking_tutorial", "true");
                TutorialPopup.SetActive(true);
            }
        }
        yield return null;
    }
    public void FinishTutorial(GameObject lastPopup)
    {
        Game_AppManager.instance.Tutorial = true;
        lastPopup.SetActive(false);
    }
    public void OnClicK_PopupOpen(GameObject ob)
    {
        SoundCtrl.Instance.ClickButton_Sound();
        Time.timeScale = 0;
        ob.SetActive(true);
    }
    public void OnClick_PopupClose(GameObject ob)
    {
        SoundCtrl.Instance.ClickButton_Sound();
        Time.timeScale = 1;
        ob.SetActive(false);
    }
    //�絵��
    public void OnClick_Retry()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        Time.timeScale = 1;
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }
    //������
    public void OnClick_GameExit()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        Time.timeScale = 1;
        SceneManager.LoadScene("02.ChooseMode");
    }
    //���� �ܰ��
    public void OnClick_NextLevel()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        if (GameManager.instance.CoreGame_Name_str.Equals("DeliveryMan")) 
        {
            SceneManager.LoadScene("DeliveryMan");
            GameManager.instance.CoreGame_Stage_i += 1; 
        }
        else if (GameManager.instance.CoreGame_Name_str.Equals("TowerMaker")) 
        { 
            SceneManager.LoadScene("TowerMaker");
            GameManager.instance.CoreGame_Stage_i += 1;
        }
        else if (GameManager.instance.CoreGame_Name_str.Equals("WireWalking")) 
        { 
            SceneManager.LoadScene("WireWalking");
            GameManager.instance.CoreGame_Stage_i += 1;
        }
    }
    ///////////////////////////// Delivery Man ////////////////////////////////////
    public void DeliveryMan_Play()
    {
        StopCoroutine(_DeliveryMan_Play());
        StartCoroutine(_DeliveryMan_Play());
    }
    IEnumerator _DeliveryMan_Play()
    {
        DeliveryMan_BasicSetting(); //���� Ȱ��ȭ
        yield return new WaitUntil(() => Game_AppManager.instance.PlayStart == true);
        Game_DataManager.instance.Game_Succsed = "Clear";

        float distRatio;
        float AnimationMoveValue;

        cDollyCart[0].m_Speed = 5;
        while (Game_AppManager.instance.PlayStart == true)
        {
            float value = DeliveryManAni.GetFloat("Blend");
            //��鸲 �ִ�ġ �� ���, ������ ����
            //�ھ �����ϰ� �ð����� ���� ���� ���, clear
            //�߰��� ��������, Fail
            //if (value == 1)
            //{
            //    Game_DataManager.instance.Game_Succsed = "Fail";
            //    int random = UnityEngine.Random.RandomRange(0, 1); //������,���� ���� �ִϸ��̼� ��
            //    if (random.Equals(0)) { DeliveryManAni.SetTrigger("Fail_left"); }
            //    else { DeliveryManAni.SetTrigger("Fail_right"); }
            //    Game_AppManager.instance.PlayStart = false;
            //    break;
            //}

            distRatio = IndicatorCursor.distance;
            AnimationMoveValue = Mathf.InverseLerp(0f, 180f, distRatio);    // 0 ~ 1

            //float value = DeliveryManAni.GetFloat("Blend");
            //��鸲 �ִ�ġ �� ���, ������ ����
            //�ھ �����ϰ� �ð����� ���� ���� ���, clear
            //�߰��� ��������, Fail
            if (AnimationMoveValue >= 1)
            {
                Game_DataManager.instance.Game_Succsed = "Fail";
                int random = UnityEngine.Random.Range(0, 1); //������,���� ���� �ִϸ��̼� ��
                if (random.Equals(0)) { DeliveryManAni.SetTrigger("Fail_left"); }
                else { DeliveryManAni.SetTrigger("Fail_right"); }
                Game_AppManager.instance.PlayStart = false;
                break;
            }

            yield return null;
        }
        //Ÿ�̸Ӱ� 0�� �Ǹ� playerStart�� false�� �Ǹ鼭 �Ѿ
        yield return new WaitUntil(() => Game_AppManager.instance.PlayStart == false);
        cDollyCart[0].m_Speed = 0;

        Debug.Log("��Ƽ������ ��Ȱ��ȭ");
        IndicatorCursor.distance = 0f;
        Game_AppManager.instance.indicator.SetActive(false);

        //Game_Succsed�� "Clear"�� ���, īƮ ���߰� ���� ���
        if (Game_DataManager.instance.Game_Succsed.Equals("Clear"))
        {
            Debug.Log("����");
            DeliveryManAni.SetTrigger("Clear");
            Game_DataManager.instance.Save_GameClearData(); //���� ������ ����
            yield return new WaitForSeconds(2f);
            Clear_Fevertime_img.gameObject.SetActive(true);
            Clear_Fevertime_img.sprite = Clear_FeverTime_sp[0]; //clear img
            //��ƼŬ
            yield return new WaitForSeconds(2f);
            Clear_Fevertime_img.gameObject.SetActive(false);
            //���� ����
            ClearPopup.SetActive(true);
        }
        //Game_Succsed�� "Fail"�� ���,
        else
        {
            int randSoundPlay = UnityEngine.Random.Range(0, 2);
            if (randSoundPlay.Equals(0))
            {
                SoundManager.instance.PlaySFX_OneShot("fall3");
            }
            else { SoundManager.instance.PlaySFX_OneShot("fall2"); }
            Debug.Log("����!");
            Debug.Log("����!");
            FailPopup.SetActive(true);
            //���� ����
        }

    }
    public void DeliveryMan_BasicSetting()
    {
        if (GameManager.instance.CoreGame_Stage_i.Equals(1))
        {
            Mandu_left_ob[0].SetActive(true); Mandu_right_ob[0].SetActive(true);
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(2))
        {
            for (int i = 0; i < 2; i++) { Mandu_left_ob[i].SetActive(true); Mandu_right_ob[i].SetActive(true); }
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(3))
        {
            for (int i = 0; i < 3; i++) { Mandu_left_ob[i].SetActive(true); Mandu_right_ob[i].SetActive(true); }
        }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(4))
        {
            for (int i = 0; i < 4; i++) { Mandu_left_ob[i].SetActive(true); Mandu_right_ob[i].SetActive(true); }
        }
    }
    ///////////////////////////// TowerMaker ////////////////////////////////////
    /*1�ܰ� :  30�ʵ��� 10��
      2�ܰ� :  50�ʵ��� 15��
      3�ܰ� :  70�ʵ���  30��
      6�ܰ�  :  90�ʵ��� 45��*/

    public void TowerMaker_Initialization()
    {
        //�����̴� �� �ʱ�ȭ
        TowerCountSlider.value = 0;
        Stage_i = GameManager.instance.CoreGame_Stage_i - 1;
        if (GameManager.instance.CoreGame_Stage_i.Equals(1)) { TowerCountSlider.maxValue = 10; }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(2)) { TowerCountSlider.maxValue = 15; }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(3)) { TowerCountSlider.maxValue = 30; }
        else if (GameManager.instance.CoreGame_Stage_i.Equals(4)) { TowerCountSlider.maxValue = 45; }

        if (Stage_i.Equals(0)) { Set_StageCanvase(true, false, false, false); }
        else if (Stage_i.Equals(1)) { Set_StageCanvase(false, true, false, false); }
        else if (Stage_i.Equals(2)) { Set_StageCanvase(false, false, true, false); }
        else if (Stage_i.Equals(3)) { Set_StageCanvase(false, false, false, true); }
    }
    public void TowerMaker_Play()
    {
       StopCoroutine(_TowerMaker_Play());
       StartCoroutine(_TowerMaker_Play());
    }
    IEnumerator _TowerMaker_Play()
    {
        if (Stage_i.Equals(0))
        {
            yield return new WaitUntil(() => ClimbCounts.Count >= 10);
        }
        else if (Stage_i.Equals(1))
        {
            yield return new WaitUntil(() => ClimbCounts.Count >= 15);
        }
        else if (Stage_i.Equals(2))
        {
            yield return new WaitUntil(() => ClimbCounts.Count >=30);
        }
        else if (Stage_i.Equals(3))
        {
            yield return new WaitUntil(() => ClimbCounts.Count >= 45);
        }
        TowerMakerClear();        
        yield return new WaitUntil(() => Game_AppManager.instance.PlayStart == false); //������ ������ false�� ��
        Clear_Fevertime_img.gameObject.SetActive(false);
        //Game_Succsed�� "Clear"�� ���, īƮ ���߰� ���� ���
        if (Game_DataManager.instance.Game_Succsed.Equals("Clear"))
        {
         //   Debug.Log("����");
            //���� ����
            ClearPopup.SetActive(true);
        }
        //Game_Succsed�� "Fail"�� ���,
        else
        {
           // Debug.Log("����!");
            FailPopup.SetActive(true);
            //���� ����
        }
        yield return null;
    }
    public void MoveFollow_CamMotion()
    {
        StartCoroutine(_MoveFollow_CamMotion());
    }
    IEnumerator _MoveFollow_CamMotion()
    {
        float posY = cam.gameObject.transform.position.y;
    
        while (cam.gameObject.transform.position.y < PosVector.y)
        {
            Debug.Log(" posY" + posY);
            Debug.Log(" PosVector.y" + PosVector.y);
            posY += 15f;
            cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x, posY, cam.gameObject.transform.position.z);
            if (cam.gameObject.transform.position.y < posY)
            {
                cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x, PosVector.y, cam.gameObject.transform.position.z);
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    void TowerMakerClear()
    {
        StopCoroutine(_TowerMakerClear());
        StartCoroutine(_TowerMakerClear());
    }
    IEnumerator _TowerMakerClear()
    {
        Game_DataManager.instance.Save_GameClearData(); //���� ������ ����
        ClearParticle.SetActive(true);
        //yield return new WaitUntil(() => cDollyCart[0].m_Speed == 0);
        FeverTime = true;
        Stage_Clear_Fairy[Stage_i].SetActive(true);
        Debug.Log("FeverTime !!!!!!!!!!!!!!!!");
        Clear_Fevertime_img.gameObject.SetActive(true);
        Clear_Fevertime_img.sprite = Clear_FeverTime_sp[0]; //�ǹ�Ÿ��
        yield return new WaitForSeconds(2f);
        Clear_Fevertime_img.sprite = Clear_FeverTime_sp[1]; //�ǹ�Ÿ��
        Game_DataManager.instance.Game_Succsed = "Clear";
    }
    void Set_StageCanvase(bool s1, bool s2, bool s3, bool s4)
    {
        StageGrup[0].SetActive(s1);
        StageGrup[1].SetActive(s2);
        StageGrup[2].SetActive(s3);
        StageGrup[3].SetActive(s4);

    }
    public void ClimbCounts_Slider()
    {
        StopCoroutine(_ClimbCounts_Slider());
        StartCoroutine(_ClimbCounts_Slider());
    }
    IEnumerator _ClimbCounts_Slider()
    {
        while (true)
        {
            // ī����
            if (SensorManager.instance._connected && Game_AppManager.instance.PlayStart)
            {
                Debug.Log($"Cube : {SensorManager.instance.cube.rotation.eulerAngles.y} / reset : {isResetPosition}");
                if (SensorManager.instance.cube.rotation.eulerAngles.y <= 10 && SensorManager.instance.cube.rotation.eulerAngles.y >= 0
                    || SensorManager.instance.cube.rotation.eulerAngles.y <= 360 && SensorManager.instance.cube.rotation.eulerAngles.y >= 350)
                {
                    Debug.Log("Reset : " + isResetPosition);
                    // Reset
                    isResetPosition = true;
                }

                if (SensorManager.instance.cube.rotation.eulerAngles.y >= 45 && SensorManager.instance.cube.rotation.eulerAngles.y <= 90)
                {
                    // 45 ~ 90
                    if (isResetPosition)
                    {
                        isResetPosition = false;
                        //ClimbCounts_i += 1;
                        Create_TikTok_Prefebs();
                    }
                }
                else if (SensorManager.instance.cube.rotation.eulerAngles.y <= 315 && SensorManager.instance.cube.rotation.eulerAngles.y >= 270)
                {
                    // -45 ~ -90
                    if (isResetPosition)
                    {
                        isResetPosition = false;
                        //ClimbCounts_i += 1;
                        Create_TikTok_Prefebs();
                    }
                }
            }

            ClimbCounts_text.text = ClimbCounts_i.ToString(); //ƽ�� �?
            TowerCountSlider.value = ClimbCounts.Count; //�����̴� ���� �÷���
            if (Game_AppManager.instance.PlayStart == false) { break; }
            yield return null;
        }
    }
    public void Create_TikTok_Prefebs()
    {
        if (Game_AppManager.instance.PlayStart)
        {
   
            int rand_TikTok = UnityEngine.Random.Range(0, Tik_tok_prefebs.Length);
            ClimbCounts_i += 1; //�ѹ� �Ҷ����� �ϳ��� �÷���
                                //ƽ���� �ִ� ���� ����
            if (Stage_i.Equals(0) && ClimbCounts.Count < 10)
            {
                GameObject spawn = Instantiate(Tik_tok_prefebs[rand_TikTok], createTrs.position, Quaternion.identity);
                ClimbCounts.Add(spawn); //����ƾ Ŭ���̹� Ƚ��
            }
            else if (Stage_i.Equals(1) && ClimbCounts.Count < 15)
            {
                GameObject spawn = Instantiate(Tik_tok_prefebs[rand_TikTok], createTrs.position, Quaternion.identity);
                ClimbCounts.Add(spawn); //����ƾ Ŭ���̹� Ƚ��
            }
            else if (Stage_i.Equals(2) && ClimbCounts.Count < 30)
            {
                GameObject spawn = Instantiate(Tik_tok_prefebs[rand_TikTok], createTrs.position, Quaternion.identity);
                ClimbCounts.Add(spawn); //����ƾ Ŭ���̹� Ƚ��
            }
            else if (Stage_i.Equals(3) && ClimbCounts.Count < 45)
            {
                GameObject spawn = Instantiate(Tik_tok_prefebs[rand_TikTok], createTrs.position, Quaternion.identity);
                ClimbCounts.Add(spawn); //����ƾ Ŭ���̹� Ƚ��
            }
        }
    }
    ///////////////////////////// WireWalking Game ////////////////////////////////////
    public void WireWalking_Initialization()
    {
        //���������� ���� ĵ���� Ȱ��ȭ ��Ŵ
        Stage_i = GameManager.instance.CoreGame_Stage_i - 1;
        if (Stage_i.Equals(0))
        {
            Set_StageCanvase(true, false, false, false);
            WireWalk.Stage1_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 0;

        }
        else if (Stage_i.Equals(1))
        {
            Set_StageCanvase(false, true, false, false);
            WireWalk.Stage2_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 0;
        }
        else if (Stage_i.Equals(2))
        {
            Set_StageCanvase(false, false, true, false);
            WireWalk.Stage3_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 0;
        }
        else if (Stage_i.Equals(3))
        {
            Set_StageCanvase(false, false, false, true);
            WireWalk.Stage4_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 0;
        }
       // WireWalk.Fail_Cam.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 0; //���� �� ī�޶� 

    }
    public void WireWalking_start()
    {
        StopCoroutine(_WireWalking_start());
        StartCoroutine(_WireWalking_start());
    }
    IEnumerator _WireWalking_start()
    {
        //ī��Ʈ �ٿ� ������ ���� ���� ���� 
        yield return new WaitUntil(() => Game_AppManager.instance.PlayStart == true);
        TikTok_animation_Play(); //ƽ�� �ִϸ��̼� ����

        // IndicatorCursor.distance : 0 ~ maxDistance
        float distRatio;

        Game_DataManager.instance.Game_Succsed = "Clear"; //�����ϱ� �� ������ Clear ���� , �Ѿ��� ���, Fail
        while (Game_AppManager.instance.PlayStart == true)
        {
            //if (!AnimationMoveValue.Equals(1))
            //{
            //    //1 stage
            //    if (Stage_i.Equals(0))
            //    {
            //        WireWalk.TikTok_stage1_ani[0].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage1_ani[1].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage1_ani[2].SetFloat("Blend", AnimationMoveValue);
            //    }
            //    //2 Stage
            //    else if (Stage_i.Equals(1))
            //    {
            //        WireWalk.TikTok_stage2_ani[0].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage2_ani[1].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage2_ani[2].SetFloat("Blend", AnimationMoveValue);
            //    }
            //    //3 Stage
            //    else if (Stage_i.Equals(2))
            //    {
            //        WireWalk.TikTok_stage3_ani[0].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage3_ani[1].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage3_ani[2].SetFloat("Blend", AnimationMoveValue);
            //    }
            //    //4 Stage
            //    else if (Stage_i.Equals(3))
            //    {
            //        WireWalk.TikTok_stage4_ani[0].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage4_ani[1].SetFloat("Blend", AnimationMoveValue);
            //        WireWalk.TikTok_stage4_ani[2].SetFloat("Blend", AnimationMoveValue);
            //    }

            //IndicatorCursor.distance            
            // Calc > 0 ~ 180 => 0 ~ 1            
            distRatio = IndicatorCursor.distance;
            AnimationMoveValue = Mathf.InverseLerp(0f, 180f, distRatio);

            if (AnimationMoveValue < 1)
            {
                //1 stage
                if (Stage_i.Equals(0))
                {
                    WireWalk.TikTok_stage1_ani[0].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage1_ani[1].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage1_ani[2].SetFloat("Blend", AnimationMoveValue);
                }
                //2 Stage
                else if (Stage_i.Equals(1))
                {
                    WireWalk.TikTok_stage2_ani[0].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage2_ani[1].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage2_ani[2].SetFloat("Blend", AnimationMoveValue);
                }
                //3 Stage
                else if (Stage_i.Equals(2))
                {
                    WireWalk.TikTok_stage3_ani[0].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage3_ani[1].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage3_ani[2].SetFloat("Blend", AnimationMoveValue);
                }
                //4 Stage
                else if (Stage_i.Equals(3))
                {
                    WireWalk.TikTok_stage4_ani[0].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage4_ani[1].SetFloat("Blend", AnimationMoveValue);
                    WireWalk.TikTok_stage4_ani[2].SetFloat("Blend", AnimationMoveValue);
                }
            }
            else
            {
                //���� ��! ƽ�� ������
                Game_DataManager.instance.Game_Succsed = "Fail";
                Game_AppManager.instance.PlayStart = false;
                break;
            }
            yield return null;
        }
        //Ÿ�̸Ӱ� 0�� �Ǹ� playerStart�� false�� �Ǹ鼭 �Ѿ
        yield return new WaitUntil(() => Game_AppManager.instance.PlayStart == false);
        TikTok_animation_Finish(); //�ֵ� ����!
        Debug.Log("��Ƽ������ ��Ȱ��ȭ");
        IndicatorCursor.distance = 0;
        Game_AppManager.instance.indicator.SetActive(false);

        //Game_Succsed�� "Clear"�� ���, īƮ ���߰� ���� ���
        if (Game_DataManager.instance.Game_Succsed.Equals("Clear"))
        {
            Debug.Log("����");
            TikTok_animation_Clear_Finish(); //ī�޶� ��� ����
            yield return new WaitUntil(() => WireWalk.CmMotionFinish == true); // ķ��� ��!
            Game_DataManager.instance.Save_GameClearData(); //���� ������ ����
            Clear_Fevertime_img.gameObject.SetActive(true);
            Clear_Fevertime_img.sprite = Clear_FeverTime_sp[0]; //clear img
            yield return new WaitForSeconds(2f);
            Clear_Fevertime_img.gameObject.SetActive(false);
            //���� ����
            ClearPopup.SetActive(true);
        }
        //Game_Succsed�� "Fail"�� ���,
        else
        {
            SoundManager.instance.PlaySFX_OneShot("drop2");
            TikTok_animation_Fail_Finish();
            yield return new WaitUntil(() => WireWalk.CmMotionFinish == true); // ķ��� ��!

            yield return new WaitForSeconds(2f);
            Debug.Log("����!");
            FailPopup.SetActive(true);
            //���� ����
        }
        yield return null;
    }
    public void WireWalk_StartCamMotion()
    {
        Debug.Log("���ˤ�????????????????");
        //1 stage
        if (Stage_i.Equals(0))
        {
            WireWalk.Stage1_Cam[2].m_Priority = 10;
        }
        //2 Stage
        else if (Stage_i.Equals(1))
        {
            WireWalk.Stage2_Cam[2].m_Priority = 10;
        }
        //3 Stage
        else if (Stage_i.Equals(2))
        {
            WireWalk.Stage3_Cam[2].m_Priority = 10;
        }
        //4 Stage
        else if (Stage_i.Equals(3))
        {
           WireWalk.Stage4_Cam[2].m_Priority = 10;
        }
    }

    //ƽ�� �ִϸ��̼� ���� , īƮ m_speed= 5 ����
    public void TikTok_animation_Play()
    {
        //1 stage 
        if (Stage_i.Equals(0))
        {
            for (int i = 0; i < WireWalk.TikTok_stage1_ani.Length; i++)
            {
                WireWalk.TikTok_stage1_ani[i].SetTrigger("GamePlay");
            }
            cDollyCart[0].m_Speed = 5;
        }
        //2 Stage
        else if (Stage_i.Equals(1))
        {
            for (int i = 0; i < WireWalk.TikTok_stage2_ani.Length; i++)
            {
                WireWalk.TikTok_stage2_ani[i].SetTrigger("GamePlay");
            }
            cDollyCart[1].m_Speed = 5;
        }
        //3 Stage
        else if (Stage_i.Equals(2))
        {
            for (int i = 0; i < WireWalk.TikTok_stage3_ani.Length; i++)
            {
                WireWalk.TikTok_stage3_ani[i].SetTrigger("GamePlay");
            }
            cDollyCart[2].m_Speed = 5;
        }
        //4 Stage
        else if (Stage_i.Equals(3))
        {
            for (int i = 0; i < WireWalk.TikTok_stage4_ani.Length; i++)
            {
                WireWalk.TikTok_stage4_ani[i].SetTrigger("GamePlay");
            }
            cDollyCart[3].m_Speed = 5;
        }
    }
    public void TikTok_animation_Finish()
    {
        //1 stage 
        if (Stage_i.Equals(0))
        {
            for (int i = 0; i < WireWalk.TikTok_stage1_ani.Length; i++)
            {
                WireWalk.TikTok_stage1_ani[i].SetTrigger("Clear");
            }
            cDollyCart[0].m_Speed = 0;
        }
        //2 Stage
        else if (Stage_i.Equals(1))
        {
            for (int i = 0; i < WireWalk.TikTok_stage2_ani.Length; i++)
            {
                WireWalk.TikTok_stage2_ani[i].SetTrigger("Clear");
            }
            cDollyCart[1].m_Speed = 0;
        }
        //3 Stage
        else if (Stage_i.Equals(2))
        {
            for (int i = 0; i < WireWalk.TikTok_stage3_ani.Length; i++)
            {
                WireWalk.TikTok_stage3_ani[i].SetTrigger("Clear");
            }
            cDollyCart[2].m_Speed = 0;
        }
        //4 Stage
        else if (Stage_i.Equals(3))
        {
            for (int i = 0; i < WireWalk.TikTok_stage4_ani.Length; i++)
            {
                WireWalk.TikTok_stage4_ani[i].SetTrigger("Clear");
            }
            cDollyCart[3].m_Speed = 0;
        }
    }

    //WireWalking ���� ���� �� ! VideoMotion
    public void TikTok_animation_Clear_Finish()
    {
        WireWalk.ClearParticleGrup[Stage_i].SetActive(true);
        //1 stage 
        if (Stage_i.Equals(0))
        {
            WireWalk.Stage1_Cam[1].m_Priority = 12; // ī�޶� �켱����
            float motionT = WireWalk.Stage1_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width;
            while (motionT < 14)
            {
                motionT += Time.deltaTime * 0.5f;
                WireWalk.Stage1_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = motionT;
                if (motionT >= 14)
                {
                    WireWalk.Stage1_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 14;
                    WireWalk.CmMotionFinish = true;// ķ��� ��!
                    break;
                }
            }
        }
        //2 Stage
        else if (Stage_i.Equals(1))
        {
            WireWalk.Stage2_Cam[1].m_Priority = 12; // ī�޶� �켱���� �ֱ�
            float motionT = WireWalk.Stage2_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width;
            while (motionT < 14)
            {
                motionT += Time.deltaTime * 0.5f;
                WireWalk.Stage2_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = motionT;
                if (motionT >= 14)
                {
                    WireWalk.Stage2_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 14;
                    WireWalk.CmMotionFinish = true;// ķ��� ��!
                    break;
                }
            }
        }
        //3 Stage
        else if (Stage_i.Equals(2))
        {
            WireWalk.Stage3_Cam[1].m_Priority = 12; // ī�޶� �켱���� �ֱ�
            float motionT = WireWalk.Stage3_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width;
            while (motionT < 14)
            {
                motionT += Time.deltaTime * 0.5f;
                WireWalk.Stage3_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = motionT;
                if (motionT >= 14)
                {
                    WireWalk.Stage3_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 14;
                    WireWalk.CmMotionFinish = true;// ķ��� ��!
                    break;
                }
            }
        }
        //4 Stage
        else if (Stage_i.Equals(3))
        {
            WireWalk.Stage4_Cam[1].m_Priority = 12; // ī�޶� �켱���� �ֱ�
            float motionT = WireWalk.Stage4_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width;
            while (motionT < 14)
            {
                motionT += Time.deltaTime * 0.5f;
                WireWalk.Stage4_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = motionT;
                if (motionT >= 14)
                {
                    WireWalk.Stage4_Cam[1].gameObject.GetComponent<CinemachineFollowZoom>().m_Width = 14;
                    WireWalk.CmMotionFinish = true;// ķ��� ��!
                    break;
                }
            }
        }


    }
    //��Ÿ�� ���� ���� 
    public void TikTok_animation_Fail_Finish()
    {
        StopCoroutine(_TikTok_animation_Fail_Finish());
        StartCoroutine(_TikTok_animation_Fail_Finish());
    }
    IEnumerator _TikTok_animation_Fail_Finish()
    {
        WireWalk.Fail_Cam.m_Priority = 12;
        while (pos > -900)
        {
            pos -= 5;
            WireWalk.FailCameraMotion_vec = new Vector3(WireWalk.FailCameraMotion_vec.x, pos, WireWalk.FailCameraMotion_vec.z);
            WireWalk.FailCameraMotion_ob.transform.position = WireWalk.FailCameraMotion_vec;
            yield return new WaitForFixedUpdate();
        }
        WireWalk.CmMotionFinish = true;
    }
}
