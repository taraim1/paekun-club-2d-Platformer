using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{

    public GameObject cam;
    public GameObject SceneCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!CutSceneController.instance.isInCutScene)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(SceneCam.transform.position.x, SceneCam.transform.position.y, transform.position.z);
        }
    }
}
