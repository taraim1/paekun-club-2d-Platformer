using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool FallTrigger = false;
    public float FallSpeed;
    public float FallDelay;
    Rigidbody2D rb2D;


    void StartFalling() 
    {
        rb2D.gravityScale = FallSpeed;
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !FallTrigger) //ÇÃ·¹ÀÌ¾î¿Í ºÎµúÈ÷¸é ¶³¾îÁü
        {
            Invoke("StartFalling", FallDelay);
            FallTrigger = true;

        }
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


}


