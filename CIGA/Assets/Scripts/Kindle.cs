using System;
using Game.Global;
using Game.Script;
using UnityEngine;


    public class Kindle : MonoBehaviour
    {
        public enum WorkType
        {
            backToStart,
            pushRight,
            pushUp,
            pushLeft
        }
           
        
        public WorkType workType;
        public float horizontalPushSpeed;
        public float pushUpSpeed;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name=="Player_1")
            {
                switch (workType)
                {
                    case WorkType.backToStart:
                        other.transform.position =GlobalVar.startPos.transform.position;
                        break;
                    case WorkType.pushRight:
                        MainCharacter.Instance._playerVelocityX = horizontalPushSpeed;
                        break;
                    case WorkType.pushUp:
                        MainCharacter.Instance._playerVelocityY = pushUpSpeed;
                        break;
                    case WorkType.pushLeft:
                        MainCharacter.Instance._playerVelocityX = -horizontalPushSpeed;
                        break;
                }
                other.GetComponent<Player1Life>().TakeDamage();
            }
        }
    }