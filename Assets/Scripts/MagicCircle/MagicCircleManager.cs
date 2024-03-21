using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleManager : MonoBehaviour
{
    public GameObject magicCircle;
    public void SummonMagicCircle() 
    { 
        magicCircle.SetActive(true);
    }
}
