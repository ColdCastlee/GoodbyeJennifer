using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePusher : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(MoveSpeed,0.0f,0.0f);
    }
}
