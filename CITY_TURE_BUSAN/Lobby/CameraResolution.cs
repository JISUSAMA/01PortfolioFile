using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{

    private void Awake()
    {
        //Camera.main.aspect = 16f / 9f;
        Camera_Resolution();
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);

    //카메라 9:16 비율로 레터박스 해주는 함수
    void Camera_Resolution()
    {
        Camera camera = GetComponent<Camera>();

        //카메라 컴포넌트의 Viewport Rect
        Rect rt = camera.rect;

        //가로모드 16:9
        float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        camera.rect = rt;
    }
   
}
