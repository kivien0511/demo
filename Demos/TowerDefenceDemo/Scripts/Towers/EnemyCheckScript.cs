using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckScript : MonoBehaviour {

	TowerTest tower ;
	// Use this for initialization
	void Start () {
		tower = transform.parent.GetComponent<TowerTest>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		tower.SetEnemy(other);
	}

	void OnTriggerExit(Collider other)
	{
		tower.RemoveEnemy(other);
	}
}
