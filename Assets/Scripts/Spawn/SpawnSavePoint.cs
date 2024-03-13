using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavePoint : MonoBehaviour
{
    public GameObject SavePoint_prefab;
    float savePointZ = 2f;
    GameObject clone;
    void spawnSavePoint(int num, Vector2 pos) //���̺�����Ʈ ���� �Լ�, num��ȣ�� ���̺� ����Ʈ�� pos ��ġ�� ��ȯ
    {
        clone = Instantiate(SavePoint_prefab, new Vector3(pos.x, pos.y, savePointZ), Quaternion.identity);
        clone.GetComponent<SavePoint>().SavePointNum = num;
    }


    void Start()
    {
        spawnSavePoint(1, new Vector2(72f, 3.7f));
        spawnSavePoint(2, new Vector2(188f, 3.7f));
        spawnSavePoint(3, new Vector2(243f, 2.1f));
    }
}
