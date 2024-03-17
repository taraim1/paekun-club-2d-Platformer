using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    int cutSceneTriggerNum;
    bool isInCutScene = false;
    bool isInCutSceneTrigger = false;
    GameObject currentCutSceneTrigger;


    void DestroyPressE() 
    {
        GameObject pressE = GameObject.Find("PressE(Clone)");
        Destroy(pressE);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CutSceneTrigger") //컷씬트리거 진입 감지
        {

            isInCutSceneTrigger = true;
            currentCutSceneTrigger = other.gameObject;
            cutSceneTriggerNum = int.Parse(other.gameObject.name); // 컷씬 번호 알아내기
            switch(cutSceneTriggerNum) //컷씬트리거 진입 시 달라지는것들
            {
                case 0:
                    EntitySpawn.instance.spawnEntity("pressE", new Vector3(262.5f, -37.2f, -1));
                    break;

            }

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CutSceneTrigger") //컷씬트리거 나가는 거 감지
        {
            isInCutSceneTrigger = false;
            switch (cutSceneTriggerNum) //컷씬트리거 나갈 때 달라지는것들
            {
                case 0:
                    DestroyPressE();
                    break;

            }

        }

    }


    void Update()
    {
        if (isInCutSceneTrigger) 
        {
            switch (cutSceneTriggerNum) //컷씬에 따라 돌입방법 및 캐릭터 이동 등 다르게 하기
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.E) && !isInCutScene)
                    {
                        currentCutSceneTrigger.SetActive(false);
                        DestroyPressE();
                        isInCutScene = true;
                    }
                    break;

            }
        }
    }
}
