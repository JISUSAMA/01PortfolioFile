using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }
    public GameObject countdownPanel;
    Image coutndownPanel_Color;
    public GameObject StartCountImg;
    Image StartCountImg_Color;
    //public Sprite ClassicTutorialImg;
    public GameObject GameStartImg;

    public bool gameRunning { get; set; }
    public UnityEvent onStart = null; //���� �� ��, �̺�Ʈ
    public UnityEvent onEnd = null; //�� ���� ��, �̺�Ʈ 
    SceneLoader _sceneLoader;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
        coutndownPanel_Color = countdownPanel.GetComponent<Image>();
        StartCountImg_Color = StartCountImg.GetComponent<Image>();
        //Time.timeScale = 1f;
    }
    private void Start()
    {

        StartCountdown();

    }
    void OnEnable()
    {
        SetInitialReferences();
        // StartCountdown();
        Timer.RoundEnd += OnRoundEnd;

    }
    //��Ȱ��ȭ�� ȣ��Ǵ� �Լ� 
    void OnDisable()
    {
        Timer.RoundEnd -= OnRoundEnd;
    }

    public void ResetLevel()
    {
        //New Scene�� ���� ���� �߰������� �ε�(single�� ���� �� close)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    //�ʱ� ���� ����
    protected virtual void SetInitialReferences()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    //������ ���� �Ҷ� ī��Ʈ �ٿ� ����
    public void StartButtonON()
    {
        StartCoroutine(StartCountdown_());
    }
    public void StartCountdown()
    {
        StopAllCoroutines();
        StartCoroutine(StartCountdown_());

    }
    IEnumerator StartCountdown_()
    {
        //��ŷ��� �� ���
        if (GameManager.RankMode.Equals(true))
        {
            UIManager.Instance.panel_Intro.SetActive(true);
            yield return new WaitForSeconds(1f);
            UIManager.Instance.panel_Intro.SetActive(false);
            countdownPanel.SetActive(true); //ī��Ʈ �ٿ� �ǳ� Ȱ��ȭ 
            yield return new WaitForSeconds(1f);
            SoundManager.Instance.countdown.Play(); //ī��Ʈ �ٿ� ���� Play
            yield return new WaitForSeconds(2.5f);
            AccelerometerCalibrator.Calibrate(); //
        }
        //Ŭ���� ����� ���
        else
        {
            GameManager.countForAdvertising += 1; //���� ������� Ƚ�� 
            PlayerPrefs.SetInt("AdPlayCount", GameManager.countForAdvertising); //�߰��� ���� Ƚ�� �����ϱ� 
            //countdownPanel.GetComponent<Image>().sprite = ClassicTutorialImg;
            countdownPanel.SetActive(true); //ī��Ʈ �ٿ� �ǳ� Ȱ��ȭ 
            StartCountImg.SetActive(true);
            yield return new WaitForSeconds(1f);//2
            SoundManager.Instance.countdown.Play(); //ī��Ʈ �ٿ� ���� Play         
            yield return new WaitForSeconds(2f);//2
            InStartFadeAnim();
            yield return new WaitForSeconds(0.5f);//1       
            OnRoundStart();
            countdownPanel.SetActive(false);
            AccelerometerCalibrator.Calibrate(); //
        }

        yield return null;
    }

    public void InStartFadeAnim()
    {
        StartCoroutine(fadeoutplay());
    }

    IEnumerator fadeoutplay()
    {
        Color Panelcolor1 = StartCountImg_Color.color;
        Color Panelcolor2 = coutndownPanel_Color.color;
        float time = 0f;
        float start = 1;
        float end = 0;
        float FadeTime = 0.5f;

        Panelcolor1.a = Mathf.Lerp(start, end, time);
        while (Panelcolor1.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            Panelcolor1.a = Mathf.Lerp(start, end, time);
            Panelcolor2.a = Mathf.Lerp(start, end, time);
            StartCountImg_Color.color = Panelcolor1;
            coutndownPanel_Color.color = Panelcolor2;
            yield return null;
        }



    }
    //�� �ε� 
    public void GlobalLoad(int index)
    {
        _sceneLoader.LoadScene(index);
    }
    //���� ����
    public void OnRoundStart()
    {
        AccelerometerCalibrator.Calibrate(); //��ġ�� ������ ����
        StartCoroutine(_OnRoundStart()); //�ڷ�ƾ ����
    }

    protected virtual IEnumerator _OnRoundStart()
    {
        GameStartImg.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameStartImg.SetActive(false);
        yield return UIManager.Instance.OnRoundStart(); //UI
        yield return DataManager.Instance.OnRoundStart(); //Data

        gameRunning = true; //bool check
        if (onStart != null) onStart.Invoke(); //onStart Event ����
    }
    //���� ��
    protected void OnRoundEnd()
    {
        StartCoroutine(_OnRoundEnd());
    }

    protected virtual IEnumerator _OnRoundEnd()
    {
        if (onEnd != null) onEnd.Invoke(); //onEnd Event ����
        yield return DataManager.Instance.OnRoundEnd(); //Data
        yield return UIManager.Instance.OnRoundEnd(); //UI
        gameRunning = false; //Bool unCheck       
        yield return new WaitForSeconds(0.5f);
        ShowAd();
    }

    void ShowAd()
    {
        //Ŭ���� ����� ���, ���� ���
        if (GameManager.countForAdvertising >= 2 && GameManager.RankMode.Equals(false))
        {
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
                GameManager.countForAdvertising = 0;
                PlayerPrefs.SetInt("AdPlayCount", GameManager.countForAdvertising); //Ŭ���� ��� ���� �ʱ�ȭ // 0
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }

        }
        //��ŷ ����� ���, ���� ���
        if ((GameManager.RandListCountCheck.Equals(3) || GameManager.RandListCountCheck.Equals(7)) && GameManager.RankMode.Equals(true))
        {
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }
        }
    }
}