using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGem : MonoBehaviour
{

    IEnumerator SummonSpark() 
    {
        Vector3 sparkPos;

        while (true)
        {
            sparkPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
            EntitySpawn.instance.spawnEntity("gemSpark", sparkPos);
            yield return new WaitForSeconds(1f);
        }
    
    }
    void Start()
    {
        StartCoroutine("SummonSpark");
    }

}
