using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReceiver : MonoBehaviour
{
    // 달까지 거리 체크 
    private float moonDist;
    public float narrationDist = 0.6f;

    // 휴게소까지 거리 체크...

    // Start is called before the first frame update
    void Start()
    {
        moonDist = Game_DataManager.instance.moonDis;

        StartCoroutine(_Receiver());
    }

    IEnumerator _Receiver()
    {
        float delta;

        while (Game_DataManager.instance.gamePlaying)
        {
            delta = moonDist - Game_DataManager.instance.moonDis;

            if (delta > narrationDist)
            {
                delta = 0f;
                moonDist = Game_DataManager.instance.moonDis;

                // 텍스트

                // 상황에 맞는 텍스트 뿌리기, 나레이션 + 랜덤
                if (true)
                {

                }
            }

            yield return null;
        }

        yield return null;
    }
}
