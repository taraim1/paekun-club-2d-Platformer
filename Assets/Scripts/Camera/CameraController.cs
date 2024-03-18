using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    /* 이건 나중에 쓸모있을듯해서 남겨놓음
    new public GameObject camera;

    public float smoothTime;
    int camNumber = 1;
    List<Vector2> camVec2 = new List<Vector2>() { //여기에 캠 이동 위치 저장
        new Vector2(0,0), 
        new Vector2(14,0),
        new Vector2(40,0),
        new Vector2(60,0)
    };
    float camZ = -10;

    void MoveCameraLerp(float x, float y, float z)
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(x, y, z), Time.deltaTime * smoothTime);
    }


    void FixedUpdate()
    {
        MoveCameraLerp(camVec2[camNumber-1].x, camVec2[camNumber - 1].y, camZ);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "camTrigger")
        {   
            //camTrigger들 이름에서 숫자 뺴내는 작업
            //camTrigger 태그 오브젝트 이름은 cam (숫자) 형태여야함
            string camName = other.gameObject.name;
            camName = camName.Substring(5, camName.Length - 6);
            camNumber = int.Parse(camName);

        }
        
    }
    
    */
    public GameObject cam;
    public float camSpeed;

    private void FixedUpdate()
    {

        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), Time.deltaTime * camSpeed);
     
    }
}
