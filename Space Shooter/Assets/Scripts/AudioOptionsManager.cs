using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectVolume { get; private set; }

    [SerializeField] private Text musicSliderText;
    [SerializeField] private Text soundEffectSliderText;

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        musicSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMusicMixerVolume();
    }

    public void OnSoundEffectSliderValueChange(float value)
    {
        soundEffectVolume = value;
        soundEffectSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateSoundEffectMixerVolume();
    }
}
