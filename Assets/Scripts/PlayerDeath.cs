using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject player;
    public bool killAllEnemiesTrigger = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 충돌로 장애물 감지
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle") 
        {
            transform.position = new Vector3(-10, 0, 0);
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
