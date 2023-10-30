using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstep;
    public FPSController fpsController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
