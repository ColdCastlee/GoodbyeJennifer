using System;
using System.Collections;
using System.Collections.Generic;
using Game.Common;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Player2Attack : MonoSingleton<Player2Attack>
{
   // public AbstractDragSkill currentDragSkill;
    public AbstractClickSkill currentClickSkill;

    [FormerlySerializedAs("PlayerTwoPosition")] public GameObject PlayerTwo;


    [Header("长按")]
    public GameObject heartPrefab;
    public float sizeRate;
    public float maxClickTime;
    public float heartGravity;
    public float shottingInterval;


    public GameObject slider;

    private bool pushDown = false;
    private bool canShot = true;

    private float realWaitTime = 0;
    private float lastTime = 0;
    private float numberOfShots = 0;
    private bool isSuperBullet = false;
    //private Vector3 screenStartPos;
    

    float time = 0;


    private void Start()
    {
        //screenStartPos = Camera.main.WorldToScreenPoint(PlayerTwo.transform.position);
        //screenStartPos.z = 0;
        //screenStartPos = Camera.main.ScreenToWorldPoint(screenStartPos);
        currentClickSkill = new PushHeart(heartPrefab, heartGravity , sizeRate , Vector3.zero, slider);
        time = Time.time;
    }


    private void Update()
    {
        ((PushHeart) currentClickSkill).startPos = PlayerTwo.transform.position;
        
        if (realWaitTime >= shottingInterval)
        {
            canShot = true;
            realWaitTime = 0;
        }
        realWaitTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
//            print("Input.GetMouseButtonDown(0)");
            pushDown = true;
        } 

        if (Input.GetMouseButtonUp(0))
        {
            if (canShot == true)
            {
                if (numberOfShots <= 5 || lastTime > 1.0f)
                {
                    AudioMgr.Instance.PlayAuEffect("Fire");
                    
                    var worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    
                    worldMousePos.z = PlayerTwo.transform.position.z;
                    //print("currentStartPos:"+PlayerTwo.transform.position+"   currentWorldMousePos:"+worldMousePos);
                    Vector3 direction =(worldMousePos-PlayerTwo.transform.position).normalized;
                    lastTime = lastTime < 0.3f ? 0.3f : lastTime;
                    currentClickSkill?.OnClick(lastTime > maxClickTime ? maxClickTime : lastTime, direction, isSuperBullet);
                    canShot = false;
                    numberOfShots++;
                }
                else
                {
                    AudioMgr.Instance.PlayAuEffect("Fire_1");
                    isSuperBullet = true;
                    var worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldMousePos.z = PlayerTwo.transform.position.z;
                    Vector3 direction =(worldMousePos-PlayerTwo.transform.position).normalized;
                    lastTime = lastTime < 0.3f ? 0.3f : lastTime;
                    numberOfShots = 0;
                    currentClickSkill?.OnClick(1.2f, direction, isSuperBullet);
                    canShot = false;
                    numberOfShots = 0;
                    isSuperBullet = false;
                }
            }         
            lastTime = 0;
            pushDown = false;
        }

        if (pushDown)
            lastTime += Time.deltaTime;
    }
    
}
