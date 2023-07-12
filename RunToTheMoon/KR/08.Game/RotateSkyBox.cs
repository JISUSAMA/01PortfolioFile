using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
        //To set the speed, just multiply Time.time with whatever amount you want. 
    }
}
