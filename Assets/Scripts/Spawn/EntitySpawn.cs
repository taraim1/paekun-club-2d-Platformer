using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitySpawn : MonoBehaviour
{
    public static EntitySpawn instance;

    public int currentSavePoint; // �̰� SavePoint���� ���� ����

    public GameObject greenSlime_prefab;
    public GameObject BlueSlimeWithBallon_prefab;
    public GameObject FallingPlatform_prefab;

    public List<bool> isEntitiesOfSpawnPointSpawned = new List<bool>(); //��ȯ Ȯ�� ����Ʈ, PlayerDeath������ ������
    int EnemySpawnTriggerNum;
    public List<Vector3> linearTempDestinations = new List<Vector3>();
    GameObject clone;

    void spawnEntity(string type, Vector3 pos) //��ü ���� �Լ�, type�� ��ü�� pos ��ġ�� ��ȯ
    {
        GameObject entity = greenSlime_prefab;
        switch (type) // ��ȯ�� ��ü ����
        {
            case "greenSlime":
                entity = greenSlime_prefab; //�⺻ ��ü
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
        for (int i = 0; i < linearTempDestinations.Count; i++)//�������� (�����ϰ�) �ذ�
        {
            clone.GetComponent<EnemyLinearMove>().destinations.Add(linearTempDestinations[i]);   
        }


    }

    void spawnEntityRegardingTriggerNum(int triggerNum) //��ȯ Ʈ���� ��ȣ�� ���� �´� ��ü ����
    {
        switch (triggerNum)
        {
            case 0:
                spawnEntity("greenSlime", new Vector3(45, 3f, 1.5f));
                spawnEntity("greenSlime", new Vector3(54, 3f, 1.5f));
                break;
            case 1:
                spawnEntity("blueSlimeWithBallon", new Vector3(97, 8.3f, 1.5f));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(85, 8.3f, 1.5f));
                linearTempDestinations.Add(new Vector3(97, 8.3f, 1.5f));
                SetLinearMoveDestinations();
                clone.GetComponent<EnemyLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(120, 6.5f, 1.5f));
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(128, 6.5f, 1.5f));
                linearTempDestinations.Clear();
                linearTempDestinations.Add(new Vector3(128, 6.5f, 1.5f));
                linearTempDestinations.Add(new Vector3(135, 6.5f, 1.5f));
                SetLinearMoveDestinations();
                clone.GetComponent<EnemyLinearMove>().DoThisObjectLinearMove = true;
                clone.GetComponent<EnemyLinearMove>().startTrembleY = true;

                spawnEntity("blueSlimeWithBallon", new Vector3(150, 6.5f, 1.5f));
                spawnEntity("blueSlimeWithBallon", new Vector3(157, 6.5f, 1.5f));
                spawnEntity("blueSlimeWithBallon", new Vector3(164, 6.5f, 1.5f));
                spawnEntity("blueSlimeWithBallon", new Vector3(171, 6.5f, 1.5f));
                spawnEntity("blueSlimeWithBallon", new Vector3(178, 6.5f, 1.5f));
                spawnEntity("blueSlimeWithBallon", new Vector3(185, 6.5f, 1.5f));
                break;
            case 2:
                spawnEntity("fallingPlatform", new Vector3(225, 10.7f, 1f));
                spawnEntity("fallingPlatform", new Vector3(236.5f, 10.7f, 1f));
                break;
            case 3:
                spawnEntity("fallingPlatform", new Vector3(267.5f, 0f, 1f));
                clone.transform.localScale = new Vector3(2f, 6f, 1f);
                spawnEntity("greenSlime", new Vector3(280, -4.5f, 1.5f));
                spawnEntity("greenSlime", new Vector3(273, -11.5f, 1.5f));
                break;
        }
        isEntitiesOfSpawnPointSpawned[triggerNum] = true;

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
            if (isEntitiesOfSpawnPointSpawned[EnemySpawnTriggerNum] == false)
            {
                spawnEntityRegardingTriggerNum(EnemySpawnTriggerNum);
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
    void Start()
    {

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemySpawnTrigger").Length; i++) //�� ��ȯ Ȯ�� ����Ʈ �ʱ�ȭ
        {
            isEntitiesOfSpawnPointSpawned.Add(false);
        }
    }
}


