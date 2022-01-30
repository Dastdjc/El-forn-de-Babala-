using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{
    private static float lastVol = 1;
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        lastVol = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = 0.1f;
        audioSource.volume = startVolume;
        while (audioSource.volume < lastVol)
        {
            audioSource.volume += 1 * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = lastVol;
    }

}
