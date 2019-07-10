using System;
using UnityEngine;

    public class BaseTile : MonoBehaviour
    {
        public static bool Enable = true;
        
        public event Action<GameObject> onTouchPlayer;
        public event Action<GameObject> onLeavePlayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Enable && other.CompareTag("Player"))
            {
                print("enter");
                OnTouchPlayer(other.gameObject);
                onTouchPlayer?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Enable && other.CompareTag("Player"))
            {
                OnLeavePlayer(other.gameObject);
                onLeavePlayer?.Invoke(other.gameObject);
            }
        }

        protected virtual void OnLeavePlayer(GameObject other)
        {
            
        }

        protected virtual void OnTouchPlayer(GameObject other)
        {
            
        }
    }