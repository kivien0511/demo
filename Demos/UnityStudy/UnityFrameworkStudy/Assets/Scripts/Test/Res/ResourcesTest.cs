using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			//ResourcesManager.GetInstance().LoadAsync<GameObject>("Test/Cube",()=>{});
			GameObject obj = ResourcesManager.GetInstance().Load<GameObject>("Test/Cube");
			obj.transform.localScale = Vector3.one * 2;
		}

		if(Input.GetMouseButtonDown(1))
		{
			ResourcesManager.GetInstance().LoadAsync<GameObject>("Test/Sphere",(obj)=>{
				obj.transform.localScale = Vector3.one * 2;
			});			
		}
	}

	// private void sth(GameObject obj)
	// {
	// 	obj.transform.localScale = Vector3.one * 2;
	// }
}
