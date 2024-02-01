using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSoundClips;
    public void playFootstep()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(footstepSoundClips, transform, 1f);

    }
}
