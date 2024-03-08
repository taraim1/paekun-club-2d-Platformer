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


    // 충돌로 장애물 감지
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            for (int i = 0; i < player.GetComponent<EnemySpawn>().isEnemiesOfSpawnPointSpawned.Count; i++) //적 소환 확인 리스트 초기화
            {
                player.GetComponent<EnemySpawn>().isEnemiesOfSpawnPointSpawned[i] = false;

            }



            transform.position = respawnPoint;
            DestroyAllCloneEnemies();
        }
    }


}  
