using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitySpawn : MonoBehaviour
{
    public static EntitySpawn instance;

    public float EnemyZ;
    public float PlatformZ;
    public int currentSavePoint; // 이거 SavePoint에서 쓰고 있음

    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    public GameObject FallingPlatform_prefab;

    public List<bool> isEntitiesOfSpawnPointSpawned = new List<bool>(); //소환 확인 리스트, PlayerDeath에서도 참조중
    int SpawnTriggerNum;
    public List<Vector3> linearTempDestinations = new List<Vector3>();
    GameObject clone;

    void spawnEntity(string type, Vector3 pos) //개체 스폰 함수, type의 개체를 pos 위치에 소환
    {
        GameObject entity = greenSlime_prefab;
        switch (type) // 소환할 개체 설정
        {
            case "greenSlime":
                entity = greenSlime_prefab; //기본 개체
                break;
            case "blueSlimeWithBallon":
                entity = BlueSlimeWithBallon_prefab;
                break;
            case "fallingPlatform":
                entity = FallingPlatform_prefab;
                break;

        }

        clone = Instantiate(entity, pos, Quaternion.identity);
        clone.tag = "CloneEntity";
    }


    void SetLinearMoveDestinations() 
    {
        for (int i = 0; i < linearTempDestinations.Count; i++)//얕은복사 (무식하게) 해결
        {
            clone.GetComponent<EntityLinearMove>().destinations.Add(linearTempDestinations[i]);   
        }


    }

    void spawnEntityRegardingTriggerNum(int triggerNum) //소환 트리거 번호에 따라 맞는 개체 스폰
    {
        switch (triggerNum)
        {
            case 0:
                spawnEntity("greenSlime", new Vector3(45, 3f, EnemyZ));
                spawnEntity("greenSlime", new Vector3(54, 3f, EnemyZ));
                break;
            case 1:
                spawnEntity("blueSlimeWithBallon", new Vector3(97, 8.3f, EnemyZ));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(85, 8.3f, EnemyZ));
                linearTempDestinations.Add(new Vector3(97, 8.3f, EnemyZ));
                SetLinearMoveDestinations();
                clone.GetComponent<EntityLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EntityLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(120, 6.5f, EnemyZ));
                clone.GetComponent<EntityLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(128, 6.5f, EnemyZ));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(128, 6.5f, EnemyZ));
                linearTempDestinations.Add(new Vector3(135, 6.5f, EnemyZ));
                SetLinearMoveDestinations();
                clone.GetComponent<EntityLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EntityLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(150, 6.5f, EnemyZ));
                spawnEntity("blueSlimeWithBallon", new Vector3(157, 6.5f, EnemyZ));
                spawnEntity("blueSlimeWithBallon", new Vector3(164, 6.5f, EnemyZ));
                spawnEntity("blueSlimeWithBallon", new Vector3(171, 6.5f, EnemyZ));
                spawnEntity("blueSlimeWithBallon", new Vector3(178, 6.5f, EnemyZ));
                spawnEntity("blueSlimeWithBallon", new Vector3(185, 6.5f, EnemyZ));
                break;
            case 2:
                spawnEntity("fallingPlatform", new Vector3(225, 10.7f, PlatformZ));
                spawnEntity("fallingPlatform", new Vector3(236.5f, 10.7f, PlatformZ));
                break;
            case 3:
                spawnEntity("fallingPlatform", new Vector3(267.5f, 0f, PlatformZ));
                clone.transform.localScale = new Vector3(2f, 6f, 1f);
                clone.GetComponent<FallingPlatform>().FallSpeed = 6.6f;
                spawnEntity("greenSlime", new Vector3(280, -4.5f, EnemyZ));
                spawnEntity("greenSlime", new Vector3(273, -11.5f, EnemyZ));
                break;
            case 4:
                spawnEntity("fallingPlatform", new Vector3(232f, -18f, PlatformZ));
                clone.transform.localScale = new Vector3(3f, 4f, 1f);
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(232f, -18f, EnemyZ));
                linearTempDestinations.Add(new Vector3(222f, -18f, EnemyZ));
                SetLinearMoveDestinations();
                clone.GetComponent<EntityLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EntityLinearMove>().isThisFallingPlatform = true;
                clone.GetComponent<EntityLinearMove>().speed = 7.2f;
                clone.GetComponent<FallingPlatform>().FallSpeed = 4.5f;
                break;
        }
        isEntitiesOfSpawnPointSpawned[triggerNum] = true;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemySpawnTrigger")
        {


            SpawnTriggerNum = int.Parse(other.gameObject.name);
            if (isEntitiesOfSpawnPointSpawned[SpawnTriggerNum] == false)
            {
                spawnEntityRegardingTriggerNum(SpawnTriggerNum);
            }
        }

    }
    private void Awake()
    {
        if (instance == null) //싱글톤 생성
        {
            instance = this;
        }
    }
    void Start()
    {

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemySpawnTrigger").Length; i++) //적 소환 확인 리스트 초기화
        {
            isEntitiesOfSpawnPointSpawned.Add(false);
        }
    }
}


