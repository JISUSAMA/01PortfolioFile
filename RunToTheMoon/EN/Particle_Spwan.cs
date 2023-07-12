using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class Particle_Spwan : MonoBehaviour
{
    [SerializeField] Camera ca;
    public GameObject Touch_Prefeb;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {   
            Creat_touch();
            SoundFunction.Instance.BasicTouch_Sound();
                 
        }

#if UNITY_ANDROID || UNITY_IOS

        // Touch myTouch = Input.GetTouch(0);
        Touch[] myTouches = Input.touches;
        if (Input.touchCount > 0)
        {
            if(Input.touchCount == 0)
            {
                SoundFunction.Instance.BasicTouch_Sound();
            }
            Debug.Log("'********1");
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touchCount < 10) { Creat_touch(myTouches[i].position); }
            }
        }
#endif  
    }
    private void Creat_touch()
    {
        Vector3 mPosition = ca.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 100;
      //  Debug.Log(mPosition);
        Debug.DrawLine(Vector3.zero, mPosition, Color.red);
        Instantiate(Touch_Prefeb, mPosition, Quaternion.identity);
    }

    private void Creat_touch(Vector3 _touchPos)
    {
        Vector3 mPosition = ca.ScreenToWorldPoint(_touchPos);
        mPosition.z = 100;
     //   Debug.Log(mPosition);
        Debug.DrawLine(Vector3.zero, mPosition, Color.red);
        Instantiate(Touch_Prefeb, mPosition, Quaternion.identity);

    }
}
