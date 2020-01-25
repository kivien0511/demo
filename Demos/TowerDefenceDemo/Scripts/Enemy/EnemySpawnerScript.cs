using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

	public List<string[]> road;

	public List<Vector3> wayPoints = new List<Vector3>();

	public GameObject EnemyTest;

	float time = 0;

	float spawnTime = 0;

	int count  = 0;

	// Use this for initialization
	void Start () {
		
	}

	public void Init()
	{
		CsvReader csv = CsvReader.GetInstance();
		csv.loadFile("road");
		road = csv.getArrayData();		
		SetWayPoints(road);
		Settings.Wave = 1;
		for(int i = 0; i< wayPoints.Count;i++)
		{
			Debug.Log("index:"+i+",Pos:"+ wayPoints[i]);
		}
		// int hp = (Settings.Wave + 100)*(Settings.Wave % 10);
		// SpawnEnemy(wayPoints,hp);
	}
	
	// Update is called once per frame
	void Update () {		
		if(Settings.Wave == 1)
		{
			SpawnEnemyWave(10,20);
		}
		else
		{
			SpawnEnemyWave(60,20);
		}		
	}

	public void SetWayPoints(List<string[]> roadData)
	{
		for(int i = 0;i<roadData.Count ;i++)
		{
			int x = int.Parse(roadData[i][0]);
			int z = int.Parse(roadData[i][1]);
			wayPoints.Add(new Vector3(x,0.7f,z));
		}
	}

	void SpawnEnemy(List<Vector3> wayPoints,int hp)
	{
		EnemyTest.GetComponent<EnemyScript>().wayPoints = wayPoints;
		EnemyTest.GetComponent<EnemyScript>().hp = hp;
		GameObject go = GameObject.Instantiate(EnemyTest,wayPoints[0],Quaternion.identity);
		go.transform.parent = this.transform.GetChild(2);
	}

	void SpawnEnemyWave(int waveTime,int waveCount)
	{
		if(time > waveTime)
		{
			spawnTime += Time.deltaTime;
			if(count < waveCount)
			{
				if(spawnTime > 0.5f)
				{
					int hp = (Settings.Wave + 100)*(Settings.Wave % 10);

					SpawnEnemy(wayPoints,hp);
					count ++;
					spawnTime = 0;
				}
			}
			else
			{
				count = 0;
				time = 0;
				Settings.Wave ++;
			}
		}
		else
		{
			time += Time.deltaTime;
		}
	}
}
