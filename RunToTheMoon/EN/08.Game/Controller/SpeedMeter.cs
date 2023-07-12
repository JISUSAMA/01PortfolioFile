using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField] RectTransform velocityPin;    // 130 ~ -45
    [SerializeField] TMP_Text velocityText;
    public string velocity_str;
    public float velocity_f; 

    public static SpeedMeter instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    void Update()
    {
        if (!Game_DataManager.instance.runEndState && Game_DataManager.instance.gamePlaying)
        {
            // velocityPin.eulerAngles.z > 130 ~ -45
            // velocityText > 0 ~ 20
            velocityPin.eulerAngles = new Vector3(0 , 0 , Mathf.Lerp(130f, -45f, Mathf.InverseLerp(0, 1.1f, RunnerPlayer1.instance.speed)));
            velocityText.text = Math.Truncate(Mathf.Lerp(0, 20, Mathf.InverseLerp(0, 1.1f, RunnerPlayer1.instance.speed))).ToString();
            velocity_f =  float.Parse(velocityText.text);
        }
    }
}
