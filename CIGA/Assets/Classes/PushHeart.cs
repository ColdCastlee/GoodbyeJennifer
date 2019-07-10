using UnityEngine;
using UnityEngine.UI;

public class PushHeart:AbstractClickSkill
{
    public PushHeart(GameObject heartPrefab,float gravityScale, float rate, Vector3 startPos,GameObject slider)
    {
        this.gravityScale = gravityScale;
        this.heartPrefab = heartPrefab;
        this.rate = rate;
        this.startPos = startPos;
        this.slider = slider;
    }


    
    GameObject slider;

    private GameObject heartPrefab;
    private float rate;
    private float gravityScale = 1.0f;
    public Vector3 startPos;


    public override void OnClick(float time,Vector3 direction,bool isSuper)
    {
        float forceMagnitude;
        var heart = PoolManager.GetHeart(this.startPos);
        heart.GetComponent<Heart>().bloodSlider = slider.GetComponent<Slider>();
        if (isSuper == true)
        {
            forceMagnitude = 15.0f;
            heart.transform.localScale *= 2.0f;
            heart.GetComponent<ParticalForHeart>().Partical2();
        }
        else
        {
            heart.transform.localScale *= rate * time;
            if (time > 1.0f)
            {
                heart.GetComponent<ParticalForHeart>().Partical1();
            }
            else
                heart.GetComponent<ParticalForHeart>().Partical0();
            forceMagnitude = 9.0f * (1- (1.0f/30.0f)* rate * time);
        }
       
        Rigidbody2D rigidbody2D = heart.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForceAtPosition(new Vector2(direction.x,direction.y) * forceMagnitude, direction, ForceMode2D.Impulse);
    }
}