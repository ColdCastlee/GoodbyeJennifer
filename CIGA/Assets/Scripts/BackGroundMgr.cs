using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackGroundMgr : MonoBehaviour
    {
        public GameObject first;
        public GameObject second;
        public float speed;

        private Queue<GameObject> backgrounds = new Queue<GameObject>();

        private Vector3 firstStart;
        private Vector3 secondStart;
        private Vector3 markPos;

        private void Start()
        {
            markPos = Camera.main.ScreenToWorldPoint(new Vector3(-0.5f * Screen.width, Screen.height, 0));
            firstStart = first.transform.position;
            secondStart = second.transform.position;
            backgrounds.Enqueue(first);
            backgrounds.Enqueue(second);
        }

        private void Update()
        {
            Move();
            foreach (var VARIABLE in backgrounds)
            {
                if (VARIABLE.transform.position.x <= markPos.x)
                {
                    VARIABLE.transform.position = secondStart;
                }
            }
        }

        void Move()
        {
            foreach (var VARIABLE in backgrounds)
            {
                VARIABLE.transform.Translate(Vector3.left*Time.deltaTime*speed,Space.World);
            }
        }
    }
}