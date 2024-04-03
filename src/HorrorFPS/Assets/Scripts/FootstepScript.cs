using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstep;
    public AudioSource thud;
    public FPSController fpsController;
    private float defaultPitch;
    // Start is called before the first frame update
    void Start()
    {
        defaultPitch = footstep.pitch;   
    }

    // Update is called once per frame
    void Update()
    {
        footstep.pitch = fpsController.isRunning ? defaultPitch*1.3f : defaultPitch;
        if (fpsController.characterController.isGrounded)
        {
            if (fpsController.isJumping)
            {
                thud.PlayOneShot(thud.clip);
                fpsController.isJumping = false;
            }
        }
        if(fpsController.isMoving && fpsController.characterController.isGrounded)
        {
            if(!footstep.isPlaying) // Check if the sound is not already playing
            {
                footstep.Play(); // Play the footstep sound
            }
            
        }
        else
        {
            footstep.Stop(); // Stop the footstep sound
        }
    }
}
