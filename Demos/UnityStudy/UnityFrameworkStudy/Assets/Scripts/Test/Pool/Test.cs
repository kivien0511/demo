﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(0) )
		{
			PoolManager.GetInstance().GetObj("Test/Cube");
		}

		if( Input.GetMouseButtonDown(1) )
		{
			PoolManager.GetInstance().GetObj("Test/Sphere");
		}
	}
}
