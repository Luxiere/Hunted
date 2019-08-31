using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("music sound") || !PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefsController.SetMusicVolume(.5f);
            PlayerPrefsController.SetSoundVolume(.5f);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMusicVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
