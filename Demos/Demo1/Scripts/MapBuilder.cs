using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	public GameObject floor;

	//间隔
	public float sprite;

	//横向方块个数,竖向方块个数
	public float x , y;

	[HideInInspector]
	public List<GameObject> Map = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for(float i = 0;i<x;i+=sprite)
		{
			for(float j = 0;j<y;j+=sprite)
			{		
				GameObject prefabInstance = Instantiate(floor,new Vector3(i+sprite/2,j+sprite/2,0),Quaternion.identity);
				prefabInstance.transform.parent = this.transform;
			}
		}

		this.transform.position -= new Vector3(x/2,y/2,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
