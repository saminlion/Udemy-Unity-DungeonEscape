using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    //handle to the spider 
    private Spider _spider;

    private void Start()
    {
        //assign handle to the spider    
        _spider = GetComponentInParent<Spider>();
    }

    public void Fire()
    {
        //Tell Spider to fire acid
        //use handle to call Attack method on spider
        _spider.Attack();
    }
}
