using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    bool isPointerOn = false;
    private RectTransform rectTransform;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectTransform.position = Input.mousePosition;
    }

    private void Update()
    {
        if (isPointerOn) 
        {
            Cursor.visible = true;
        }
        else 
        {
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isPointerOn)
            {
                isPointerOn = false;
            }
            else 
            {
                isPointerOn = true;
            }
            
        }
    }
}
