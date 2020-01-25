using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour {

	public Transform target;
	Transform trans;

	// Use this for initialization
	void Start () {
		trans = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 followPosition = new Vector3(trans.position.x,target.position.y,trans.position.z);
		trans.position = Vector3.Lerp(trans.position,followPosition,1);
	}
}
