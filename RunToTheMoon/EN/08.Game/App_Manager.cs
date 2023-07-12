using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 수정중
public class App_Manager : MonoBehaviour
{
    public bool gameRunning { get; set; }
    public UnityEvent onStart = null; //시작 할 때, 이벤트
    public UnityEvent onEnd = null; //끝 났을 때, 이벤트 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //라운드 시작
    public void OnRoundStart()
    {
        StartCoroutine(_OnRoundStart()); //코루틴 시작
    }

    protected virtual IEnumerator _OnRoundStart()
    {
        yield return new WaitForSeconds(1f);
        //yield return UIManager.Instance.OnRoundStart(); //UI
        //yield return DataManager.Instance.OnRoundStart(); //Data

        gameRunning = true; //bool check
        if (onStart != null) onStart.Invoke(); //onStart Event 실행
    }
    //라운드 끝
    protected void OnRoundEnd()
    {
        StartCoroutine(_OnRoundEnd());
    }

    protected virtual IEnumerator _OnRoundEnd()
    {
        if (onEnd != null) onEnd.Invoke(); //onEnd Event 실행
        //yield return DataManager.Instance.OnRoundEnd(); //Data
        //yield return UIManager.Instance.OnRoundEnd(); //UI
        gameRunning = false; //Bool unCheck       
        yield return new WaitForSeconds(0.5f);

        // 서버에 기록 저장 후 로비나올 때 ServerManager 스크립트에 다시 저장

    }
}
