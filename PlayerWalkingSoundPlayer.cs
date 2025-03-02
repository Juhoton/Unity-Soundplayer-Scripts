using UnityEngine;

/// <summary>
/// Plays walking sounds for the player movement. Needs to be attached to the player object with PlayerMovement script.
/// </summary>
public class PlayerWalkingSoundPlayer : SoundPlayer
{
    private PlayerMovement playerMovement;
    private bool hasWalked = false;
    private Vector3 prevLocation;
    [SerializeField] private float walkingStepDistance = 1f;
    [SerializeField] private float walkingStepVol = 0.3f;
    [SerializeField] private float crouchingStepDistance = 0.5f;
    [SerializeField] private float crouchingStepVol = 0.1f;
    [SerializeField] private float sprintStepVol = 0.5f;

    protected override void OnAwake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        prevLocation = transform.position;
    }

    /// <summary>
    /// Makes different walking sounds whether the player is walking, sprinting or crouching.
    /// </summary>
    private void Update()
    {
        if (!playerMovement.GetCrouchState())
        {
            if (!playerMovement.GetSprintState())
            {
                WalkingSoundLogic(walkingStepDistance, walkingStepVol);
            }
            else
            {
                WalkingSoundLogic(walkingStepDistance, sprintStepVol);
            }

        }
        else
        {
            WalkingSoundLogic(crouchingStepDistance, crouchingStepVol);
        }
    }

    /// <summary>
    /// Logic for checking when to make sounds.
    /// </summary>
    /// <param name="stepDistance"></param>
    /// <param name="stepVol"></param>
    private void WalkingSoundLogic(float stepDistance, float stepVol)
    {
        if (checkStepDistance(stepDistance))
        {
            PlayStepSound(stepVol);
            hasWalked = true;
        }
        if (!playerMovement.GetMovingState() && hasWalked)
        {
            hasWalked = false;
            prevLocation = transform.position;
        }
    }

    /// <summary>
    /// Checks if the given step distance has been covered. Avoids having to copy and paste this code.
    /// </summary>
    /// <param name="stepDistance"></param>
    /// <returns></returns>
    private bool checkStepDistance(float stepDistance)
    {
        if (Vector3.Distance(transform.position, prevLocation) >= stepDistance) return true;
        return false;
    }

    /// <summary>
    /// Plays stepSound and marks step position.
    /// </summary>
    /// <param name="stepVol"></param>
    private void PlayStepSound(float stepVol)
    {
        PlaySound(stepVol);
        prevLocation = transform.position;
    }
}
