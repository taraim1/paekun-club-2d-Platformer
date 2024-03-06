using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0; // 이거 enemyDie에서 쓰고 있음
    public int currentSavePoint; // 이거 SavePoint에서 쓰고 있음
    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    public List<bool> isEnemiesOfSpawnPointSpawned = new List<bool>(); //적 소환 확인 리스트, PlayerDeath에서도 참조중
    int EnemySpawnTriggerNum;
    public List<Vector3> linearTempDestinations = new List<Vector3>();
    GameObject clone;

    void spawnEnemy(string type, Vector3 pos) //적 스폰 함수, type의 적을 pos 위치에 소환
    {
        GameObject enemy = greenSlime_prefab;
        switch (type)
        {
            case "greenSlime":
                enemy = greenSlime_prefab; //기본 적
                break;
            case "blueSlimeWithBallon":
                enemy = BlueSlimeWithBallon_prefab;
                break;

        }

        clone = Instantiate(enemy, pos, Quaternion.identity);
        clone.tag = "Untagged";
        enemyCount++;
    }

    void SetLinearMoveDestinations() 
    {
        for (int i = 0; i < linearTempDestinations.Count; i++)//얕은복사 (무식하게) 해결
        {
            clone.GetComponent<EnemyLinearMove>().destinations.Add(linearTempDestinations[i]);   
        }


    }

    void spawnEnemyRegardingTriggerNum(int triggerNum) //적 소환 트리거 번호에 따라 맞는 적 스폰
    {
        switch (triggerNum)
        {
            case 0:
                spawnEnemy("greenSlime", new Vector3(45, 4, 1.5f));
                spawnEnemy("greenSlime", new Vector3(54, 4, 1.5f));
                break;
            case 1:
                spawnEnemy("blueSlimeWithBallon", new Vector3(97, 8.3f, 1.5f));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(85, 8.3f, 1.5f));
                linearTempDestinations.Add(new Vector3(97, 8.3f, 1.5f));
                SetLinearMoveDestinations();
                clone.GetComponent<EnemyLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;

                spawnEnemy("blueSlimeWithBallon", new Vector3(120, 6.5f, 1.5f));
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;

                spawnEnemy("blueSlimeWithBallon", new Vector3(128, 6.5f, 1.5f));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(128, 6.5f, 1.5f));
                linearTempDestinations.Add(new Vector3(135, 6.5f, 1.5f));
                SetLinearMoveDestinations();
                clone.GetComponent<EnemyLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;
                break;

        }
        isEnemiesOfSpawnPointSpawned[triggerNum] = true;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemySpawnTrigger")
        {

            //EnemySpawnTrigger들 이름에서 숫자 뺴내는 작업
            //EnemySpawnTrigger 태그 오브젝트 이름은 EnemySpawnTrigger (숫자) 형태여야함
            string TriggerName = other.gameObject.name;
            TriggerName = TriggerName.Substring(19, TriggerName.Length - 20);
            EnemySpawnTriggerNum = int.Parse(TriggerName);
            if (isEnemiesOfSpawnPointSpawned[EnemySpawnTriggerNum] == false)
            {
                spawnEnemyRegardingTriggerNum(EnemySpawnTriggerNum);
            }
        }

    }


    void Start()
    {

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemySpawnTrigger").Length; i++) //적 소환 확인 리스트 초기화
        {
            isEnemiesOfSpawnPointSpawned.Add(false);
        }
    }
}


