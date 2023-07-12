using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Lobby_SoundManager : MonoBehaviour
{
    //사운드 조절
    public Slider backBGMVolume;
    public AudioSource audioBGM;

    float backBGMVol = 1f;

    void Start()
    {
        Initialization();
    }

    //초기화
    void Initialization()
    {
        audioBGM.volume = PlayerPrefs.GetFloat("Busan_BackBGMVol");
        backBGMVolume.value = PlayerPrefs.GetFloat("Busan_BackBGMVol");
    }


    private void Update()
    {
        SoundBGMSlider();
    }


    public void SoundBGMSlider()
    {
        audioBGM.volume = backBGMVolume.value;
        backBGMVol = backBGMVolume.value;
        PlayerPrefs.SetFloat("Busan_BackBGMVol", backBGMVol);
    }
}
