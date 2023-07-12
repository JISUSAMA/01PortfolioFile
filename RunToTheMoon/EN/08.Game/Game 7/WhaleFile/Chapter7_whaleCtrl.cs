using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Chapter7_whaleCtrl : MonoBehaviour
{
    //45.04015,4.2,-1.707056 //돌리 카메라 
    //고래 꼬리빛 이벤트 관리
    public GameObject DollyCart_Whale_ob;
    public CinemachineDollyCart Whale_dolly;
    //마지막 사라지는 이벤트
    public GameObject[] WhaleOb;
    public Material[] Whale_Material;
    public Color Whale_color;
    public ParticleSystem WhaleDisappearParticle; //반짝이 파티클
    public GameObject GlowParticle; //꼬리 파티클

    //조각
    public GameObject pice_ob;
    float targetAlpha = 0.0f;
    private void Awake()
    {
        Whale_dolly.m_Speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ob = other.gameObject;

        if (ob.name.Equals("EndPointWayWhale"))
        {
            Chapter7_Manager.instance.WhaleEventFinish = true;
        }
    }
}
