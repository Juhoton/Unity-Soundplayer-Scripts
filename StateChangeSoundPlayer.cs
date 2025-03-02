using UnityEngine;

/// <summary>
/// SoundPlayer for the enemy's different state changes. Enemy Patrol script uses this to play pickup sounds.
/// </summary>
public class StateChangeSoundPlayer : SoundPlayer
{
    [SerializeField] private AudioClip alertSound;
    [SerializeField] private float alertVol = 0.5f;
    [SerializeField] private AudioClip alertStopSound;
    [SerializeField] private float alertStopVol = 0.5f;

    [SerializeField] private AudioClip chaseSound;
    [SerializeField] private float chaseVol = 0.5f;

    public void PlayAlertSound()
    {
        PlaySound(alertSound, alertVol);
    }

    public void PlayAlertStopSound()
    {
        PlaySound(alertStopSound, alertStopVol);
    }

    public void PlayChaseSound()
    {
        PlaySound(chaseSound, chaseVol);
    }
}
