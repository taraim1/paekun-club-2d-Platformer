using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;

    //����
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




    //�Լ�

    void Jump() /*����*/
    {   
        if (!animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            PlayRandomJumpSound();
        }
    }

    void CorrectedJump() /*����Ű ���� ������ ���� �����°� ������ ����*/
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
    
    void PlayRandomJumpSound() /*���� ���� ���� ���*/
    {
        audioSource.Stop();
        AudioClip[] jumpingSounds = new AudioClip[4] { jumpingSound1, jumpingSound2, jumpingSound3, jumpingSound4 };
        audioSource.clip = jumpingSounds[Random.Range(0, 4)];
        audioSource.Play();
    }

    void PlayRandomWalkingSound() /*���� �ȱ� ���� ���*/
    {
        AudioClip[] walkingSounds = new AudioClip[7] { walkingSound1, walkingSound2, walkingSound3, walkingSound4, walkingSound5, walkingSound6, walkingSound7 };
        audioSource.clip = walkingSounds[Random.Range(0, 7)];
        audioSource.Play();
    }





    

    //����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) // ����
        {
            CorrectedJump();
           
        }

        if (Input.GetButtonUp("Horizontal"))//�¿� Ű ���� ����
        { 
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //AD ���ÿ� ������ �̲������°� ����
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") == 1 && !animator.GetBool("isAttacking"))//�¿� �̵��� ���� ��������Ʈ ���� ��ȯ
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && !animator.GetBool("isAttacking"))
        {
            spriteRenderer.flipX = true;
        }

        if (Mathf.Abs(rigid.velocity.x ) < 0.1f) //idle -> run �ִϸ��̼� ��ȯ
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
        //�¿� ������
        float horizontal_force = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * horizontal_force, ForceMode2D.Impulse);


        //�ִ� �¿� �ӵ� ����
        if (rigid.velocity.x > maxSpeed)//���� �ӷ� ����
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//���� �ӷ� ����
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //����ĳ�������� �÷��� �����ؼ� �ٴ� �Ҹ� ���
        RaycastHit2D rayHitGr = Physics2D.Raycast(new Vector2(rigid.position.x, rigid.position.y - 0.1f), new Vector2(0, -3f), 1, LayerMask.GetMask("platform"));
        if (rayHitGr.collider != null && rayHitGr.distance < 1f && animator.GetBool("isWalking") && !audioSource.isPlaying)
        {
            PlayRandomWalkingSound();
        }


        //����ĳ�������� ���� �� ���� ����
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

        //������ ���ڵ� �����̴� ���� ����

        if (rigid.velocity.x < 0.005f && rigid.velocity.x > -0.005f && horizontal_force == 0)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        // ghost vertics ���� (Ÿ�� ���� �ɸ��� ����) ����
        rigid.AddForce(Vector2.up * 0.01f, ForceMode2D.Impulse);
    }
}
