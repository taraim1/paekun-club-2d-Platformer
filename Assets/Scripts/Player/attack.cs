using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    public float detectLength;
    public float enemyDetectionCorrection;
    public float AttackDuration;
    public float floatingTime;
    float currentAttackDuration = 0f;
    public GameObject cursor;
    public GameObject attackSoundPlayer;
    public GameObject player;
    public int detectedEnemyId;
    int detectingRayNumber = 7;
    public bool AttackAnimationPlay = false; // �̰� EnemyDie���� ������
    public bool AttackSoundPlay = false; // �̰� EnemyDie���� ������
    RaycastHit2D[] enemyHits = new RaycastHit2D[7];
    List<int> detectedEnemiesIDs = new List<int>();
    List<Vector3> detectedEnemiesPoses = new List<Vector3>();
    Rigidbody2D rigid;
    LineRenderer lineRenderer;
    Animator animator;


    void AddIntToListIfNotExist(List<int> list,int Int) 
    {
        if (!list.Contains(Int)) 
        {
            list.Add(Int);
        }
    }

    void AddVector3ToListIfNotExist(List<Vector3> list, Vector3 vec)
    {
        if (!list.Contains(vec))
        {
            list.Add(vec);
        }
    }

    void LeaveOnlyOneDetectedObjext() //detectedEnemiesIDs �� �÷��̾���� �Ÿ��� ���� ����� ���� ID �ϳ��� ����� �װ� detectedEnemyId�� ����
    
    {
        if (detectedEnemiesIDs.Count >= 2)
        {
            for (int i = 0; i < detectedEnemiesIDs.Count - 1; i++)
            {
                if (Mathf.Sqrt(Mathf.Pow(detectedEnemiesPoses[0].x - rigid.position.x, 2) + Mathf.Pow(detectedEnemiesPoses[0].y - rigid.position.y, 2)) <= Mathf.Sqrt(Mathf.Pow(detectedEnemiesPoses[1].x - rigid.position.x, 2) + Mathf.Pow(detectedEnemiesPoses[1].y - rigid.position.y, 2)))
                {
                    detectedEnemiesPoses.RemoveAt(1);
                    detectedEnemiesIDs.RemoveAt(1);
                }
                else
                {
                    detectedEnemiesPoses.RemoveAt(0);
                    detectedEnemiesIDs.RemoveAt(0);
                }
            }

            detectedEnemyId = detectedEnemiesIDs[0];
        }
        else if (detectedEnemiesIDs.Count == 1)
        {
            detectedEnemyId = detectedEnemiesIDs[0];
        }
        else 
        {
            detectedEnemyId = 0;
        }
    }

    void ChangeCursor() //Ŀ�� �̹��� ����
    {
        if (detectedEnemiesIDs.Count == 0 || player.GetComponent<PlayerDeath>().isDead)
        {
            cursor.GetComponent<pointerImageChange>().CursorNumber = 1;
        }
        else
        {
            cursor.GetComponent<pointerImageChange>().CursorNumber = 2;
        }


    }

    void DrawLineBetweenPlayerAndEnemy() //�÷��̾�� �� ���̸� ���η������� �̾���
    {
        if (detectedEnemiesIDs.Count == 0 || player.GetComponent<PlayerDeath>().isDead) // �װų� ���� Ÿ�����ϰ� ���� ������ ����
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.SetPosition(0, new Vector3(rigid.position.x, rigid.position.y, 2));
            lineRenderer.SetPosition(1, new Vector3(detectedEnemiesPoses[0].x, detectedEnemiesPoses[0].y, 2));
            lineRenderer.enabled = true;
        }

    }


    void MakeAttackAnimationNotLoop()
    {
        animator.SetBool("AttackAniPlay", false);
    }


    void DetectEnemy(Vector2 cursorPosition) 
    {

        //�� ������ raycastHit, enemyHits �迭�� ����
        for (int i = 0; i < detectingRayNumber; i++)
        {

            float relX = cursorPosition.x - rigid.position.x;
            float relY = cursorPosition.y - rigid.position.y;
            int RayHalf = ((detectingRayNumber - 1) / 2);
            float correctionAngle = (((float)i - RayHalf) / RayHalf) * enemyDetectionCorrection;

            //�� �۾����� Ŀ�� �翷���� ���� ������ŭ ������ ���������� �ȴ�
            if (i - RayHalf != 0)
            {
                float CorCos = Mathf.Cos(correctionAngle * Mathf.Deg2Rad);
                float CorSin = Mathf.Sin(correctionAngle * Mathf.Deg2Rad);
                enemyHits[i] = Physics2D.Raycast(new Vector2(rigid.position.x, rigid.position.y), new Vector2(relX * CorCos - relY * CorSin, relX * CorSin + relY * CorCos), detectLength, LayerMask.GetMask("enemy"));
            }
            else
            {
                enemyHits[i] = Physics2D.Raycast(new Vector2(rigid.position.x, rigid.position.y), new Vector2(relX, relY), detectLength, LayerMask.GetMask("enemy"));
            }

            //���� �����Ǹ� ����Ʈ�� �ִ´�
            if (enemyHits[i].collider != null)
            {
                AddIntToListIfNotExist(detectedEnemiesIDs, enemyHits[i].transform.gameObject.GetInstanceID());
                AddVector3ToListIfNotExist(detectedEnemiesPoses, enemyHits[i].transform.position);
            }

        }

    }
    void SetGravityScale(float scale) 
    {
        rigid.gravityScale = scale;
    
    }

    IEnumerator AttackFallingCorrection() // ���� ���� ��� ���ְ� �ϴ� �ڷ�ƾ
    {

        SetGravityScale(0.2f);
        yield return new WaitForSeconds(floatingTime);
        SetGravityScale(6);
        yield break;
    
    
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        animator = GetComponent<Animator>();
        

    }

    void FixedUpdate()
    {
        detectedEnemiesIDs.Clear();
        detectedEnemiesPoses.Clear();
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(cursor.transform.position);


        DetectEnemy(cursorPos); //���� �����ϰ�

        LeaveOnlyOneDetectedObjext(); //������ ���� �� ���� ����� �ϳ��� �����Ѵ�

        ChangeCursor(); //�� ������ Ŀ���� �ٲ۴�

        DrawLineBetweenPlayerAndEnemy(); // �� ������ �÷��̾�� �� ���̿� ������ �׸���


        if (currentAttackDuration > 0) // currentAttackDuration�� �ڿ� ���� ���ϸ��̼� ����
        {
            animator.SetBool("isAttacking", true);
            currentAttackDuration -= 0.02f;

        }
        else 
        {
            animator.SetBool("isAttacking", false);
        }


        if (AttackAnimationPlay) // AttackAnimationPlay�̸� currentAttackDuration ����, ���� �ִϸ��̼� ����
        {
            StartCoroutine("AttackFallingCorrection"); // ���ݽ� ��� õõ�� ������
            currentAttackDuration = AttackDuration;
            animator.SetBool("isAttacking", true);
            animator.SetBool("AttackAniPlay", true);
            Invoke("MakeAttackAnimationNotLoop", 0.02f);
            AttackAnimationPlay = false;
            if (AttackSoundPlay) 
            {
                attackSoundPlayer.GetComponent<AttackSoundPlayer>().PlayRandomAttackSound();
                AttackSoundPlay = false;
            }

        }


        

    }
}

