using System;
using UnityEngine;


public class TouchToDispear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (MainCharacter.Instance.isDashing)
            {
                Destroy(this.gameObject);
            }
             
        }
    }
}