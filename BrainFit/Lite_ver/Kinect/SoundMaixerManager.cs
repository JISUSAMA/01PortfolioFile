using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;

public class SoundMaixerManager : MonoBehaviour
{
    public static SoundMaixerManager instance { get; private set; }

    public VideoPlayer videoPlayer;
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public AudioSource narraAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip click_Clip;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void AudioControl()
    {
        float sound = audioSlider.value;

        //Debug.Log("sound " + sound);

        if (sound == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
            masterMixer.SetFloat("SFX", -80);
            masterMixer.SetFloat("NARRA", -80);
        }
        else
        {
            float floatLerp = Mathf.InverseLerp(-40, 0, sound);
            //Debug.Log(" floatLerp " + floatLerp);
            masterMixer.SetFloat("BGM", sound);
            masterMixer.SetFloat("SFX", sound);
            masterMixer.SetFloat("NARRA", sound);

            //���� ������ ���� �۰��ϱ� ���ؼ�
            videoPlayer.SetDirectAudioVolume(0, floatLerp);
        }
    }

    void Start()
    {
        AudioControl();

        //PlayNarration("84 õõ�� �������ּ���.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayNarration(string _folderName, string _name)
    {
        string path = "";
        path = "Sound/" + _folderName  + "/" + _name;

        if(narraAudioSource.isPlaying.Equals(false))
        {
            narraAudioSource.clip = Resources.Load<AudioClip>(path);
            narraAudioSource.Play();
        }
    }

    public void ClickSound()
    {
        sfxAudioSource.clip = click_Clip;
        sfxAudioSource.Play();
    }

}
