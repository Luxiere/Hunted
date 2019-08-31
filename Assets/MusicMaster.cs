using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaster : MonoBehaviour
{
    public static IEnumerator FadeIn(AudioSource audioSourceOut, AudioSource audioSourceIn, float FadeTime)
    {
        float startVolumeIn = PlayerPrefsController.GetMusicVolume();
        float startVolumeOut = PlayerPrefsController.GetMusicVolume();

        while (audioSourceIn.volume > 0)
        {
            audioSourceIn.volume -= startVolumeIn * Time.deltaTime / FadeTime;
            audioSourceOut.volume += startVolumeOut * Time.deltaTime / FadeTime;

            yield return null;
        }
    }
}
