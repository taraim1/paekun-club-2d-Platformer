using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLinearMove : MonoBehaviour
{
    public List<Vector3> destinations = new List<Vector3>(); // 목표지점들 리스트
    int currentDestinationNum = 0; // 현재 목표 지점 번호
    public float speed;
    Vector3 currentDestination; // 현재 목표 지점

    void FixedUpdate()
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
}
