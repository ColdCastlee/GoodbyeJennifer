using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForGlassAnimat : BaseItem
{
    public Animator animator;
    public float delay;
    public RuntimeAnimatorController ac;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = ac;
        IdelAnimt((int) Random.Range(0f, 5f));
    }

    //调用方法
    public void IdelAnimt(int Number)
    {
        if (Number == 0)
        {
            animator.SetBool("0", true);
        }

        if (Number == 1)
        {
            animator.SetBool("1", true);
        }

        if (Number == 2)
        {
            animator.SetBool("2", true);
        }

        if (Number == 3)
        {
            animator.SetBool("3", true);
        }

        if (Number == 4)
        {
            animator.SetBool("4", true);
        }

        if (Number == 5)
        {
            animator.SetBool("5", true);
        }

        if (Number == 6)
        {
            animator.SetBool("IsBrocken", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Break();
        }
    }


    public void Break()
    {
        animator.Play(Animator.StringToHash("Brocken"));
        Destroy(this.gameObject, delay);
    }
}