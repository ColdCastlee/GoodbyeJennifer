using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBullet : MonoBehaviour
{
    private Vector2 _veloctiy;
    private float _lifeTime;
    private Vector3 _position;

    PixelBullet(Vector2 velocity, float lifeTime)
    {
        this._veloctiy = velocity;
        this._lifeTime = lifeTime;
    }

    private void Start()
    {
        //test
//        this._veloctiy = new Vector2(0.02f,0.02f);
//        this._lifeTime = 2.0f;
    }

    public Vector2 Veloctiy
    {
        get { return _veloctiy; }
        set { _veloctiy = value; }
    }

    public float LifeTime
    {
        get { return _lifeTime; }
        set { _lifeTime = value; }
    }

    public void Die()
    {
        Invoke(nameof(DestroySelf),0.13f);
        _veloctiy = new Vector2(0,0);
        //播放动画(0.5s)
        //多少秒后销毁
    }

    public void DieImmediately()
    {
        DestroySelf();
    }
    
    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        _position = this.transform.position;
        
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            this.Die();
        }   
        
        //移动子弹
        this.transform.position = new Vector3(this._position.x + _veloctiy.x,
            this._position.y + _veloctiy.y, this._position.z);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("TOUCHED！");         
            Die();
        }

    }
}
