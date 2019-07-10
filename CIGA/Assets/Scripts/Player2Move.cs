using System;
using Game.Const;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;
using UnityEngine.UI;

    public class Player2Move : MonoBehaviour
    {

        
        public GameObject target;
        public float time;
        [Range(0, 1)] public float boundary = 0.5f;
        public float returnTime;

        public float rate;
        
        private float speed;
        private bool working = true;
        private Vector3 startPos;
        private void Start()
        {
            startPos = Camera.main.WorldToScreenPoint(transform.position);
            speed = (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*boundary ,Screen.height,0))-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0))).x/time;
//            print("speed   "+ speed);
        }

        private void Update()
        {
            //print(11);
            if (target)
            {
                transform.LookAt(target.transform.position);
            }
            if (working)
            {
                //print(222);
                   transform.Translate(Vector3.right * Time.deltaTime * speed,Space.World);
                   var pos = Camera.main.WorldToScreenPoint(transform.position);
                   if (pos.x / Screen.width < boundary)
                   {
                       working = false;
                       MainLoop.Instance.UpdateForSeconds(() =>
                       {
                           //print(33);
                           var target = Camera.main.ScreenToWorldPoint(startPos);
                           transform.position = Vector3.Lerp(transform.position, target, rate);

                       }, returnTime, () =>
                       {
                           transform.position = Camera.main.ScreenToWorldPoint(startPos);
                           working = true;
                       });
                   }         
            }

        }


    }