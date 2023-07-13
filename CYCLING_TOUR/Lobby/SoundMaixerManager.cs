﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundMaixerManager : MonoBehaviour
{
    public static SoundMaixerManager instance { get; private set; }


    GameObject sliderParents;
    public AudioMixer masterMixer;

    Slider audioSlider;
    Slider sfxSlider;

    public AudioSource bgmSource;
    public AudioSource bicycleSource;   //자전거 사운드 소스
    public AudioClip bgmLobby;  //게임외 bgm
    public AudioClip bgmInGame; //게임 bgm
    public AudioClip bgmStore;  //상점 bgm
    public AudioClip bgmQuset;  //퀘스트 bgm
    public AudioClip bgmRank;   //랜킹 bgm

    public AudioSource clickAudioSource;
    public AudioClip click_Clip;    //클릭사운드
    public AudioClip popupClick_Clip;   //팝업닫기 사운드
    public AudioClip toggle_Clip;   //토글 사운드
    public AudioClip mapChoice_Clip;    //맵 선택 사운드
    public AudioClip reward_Clip;   //보상받기 사운드
    public AudioClip coinBox_Clip;  //보물상자 열기 사운드
    public AudioClip chain_Clip;    //자전거 돌리는 소리

    public AudioClip onetwothree_Clip;  //1.2.3사운드
    public AudioClip peopleCherr_Clip;  //환호소리

    float backBGMVol = 1f;

    public bool chainStartState;   //체인소리 시작여부


    private void OnEnable()
    {
        Initialization();
    }

    public void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        Initialization();
    }


    //초기화
    void Initialization()
    {
        //sliderParents = GameObject.Find("UICanvas");

        //audioSlider = sliderParents.transform.Find("PopupGroup").transform.GetChild(0).transform.Find("BackMusicSlider").GetComponent<Slider>();
        //sfxSlider = sliderParents.transform.Find("PopupGroup").transform.GetChild(0).transform.Find("SoundEffectSlider").GetComponent<Slider>();

        //audioSlider.value = PlayerPrefs.GetFloat("BackBGMVol");
        //sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");

        //float sound = audioSlider.value;
        //float sfx_sound = sfxSlider.value;

        float sound = PlayerPrefs.GetFloat("AT_BackBGMVol");
        float sfx_sound = PlayerPrefs.GetFloat("AT_SFXVol");

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", sound);

        if (sfx_sound.Equals(-40f)) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", sfx_sound);

        BicycleChainSoundStop();
    }

    public void AudioControl()
    {
        sliderParents = GameObject.Find("UICanvas");

        audioSlider = sliderParents.transform.Find("PopupGroup").transform.GetChild(0).transform.Find("BackMusicSlider").GetComponent<Slider>();

        float sound = audioSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", sound);

        PlayerPrefs.SetFloat("AT_BackBGMVol", sound);
    }

    public void SFXAudioControl()
    {
        sliderParents = GameObject.Find("UICanvas");

        sfxSlider = sliderParents.transform.Find("PopupGroup").transform.GetChild(0).transform.Find("SoundEffectSlider").GetComponent<Slider>();

        float sfx_Sound = sfxSlider.value;

        if (sfx_Sound.Equals(-40f)) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", sfx_Sound);

        PlayerPrefs.SetFloat("AT_SFXVol", sfx_Sound);
    }


    ////////////배경음 메서드///////////////
    public void OutGameBGMPlay()
    {
        bgmSource.clip = bgmLobby;
        bgmSource.Play();
    }

    public void InGameBGMPlay()
    {
        bgmSource.clip = bgmInGame;
        bgmSource.Play();
    }

    public void StoreBGMPlay()
    {
        bgmSource.clip = bgmStore;
        bgmSource.Play();
    }

    public void QusetBGMPlay()
    {
        bgmSource.clip = bgmQuset;
        bgmSource.Play();
    }

    public void RankBGMPlay()
    {
        bgmSource.clip = bgmRank;
        bgmSource.Play();
    }


    ///////효과음 메서드/////////
    public void ClickSoundPlay()
    {
        clickAudioSource.clip = click_Clip;
        clickAudioSource.Play();
        //Debug.Log("클링");
    }

    public void PopupClickSoundPlay()
    {
        clickAudioSource.clip = popupClick_Clip;
        clickAudioSource.Play();
    }

    public void ToggleSoundPlay()
    {
        clickAudioSource.clip = toggle_Clip;
        clickAudioSource.Play();
    }

    public void MapChoiceSoundPlay()
    {
        clickAudioSource.clip = mapChoice_Clip;
        clickAudioSource.Play();
    }

    public void RewardGetSoundPlay()
    {
        clickAudioSource.clip = reward_Clip;
        clickAudioSource.Play();
    }

    public void CoinBoxOpenSoundPlay()
    {
        clickAudioSource.clip = coinBox_Clip;
        clickAudioSource.Play();
    }

    public void OneTwoThreeSoundPlay()
    {
        clickAudioSource.clip = onetwothree_Clip;
        clickAudioSource.Play();
    }

    public void PeopleCheerSoundPlay()
    {
        clickAudioSource.clip = peopleCherr_Clip;
        clickAudioSource.Play();
    }

    public void PeopleCheerSoundStop()
    {
        clickAudioSource.clip = peopleCherr_Clip;
        clickAudioSource.Stop();
    }

    public void BicycleChainSoundPlay()
    {
        //Debug.Log("사운드 시작");
        chainStartState = true; //체인소리 시작
        bicycleSource.clip = chain_Clip;
        bicycleSource.playOnAwake = true;
        bicycleSource.Play();
        bicycleSource.loop = true;
    }

    public void BicycleChainSoundStop()
    {
        chainStartState = false;    //체인소리 멈춤
        bicycleSource.clip = chain_Clip;
        bicycleSource.playOnAwake = false;
        bicycleSource.loop = false;
        bicycleSource.Stop();
    }
}
