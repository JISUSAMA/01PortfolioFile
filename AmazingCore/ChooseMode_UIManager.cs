using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class ChooseMode_UIManager : MonoBehaviour
{
    [Header("UICavase Grup")]
    public ToggleGroup MainToggleGrup;
    public GameObject[] Toggle_OpenCanvas;

    //������ 
    public TextMeshProUGUI UserName; //����� �̸�
    public Text UserAge; //����� ����
    
    public Image ProfileLevel_img;
    public Sprite[] ProfileLevel_sp;

    public Text StartExerciseDay; //� ������
    public Text  LastExerciseDay;  //������ ���
    public Text UserHeight_weight;

    public Slider Training_Progress_slider;
    public Text Training_Progress_text;
    //�����ϱ� 
    public Text[] Placeholder; //������ �⺻�� 0.�̸�, ���� , Ű , ������
    public InputField name_inf; //���� ����� �̸�
    public InputField age_inf; //���� ����� ����
    public InputField height_inf; //���� ����� Ű
    public InputField weight_inf;//���� ����� ������

    [System.Serializable]
    public struct MyProfile
    {
        public Text age;
        public Text name;
        public Text height;
        public Text weight;

    }
    public MyProfile myProfile;

    [Header("� ����Ʈ")]
    public Text CoreTraing_Count;
    public Text CoreGame_Count;
    public Text TotalPlay_Time;

    //����ȹ��
    public Image []Puzzle_img;
    public Sprite[] Tower_puzzle_sp;
    public Sprite[] Delivery_puzzle_sp;
    public Sprite[] Wire_puzzle_sp;

    [Header("PopupCavase Grup")]
    public GameObject bleDisconnectAlert;
    public Text bleDesc;
    public GameObject newBLERegOption;
    public GameObject commitButton;

    [Space(10f)]
    [Header("Training Mode")]
    public GameObject TrainingModePanel;
    public GameObject[] LevelGrups; //0 :�ʱ� 1: �߱� 2: ���
    public Button[] Training_ArrowBtn;
    public int levelGrupPos_i = 0; //ĵ���� ��ġ 
    public string SelectLevel_str; //������ ������ ����
    [Header("Game Mode")]
    public GameObject GameModePanel;
    //public GameObject[] GameGrups; //0 :ž �ױ� 1: ���������� 2: ��Ÿ��
    public Button[] Game_ArrowBtn;
    public int GameGrupPos_i = 1; //������ ������ ����/��ȣ
    public string SelectGame_str; //������ ������ �̸�
    //public Scrollbar GameMode_Scrollbar; //������ ������ ����
    public Animator GrupMove_ani;

    public GameObject[] TowerMaker_lock_ob;
    public GameObject[] DeliveryMan_lock_ob;
    public GameObject[] WireWalking_lock_ob;

    [System.Serializable]
    public struct View3dMode
    {
        public int ViewModePos_i;
        public Image ViewTrainingImg;
        public Sprite[] ViewTrainingImg_sp;
        public Button[] ArrowBtn;
        public GameObject[] LevelCanvasGrup;
    }
    [Header("3D Viewer Mode")]
    public View3dMode _3dMode;
    public GameObject _3DViewerModePanel;
    
    [Space(10)]
    [Header("Connect Tutorials")]
    public GameObject sensorTutorialsGroup;
    public GameObject[] sensorTutorials;
    private void Start()
    {
        SoundCtrl.Instance.BGM_MainSound();
        GameManager.instance.Data_Initialization();
        UI_Initialization();
    }
    private void OnEnable()
    {
       
        GameGrupPos_i = 1;
        _3dMode.ViewModePos_i = 0;
    }
    public void UI_Initialization()
    {
        UserName.text = GameManager.instance.UserName;//����� �̸�
        UserAge.text = "�� "+GameManager.instance.UserAge+"��";//����� ����
        ProfileLevel_img.sprite = ProfileLevel_sp[GameManager.instance.ProfileLevel];
        StartExerciseDay.text = GameManager.instance.StartExerciseDay; //� ������
        LastExerciseDay.text = GameManager.instance.LastExerciseDay; //������ ���
        UserHeight_weight.text = GameManager.instance.UserHeight + "m " + GameManager.instance.UserWeight + "Kg";

        Training_Progress_slider.value = GameManager.instance.ProfileLevel;

        CoreTraing_Count.text = GameManager.instance.CoreTraing_Count+"ȸ";
        CoreGame_Count.text = GameManager.instance.CoreGame_Count + "ȸ";

        TotalPlay_Time.text = getParseTime(GameManager.instance.TotalPlay_Time) + "��";
        //TotalPlay_Time.text = getParseTime(6000) + "��";

        Puzzle_img[0].sprite = Tower_puzzle_sp[GameManager.instance.Tower_Clear_level]; //ž�ױ�
        Puzzle_img[1].sprite = Delivery_puzzle_sp[GameManager.instance.DeliveryMan_Clear_level]; //���
        Puzzle_img[2].sprite = Wire_puzzle_sp[GameManager.instance.WireWalk_Clear_level]; //��Ÿ��

        //setting �κ� �ؽ�Ʈ ����
        myProfile.age.text = GameManager.instance.UserAge; 
        myProfile.name.text = GameManager.instance.UserName; 
        myProfile.height.text = GameManager.instance.UserHeight; 
        myProfile.weight.text = GameManager.instance.UserWeight;

        if (!GameManager.instance.ProfileLevel.Equals(3))
        {
            Training_Progress_slider.value = 33 * GameManager.instance.ProfileLevel;
            Training_Progress_text.text = (33 * GameManager.instance.ProfileLevel).ToString() + "%";
        }
        else { Training_Progress_slider.value = 100; Training_Progress_text.text = "100%"; } 
     
        //�� Ȱ��ȭ 
        for (int i =0; i<GameManager.instance.Tower_Clear_level;i++)
        {
            if (GameManager.instance.Tower_Clear_level.Equals(4))
            {
                TowerMaker_lock_ob[i].SetActive(false);
            }
            else
            {
                TowerMaker_lock_ob[i + 1].SetActive(false);
            }
        }
        for (int j = 0; j < GameManager.instance.DeliveryMan_Clear_level; j++)
        {
            if (GameManager.instance.DeliveryMan_Clear_level.Equals(4))
            {
                DeliveryMan_lock_ob[j].SetActive(false);
            }
            else
            {
                DeliveryMan_lock_ob[j + 1].SetActive(false);
            }
          
        }
        for (int p = 0; p <GameManager.instance.WireWalk_Clear_level; p++)
        {
            if (GameManager.instance.WireWalk_Clear_level.Equals(4))
            {
                WireWalking_lock_ob[p].SetActive(false);
            }
            else
            {
                WireWalking_lock_ob[p + 1].SetActive(false);
            }
        }
    }
    //������ �����ϱ� Ŭ����
    public void OnClick_ModifyBtn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        //�����ʿ� �⺻�� �����ϱ�
        Placeholder[0].text = GameManager.instance.UserName;
        Placeholder[1].text = GameManager.instance.UserAge;
        Placeholder[2].text = GameManager.instance.UserHeight;
        Placeholder[3].text = GameManager.instance.UserWeight;

    }
    public void OnClick_Save_NewProFile()
    {
        if (!name_inf.text.Length.Equals(0)) 
        {
            GameManager.instance.UserName = name_inf.text;
            PlayerPrefs.SetString("UserName", name_inf.text);
        }
        if (!age_inf.text.Length.Equals(0))
        {
            GameManager.instance.UserAge = age_inf.text;
            PlayerPrefs.SetString("UserAge", age_inf.text);
        }
        if (!height_inf.text.Length.Equals(0))
        {
            GameManager.instance.UserHeight = height_inf.text;
            PlayerPrefs.SetString("UserHeight", height_inf.text);
        }
        if (!weight_inf.text.Length.Equals(0))
        {
            GameManager.instance.UserWeight = weight_inf.text;
            PlayerPrefs.SetString("UserWeight", weight_inf.text);
        }
        SoundCtrl.Instance.ClickButton_Sound();
        UI_Initialization();
    }
    public void OnClick_ChooseModeBtn(string sceneName)
    {
        GameManager.instance.LoadSceneName(sceneName);
        SoundCtrl.Instance.ClickButton_Sound();
    }
    public Toggle ToggleCurrentSeletion
    {
        get { return MainToggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_toggle()
    {
        if (MainToggleGrup.ActiveToggles().Any())
        {
            if (ToggleCurrentSeletion.name.Equals("HomeToggle"))
            {
                OnClick_Toggle_OpenCanvas(true, false, false);

            }
            else if (ToggleCurrentSeletion.name.Equals("ChooseModeToggle"))
            {
                OnClick_Toggle_OpenCanvas(false, true, false); //Debug.Log("------------------" + name);
                if (levelGrupPos_i.Equals(0)) { SelectLevel_str = "Beginner"; }
                else if (levelGrupPos_i.Equals(1)) { SelectLevel_str = "intermediate"; }
                else if (levelGrupPos_i.Equals(2)) { SelectLevel_str = "Advanced"; }

            }
            else if (ToggleCurrentSeletion.name.Equals("MyProfileToggle"))
            {
                OnClick_Toggle_OpenCanvas(false, false, true);// Debug.Log("------------------" + name);
            }

        }
    }
    void OnClick_Toggle_OpenCanvas(bool home, bool mode, bool my)
    {
        Toggle_OpenCanvas[0].SetActive(home);
        Toggle_OpenCanvas[1].SetActive(mode);
        Toggle_OpenCanvas[2].SetActive(my);
    }

    public void TrainingMode_Init()
    {

    }
    public void OnClick_Level_left()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        if (levelGrupPos_i > 0)
        {
            levelGrupPos_i -= 1;
        }

        //��ư Ȱ��ȭ /��Ȱ��ȭ
        if (!levelGrupPos_i.Equals(0))
            Training_ArrowBtn[0].gameObject.SetActive(true);
        else
            Training_ArrowBtn[0].gameObject.SetActive(false);

        if (!levelGrupPos_i.Equals(2))

            Training_ArrowBtn[1].gameObject.SetActive(true);
        else
            Training_ArrowBtn[1].gameObject.SetActive(false);

        LevelGrups[levelGrupPos_i].SetActive(true);
        LevelGrups[levelGrupPos_i + 1].SetActive(false);

        if (levelGrupPos_i.Equals(0)) { SelectLevel_str = "Beginner"; }
        else if (levelGrupPos_i.Equals(1)) { SelectLevel_str = "intermediate"; }
        else if (levelGrupPos_i.Equals(2)) { SelectLevel_str = "Advanced"; }
    }
    public void OnClick_Level_right()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        if (levelGrupPos_i < 2)
        {
            levelGrupPos_i += 1;
        }

        //��ư Ȱ��ȭ /��Ȱ��ȭ
        if (!levelGrupPos_i.Equals(0))
            Training_ArrowBtn[0].gameObject.SetActive(true);
        else
            Training_ArrowBtn[0].gameObject.SetActive(false);
        if (!levelGrupPos_i.Equals(2))
            Training_ArrowBtn[1].gameObject.SetActive(true);
        else
            Training_ArrowBtn[1].gameObject.SetActive(false);

        LevelGrups[levelGrupPos_i].SetActive(true);
        LevelGrups[levelGrupPos_i - 1].SetActive(false);

        if (levelGrupPos_i.Equals(0)) { SelectLevel_str = "Beginner"; }
        else if (levelGrupPos_i.Equals(1)) { SelectLevel_str = "intermediate"; }
        else if (levelGrupPos_i.Equals(2)) { SelectLevel_str = "Advanced"; }
    }

    public void OnClick_SensorTutorial_CheckAndConnect()
    {
        if (!SensorManager.instance._connected)
        {
            // �Ⱥ����� Ʃ�� ������
            sensorTutorialsGroup.SetActive(true);
            sensorTutorials[0].SetActive(true);
        }
        else
        {
            bleDisconnectAlert.SetActive(true);
            commitButton.SetActive(true);
            newBLERegOption.SetActive(false);
            bleDesc.text = "���±׿� ����Ǿ� �ֽ��ϴ�.";
            StartCoroutine(OffDelay());
        }
    }

    // ���� ���� : ������ , ����ϱ�, ����ϸ鼭 ����ID �߰�
    public void OnClick_SensorRegister(Button _regbtn)
    {
        SoundCtrl.Instance.ClickButton_Sound();

        bleDisconnectAlert.SetActive(true);
        commitButton.SetActive(false);
        newBLERegOption.SetActive(false);

        bleDesc.text = "���±׸� ���� ���Դϴ�.\n��ø� ��ٷ��ּ���!";

        SensorManager.instance.Register(_regbtn, (complete, _anotherSensorDiscovered, desc) => {

            if (_anotherSensorDiscovered)
            {
                // ���ο�� �߰�
                bleDesc.text = desc;
                commitButton.SetActive(false);
                newBLERegOption.SetActive(true);
            }
            else
            {
                // �����Ǽ��� �߰� or ���� ����
                bleDesc.text = desc;
                commitButton.SetActive(true);
                newBLERegOption.SetActive(false);
                StartCoroutine(OffDelay());
            }

            _regbtn.enabled = true;
            Debug.Log("Current ConnectState >> " + complete);
        });
    }

    IEnumerator OffDelay()
    {
        WaitForSeconds ws = new WaitForSeconds(2.5f);
        yield return ws;

        commitButton.SetActive(false);
        newBLERegOption.SetActive(false);
        bleDisconnectAlert.SetActive(false);
    }

    public void NewSensorConnect()
    {
        SensorManager.instance.StartProcess();
    }

    public void OnClick_Tranining_StartButton()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        GameManager.instance.CoreLevel_str = SelectLevel_str;
        GameManager.instance.LoadSceneName("04.TrainingGame");
    }

    public void OnClick_GameMode_LeftBtn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        Debug.Log("GameGrupPos_i : " + GameGrupPos_i);
        if (GameGrupPos_i > 0)
        {
            GameGrupPos_i -= 1;
        }

        //��ư Ȱ��ȭ /��Ȱ��ȭ
        if (!GameGrupPos_i.Equals(0))
            Game_ArrowBtn[0].gameObject.SetActive(true);
        else
            Game_ArrowBtn[0].gameObject.SetActive(false);

        if (!GameGrupPos_i.Equals(2))
            Game_ArrowBtn[1].gameObject.SetActive(true);
        else
            Game_ArrowBtn[1].gameObject.SetActive(false);

        if (GameGrupPos_i.Equals(0))
        {
            GrupMove_ani.SetTrigger("left");
            SelectGame_str = "TowerMaker"; 
        }
        else if (GameGrupPos_i.Equals(1)) 
        {
            GrupMove_ani.SetTrigger("mid_left");
            SelectGame_str = "DeliveryMan"; 
        }
        else if (GameGrupPos_i.Equals(2)) 
        {
            SelectGame_str = "WireWalking"; 
        }

      
    }
    public void OnClick_GameMode_RigthBtn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        if (GameGrupPos_i < 2)
        {
            Debug.Log("GameGrupPos_i : " + GameGrupPos_i);
            GameGrupPos_i += 1;
        }

        //��ư Ȱ��ȭ /��Ȱ��ȭ
        if (!GameGrupPos_i.Equals(0))
            Game_ArrowBtn[0].gameObject.SetActive(true);
        else
            Game_ArrowBtn[0].gameObject.SetActive(false);

        if (!GameGrupPos_i.Equals(2))

            Game_ArrowBtn[1].gameObject.SetActive(true);
        else
            Game_ArrowBtn[1].gameObject.SetActive(false);

        if (GameGrupPos_i.Equals(0))
        {
            GrupMove_ani.SetTrigger("right");
            SelectGame_str = "TowerMaker";
        }
        else if (GameGrupPos_i.Equals(1))
        {
            GrupMove_ani.SetTrigger("mid_right");
            SelectGame_str = "DeliveryMan";
        }
        else if (GameGrupPos_i.Equals(2))
        {
            GrupMove_ani.SetTrigger("right");
            SelectGame_str = "WireWalking";
        }
        //gameGrup.localPosition = new Vector3(Move_scrolValue, gameGrup.localPosition.y, gameGrup.localPosition.z);
    }
    //���� ����
    public void OnClick_CoreGame(string game)
    {
        SoundCtrl.Instance.ClickButton_Sound();
        GameManager.instance.CoreGame_Name_str = game;
    }
    //���� �������� ����
    public void OnClick_GameLevel_Stage(int i)
    {
        SoundCtrl.Instance.ClickButton_Sound();
        if (i == 1) { GameManager.instance.CoreGame_Stage_i = 1; }
        else if (i == 2) { GameManager.instance.CoreGame_Stage_i = 2; }
        else if (i == 3) { GameManager.instance.CoreGame_Stage_i = 3; }
        else if (i == 4) { GameManager.instance.CoreGame_Stage_i = 4; }
        if (GameManager.instance.CoreGame_Name_str.Equals("DeliveryMan")) { LoadingSceneManager.LoadScene("DeliveryMan"); }
        else if (GameManager.instance.CoreGame_Name_str.Equals("TowerMaker")) { LoadingSceneManager.LoadScene("TowerMaker"); }
        else if (GameManager.instance.CoreGame_Name_str.Equals("WireWalking")) { LoadingSceneManager.LoadScene("WireWalking"); }
    }
    //3D ����� ���� 
    public void OnClick_3dView_Left()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        _3dMode.ViewModePos_i -= 1;
        if (_3dMode.ViewModePos_i.Equals(0))
        {
            _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
            _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);

            _3dMode.ArrowBtn[0].gameObject.SetActive(false);
            _3dMode.ArrowBtn[1].gameObject.SetActive(true);
        }
        else
        {
            _3dMode.ArrowBtn[0].gameObject.SetActive(true);
            _3dMode.ArrowBtn[1].gameObject.SetActive(true);
            if (_3dMode.ViewModePos_i.Equals(1)) 
            { 
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true); 
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i+1].SetActive(false); 
            }
            else if (_3dMode.ViewModePos_i.Equals(2))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(3))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(4))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(5))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(6))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i + 1].SetActive(false);
            }
        }
    }
    public void OnClick_3dView_Right()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        _3dMode.ViewModePos_i += 1;
        if (_3dMode.ViewModePos_i.Equals(6))
        {
            _3dMode.ArrowBtn[0].gameObject.SetActive(true);
            _3dMode.ArrowBtn[1].gameObject.SetActive(false);
   
            _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
            _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
        }
        else
        {
            _3dMode.ArrowBtn[0].gameObject.SetActive(true);
            _3dMode.ArrowBtn[1].gameObject.SetActive(true);
            if (_3dMode.ViewModePos_i.Equals(0))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(1))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(2))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(3))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(4))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
            else if (_3dMode.ViewModePos_i.Equals(5))
            {
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i].SetActive(true);
                _3dMode.LevelCanvasGrup[_3dMode.ViewModePos_i - 1].SetActive(false);
            }
        }
    }
    public void URL_Link(string url)
    {
        Application.OpenURL(url);
    }
    public void OnClick_GameModePanelButton(GameObject select)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (!SensorManager.instance._connected)
        {
            // Alert            
            bleDisconnectAlert.SetActive(true);
            bleDesc.text = "������� ������ ���������ϴ�.\n���±׸� ���� �������ּ���!";
        }
        else
        {
            GameModePanel.SetActive(true);
        }
#else
        StartCoroutine(_Wait_Time(select, GameModePanel));
       // GameModePanel.SetActive(true);
#endif
    }

    public void OnClick_TrainingModePanelButton(GameObject select)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (!SensorManager.instance._connected)
        {
            // Alert
            bleDisconnectAlert.SetActive(true);
            bleDesc.text = "������� ������ ���������ϴ�.\n���±׸� ���� �������ּ���!";
        }
        else
        {
            TrainingModePanel.SetActive(true);
        }
#else
        StartCoroutine(_Wait_Time(select, TrainingModePanel));
      //  TrainingModePanel.SetActive(true);
#endif
    }

    public void OnClick_3DViewerModePanelButton(GameObject select)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (!SensorManager.instance._connected)
        {
            // Alert            
            bleDisconnectAlert.SetActive(true);
            bleDesc.text = "������� ������ ���������ϴ�.\n���±׸� ���� �������ּ���!";
        }
        else
        {
            _3DViewerModePanel.SetActive(true);
        }
#else
        StartCoroutine(_Wait_Time(select, _3DViewerModePanel));
      //  _3DViewerModePanel.SetActive(true);
#endif
    }
    //�ð� �� ���� ��ȯ
    public string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + '.' + tokens[1];
    }
    IEnumerator _Wait_Time(GameObject select, GameObject panel)
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //�̺�Ʈ ��ư ������Ʈ ��������
        obj.GetComponent<Button>().interactable = false;
        select.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        select.gameObject.SetActive(false);
        panel.SetActive(true);
        obj.GetComponent<Button>().interactable = true;
    }
}
