using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBulider : MonoBehaviour {

	public GameObject[] enemys;
	public GameObject[] items;

	public GameObject[] blocks;

	public GameObject wall;

	GameObject player;

	float t;

	enum phase
	{
		phase1,
		phase2,
		phase3,
		phase4,
		phase5,
		phase6,
		phase7
	}

	phase p = phase.phase1;

	bool isBlockBulid = false;
	bool isWallBuild = false;

	int count = 0;

	bool isWallBuildOver = false;

	int enemyNum = 0;

	int itemNum = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		BlockBuild(p);


	}
	
	// Update is called once per frame
	void Update () {
		EnemyBulid();
		BlockBuild();
		WallBuild();
		ItemBulid();
	}

	void WallBuild()
	{
		if(player.transform.position.y % 100 > 50 && !isWallBuild)
		{
			count += 1;
			isWallBuild = true;
		}

		if(isWallBuild && !isWallBuildOver)
		{
			Instantiate(wall,new Vector2(0,99*count),Quaternion.identity);
			isWallBuild = false;
			isWallBuildOver = true;
		}

		if(player.transform.position.y % 100 > 90)
		{
			isWallBuildOver = false;			
		}
	}

	void BlockBuild()
	{
		if(player.transform.position.y > 5 && p == phase.phase1)
		{
			isBlockBulid = true;
			p = phase.phase2;
		}
		else if(player.transform.position.y > 140 && p == phase.phase2)
		{
			isBlockBulid = true;
			p = phase.phase3;
		}
		else if(player.transform.position.y > 240 && p == phase.phase3)
		{
			isBlockBulid = true;
			p = phase.phase4;
		}
		else if(player.transform.position.y > 440 && p == phase.phase4)
		{
			isBlockBulid = true;
			p = phase.phase5;
		}
		else if(player.transform.position.y > 740 && p == phase.phase5)
		{
			isBlockBulid = true;
			p = phase.phase6;
		}
		else if(player.transform.position.y > 940 && p == phase.phase6)
		{
			isBlockBulid = true;
			p = phase.phase7;
		}

		if(isBlockBulid)
		{
			BlockBuild(p);
			isBlockBulid = false;
		}


	}

	void BlockBuild(phase p)
	{
		switch(p)
		{
			case phase.phase1 :
				StartCoroutine(blockbulider(0,10,2));
				break;
			case phase.phase2 :
				StartCoroutine(blockbulider(10,150,3));
				break;
			case phase.phase3 :
				StartCoroutine(blockbulider(150,300,3.5f));
				break;
			case phase.phase4 :
				StartCoroutine(blockbulider(300,500,3.5f));
				break;
			case phase.phase5 :
				StartCoroutine(blockbulider(500,800,4));
				break;
			case phase.phase6 :
				StartCoroutine(blockbulider(800,1000,4));
				break;
			case phase.phase7 :
				StartCoroutine(blockbulider(1000,2000,4));
				break;
		}
	}

	IEnumerator blockbulider(float y,float max,float count)
	{
		int x = Random.Range(-3,3);

		while(y < max)
		{
			Instantiate(blocks[0],new Vector2(x,y),Quaternion.identity);
			y += count;

			x = Random.Range(-3,3);

			if(x > 0)
			{
				x += Random.Range(-2,0);
			}
			else if(x < 0)
			{
				x += Random.Range(0,2);
			}
			
		}

		yield return 0;
	}

	void ItemBulid()
	{
		switch(p)
			{
				case phase.phase1 :
					itemNum = 1;
					break;
				case phase.phase2 :			
				case phase.phase3 :
					itemNum = 2;
					break;
				case phase.phase4 :
				case phase.phase5 :
					itemNum = 3;
					break;
				case phase.phase6 :
					itemNum = 5;
					break;
				case phase.phase7 :
					itemNum = 6;
					break;
				}

		t += Time.deltaTime;
		if(t > 3)
		{
			t = 0;
			int a = Random.Range(0,10);
			if(a > 7)
			{
				
				
				float x = Random.Range(-2,2);
				float y = player.transform.position.y + Random.Range(5,10);
				if(itemNum != 0)
				{
					Debug.Log("道具生成");//ItemCreated
					int e = Random.Range(0,itemNum);
					Instantiate(items[e],new Vector2(x,y),Quaternion.identity);
				}
				
			}
		}
	}

	void EnemyBulid()
	{
		switch(p)
			{
				case phase.phase1 :
					enemyNum = 1;
					break;
				case phase.phase2 :			
				case phase.phase3 :
					enemyNum = 3;
					break;
				case phase.phase4 :
				case phase.phase5 :
					enemyNum = 5;
					break;
				case phase.phase6 :
					enemyNum = 7;
					break;
				case phase.phase7 :
					enemyNum = 8;
					break;
				}

		t += Time.deltaTime;
		if(t > 3)
		{
			t = 0;
			int a = Random.Range(0,10);
			if(a > 7)
			{
				
				
				float x = Random.Range(-3,3);
				float y = player.transform.position.y + Random.Range(10,15);
				if(enemyNum != 0)
				{
					Debug.Log("敌人生成");//EnemyCreated
					int e = Random.Range(0,enemyNum);
					Instantiate(enemys[e],new Vector2(x,y),Quaternion.identity);
				}
				
			}
		}
	}
}
