using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public static PlayerDeath instance;

    public GameObject deathSoundPlayer;
    public bool isDead = false;
    public float respawnTime;
    public Vector3 respawnPoint = new Vector3(-10, 0, 0);
    Animator animator;
    AudioSource audioSource_deathSoundPlayer;

    void DestroyAllCloneEnemies()
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag("CloneEntity");

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

    private void Awake()
    {
        if (instance == null) //�̱��� ����
        { 
            instance = this;
        }
    }
    void Start()
    {
           animator = GetComponent<Animator>(); 
           audioSource_deathSoundPlayer = deathSoundPlayer.GetComponent<AudioSource>();
    }


    // �浹�� ��ֹ� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle" && !isDead)
        {
            for (int i = 0; i < EntitySpawn.instance.isEntitiesOfSpawnPointSpawned.Count; i++) //�� ��ȯ Ȯ�� ����Ʈ �ʱ�ȭ
            {
                EntitySpawn.instance.isEntitiesOfSpawnPointSpawned[i] = false;

            }

            isDead = true;
            animator.SetBool("isDead", true);

            audioSource_deathSoundPlayer.Play();

            Invoke("Respawn", respawnTime);
            Invoke("DestroyAllCloneEnemies", respawnTime);
        }
    }


}  
