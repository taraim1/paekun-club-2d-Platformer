using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject player;
    public Vector3 respawnPoint = new Vector3(-10, 0, 0);
    public bool killAllEnemiesTrigger = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // �浹�� ��ֹ� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle") 
        {
            for (int i = 0; i < player.GetComponent<EnemySpawn>().isEnemiesOfSpawnPointSpawned.Count; i++) //�� ��ȯ Ȯ�� ����Ʈ �ʱ�ȭ
            {
                player.GetComponent<EnemySpawn>().isEnemiesOfSpawnPointSpawned[i] = false;

            }



            transform.position = respawnPoint;
            killAllEnemiesTrigger = true;
        }
    }

    private void Update()
    {
        if (player.GetComponent<EnemySpawn>().enemyCount == 0) 
        {
            killAllEnemiesTrigger = false;
        }
    }
}
