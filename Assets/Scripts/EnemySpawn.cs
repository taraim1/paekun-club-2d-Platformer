using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{

    public int enemyCount = 0; // �̰� enemyDie���� ���� ����
    public int currentSavePoint; // �̰� SavePoint���� ���� ����
    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    public List<bool> isEnemiesOfSpawnPointSpawned = new List<bool>(); //�� ��ȯ Ȯ�� ����Ʈ, PlayerDeath������ ������
    int EnemySpawnTriggerNum;
    public List<Vector3> linearTempDestinations = new List<Vector3>();
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

    void SetLinearMoveDestinations() 
    {
        for (int i = 0; i < linearTempDestinations.Count; i++)//�������� (�����ϰ�) �ذ�
        {
            clone.GetComponent<EnemyLinearMove>().destinations.Add(linearTempDestinations[i]);   
        }


    }

    void spawnEnemyRegardingTriggerNum(int triggerNum) //�� ��ȯ Ʈ���� ��ȣ�� ���� �´� �� ����
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

            //EnemySpawnTrigger�� �̸����� ���� ������ �۾�
            //EnemySpawnTrigger �±� ������Ʈ �̸��� EnemySpawnTrigger (����) ���¿�����
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

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemySpawnTrigger").Length; i++) //�� ��ȯ Ȯ�� ����Ʈ �ʱ�ȭ
        {
            isEnemiesOfSpawnPointSpawned.Add(false);
        }
    }
}


