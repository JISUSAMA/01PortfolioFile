using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SoundMaixer : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;
    public Slider sfxSlider;


    public AudioSource clickAudioSource;
    public AudioClip click_Clip;    //클릭사운드
    public AudioClip popupClick_Clip;   //팝업닫기 사운드
    public AudioClip toggle_Clip;   //토글 사운드
    public AudioClip mapChoice_Clip;    //맵 선택 사운드
    public AudioClip reward_Clip;   //보상받기 사운드
    public AudioClip coinBox_Clip;  //보물상자 열기 사운드




    float backBGMVol = 1f;

    private void OnEnable()
    {
        Initialization();
    }

    void Start()
    {
        //Initialization();
    }

    //초기화
    void Initialization()
    {
        if(SceneManager.GetActiveScene().name != "AsiaMap 3" && SceneManager.GetActiveScene().name != "GameFinish" && SceneManager.GetActiveScene().name != "Join"
            && SceneManager.GetActiveScene().name != "NickName" && SceneManager.GetActiveScene().name != "CharacterChoice" && SceneManager.GetActiveScene().name != "GameFinish")
        {
            audioSlider.value = PlayerPrefs.GetFloat("AT_BackBGMVol");
            sfxSlider.value = PlayerPrefs.GetFloat("AT_SFXVol");

            float sound = audioSlider.value;
            float sfx_sound = sfxSlider.value;

            if (sound == -40f) masterMixer.SetFloat("BGM", -80);
            else masterMixer.SetFloat("BGM", sound);

            if (sfx_sound.Equals(-40f)) masterMixer.SetFloat("SFX", -80);
            else masterMixer.SetFloat("SFX", sfx_sound);
        }
    }


    public void AudioControl()
    {
        if (SceneManager.GetActiveScene().name != "AsiaMap 3" && SceneManager.GetActiveScene().name != "GameFinish" && SceneManager.GetActiveScene().name != "Join"
            && SceneManager.GetActiveScene().name != "NickName" && SceneManager.GetActiveScene().name != "CharacterChoice")
        {
            float sound = audioSlider.value;

            if (sound == -40f) masterMixer.SetFloat("BGM", -80);
            else masterMixer.SetFloat("BGM", sound);

            PlayerPrefs.SetFloat("AT_BackBGMVol", sound);
        }
    }

    public void SFXAudioControl()
    {
        if (SceneManager.GetActiveScene().name != "AsiaMap 3" && SceneManager.GetActiveScene().name != "GameFinish" && SceneManager.GetActiveScene().name != "Join"
            && SceneManager.GetActiveScene().name != "NickName" && SceneManager.GetActiveScene().name != "CharacterChoice")
        {
            float sfx_Sound = sfxSlider.value;

            if (sfx_Sound.Equals(-40f)) masterMixer.SetFloat("SFX", -80);
            else masterMixer.SetFloat("SFX", sfx_Sound);

            PlayerPrefs.SetFloat("AT_SFXVol", sfx_Sound);
        }
    }

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

    //BGM변경
    public void OutGameBGMPlay()
    {
        SoundMaixerManager.instance.OutGameBGMPlay();
    }
}
