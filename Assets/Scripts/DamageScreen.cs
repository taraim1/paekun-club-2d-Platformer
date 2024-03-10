using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{

    /*
     
     이건 안 쓰는 스크립트인데 나중에 쓸 데 있을 것 같아서 남겨놓음
     
     */
    Image image;
    bool isFadeIn = false;

    public void DamageScreenOff() 
    {
        StartCoroutine("DamageScreenFadeOut");
    }

    public void DamageScreenOn()
    {
        StartCoroutine("DamageScreenFadeIn");
    }

    IEnumerator DamageScreenFadeIn() 
    {
        image.color = new Color(1, 1, 1, 0f);
        float alpha = 0;
        isFadeIn = true;

        while (true)
        {
            if (alpha >= 1)
            {
                isFadeIn = false;
                yield break;
            }
            else 
            {
                alpha += 0.08f;
                image.color = new Color(1, 1, 1, alpha);
                yield return new WaitForSeconds(0.02f);
            }
            
        }

    }

    IEnumerator DamageScreenFadeOut()
    {
        float alpha = image.color.a;

        while (true)
        {
            if (alpha < 0)
            {
                image.color = new Color(1, 1, 1, 0);
                yield break;
            }
            else if (!isFadeIn)
            {
                alpha -= 0.09f;
                image.color = new Color(1, 1, 1, alpha);
                
            }
            yield return new WaitForSeconds(0.02f);
        }

    }

    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);
    }

}
