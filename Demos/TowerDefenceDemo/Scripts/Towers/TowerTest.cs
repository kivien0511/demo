using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTest : MonoBehaviour {

	// [HideInInspector]
	public bool isSelected = false;

	// [HideInInspector]
	public int level;

	// [HideInInspector]
	public int index;

	// [HideInInspector]
	public int TowerType;
	
	public enum TowerTypes
	{
		HighSpeed,
		HighAttack,
		MagicAttack,
		SpeedDown,
		AttackSpeedUp,
		CountAttack
	}

	public GameObject Bullet;

	public List<GameObject> enemyList = new List<GameObject>();
	
	public int attack;

	float attackSpeed = 1.0f;

	public float attackRange;

	float time = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log("敌人"+enemyList.Count);
		
		Attack();

		
		
	}

	void Attack()
	{
		
		if(enemyList.Count > 0)
		{
			if(time > attackSpeed)
			{
				Bullet.GetComponent<BulletScript>().attack = attack;

				if(enemyList.Count > 0)
				{
					if(enemyList[0] == null)
					{
						enemyList.Remove(enemyList[0]);
					}
					else
					{
						Bullet.GetComponent<BulletScript>().target = enemyList[0].transform;
						Instantiate(Bullet,this.transform.position,Quaternion.identity);
						time = 0;
					}
					
				}
					
			}
			else
			{
				time += Time.deltaTime;
			}
		
		}
	}

	public void SetEnemy(Collider other)
	{
		if(other.tag == "Enemy")
		{
			enemyList.Add(other.gameObject);
		}
	}

	public void RemoveEnemy(Collider other)
	{
		if(other.tag == "Enemy")
		{
			enemyList.Remove(other.gameObject);
		}
	}

}
