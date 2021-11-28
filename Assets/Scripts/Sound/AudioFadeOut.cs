using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = 0.1f;
        audioSource.volume = startVolume;
        while (audioSource.volume < 0.9)
        {
            audioSource.volume += 1 * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = 1;
    }

}
