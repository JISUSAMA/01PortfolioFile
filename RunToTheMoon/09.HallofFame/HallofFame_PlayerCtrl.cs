using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HallofFame_PlayerCtrl : MonoBehaviour
{
    [SerializeField] Animator playerAni;
    float movePlayer;
    IEnumerator coroutine;

    public GameObject Story_ob;
    public Animator Story_ani; //대화 이벤트 애니메이션
    public TMP_Text m_StoryTypingTex_mesh;  //대화 이벤트 텍스트
    public string m_Message;
    float m_Speed = 0.09111111f;

    private void Awake()
    {
 
        StartPlayerMove();
        Show_EventStoryText();

    }
    private void Start()
    {
        movePlayer = this.gameObject.transform.position.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.name.Equals("Yah"))
        {
            StopPlayerMove();

        }
    }
 
    public void StartPlayerMove()
    {
        coroutine = _PlayerMove();
        StartCoroutine(coroutine);

    }
    public void StopPlayerMove()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            StartYahAni();
        }
    }
    IEnumerator _PlayerMove()
    {
       
        yield return new WaitForSeconds(0.2f);
        playerAni.SetFloat("MoveY", 0.5f);
        while (true)
        {
            movePlayer += Time.deltaTime * 0.8f;
            transform.position = new Vector3(transform.position.x, transform.position.y, movePlayer);
            yield return null;
        }

    }
    public void StartYahAni()
    {
        coroutine = _YahAni();
        StartCoroutine(coroutine);
    }
    public void StopYahAni()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator _YahAni()
    {
        StartCoroutine(playAndWaitForAnim("Victory"));
        yield return null;
    }
    IEnumerator playAndWaitForAnim(string clipName)
    {
        Debug.Log("-----------------1");
        bool aniState = false; 
        playerAni.SetTrigger("Victory");
        while (aniState.Equals(false))
        {
            if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Victory") &&
         playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("-----------------2");
                HallofFame_UIManager.instance.ReachPointPanel.SetActive(true);
                aniState = true;
              
            }
            yield return null;
        }
    }

    public void Show_EventStoryText()
    {
        string message = "여기는 달인가...? \n 드디어 도착했어!\n  소원석에 이름을 새기면, 소원을 빌 수 있어.";
        StartCoroutine(Typing_Story(message));
        Debug.Log("Show_EventStoryText()");
    }
    IEnumerator Typing_Story(string message)
    {
        m_StoryTypingTex_mesh.text = " ";
        Story_ani.SetTrigger("open");
        yield return new WaitForSeconds(1.4f);
        for (int i = 0; i < message.Length; i++)
        {
            m_StoryTypingTex_mesh.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(m_Speed);
        }
        yield return new WaitForSeconds(1.4f);
        Story_ani.SetTrigger("close");
        m_Speed = 0.09111111f; //속도 초기화
    }

}
