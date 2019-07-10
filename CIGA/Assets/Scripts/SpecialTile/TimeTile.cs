using System;
using UnityEngine;
using UnityStandardAssets._2D;


public class TimeTile:MonoBehaviour
{
    public float maxTime;
    private float timer = 0;
    private bool start = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(2333);
        if(BaseTile.Enable&&other.transform.CompareTag("Player"))
            start = true;
    }
    

    private void Update()
    {
        if (start)
        {
            print(2);
            timer += Time.deltaTime;
        }
        if (timer > maxTime)
        {
            Destroy(this.gameObject);
        }
        
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (BaseTile.Enable && other.transform.CompareTag("Player"))
        {
            timer = 0;
            start = false;
        }
    }

}