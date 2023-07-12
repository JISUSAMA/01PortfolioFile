using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SoundMaixer : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider BGM_audioSlider;
    public Slider SFX_audioSlider;


    public void BMG_AudioControl()
    {
        float sound = BGM_audioSlider.value;

        if (sound == -80f) masterMixer.SetFloat("BGM", -40);
        else masterMixer.SetFloat("BGM", sound);
    }
    public void SFX_AudioControl()
    {
        float sound = SFX_audioSlider.value;

        if (sound == -80f) masterMixer.SetFloat("SFX", -40);
        else masterMixer.SetFloat("SFX", sound);
    }


}
