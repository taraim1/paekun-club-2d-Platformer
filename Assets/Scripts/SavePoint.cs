using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    Animator animator;

    public int SavePointNum;
    public bool isActivated = false;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //활성화 여부에 따라 세이브포인트 바꾸고 애니메이션 바꿈
        if (isActivated)
        {
            animator.SetBool("isActivated", true);
            player.GetComponent<EnemySpawn>().currentSavePoint = SavePointNum;
            player.GetComponent<PlayerDeath>().respawnPoint = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated) 
        {
            isActivated = true;

        }
    }
}
