using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    public float detectLength;
    public float enemyDetectionCorrection;
    public GameObject cursor;
    public int detectedEnemyId;
    int detectingRayNumber = 7;
    public bool isAttacking = false;
    public bool isAttackSound = false;
    RaycastHit2D[] enemyHits = new RaycastHit2D[7];
    List<int> detectedEnemiesIDs = new List<int>();
    List<Vector3> detectedEnemiesPoses = new List<Vector3>();

    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;
    public AudioClip attackSound4;
    public AudioClip attackSound5;

    Rigidbody2D rigid;
    LineRenderer lineRenderer;
    Animator animator;
    AudioSource audioSource;

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

    void LeaveOnlyOneDetectedObjext() //detectedEnemiesIDs 중 플레이어와의 거리가 가장 가까운 적의 ID 하나만 남기고 그걸 detectedEnemyId에 넣음
    
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

    void ChangeCursor() //커서 이미지 변경
    {
        if (detectedEnemiesIDs.Count == 0)
        {
            cursor.GetComponent<pointerImageChange>().CursorNumber = 1;
        }
        else
        {
            cursor.GetComponent<pointerImageChange>().CursorNumber = 2;
        }


    }

    void DrawLineBetweenPlayerAndEnemy() //플레이어와 적 사이를 라인렌더러로 이어줌
    {
        if (detectedEnemiesIDs.Count == 0)
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

    void StopAttackAnimation() 
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    void PlayRandomAttackSound() /*랜덤 공격 사운드 재생*/
    {
        audioSource.Stop();
        AudioClip[] attackSounds = new AudioClip[5] { attackSound1, attackSound2, attackSound3, attackSound4, attackSound5 };
        audioSource.clip = attackSounds[Random.Range(0, 5)];
        audioSource.Play();
    }

    void DetectEnemy(Vector2 cursorPosition) 
    {

        //적 감지용 raycastHit, enemyHits 배열에 저장
        for (int i = 0; i < detectingRayNumber; i++)
        {

            float relX = cursorPosition.x - rigid.position.x;
            float relY = cursorPosition.y - rigid.position.y;
            int RayHalf = ((detectingRayNumber - 1) / 2);
            float correctionAngle = (((float)i - RayHalf) / RayHalf) * enemyDetectionCorrection;

            //이 작업으로 커서 양옆으로 보정 각도만큼 선들이 퍼져나가게 된다
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

            //적이 감지되면 리스트에 넣는다
            if (enemyHits[i].collider != null)
            {
                AddIntToListIfNotExist(detectedEnemiesIDs, enemyHits[i].transform.gameObject.GetInstanceID());
                AddVector3ToListIfNotExist(detectedEnemiesPoses, enemyHits[i].transform.position);
            }

        }

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
        audioSource = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        detectedEnemiesIDs.Clear();
        detectedEnemiesPoses.Clear();
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(cursor.transform.position);


        DetectEnemy(cursorPos); //적을 감지하고

        LeaveOnlyOneDetectedObjext(); //감지된 적들 중 가장 가까운 하나만 선별한다

        ChangeCursor(); //적 감지시 커서를 바꾼다

        DrawLineBetweenPlayerAndEnemy(); // 적 감지시 플레이어와 적 사이에 라인을 그린다




        if (isAttacking) // isattacking이면 0.3초 뒤에 공격 에니메이션 꺼짐
        {   
            animator.SetBool("isAttacking", true);
            if (isAttackSound) 
            {
                PlayRandomAttackSound();
                isAttackSound = false;
            }

            Invoke("StopAttackAnimation",0.3f);
        }


        

    }
}

