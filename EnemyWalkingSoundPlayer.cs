using UnityEngine;

/// <summary>
/// Plays walking sounds for the enemy movement. Needs to be attached to the enemy object with Patrol script.
/// </summary>
public class EnemyWalkingSoundPlayer : SoundPlayer
{
    private Patrol patrol;
    private bool hasWalked = false;
    private Vector3 prevLocation;
    [SerializeField] private float stepDistance = 1.5f;

    protected override void OnAwake()
    {
        patrol = transform.GetComponent<Patrol>();
        prevLocation = transform.position;
    }

    /// <summary>
    /// Checks if the enemy has moved far enough for a step or if it has stopped, then plays a step sound
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(transform.position, prevLocation) >= stepDistance)
        {
            PlayStepSound();
            hasWalked = true;
        }

        if (patrol != null) if (patrol.IsStopped() && hasWalked)
            {
                PlayStepSound();
                hasWalked = false;
            }
    }

    /// <summary>
    /// Plays stepSound and marks step position
    /// </summary>
    private void PlayStepSound()
    {
        PlayNextSound();
        prevLocation = transform.position;
    }
}
