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
    public float speed;
    public float maxSpeed;
    public float minSpeed;
    public float jumpPower;
    public float jumpTimingCorrection;
    float horizontal_force = 0;
    bool isOnPlatform = false;
    bool canCorrectedJump = false;
    bool isCorrectedJumpReserved = false; // ���� ������ ���� �� �����Ǵ°� ����

    public AudioClip jumpingSound1;
    public AudioClip jumpingSound2;
    public AudioClip jumpingSound3;
    public AudioClip jumpingSound4;

    public GameObject player;
    public GameObject walkingSoundPlayer;

    //�Լ�

    IEnumerator Jump() //����, ��������
    {
        while (true) 
        {
            if (player.GetComponent<PlayerDeath>().isDead) //������ ���� �� ��
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
    
    void PlayRandomJumpSound() /*���� ���� ���� ���*/
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

    void ChangeSpriteHorizontalDirection() //�¿� �̵��� ���� ��������Ʈ ���� ��ȯ
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

    void CheckIfConductCorrectedJump() // ������ ����
    {
        if (Input.GetButtonDown("Jump") && canCorrectedJump && !isCorrectedJumpReserved)
        {
            isCorrectedJumpReserved = true;
            StartCoroutine(Jump());

        }

    }


    //================����===================================================================================

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckIfConductCorrectedJump(); // ������ ����

        if (Input.GetButtonUp("Horizontal"))//�¿� Ű ���� ����
        { 
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //AD ���ÿ� ������ �̲������°� ����
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        ChangeSpriteHorizontalDirection(); //�¿� �̵��� ���� ��������Ʈ ���� ��ȯ

        if (Mathf.Abs(rigid.velocity.x ) < 0.1f && horizontal_force == 0) //idle -> run �ִϸ��̼� ��ȯ
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
        horizontal_force = Input.GetAxisRaw("Horizontal");
        if (!player.GetComponent<PlayerDeath>().isDead) 
        {
            rigid.AddForce(Vector2.right * horizontal_force * speed, ForceMode2D.Impulse);
        }
        


        //�ִ� �¿� �ӵ� ����
        if (rigid.velocity.x > maxSpeed)//���� �ӷ� ����
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//���� �ӷ� ����
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }


        //����ĳ�������� �÷��� �����ؼ� isOnPlatform, canCorrectedJump ����
        RaycastHit2D[] rayHitGrs = new RaycastHit2D[3];
        int canCorrectedJumpCount = 0;
        int isOnPlatformCount = 0;
        for (int i = -1; i < 2; i++) 
        {
            Debug.DrawRay(new Vector2(rigid.position.x +0.55f*i, rigid.position.y - 0.1f), new Vector2(0, -4f), Color.green); // scene���� ray�� �� �� �ְ� ����, ��� �Ǳ� ��
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


        //�ٴ� �Ҹ� ���
        if (horizontal_force != 0 && !walkingSoundPlayer.GetComponent<WalkingSoundPlayer>().audioSource.isPlaying && isOnPlatform && !player.GetComponent<PlayerDeath>().isDead)
        {
            walkingSoundPlayer.GetComponent<WalkingSoundPlayer>().PlayRandomWalkingSound();
        }


        //���� �� ���� �����ؼ� ���� ��� ����
        if (isOnPlatform && rigid.velocity.y <= 0.01f) 
        {
            animator.SetBool("isJumping", false);

        }


        if (isOnPlatform && horizontal_force == 0) // �̲������°� ���ֱ�
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        else if (player.GetComponent<PlayerDeath>().isDead)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y); // �׾��� �� �̲������°� ���ֱ�
        }
    }
}
