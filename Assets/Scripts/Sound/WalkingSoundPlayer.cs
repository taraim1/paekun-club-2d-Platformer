using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundPlayer : MonoBehaviour
{
    public AudioClip walkingSound1;
    public AudioClip walkingSound2;
    public AudioClip walkingSound3;
    public AudioClip walkingSound4;
    public AudioClip walkingSound5;
    public AudioClip walkingSound6;
    public AudioClip walkingSound7;

    public AudioSource audioSource;
    public void PlayRandomWalkingSound() /*·£´ý °È±â »ç¿îµå Àç»ý*/
    {
        AudioClip[] walkingSounds = new AudioClip[7] { walkingSound1, walkingSound2, walkingSound3, walkingSound4, walkingSound5, walkingSound6, walkingSound7 };
        audioSource.clip = walkingSounds[Random.Range(0, 7)];
        audioSource.Play();
    }

}
