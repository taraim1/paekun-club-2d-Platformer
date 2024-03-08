using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public GameObject player;
    public Vector3 respawnPoint = new Vector3(-10, 0, 0);


    void DestroyAllCloneEnemies()
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag("EnemyClone");

        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
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
            DestroyAllCloneEnemies();
        }
    }


}  
