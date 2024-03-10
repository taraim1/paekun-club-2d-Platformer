using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavePoint : MonoBehaviour
{
    public GameObject SavePoint_prefab;
    GameObject clone;
    void spawnSavePoint(int num, Vector3 pos) //세이브포인트 스폰 함수, num번호의 세이브 포인트를 pos 위치에 소환
    {
        clone = Instantiate(SavePoint_prefab, pos, Quaternion.identity);
        clone.GetComponent<SavePoint>().SavePointNum = num;
    }


    void Start()
    {
        spawnSavePoint(1, new Vector3(72f, 3.7f, 1.5f));
        spawnSavePoint(2, new Vector3(188f, 3.7f, 1.5f));
    }
}
