using UnityEngine;

/// <summary>
/// Soundplayer for picking up physics objects. Player Hold script uses this to play pickup sounds.
/// </summary>
public class PickUpSoundPlayer : SoundPlayer
{
    [SerializeField] private AudioClip defaultAudio;

    protected override void OnAwake()
    {
        if (defaultAudio != null && soundList.Count == 0)
        {
            soundList.Add(defaultAudio);
        }
    }

    /// <summary>
    /// Method for other scripts to play sounds.
    /// </summary>
    public void PlayPickUpSound()
    {
        PlayRandomSound();
    }
}
