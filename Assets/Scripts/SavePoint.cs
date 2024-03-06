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
        //Ȱ��ȭ ���ο� ���� ���̺�����Ʈ �ٲٰ� �ִϸ��̼� �ٲ�
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
