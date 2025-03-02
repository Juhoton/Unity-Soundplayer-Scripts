using UnityEngine;

/// <summary>
/// Soundplayer for physics objects. Uses unity engine collision system to detect when to play sounds.
/// </summary>
public class CollisionSoundPlayer : SoundPlayer
{
    [SerializeField] private AudioClip defaultAudio;
    [SerializeField] float collisionTimeLimit = 0.3f;
    private bool allowCollisionSound = true;
    [SerializeField] private float enemySoundDetectionThreshold = 1;
    [SerializeField] private bool enemyIgnoreSound = false;
    public bool touchedByPlayer = false;

    protected override void OnAwake()
    {
        if (defaultAudio != null && soundList.Count == 0)
        {
            soundList.Add(defaultAudio);
        }
        InvokeRepeating("EnableCollisionSound", 1f, collisionTimeLimit);
    }

    /// <summary>
    /// On object collision it plays a sound with volume that changes with velocity.
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter(Collision coll)
    {
        if (allowCollisionSound)
        {
            allowCollisionSound = false;
            float hitVol = coll.relativeVelocity.magnitude * volume;

            //TODO: Think a smart way to handle layers so it's more scalable and not hardcoded
            if ((coll.gameObject.layer == 6 || touchedByPlayer) && hitVol > enemySoundDetectionThreshold && !enemyIgnoreSound)
            {
                CreatePlayerGeneratedSound(coll, hitVol);
            }
            else
            {
                AudioSource.PlayClipAtPoint(soundList[0], coll.GetContact(0).point, hitVol);
            }
        }
    }

    /// <summary>
    /// Automated method that enables collisionSound every collisionTimeLimit.
    /// Started on OnAwake() with InvokeRepeating()
    /// </summary>
    void EnableCollisionSound()
    {
        allowCollisionSound = true;
    }

    void CreatePlayerGeneratedSound(Collision coll, float hitVol)
    {
        GameObject audioObj = new GameObject("CollisionSound");
        audioObj.transform.position = coll.GetContact(0).point;
        //TODO: Think a smart way to handle layers so it's more scalable and not hardcoded
        audioObj.layer = 11;
        SphereCollider collider = audioObj.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 0.5f;

        AudioSource audioSource = audioObj.AddComponent<AudioSource>();
        audioSource.clip = soundList[0];
        audioSource.volume = hitVol;
        audioSource.Play();

        Destroy(audioObj, audioSource.clip.length);
        touchedByPlayer = false;
    }
}
