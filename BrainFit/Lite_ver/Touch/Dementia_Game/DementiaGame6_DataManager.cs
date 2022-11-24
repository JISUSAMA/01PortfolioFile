using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame6_DataManager : MonoBehaviour
{
    public DementiaGame6_UIManager UIManager;
    public Touch_TimerManager TimerManager_sc;
    public List<GameObject> CurrnetPaint_Parent;
    public List<GameObject> CurrnetPaint_Child_left;
    public List<GameObject> CurrnetPaint_Child_right;

    public static DementiaGame6_DataManager instance { get; private set; }
    /// <summary>
    /// 그림 속에 숨겨진 사물 5개를 정해진 시간 안에 찾는 게임
    /// 1) 시간 내 완료하지 못하면 실패
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame6_Play();
        yield return null;
    }
    public void DementiaGame6_Play()
    {
        StopCoroutine("_DementiaGame6_Play");
        StartCoroutine("_DementiaGame6_Play");
    }
    IEnumerator _DementiaGame6_Play()
    {
        UIManager.SetCurrentPainFindBtn();
        TimerManager_sc.FindWord_sec_Timer(28, 10, 5);
        yield return null;
    }

}
