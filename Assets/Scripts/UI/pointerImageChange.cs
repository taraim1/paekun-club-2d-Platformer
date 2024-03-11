using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointerImageChange : MonoBehaviour
{
    public Sprite Cursor1;
    public Sprite Cursor2;
    public Image currentImage;
    public int CursorNumber = 1;

    void Start()
    {
        currentImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void ChangeCursorImage(int number)
    {
        if (number == 1)
        {
            currentImage.sprite = Cursor1;
        }
        else if (number == 2)
        {
            currentImage.sprite = Cursor2;
        }
    }

    void Update()
    {
        ChangeCursorImage(CursorNumber);
    }
}
