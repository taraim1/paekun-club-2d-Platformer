using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public GameObject player;
    public bool isDead = false;
    public Vector3 respawnPoint = new Vector3(-10, 0, 0);
    Animator animator;


    void DestroyAllCloneEnemies()
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag("EnemyClone");

        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
    }

    void Respawn() 
    {
        transform.position = respawnPoint;
        isDead = false;
        animator.SetBool("isDead", false);
    }

    void Start()
    {
           animator = GetComponent<Animator>(); 
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

            isDead = true;
            animator.SetBool("isDead", true);

            Invoke("Respawn", 1f);
            Invoke("DestroyAllCloneEnemies", 1f);
        }
    }


}  
