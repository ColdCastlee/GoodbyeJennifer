using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;

public class BackGroundController : MonoBehaviour
{
	public GameObject target;
	public float X_Speed;
	public float Y_Speed;
	private Vector3 beforePos;

	void Start()
	{
		this.beforePos = this.target.transform.position;
	}
	// Update is called once per frame
	void Update ()
	{
		var nowPos =target.transform.position;
		var temp = (nowPos - this.beforePos);
		temp = new Vector3(temp.x * this.X_Speed, temp.y * this.Y_Speed, 0);
		this.transform.Translate(temp*0.01f,Space.World);
		this.beforePos = nowPos;
	}
}
