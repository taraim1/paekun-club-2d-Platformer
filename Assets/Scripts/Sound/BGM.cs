using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip BGM_Normal;
    public AudioClip BGM_BossFight;

    AudioSource BGM_Source;



    public void Set_BGM_normal() 
    { 
        BGM_Source.clip = BGM_Normal;
        BGM_Source.Play();

    }
    public void Set_BGM_BossFight()
    {
        BGM_Source.clip = BGM_BossFight;
        BGM_Source.Play();

    }
    private void Start()
    {
        BGM_Source = GetComponent<AudioSource>();
    }
}
