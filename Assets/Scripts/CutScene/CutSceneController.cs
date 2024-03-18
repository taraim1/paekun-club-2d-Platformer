using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    
    public static CutSceneController instance;

    int cutSceneTriggerNum;
    public bool isInCutScene = false;
    bool isInCutSceneTrigger = false;
    GameObject currentCutSceneTrigger;


    void DestroyPressE() 
    {
        GameObject pressE = GameObject.Find("PressE(Clone)");
        Destroy(pressE);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CutSceneTrigger") //�ƾ�Ʈ���� ���� ����
        {

            isInCutSceneTrigger = true;
            currentCutSceneTrigger = other.gameObject;
            cutSceneTriggerNum = int.Parse(other.gameObject.name); // �ƾ� ��ȣ �˾Ƴ���
            switch(cutSceneTriggerNum) //�ƾ�Ʈ���� ���� �� �޶����°͵�
            {
                case 0:
                    EntitySpawn.instance.spawnEntity("pressE", new Vector3(262.5f, -37.2f, -1));
                    break;

            }

        }

    }

    public void CutSceneOff() 
    {
        isInCutScene = false;
        gameObject.GetComponent<PlayerMove>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CutSceneTrigger") //�ƾ�Ʈ���� ������ �� ����
        {
            isInCutSceneTrigger = false;
            switch (cutSceneTriggerNum) //�ƾ�Ʈ���� ���� �� �޶����°͵�
            {
                case 0:
                    DestroyPressE();
                    break;

            }

        }

    }

    private void Awake()
    {
        if (instance == null) //�̱��� ����
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isInCutSceneTrigger) 
        {
            switch (cutSceneTriggerNum) //�ƾ��� ���� ���Թ�� �� ĳ���� �̵� �� �ٸ��� �ϱ�
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.E) && !isInCutScene)
                    {
                        currentCutSceneTrigger.SetActive(false);
                        DestroyPressE();
                        isInCutScene = true;
                        TimeLineDirector.instance.PlayCurrentTimeLine();
                        gameObject.GetComponent<PlayerMove>().enabled = false;
                    }
                    break;

            }
        }
    }
}
