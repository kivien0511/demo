using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public KeyCode setKey = KeyCode.K;

	// Use this for initialization
	void Start () {
		InputManager.GetInstance().SetKeyCode(setKey);
		EventCenter.GetInstance().AddEventLinster<KeyCode>("KeyDown",CheckInsert);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(0) )
		{
			PoolManager.GetInstance().GetObj("Test/Cube",(o)=>{
				o.transform.localScale = Vector3.one * 2;
			});
		}

		if( Input.GetMouseButtonDown(1) )
		{
			PoolManager.GetInstance().GetObj("Test/Sphere",(o)=>{
				o.transform.localScale = Vector3.one * 2;
			});
		}
		
	}

	private void CheckInsert(KeyCode key)
	{
		if(key == setKey)
		{
			InsertGameObject();
		}

		// switch(key)
		// {
		// 	case KeyCode.K:
		// 		InsertGameObject();
		// 		break;
		// }
	}

	private void InsertGameObject()
	{
		Debug.Log("test");
		PoolManager.GetInstance().GetObj("Event/Monster",(o)=>{});
		PoolManager.GetInstance().GetObj("Event/Player",(o)=>{});
		PoolManager.GetInstance().GetObj("Event/Other",(o)=>{});
		PoolManager.GetInstance().GetObj("Event/Task",(o)=>{});

	}
}
