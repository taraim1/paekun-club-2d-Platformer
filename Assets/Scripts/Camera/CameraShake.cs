using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator CamShake(float duration, float magnitude, float interval) 
    { 
        Vector3 originPos = transform.position;
        float elapsedTime = 0;
        

        while (elapsedTime < duration) 
        {

            float camShakeX = Random.Range(-1f, 1f) * magnitude;
            float camShakeY = Random.Range(-1f, 1f) * magnitude;
            transform.position = originPos + new Vector3(camShakeX, camShakeY, 0f);
            elapsedTime += interval;

            yield return new WaitForSeconds(interval);

        }

        transform.position = originPos;
        yield break;
    
    }

    public void BossCamShake() 
    {
        StartCoroutine(CamShake(0.3f, 0.15f, 0.02f));
    }
}
