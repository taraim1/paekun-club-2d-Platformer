using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityRotate : MonoBehaviour
{
    public float rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
    }
}
