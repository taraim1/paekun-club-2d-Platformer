using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YUpAndDownSin : MonoBehaviour
{
    string trembleYPhase = "up";
    float theta = 0;
    public bool startYUpAndDown = true;
    public float Range = 1.5f;
    void YUpAndDown() // y축 오르내림
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
        transform.position = new Vector3(transform.position.x, transform.position.y + (theta-Mathf.PI)*Range, transform.position.z);
    }


    void FixedUpdate()
    {
        if (startYUpAndDown) 
        {
           YUpAndDown();
        }

    }
}
