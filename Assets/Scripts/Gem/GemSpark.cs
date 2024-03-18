using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpark : MonoBehaviour
{

    public float Duration;

    void DestroyThis() 
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Invoke("DestroyThis", Duration);
    }

}
