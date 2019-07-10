using System;
using UnityEngine;


public class HeartRecycler : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.StartsWith("Heart"))
        {
            PoolManager.RecycleHeart(other.gameObject);
        }
    }
}