using System;
using Game.Const;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class Heart : BaseItem
{
    public static int heartCount = 0;
    public float speedDecrease;
    public Slider bloodSlider;
    public float bigHeartSize = 4;
    public GameObject playerOneUI;

    private bool draw = false;

    public float hitBackSpeed;
    public float sizeToHitBackSpeedRate;
    public float maxYSpeed;
    public float sizeToJumpHeightRate;

    private void Start()
    {
        bloodSlider = GameObject.Find("Blood").GetComponent<Slider>();
        playerOneUI = GameObject.Find("PlayerOne");
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Glass"))
        {
            collision.GetComponent<ForGlassAnimat>().Break();

            if (bloodSlider.value > 0)
            {
                bloodSlider.value -= 10;
            }

            //          bloodSlider.value   
        }
        else if (collision.CompareTag("Player"))
        {
            if (collision.name == "Player_1")
            {
                var rig = GetComponent<Rigidbody2D>();
                //高速心扣血
                if (rig.velocity.magnitude >= 13)
                {
                    collision.GetComponent<Player1Life>().TakeDamage();
                }
                return;
            }

            heartCount++;
 
            //Debug.Log("Character Name: "+MainCharacter.Instance.name);

            if (MainCharacter.Instance.isDashing == false)
            {
                //击退和击飞
                switch (collision.name)
                {
                    case "Foot":
                        //print("jumpUp");
                        MainCharacter.Instance._playerVelocityY +=
                            collision.transform.localScale.x * sizeToJumpHeightRate;
                        //MainCharacter.Instance._playerVelocityY =
                        //    Mathf.Min(MainCharacter.Instance._playerVelocityY, maxYSpeed);
                        print("hit foot, final vertical speed: " + MainCharacter.Instance._playerVelocityY);
                        //MainCharacter.Instance._canJump = 2;
                        break;
                    case "Body":
                  //      print("hitBack");
                        MainCharacter.Instance._playerVelocityX -=
                            collision.transform.localScale.x * sizeToHitBackSpeedRate * hitBackSpeed;

                        print("hit body, final horizontal speed: " + MainCharacter.Instance._playerVelocityX);
                        
                        break;
                }
            }


            PoolManager.RecycleHeart(this.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        
    }
}