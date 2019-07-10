using System;
using UnityEngine;


public class BaseItem : MonoBehaviour
{
    [HideInInspector] public string leftClickItemLayerName = "heart";
    public event Action onTouchPlayer;
    public event Action onTouchLeftItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(("Player")))
        {
            OnTouchPlayer();
            print(999);
            onTouchPlayer?.Invoke();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(leftClickItemLayerName))
        {
            OnTouchLeft();
            onTouchLeftItem?.Invoke();
        }
    }

    protected virtual void OnTouchPlayer()
    {
        
    }

    protected virtual void OnTouchLeft()
    {
        
    }
}