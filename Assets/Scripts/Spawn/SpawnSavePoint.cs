using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavePoint : MonoBehaviour
{
    public GameObject SavePoint_prefab;
    float savePointZ = 2f;
    int savePointNum = 1;
    GameObject clone;
    void spawnSavePoint(Vector2 pos) //���̺�����Ʈ ���� �Լ�, savePointnum��ȣ�� ���̺� ����Ʈ�� pos ��ġ�� ��ȯ
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
