using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoundPlayer : MonoBehaviour
{
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;
    public AudioClip attackSound4;
    public AudioClip attackSound5;
    AudioSource audioSource;
    public void PlayRandomAttackSound() /*랜덤 공격 사운드 재생*/
    {
        audioSource.Stop();
        AudioClip[] attackSounds = new AudioClip[5] { attackSound1, attackSound2, attackSound3, attackSound4, attackSound5 };
        audioSource.clip = attackSounds[Random.Range(0, 5)];
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
