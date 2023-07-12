using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class RuneController : MonoBehaviour
{

    Animator runeAni;
    public GameObject Rune;
    public Color RuneColor;

    public Material[] OriginMat;
    public Material[] ChangeMat;

    float exitTime = 0.8f;
    private void Awake()
    {
        runeAni = this.gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Game 14" || SceneManager.GetActiveScene().name == "Game 15"
            || SceneManager.GetActiveScene().name == "Game 16" || SceneManager.GetActiveScene().name == "Game 18" || SceneManager.GetActiveScene().name == "Game 19"
            || SceneManager.GetActiveScene().name == "Game 20")
        {
                 StartCoroutine(_Random_CheckAnimationState());
        }

       
    }

    public void StartAnimation()
    {
        StartCoroutine(_StartAnimation());
    }
    IEnumerator _StartAnimation()
    {
        yield return null;
    }
    public void Random_CheckAnimationState()
    {
        StartCoroutine(_Random_CheckAnimationState());
    }
    public void Stop_Random_CheckAnimationState()
    {
        StopCoroutine("_Random_CheckAnimationState()");
        StartCoroutine(Disappear_Rune());
        StartCoroutine(_ByeBye_Rune());
 
    }
    IEnumerator _Random_CheckAnimationState()
    {
        int RandomAnimationSet = UnityEngine.Random.Range(0, 4);
        runeAni.SetInteger("MoveRand", RandomAnimationSet);
        //Idle
        if (RandomAnimationSet.Equals(0))
        {
            Debug.Log("애니메이션 시작!@" + runeAni.GetCurrentAnimatorStateInfo(0));
            while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                //전환 중일 때 실행되는 부분
                yield return null;
            }
            while (runeAni.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < exitTime)
            {
                //전환 중일 때 실행되는 부분
                //애니메이션 재생 중 실행되는 부분
                yield return null;
            }
        }
        //Flapping
        else if (RandomAnimationSet.Equals(1))
        {
            while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("Flapping"))
            {
                //전환 중일 때 실행되는 부분
                yield return null;
            }
            while (runeAni.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < exitTime)
            {
                //애니메이션 재생 중 실행되는 부분
                yield return null;
            }

        }
        //LolyTurn
        else if (RandomAnimationSet.Equals(2))
        {
            while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("LolyTurn"))
            {
                //전환 중일 때 실행되는 부분
                yield return null;
            }
            while (runeAni.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < exitTime)
            {
                //애니메이션 재생 중 실행되는 부분
                yield return null;
            }

        }
        //Turn
        else if (RandomAnimationSet.Equals(3))
        {
            while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
            {
                //전환 중일 때 실행되는 부분
                yield return null;
            }
            while (runeAni.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < exitTime)
            {
                //애니메이션 재생 중 실행되는 부분
                yield return null;
            }

        }
        //Point
        else if (RandomAnimationSet.Equals(4))
        {
            while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("Point"))
            {
                //전환 중일 때 실행되는 부분
                yield return null;
            }
            while (runeAni.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < exitTime)
            {
                //애니메이션 재생 중 실행되는 부분
                yield return null;
            }
        }
     // Debug.Log("애니메이션! 번호 " + RandomAnimationSet);
     // //애니메이션 완료 후 실행되는 부분
     // RandomAnimationSet = UnityEngine.Random.Range(0, 4);
     // Debug.Log("애니메이션! 번호 " + RandomAnimationSet);
     // runeAni.SetInteger("MoveRand", RandomAnimationSet);
     // yield return null;
    }
    //룬의 몸이 깜빡임! 19Chapter
    public void Blick_Rune()
    {
        StartCoroutine(_Blick_Rune());
    }
    IEnumerator _Blick_Rune()
    {
        Color startColor = new Color(1, 1, 1, 1); //컬러 초기화
        ChangeMat[0].color = startColor;
        Debug.Log("BlinkRune!");
        Rune.GetComponent<SkinnedMeshRenderer>().materials = ChangeMat; //몸
        while (!(Mathf.Abs(RuneColor.a) < 0.01f))
        {
            if (Mathf.Abs(RuneColor.a) < 0.01f)
            {
                RuneColor.a = 0f;
            }
            RuneColor.a = Mathf.Lerp(RuneColor.a, 0, Time.deltaTime * 1f);
            ChangeMat[0].color = RuneColor;

            yield return null;
        }
        while (!(Mathf.Abs(RuneColor.a) > 0.9f))
        {
            if (Mathf.Abs(RuneColor.a) > 0.9f)
            {
                RuneColor.a = 1f;
            }
            RuneColor.a = Mathf.Lerp(RuneColor.a, 1, Time.deltaTime * 1f);
            ChangeMat[0].color = RuneColor;
          
            yield return null;
        }
        while (!(Mathf.Abs(RuneColor.a) < 0.01f))
        {
            if (Mathf.Abs(RuneColor.a) < 0.01f)
            {
                RuneColor.a = 0f;
            }
            RuneColor.a = Mathf.Lerp(RuneColor.a, 0, Time.deltaTime * 1f);
            ChangeMat[0].color = RuneColor;

            yield return null;
        }
        while (!(Mathf.Abs(RuneColor.a) > 0.9f))
        {
            if (Mathf.Abs(RuneColor.a) > 0.9f)
            {
                RuneColor.a = 1f;
            }
            RuneColor.a = Mathf.Lerp(RuneColor.a, 1, Time.deltaTime * 1f);
            ChangeMat[0].color = RuneColor;

            yield return null;
        }
        Rune.GetComponent<SkinnedMeshRenderer>().materials = OriginMat; //몸
        yield return null;
    }
    IEnumerator Disappear_Rune()
    {
       Rune.GetComponent<SkinnedMeshRenderer>().materials = ChangeMat; //몸
        while (!(Mathf.Abs(RuneColor.a) < 0.01f))
        {
            if (Mathf.Abs(RuneColor.a) < 0.01f)
            {
                RuneColor.a = 0f;
            }
            RuneColor.a = Mathf.Lerp(RuneColor.a, 0, Time.deltaTime * 0.2f);
           ChangeMat[0].color = RuneColor;
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield return null;
    }
    //
    IEnumerator _ByeBye_Rune()
    {
        runeAni.SetTrigger("hello"); //주위 돌기!

        while (!runeAni.GetCurrentAnimatorStateInfo(0).IsName("Hello"))
        {
            //전환 중일 때 실행되는 부분
            yield return null;
        }
        while (runeAni.GetCurrentAnimatorStateInfo(0)
        .normalizedTime < exitTime)
        {
            //애니메이션 재생 중 실행되는 부분
            yield return null;
        }
        runeAni.SetTrigger("hello"); //주위 돌기!

        yield return null;
    }
}
