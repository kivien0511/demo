using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dely : MonoBehaviour {

	//当对象激活时 会进入的生命周期函数
	void OnEnable()
	{	
		Invoke("Push",1);
	}
	
	void Push()
	{
		PoolManager.GetInstance().PushObj(this.gameObject.name,this.gameObject);
	}
}
