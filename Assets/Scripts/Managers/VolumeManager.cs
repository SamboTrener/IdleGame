using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundsSource;

    [SerializeField] Button musicButton;
    [SerializeField] Slider musicSlider;

    [SerializeField] Button soundsButton;
    [SerializeField] Slider soundsSlider;

    [SerializeField] float soundLoudnessCoef;

    float lastMusicSliderPosition;
    float lastSoundsSliderPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        soundsButton.onClick.AddListener(ToggleSounds);
        musicButton.onClick.AddListener(ToggleMusic);

        soundsSlider.onValueChanged.AddListener(ChangeSoundVolume);
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    void ToggleSounds()
    {
        if (soundsSlider.value > 0)
        {
            lastSoundsSliderPosition = soundsSlider.value;
            soundsSlider.value = 0;
            ChangeSoundVolume(0);
        }
        else
        {
            soundsSlider.value = lastSoundsSliderPosition;
            ChangeSoundVolume(lastSoundsSliderPosition);
        }
    }

    void ToggleMusic()
    {
        if(musicSlider.value > 0)
        {
            lastMusicSliderPosition = musicSlider.value;
            musicSlider.value = 0;
            ChangeMusicVolume(0);
        }
        else
        {
            musicSlider.value = lastMusicSliderPosition;
            ChangeMusicVolume(lastMusicSliderPosition);
        }
    }

    void ChangeSoundVolume(float volume)
    {
        soundsSource.volume = volume * soundLoudnessCoef;
    }

    void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void PauseSounds()
    {
        musicSource.Pause();
        soundsSource.Pause();
    }

    public void UnPauseSounds()
    {
        musicSource.UnPause();
        soundsSource.UnPause();
    }
}
