using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDie : MonoBehaviour
{
    public bool isThisDetected = false;
    public GameObject player;

    LineRenderer lineRenderer;
    Animator animator;



    void CheckIfDetected() // �÷��̾��� ���� ������� ����
    {
        if (player.GetComponent<Attack>().detectedEnemyId == transform.gameObject.GetInstanceID())
        {
            isThisDetected = true;
        }
        else
        {
            isThisDetected = false;
        }
    }

    IEnumerator FadeOutLinerenderer() // ���� ���� ���� �� �帮��
    {
        while (true) 
        {
            if (lineRenderer.startColor.a < 0.11f)
            {
                lineRenderer.enabled = false;
                yield break;


            }
            lineRenderer.startColor = new Color(1, 1, 1, lineRenderer.startColor.a - 0.1f);
            lineRenderer.endColor = new Color(1, 1, 1, lineRenderer.endColor.a - 0.1f);


            yield return new WaitForSeconds(0.05f);

        }
   

    }

    void DestroyThis() // �� ������Ʈ �ı� (�� ī��Ʈ�� �پ����)
    {
       
        Destroy(gameObject);
    
    }
    void EnemyCountDecrease() // EnemySpawnŬ������ �� ī��Ʈ ���̱�
    {
        player.GetComponent<EnemySpawn>().enemyCount--;

    }
   

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        lineRenderer.startWidth = 4f;
        lineRenderer.endWidth = 4f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        lineRenderer.startColor = new Color(1,1,1,1f);
        lineRenderer.endColor = new Color(1, 1, 1, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfDetected();

        if (Input.GetMouseButtonDown(0) && isThisDetected == true) 
        {
            if (player.GetComponent<Transform>().position.x < transform.position.x) // ���� ������ �÷��̾� ��������Ʈ ���� �ٲٱ�
            {
                player.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().flipX = true;
            }

            animator.SetBool("isDead", true); // �״� �ִϸ��̼�

            lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 1));  // ���ݽ� Į �� �ܻ� �׸���
            lineRenderer.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y, 1));
            lineRenderer.enabled = true;

            // ���� ������ ���ӵ� �ް� ��
            player.GetComponent<Rigidbody2D>().AddForce(new Vector3(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, player.transform.position.z) * 2.4f, ForceMode2D.Impulse); 

            player.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            player.GetComponent<Attack>().AttackAnimationPlay = true;  // �÷��̾� attack ��ũ��Ʈ ���� ���� -> ���� �ִϸ��̼��̶� �Ҹ� ������
            player.GetComponent<Attack>().AttackSoundPlay = true;  
            StartCoroutine(FadeOutLinerenderer());

            gameObject.layer = 9; // �÷��̾�� �浹 �� �ϴ� ���̾�� �ٲٱ�
            

            EnemyCountDecrease();
            Invoke("DestroyThis", 0.8f);
            


        }

        if (player.GetComponent<PlayerDeath>().killAllEnemiesTrigger) //PlayerDeathŬ�������� killAllEnemies�̸� ���� �ı�
        {
            EnemyCountDecrease();
            DestroyThis();
        
        }
        
    }
}
