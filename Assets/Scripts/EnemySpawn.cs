using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0; // �̰� enemyDie���� ���� ����
    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    List<Vector3> linearTempDestinations = new List<Vector3>();
    GameObject clone;

    void spawnEnemy(string type, Vector3 pos) //�� ���� �Լ�, type�� ���� pos ��ġ�� ��ȯ
    {
        GameObject enemy = greenSlime_prefab;
        switch (type) 
        {
            case "greenSlime":
                enemy = greenSlime_prefab; //�⺻ ��
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
    
    void spawnEnemyRegardingCheckpoint(int ckeckPointNum) //üũ����Ʈ�� ���� �´� �� ����
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
