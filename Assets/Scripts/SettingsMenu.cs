using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
