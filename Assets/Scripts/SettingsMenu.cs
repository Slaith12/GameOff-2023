using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        volumeSlider.SetValueWithoutNotify(MusicManager.instance.music.volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVolume(float volume)
    {
        MusicManager.instance.music.volume = volume;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
