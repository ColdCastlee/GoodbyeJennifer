using System;
using Game.Script;
using UnityEngine;

public class HeartCreater : MonoBehaviour
{
    public float speed;
    public float deltaTime;
    private float adder = 0;
    private void Update()
    {
        if (adder > deltaTime)
        {
            var heart = PoolManager.GetHeart(transform.position);
            MainLoop.Instance.UpdateForSeconds(
                () => { heart.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World); }, 5, () =>
                {
                    if (heart)
                        PoolManager.RecycleHeart(heart);
                });
            adder = 0;
        }

        adder += Time.deltaTime;
    }
}