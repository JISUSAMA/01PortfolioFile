using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    float fadeTime; //fadeSpeed값이 10이면 1초(갑이 클수록 빠름)
    Image img;  //페이드 효과에 사용되는 검은 바탕 이미지

    public bool fadeStartState; //페이드 효과 상태여부



    void Start()
    {
        img = GetComponent<Image>();
    }

    public void Fade()
    {
        //Debug.Log("이벤트 놉>???");
        StartCoroutine(_Fade(1, 0));
    }


    IEnumerator _Fade(float start, float end)
    {
        fadeStartState = true;  //시작 했다고
        float currentTime = 0.0f;
        float percent = 0.0f;


        while (percent < 1)
        {
            //fadeTime으로 나누어서 fadeTime시간 동안 percent값이 0에서 1로 증가하도록함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            //알파값을 start부터 end까지 fadeTime시간동안 변화시킨다.
            Color color = img.color;
            color.a = Mathf.Lerp(start, end, percent);
            img.color = color;

            yield return null;
        }
    }
}
