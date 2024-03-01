using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0;
    public GameObject greenSlime_prefab;
    List<List<Vector3>> enemySetting = new List<List<Vector3>>();  // ���� ������ �� ��ġ�� ǥ���ϴ� ����Ʈ ex [0][1]�̸� ù��° üũ����Ʈ�� �� ��° �� ������  

    void spawnEnemy(string type, int checkPointNumber, int startIndex, int endIndex) //�� ���� �Լ�, type�� ���� enemysetting[spawnPointNumber]�� startIndex ~ endIndex �ε��������� ��ġ�� ��ȯ
    { 
        GameObject enemy = greenSlime_prefab; //�⺻ ��

        

        for (int i = 0; i < endIndex-startIndex + 1; i++) 
        {
            
            
            GameObject clone = Instantiate(enemy, new Vector3(enemySetting[checkPointNumber][startIndex + i].x, enemySetting[checkPointNumber][startIndex + i].y, enemySetting[checkPointNumber][startIndex + i].z), Quaternion.identity);
            clone.tag = "Untagged";
            clone.SetActive(true);
            enemyCount++;
        }
    
    
    }

    void spawnEnemyRegardingCheckpoint(int ckeckPointNum) //üũ����Ʈ�� ���� �´� �� ����
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
        //�� ���� ������ ��ġ ����
        List<Vector3> enemySpawnPos0 = new List<Vector3>(); // ��������Ʈ 0
        enemySpawnPos0.Add(new Vector3(45, 4, 1.5f));
        enemySpawnPos0.Add(new Vector3(57, 4, 1.5f));

        enemySetting.Add(enemySpawnPos0);


        spawnEnemy("greenSlime", 0, 0, 1); //ó������� �� ����
    }



}
