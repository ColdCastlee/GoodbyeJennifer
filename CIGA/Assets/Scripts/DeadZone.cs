using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeadZone : MonoBehaviour
    {
        public float height;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.name!="Player_1")
                return;
            
            var hitinfos = Physics2D.RaycastAll(new Vector2(other.transform.position.x, other.transform.position.y + 4),
                Vector2.up);


            if (hitinfos!=null && hitinfos.Length>=3)
            {
                var lastHit = hitinfos[hitinfos.Length-2];
                //Debug.Log("检测到："+ lastHit.transform.name+" at position: "+ lastHit.transform.position);
                other.transform.position = lastHit.transform.position + new Vector3(0, height, 0);
            }
            else
            {
                //print("没碰到啊");
                other.transform.position += new Vector3(0, 0, 100);
            }
        }

    }
}