using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    Animator animator;
    AudioSource audioSource;

    public int SavePointNum;
    public bool isActivated = false;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        //활성화 여부에 따라 세이브포인트 바꾸고 애니메이션 바꿈
        if (collision.CompareTag("Player") && !isActivated) 
        {
            isActivated = true;
            animator.SetBool("isActivated", true);
            player.GetComponent<EnemySpawn>().currentSavePoint = SavePointNum;
            player.GetComponent<PlayerDeath>().respawnPoint = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
            audioSource.Play();
        }
    }
}
