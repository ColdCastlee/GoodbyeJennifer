using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIswitchToHuman : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            Debug.Log("UP");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 10), step);
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            Debug.Log("Down");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -10), step);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            Debug.Log("L");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10, 0, 0), step);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            Debug.Log("R");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(10, 0, 0), step);
        }
        
    }
}
