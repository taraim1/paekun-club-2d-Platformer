using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavePoint : MonoBehaviour
{
    public GameObject SavePoint_prefab;
    float savePointZ = 2f;
    int savePointNum = 1;
    GameObject clone;
    void spawnSavePoint(Vector2 pos) //세이브포인트 스폰 함수, savePointnum번호의 세이브 포인트를 pos 위치에 소환
    {
        clone = Instantiate(SavePoint_prefab, new Vector3(pos.x, pos.y, savePointZ), Quaternion.identity);
        clone.GetComponent<SavePoint>().SavePointNum = savePointNum;
        savePointNum++;
    }


    void Start()
    {
        spawnSavePoint(new Vector2(72f, 3.7f));
        spawnSavePoint(new Vector2(188f, 3.7f));
        spawnSavePoint(new Vector2(243f, 2.1f));
        spawnSavePoint(new Vector2(257f, -10.8f));
        spawnSavePoint(new Vector2(238f, -27.3f));
    }
}
