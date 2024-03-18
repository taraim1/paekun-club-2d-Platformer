using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineDirector : MonoBehaviour
{
    public static TimeLineDirector instance;

    public PlayableDirector timeLineDir;
    void Awake()
    {
        timeLineDir = GetComponent<PlayableDirector>();
        if (instance == null) //�̱��� ����
        {
            instance = this;
        }

    }

    public void PlayCurrentTimeLine()
    {
        timeLineDir.Play();
    }
}
