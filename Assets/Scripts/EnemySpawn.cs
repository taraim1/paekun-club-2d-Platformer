using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0; // 이거 enemyDie에서 쓰고 있음
    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    List<Vector3> linearTempDestinations = new List<Vector3>();
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

    void SetLinearDestinationsToEnemy(List<Vector3> Vecs) 
    { 
        clone.GetComponent<EnemyLinearMove>().destinations = Vecs;
    
    }
    
    void spawnEnemyRegardingCheckpoint(int ckeckPointNum) //체크포인트에 따라 맞는 적 스폰
    {
        switch (ckeckPointNum) 
        {
            case 0:
                spawnEnemy("greenSlime", new Vector3(45, 4, 1.5f));
                spawnEnemy("greenSlime", new Vector3(57, 4, 1.5f));
                spawnEnemy("blueSlimeWithBallon", new Vector3(68, 11, 1.5f));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(68, 11, 1.5f));
                linearTempDestinations.Add(new Vector3(75, 11, 1.5f));
                SetLinearDestinationsToEnemy(linearTempDestinations);
                break;


        }
    
    }


    void Start()
    {
        spawnEnemyRegardingCheckpoint(0);
    }



}
