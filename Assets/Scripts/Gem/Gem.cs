using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject explosionEffect;
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

    public void DestroyGem() //º¸¼® ÆÄ±«
    {
        explosionEffect.SetActive(true);
        explosionEffect.transform.position = transform.position;
        gameObject.SetActive(false);

    
    }

    void Start()
    {
        StartCoroutine("SummonSpark");
    }

}
