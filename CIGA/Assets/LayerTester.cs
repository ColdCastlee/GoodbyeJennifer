using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTester : MonoBehaviour
{
    public float speed;
    private void OnTriggerEnter2D(Collider2D other)
    {
//        if(MainCharacter.Instance._playerVelocityX<=speed)
//            return;
//        print("IsDashing: "+ MainCharacter.Instance.isDashing);
        if (MainCharacter.Instance.isDashing)
        {
  //          print("Ojbk");
            if (other.gameObject.CompareTag("Glass"))
            {
            //    print("LayerTeser工作");
                other.isTrigger = true;
                other.gameObject.layer = LayerMask.NameToLayer("Trigger");
            }            
        }

    }
}
