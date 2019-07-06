using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAiTest : MonoBehaviour
{
    //----main:mission:在指定区域内随机生成点，并且让物体缓慢移动像随机生成的点
    //public GameObject Hand;
    public GameObject TargetPosition;
    public int WaitTime;
    public float timer;//生成点的时机 
    public float speed = 1.0f;
    public float moveRangezMax, moveRangezMin, moveRangexMax, moveRangexMin;
    public Vector3 centerPoint;
    //随机生成点
    //public void CreatHandMoveTarget() {

    //}
    public void Update()
    {
        timer += Time.deltaTime;//计时器
        if (timer > WaitTime)
        {

            var NewTG = Instantiate(TargetPosition, transform.position, Quaternion.identity);//克隆
            NewTG.transform.parent =transform;
            NewTG.transform.localPosition = new Vector3(TargetPosition.transform.localPosition.x + (Random.Range(-10, 10)), 0, TargetPosition.transform.localPosition.y + (Random.Range(-10, 10)));
            timer = 0;
        }
        //传入点的坐标，游戏物体移动向点
        if (transform.childCount > 0)
        {
            var ChildTarget = transform.GetChild(0).gameObject.transform.position;
            //transform.position = ChildTarget;
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, ChildTarget, step);
           // Debug.Log(ChildTarget);
        }
        //限定范围
        if (transform.position.z> moveRangezMax)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position,centerPoint,step);
        }
        if (transform.position.z < moveRangezMin)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, centerPoint, step);
        }
        if (transform.position.x > moveRangexMax)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, centerPoint, step);
        }
        if (transform.position.x < moveRangexMin)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, centerPoint, step);
        }
    }
   

}
