using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0;
    public GameObject greenSlime_prefab;
    List<List<Vector3>> enemySetting = new List<List<Vector3>>();  // 스폰 순서와 적 위치를 표현하는 리스트 ex [0][1]이면 첫번째 체크포인트의 두 번째 적 포지션  

    void spawnEnemy(string type, int checkPointNumber, int startIndex, int endIndex) //적 스폰 함수, type의 적을 enemysetting[spawnPointNumber]의 startIndex ~ endIndex 인덱스까지의 위치에 소환
    { 
        GameObject enemy = greenSlime_prefab; //기본 적

        

        for (int i = 0; i < endIndex-startIndex + 1; i++) 
        {
            
            
            GameObject clone = Instantiate(enemy, new Vector3(enemySetting[checkPointNumber][startIndex + i].x, enemySetting[checkPointNumber][startIndex + i].y, enemySetting[checkPointNumber][startIndex + i].z), Quaternion.identity);
            clone.tag = "Untagged";
            clone.SetActive(true);
            enemyCount++;
        }
    
    
    }

    void spawnEnemyRegardingCheckpoint(int ckeckPointNum) //체크포인트에 따라 맞는 적 스폰
    {
        switch (ckeckPointNum) 
        {
            case 0:
                spawnEnemy("greenSlime", 0, 0, 1);
                break;


        }
    
    }


    void Start()
    {
        //적 스폰 순서와 위치 설정
        List<Vector3> enemySpawnPos0 = new List<Vector3>(); // 스폰포인트 0
        enemySpawnPos0.Add(new Vector3(45, 4, 1.5f));
        enemySpawnPos0.Add(new Vector3(57, 4, 1.5f));

        enemySetting.Add(enemySpawnPos0);


        spawnEnemy("greenSlime", 0, 0, 1); //처음실행시 적 스폰
    }



}
