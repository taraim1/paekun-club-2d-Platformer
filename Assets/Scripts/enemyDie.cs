using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDie : MonoBehaviour
{
    public bool isThisDetected = false;
    public GameObject player;

    LineRenderer lineRenderer;
    Animator animator;



    void CheckIfDetected() // 플레이어의 공격 대상인지 감지
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

    IEnumerator FadeOutLinerenderer() // 공격 이후 남는 선 흐리게
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

    void DestroyThis() // 이 오브젝트 파괴 (적 카운트도 줄어들음)
    {
       
        Destroy(gameObject);
    
    }
    void EnemyCountDecrease() // EnemySpawn클래스의 적 카운트 줄이기
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
            if (player.GetComponent<Transform>().position.x < transform.position.x) // 공격 돌진시 플레이어 스프라이트 방향 바꾸기
            {
                player.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().flipX = true;
            }

            animator.SetBool("isDead", true); // 죽는 애니메이션

            lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 1));  // 공격시 칼 선 잔상 그리기
            lineRenderer.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y, 1));
            lineRenderer.enabled = true;

            // 공격 방향대로 가속도 받게 함
            player.GetComponent<Rigidbody2D>().AddForce(new Vector3(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, player.transform.position.z) * 2.4f, ForceMode2D.Impulse); 

            player.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            player.GetComponent<Attack>().AttackAnimationPlay = true;  // 플레이어 attack 스크립트 변수 조정 -> 공격 애니메이션이랑 소리 나오게
            player.GetComponent<Attack>().AttackSoundPlay = true;  
            StartCoroutine(FadeOutLinerenderer());

            gameObject.layer = 9; // 플레이어와 충돌 안 하는 레이어로 바꾸기
            

            EnemyCountDecrease();
            Invoke("DestroyThis", 0.8f);
            


        }

        if (player.GetComponent<PlayerDeath>().killAllEnemiesTrigger) //PlayerDeath클래스에서 killAllEnemies이면 전부 파괴
        {
            EnemyCountDecrease();
            DestroyThis();
        
        }
        
    }
}
