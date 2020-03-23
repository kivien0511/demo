using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoUpdateTest
{
	public NoUpdateTest()
	{
		MonoManager.GetInstance().StartCoroutine(TestDely());
	}

	IEnumerator TestDely()
	{
		yield return new WaitForSeconds(3f);
		Debug.Log("TestDely");
	}

	public void Update()
	{
		Debug.Log("no update");
	}
}

public class MonoTest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		NoUpdateTest no = new NoUpdateTest();
		MonoManager.GetInstance().AddUpdateLinster(no.Update);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
