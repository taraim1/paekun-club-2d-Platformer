using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EntityLinearMove : MonoBehaviour
{
    public bool isThisFallingPlatform = false;
    public bool DoThisObjectLinearMove = false;
    int currentDestinationNum = 0; // ���� ��ǥ ���� ��ȣ
    public float speed;
    Vector3 currentDestination; // ���� ��ǥ ����
    public List<Vector3> destinations = new List<Vector3>();// ��ǥ������ ����Ʈ





    void LinearMove() //�����̵�
    {
        currentDestination = destinations[currentDestinationNum];
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x - currentDestination.x) < 0.1f && Mathf.Abs(transform.position.y - currentDestination.y) < 0.1f) //��ǥ���� �����ϸ� ��ǥ���� �ٲ�
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

    void FixedUpdate()
    {
        if (DoThisObjectLinearMove) //�����̵�
        {
            LinearMove();

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isThisFallingPlatform) //������ �������� �÷����� ������ �� �¿�� �������� �ʰ� ����
        {
            DoThisObjectLinearMove = false;
        
        }
    }
}
