using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    public GameObject Bullet_Normal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    BulletEmitter()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
             CircleEmit(12, 0.05f, 1.0f);   
        }

    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    
    /// <summary>
    /// 放出圆圈状360°弹幕
    /// </summary>
    public void CircleEmit(int num, float velocity, float lifeTime)
    {
        //弹幕之间间隔的角度
        float angleEach = 360.0f / num;
        float angleCurrent = 0.0f;
        Vector2 velocityVec2 = new Vector2(0, 0);
        for (int i = 0; i < num; i++)
        {
            angleCurrent = angleEach * i;
            //这边使用了Deg2Rad，可能出现问题
            velocityVec2 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angleCurrent) * velocity,
                Mathf.Sin(Mathf.Deg2Rad * angleCurrent) * velocity);
            
            PixelBullet pixelBullet = Instantiate(Bullet_Normal.gameObject, this.transform.position,
                Quaternion.identity).GetComponent<PixelBullet>();
            Debug.Log("Angle:"+angleCurrent + "\n Velocity:" + velocityVec2
                      +"\nCos Value:" + Mathf.Cos(Mathf.Deg2Rad * angleCurrent)
                      +"\nSin Value:" + Mathf.Sin(Mathf.Deg2Rad * angleCurrent));
            
            pixelBullet.Veloctiy = velocityVec2;
            pixelBullet.LifeTime = lifeTime;
        }
        Invoke(nameof(DestroySelf),lifeTime + 1.0f);
    }
}