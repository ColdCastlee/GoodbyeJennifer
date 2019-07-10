using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.UIElements;


//public class PlayerMove : MonoBehaviour
//{
//    public float commonSpeed;
//    public float quickSpeed;
//
//    private bool isJumpping = false;
//
//
//    public float doubleClickTime = 0.3f;
//    private bool doubleClick = false;
//
//    public float firstDelta = 0.2f;
//    public float secondDelta = 0.2f;
//
//    private bool firstA;
//    private bool secondA;
//    
//    private bool canWalk = false;
//
//    private float aAdder = 0;
//
//    private bool firstD;
//    private bool secondD;
//    private float dAdder = 0;
//
//    public int jumpCount = 2;
//    private int curJumpCount = 2;
//    public float jumpForce;
//
//    private Vector2 position2D;
//
//
//    private Rigidbody2D rig;
//
//    private void Start()
//    {
//        rig = GetComponent<Rigidbody2D>();
//        Assert.IsTrue(rig);
//    }
//
//    private void FixedUpdate()
//    {
//        position2D = new Vector2(transform.position.x, transform.position.y);
//        //print("A:" + aAdder);
//        //print("D:" + dAdder);
//        if (firstA || secondA)
//            aAdder += Time.fixedDeltaTime;
//
//        if (firstD || secondD)
//            dAdder += Time.fixedDeltaTime;
//
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            //判断第二次按下
//            if (firstA && aAdder <= firstDelta)
//            {
//                print("双击 A");
//                firstA = false;
//                secondA = true;
//                aAdder = 0;
//            }
//            else
//            {
//                print("单机 A");
//                canWalk = true;
//                firstA = true;
//                aAdder = 0;
//            }
//        }
//        else if (Input.GetKeyDown(KeyCode.D))
//        {
//            //判断第二次按下
//            if (firstD && dAdder <= secondDelta)
//            {
//                print("双击 D");
//                firstD = false;
//                secondD = true;
//                dAdder = 0;
//            }
//            else
//            {
//                print("dadder: " + dAdder + "  " + "secondDelta:" + secondDelta);
//                print("单机 D");
//                canWalk = true;
//                firstD = true;
//                dAdder = 0;
//            }
//        }
//
//        if (Input.GetKeyUp(KeyCode.A))
//        {
//            canWalk = false;
//            if (firstA && aAdder >= firstDelta)
//            {
//                firstA = false;
//                secondA = false;
//                aAdder = 0;
//            }
//            else if (secondA)
//            {
//                if (aAdder < secondDelta)
//                {     
//                    print("左冲刺");
//                }
//
//                secondA = false;
//                firstA = false;
//                secondA = false;
//                aAdder = 0;
//            }
//        }
//        else if (Input.GetKeyUp(KeyCode.D))
//        {
//            canWalk = false;
//            if (firstD && dAdder >= firstDelta)
//            {
//                firstD = false;
//                secondD = false;
//                dAdder = 0;
//            }
//            else if (secondD)
//            {
//                if (dAdder < secondDelta)
//                {
//                    print("右冲刺");
//                }
//                secondD = false;
//                firstD = false;
//                secondD = false;
//                dAdder = 0;
//            }
//        }
//
//
//        if (firstA&&canWalk)
//        {
//            //正常走
//            print("正常走");
//            rig.MovePosition(position2D + Time.fixedDeltaTime * commonSpeed * Vector2.left);
//        }
//        else if (secondA && aAdder >= secondDelta)
//        {
//            //快跑
//            rig.MovePosition(position2D + Time.fixedDeltaTime * quickSpeed * Vector2.left);
//        }
//
//        if (firstD&&canWalk)
//        {
//            print("正常走");
//            rig.MovePosition(position2D + Time.fixedDeltaTime * commonSpeed * Vector2.right);
//        }
//        else if (secondD && dAdder >= secondDelta)
//        {
//            rig.MovePosition(position2D + Time.fixedDeltaTime * quickSpeed * Vector2.right);
//        }
//
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            print("jump");
//            rig.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
//            curJumpCount--;
//            print(curJumpCount);
//        }
//    }
//
//
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.layer == LayerMask.NameToLayer("floor"))
//        {
//            curJumpCount = jumpCount;
//        }
//    }
//}