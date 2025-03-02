using System.Collections;
using UnityEngine;

/// <summary>
/// Soundplayer for making sounds happen at random intervals. Mainly used for environmental sounds.
/// </summary>
public class DelayedSoundPlayer : SoundPlayer
{
    [SerializeField] private float startWait = 20f;
    [SerializeField] private float minWait = 20f;
    [SerializeField] private float maxWait = 60f;


    protected override void OnAwake()
    {
        StartCoroutine(SoundDelayer());
    }

    /// <summary>
    /// Plays sounds at random intervals between set range.
    /// </summary>
    /// <returns></returns>
    IEnumerator SoundDelayer()
    {
        yield return new WaitForSeconds(startWait);
        while (audioSource != null)
        {
            PlayRandomSound();
            yield return new WaitForSeconds(UnityEngine.Random.Range(minWait, maxWait));
        }
    }
}
