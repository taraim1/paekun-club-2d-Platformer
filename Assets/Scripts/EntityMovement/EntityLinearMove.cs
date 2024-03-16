using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EntityLinearMove : MonoBehaviour
{
    public bool isThisFallingPlatform = false;
    public bool DoThisObjectLinearMove = false;
    public bool startTrembleY = false;
    Rigidbody2D rigid;
    string trembleYPhase = "up"; // y축떨림에서 씀
    float theta = 0; // y축떨림에서 씀
    public float yTremblerate = 1.5f;
    int currentDestinationNum = 0; // 현재 목표 지점 번호
    public float speed;
    Vector3 currentDestination; // 현재 목표 지점
    public List<Vector3> destinations = new List<Vector3>();// 목표지점들 리스트




    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void LinearMove() //선형이동
    {
        currentDestination = destinations[currentDestinationNum];
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x - currentDestination.x) < 0.1f && Mathf.Abs(transform.position.y - currentDestination.y) < 0.1f) //목표지점 도착하면 목표지점 바꿈
        {
            if (currentDestinationNum < destinations.Count - 1)
            {
                 currentDestinationNum++;
            }
            else
            {
                currentDestinationNum = 0;
            }
        }

    }

    void Ytremble() // y축 떨림
    {
        if (trembleYPhase == "up")
        {
            if (theta < Mathf.PI * 2)
            {
                theta += Mathf.PI * 0.02f;
            }
            else
            {
                trembleYPhase = "down";
            }
        }
        else
        {
            if (theta > 0)
            {
                theta -= Mathf.PI * 0.02f;
            }
            else
            {

                trembleYPhase = "up";
            }

        }
        rigid.AddForce(transform.up * Mathf.Sin(theta) * yTremblerate, ForceMode2D.Force);
    }

    void FixedUpdate()
    {
        if (DoThisObjectLinearMove) //선형이동
        {
            LinearMove();

        }

        if (startTrembleY) // y축 떨림
        {
            Ytremble();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isThisFallingPlatform) //닿으면 떨어지는 플랫폼이 떨어질 때 좌우로 움직이지 않게 해줌
        {
            DoThisObjectLinearMove = false;
        
        }
    }
}
