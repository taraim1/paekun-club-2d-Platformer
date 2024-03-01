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
    public float maxSpeed;
    public float jumpPower;
    public float jumpTimingCorrection;

    public AudioClip walkingSound1;
    public AudioClip walkingSound2;
    public AudioClip walkingSound3;
    public AudioClip walkingSound4;
    public AudioClip walkingSound5;
    public AudioClip walkingSound6;
    public AudioClip walkingSound7;



    public AudioClip jumpingSound1;
    public AudioClip jumpingSound2;
    public AudioClip jumpingSound3;
    public AudioClip jumpingSound4;




    //함수

    void Jump() /*점프*/
    {   
        if (!animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            PlayRandomJumpSound();
        }
    }

    void CorrectedJump() /*점프키 빨리 누르면 점프 씹히는거 보정된 점프*/
    {
        if (animator.GetBool("isJumping"))
        {
            Invoke("Jump", jumpTimingCorrection);
        }
        else
        {
            Jump();
        }
    }
    
    void PlayRandomJumpSound() /*랜덤 점프 사운드 재생*/
    {
        audioSource.Stop();
        AudioClip[] jumpingSounds = new AudioClip[4] { jumpingSound1, jumpingSound2, jumpingSound3, jumpingSound4 };
        audioSource.clip = jumpingSounds[Random.Range(0, 4)];
        audioSource.Play();
    }

    void PlayRandomWalkingSound() /*랜덤 걷기 사운드 재생*/
    {
        AudioClip[] walkingSounds = new AudioClip[7] { walkingSound1, walkingSound2, walkingSound3, walkingSound4, walkingSound5, walkingSound6, walkingSound7 };
        audioSource.clip = walkingSounds[Random.Range(0, 7)];
        audioSource.Play();
    }





    

    //게임

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) // 점프
        {
            CorrectedJump();
           
        }

        if (Input.GetButtonUp("Horizontal"))//좌우 키 떼면 멈춤
        { 
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //AD 동시에 누르면 미끄러지는거 수정
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") == 1 && !animator.GetBool("isAttacking"))//좌우 이동에 따른 스프라이트 방향 전환
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && !animator.GetBool("isAttacking"))
        {
            spriteRenderer.flipX = true;
        }

        if (Mathf.Abs(rigid.velocity.x ) < 0.1f) //idle -> run 애니메이션 전환
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
        float horizontal_force = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * horizontal_force, ForceMode2D.Impulse);


        //최대 좌우 속도 제한
        if (rigid.velocity.x > maxSpeed)//우측 속력 제한
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//좌측 속력 제한
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //레이캐스팅으로 플랫폼 감지해서 뛰는 소리 재생
        RaycastHit2D rayHitGr = Physics2D.Raycast(new Vector2(rigid.position.x, rigid.position.y - 0.1f), new Vector2(0, -3f), 1, LayerMask.GetMask("platform"));
        if (rayHitGr.collider != null && rayHitGr.distance < 1f && animator.GetBool("isWalking") && !audioSource.isPlaying)
        {
            PlayRandomWalkingSound();
        }


        //레이캐스팅으로 점프 후 착지 감지
        if (rigid.velocity.y <= 0.01f)
        {
            RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[3];
            for (int i = 0; i < 3; i++)
            {
                Debug.DrawRay(new Vector2(rigid.position.x + (i - 1) * 0.75f, rigid.position.y - 0.1f), new Vector2(0, -2f), new Color(0, 2f, 0));
                raycastHit2Ds[i] = Physics2D.Raycast(new Vector2(rigid.position.x + (i - 1) * 0.75f, rigid.position.y - 0.1f), new Vector2(0, -3f), 1, LayerMask.GetMask("platform"));
                if (raycastHit2Ds[i].collider != null && raycastHit2Ds[i].distance < 1f)
                {
                    animator.SetBool("isJumping", false);
                }
            }

        }

        //가만히 냅뒤도 움직이는 버그 수정

        if (rigid.velocity.x < 0.005f && rigid.velocity.x > -0.005f && horizontal_force == 0)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        // ghost vertics 현상 (타일 끝에 걸리는 현상) 수정
        rigid.AddForce(Vector2.up * 0.01f, ForceMode2D.Impulse);
    }
}
