using UnityEngine;

/// <summary>
/// Soundplayer for physics objects that drag on surfaces. Uses unity engine collision system.
/// </summary>
public class DraggingSoundPlayer : SoundPlayer
{
    private Rigidbody rb;
    [SerializeField] private float draggingSoundThershold = 0.1F;
    private bool isDragging = false;
    private float draggingTargetVolume;
    private float draggingSoundChangeSpeed;
    [SerializeField] private float draggingSoundChangeSpeedDefault = 0.2F;
    [SerializeField] private float draggingSoundChangeSpeedFadeOut = 0.5F;

    protected override void OnAwake()
    {
        audioSource.clip = soundList[0];
        audioSource.loop = true;
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Starts and stops the audioSource playing if the object doesn't generate enough volume. Also changes the volume with a Mathf.Lerp() delay.
    /// </summary>
    void Update()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, draggingTargetVolume, draggingSoundChangeSpeed);

        if (audioSource.volume > draggingSoundThershold)
        {
            if (!isDragging)
            {
                audioSource.Play();
                draggingSoundChangeSpeed = draggingSoundChangeSpeedDefault;
                isDragging = true;
            }
        }
        else
        {
            if (isDragging)
            {
                audioSource.Stop();
                isDragging = false;
                draggingSoundChangeSpeed = draggingSoundChangeSpeedDefault;
            }
        }
    }

    /// <summary>
    /// Calculates new volume for dragging sound based on velocity every frame.
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionStay(Collision coll)
    {
        draggingTargetVolume = rb.linearVelocity.magnitude * volume;
    }

    /// <summary>
    /// Changes variables that stop dragging object sound
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionExit(Collision coll)
    {
        draggingTargetVolume = 0.0F;
        draggingSoundChangeSpeed = draggingSoundChangeSpeedFadeOut;
    }
}
