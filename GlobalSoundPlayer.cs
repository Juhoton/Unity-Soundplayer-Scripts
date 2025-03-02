using UnityEngine;

/// <summary>
/// Simple class that inherits the soundplayer, and sets the audio to play globally. Quick solution.
/// </summary>
public class GlobalSoundPlayer : SoundPlayer
{
    override protected void OnAwake()
    {
        audioSource.spatialBlend = 0f;
    }

    /// <summary>
    /// Plays the sound that is on the specific slot in the soundlist.
    /// </summary>
    /// <param name="slot"></param>
    public void PlaySpecificSound(int slot)
    {
        PlaySound(slot);
    }
}
