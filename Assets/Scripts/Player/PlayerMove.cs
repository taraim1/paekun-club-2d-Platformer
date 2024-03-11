using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;

    //변수
    public float speed;
    public float maxSpeed;
    public float minSpeed;
    public float jumpPower;
    public float jumpTimingCorrection;
    float horizontal_force = 0;
    bool isOnPlatform = false;
    bool canCorrectedJump = false;
    bool isCorrectedJumpReserved = false; // 점프 보정시 여러 번 점프되는거 방지

    public AudioClip jumpingSound1;
    public AudioClip jumpingSound2;
    public AudioClip jumpingSound3;
    public AudioClip jumpingSound4;

    public GameObject player;
    public GameObject walkingSoundPlayer;

    //함수

    IEnumerator Jump() //점프, 보정있음
    {
        while (true) 
        {
            if (player.GetComponent<PlayerDeath>().isDead) //죽으면 점프 안 됨
            {
                isCorrectedJumpReserved = false;
                yield break;
            }

            if (!animator.GetBool("isJumping") && isOnPlatform)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
                isCorrectedJumpReserved = false;
                PlayRandomJumpSound();
                yield break;
            }
            yield return new WaitForSeconds(0.02f);

        }
    
    
    }
    
    void PlayRandomJumpSound() /*랜덤 점프 사운드 재생*/
    {
        audioSource.Stop();
        AudioClip[] jumpingSounds = new AudioClip[4] { jumpingSound1, jumpingSound2, jumpingSound3, jumpingSound4 };
        audioSource.clip = jumpingSounds[Random.Range(0, 4)];
        audioSource.Play();
    }


    bool ReturnTrueIfIntNot0(int Int) 
    {
        if (Int == 0)
        {
            return false;
        }
        else 
        {
            return true;
        }

    }

    void ChangeSpriteHorizontalDirection() //좌우 이동에 따른 스프라이트 방향 전환
    {
        if (!animator.GetBool("isAttacking") && !player.GetComponent<PlayerDeath>().isDead)
        {
            switch (horizontal_force)
            {
                case 1:
                    spriteRenderer.flipX = false;
                    break;
                case -1:
                    spriteRenderer.flipX = true;
                    break;
            }
        }

    }

    void CheckIfConductCorrectedJump() // 보정된 점프
    {
        if (Input.GetButtonDown("Jump") && canCorrectedJump && !isCorrectedJumpReserved)
        {
            isCorrectedJumpReserved = true;
            StartCoroutine(Jump());

        }

    }


    //================게임===================================================================================

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckIfConductCorrectedJump(); // 보정된 점프

        if (Input.GetButtonUp("Horizontal"))//좌우 키 떼면 멈춤
        { 
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //AD 동시에 누르면 미끄러지는거 수정
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        ChangeSpriteHorizontalDirection(); //좌우 이동에 따른 스프라이트 방향 전환

        if (Mathf.Abs(rigid.velocity.x ) < 0.1f && horizontal_force == 0) //idle -> run 애니메이션 전환
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    void FixedUpdate()
    {
        //좌우 움직임
        horizontal_force = Input.GetAxisRaw("Horizontal");
        if (!player.GetComponent<PlayerDeath>().isDead) 
        {
            rigid.AddForce(Vector2.right * horizontal_force * speed, ForceMode2D.Impulse);
        }
        


        //최대 좌우 속도 제한
        if (rigid.velocity.x > maxSpeed)//우측 속력 제한
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//좌측 속력 제한
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }


        //레이캐스팅으로 플랫폼 감지해서 isOnPlatform, canCorrectedJump 갱신
        RaycastHit2D[] rayHitGrs = new RaycastHit2D[3];
        int canCorrectedJumpCount = 0;
        int isOnPlatformCount = 0;
        for (int i = -1; i < 2; i++) 
        {
            Debug.DrawRay(new Vector2(rigid.position.x +0.55f*i, rigid.position.y - 0.1f), new Vector2(0, -4f), Color.green); // scene에서 ray를 볼 수 있게 해줌, 없어도 되긴 됨
            rayHitGrs[i+1] = Physics2D.Raycast(new Vector2(rigid.position.x + 0.55f * i, rigid.position.y - 0.1f), new Vector2(0, -1f), 4, LayerMask.GetMask("platform"));
            if (rayHitGrs[i+1].collider != null)
            {
                if (rayHitGrs[i + 1].distance < (1.1f + jumpTimingCorrection)) 
                {
                    canCorrectedJumpCount++;
                }
                if (rayHitGrs[i + 1].distance < (1.1f))
                {
                    isOnPlatformCount++;
                }
            }

        }
        canCorrectedJump = ReturnTrueIfIntNot0(canCorrectedJumpCount);
        isOnPlatform = ReturnTrueIfIntNot0(isOnPlatformCount);


        //뛰는 소리 재생
        if (horizontal_force != 0 && !walkingSoundPlayer.GetComponent<WalkingSoundPlayer>().audioSource.isPlaying && isOnPlatform && !player.GetComponent<PlayerDeath>().isDead)
        {
            walkingSoundPlayer.GetComponent<WalkingSoundPlayer>().PlayRandomWalkingSound();
        }


        //점프 후 착지 감지해서 점프 모션 해제
        if (isOnPlatform && rigid.velocity.y <= 0.01f) 
        {
            animator.SetBool("isJumping", false);

        }


        if (isOnPlatform && horizontal_force == 0) // 미끄러지는거 없애기
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        else if (player.GetComponent<PlayerDeath>().isDead)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y); // 죽었을 때 미끄러지는거 없애기
        }
    }
}
