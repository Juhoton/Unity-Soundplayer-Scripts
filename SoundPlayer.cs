using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Parent class for all soundPlayers. Initializes the needed stuff to make sounds happen, and has default methods for sounds. 
/// Classes that inherit this class will determine when the sounds are played. Stores all the sounds that will be played.
/// </summary>
public abstract class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    protected AudioSource audioSource;
    [SerializeField] protected List<AudioClip> soundList;
    private int lastSound = -1;
    [SerializeField] private float lowPitchRange = 0.9F;
    [SerializeField] private float highPitchRange = 1.1F;
    [SerializeField] protected float volume = 0.3f;

    /// <summary>
    /// Initializes the AudioSource. Override OnAwake method for child class awake operations.
    /// </summary>
    private void Awake()
    {
        if (gameObject.GetComponents<SoundPlayer>().Length == 1 && gameObject.TryGetComponent(out AudioSource existingAudioSource))
        {
            audioSource = existingAudioSource;
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1F;
        audioSource.outputAudioMixerGroup = mixer;
        setVolume();
        OnAwake();
    }

    /// <summary>
    /// Awake method for child classes to initialize stuff. Needs override.
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// Randomizes the pitch for the different audiosources when called
    /// </summary>
    protected void RandomizePitch()
    {
        var pitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);
        audioSource.pitch = pitch;
    }

    /// <summary>
    /// Plays a random sound from the list, but not the previous.
    /// </summary>
    protected void PlayRandomSound()
    {
        if (soundList.Count == 1)
        {
            PlaySound();
            return;
        }

        int nextSound = lastSound;
        while (lastSound == nextSound)
        {
            nextSound = UnityEngine.Random.Range(0, soundList.Count - 1);
        }
        PlaySound(nextSound);
    }

    /// <summary>
    /// Plays the first sound from the list, and sets it as played
    /// </summary>
    protected void PlaySound()
    {
        PlaySound(0);
    }

    /// <summary>
    /// Plays the first sound from the list with specific volume, and sets it as played
    /// </summary>
    /// <param name="vol"></param>
    protected void PlaySound(float vol)
    {
        PlaySound(0, vol);
    }

    /// <summary>
    /// Plays the asked sound from the list, and sets it as played
    /// </summary>
    /// <param name="number"></param>
    protected void PlaySound(int number)
    {
        PlaySound(number, 1f);
    }

    /// <summary>
    /// Plays the asked sound from the list with specific volume, and sets it as played.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="vol"></param>
    protected void PlaySound(int number, float vol)
    {
        if (soundList.Count == 0) { Debug.Log("SoundList is empty"); return; }
        if (soundList.Count < number) { Debug.Log("Requested sound is out of range of soundlist"); return; }
        if (soundList[number] == null) { Debug.Log("Requested sound is missing"); return; }
        RandomizePitch();
        setVolume();
        audioSource.PlayOneShot(soundList[number], vol);
        lastSound = number;
    }

    /// <summary>
    /// Plays the given audioclip with specific volume.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="vol"></param>
    protected void PlaySound(AudioClip clip, float vol)
    {
        RandomizePitch();
        setVolume();
        audioSource.PlayOneShot(clip, vol);
    }

    /// <summary>
    /// Loops through the soundlist to play sounds.
    /// </summary>
    protected void PlayNextSound()
    {
        int nextSound = lastSound + 1;
        if (nextSound >= soundList.Count) nextSound = 0;
        PlaySound(nextSound);
    }

    /// <summary>
    /// Changes the audiosource volume to the script volume variable.
    /// </summary>
    protected void setVolume()
    {
        audioSource.volume = volume;
    }

    /// <summary>
    /// Checks if object audiosource is playing.
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
