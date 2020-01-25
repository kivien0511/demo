using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChipScript : MonoBehaviour {

	public static MapChipScript mapChipScript;

	public GameObject TowerTest;

	public Material[] towerTypes;

	public GameObject[] mapChips;

	public List<string[]> map1;

	[HideInInspector]
	public List<Vector3> buildList = new List<Vector3>();

	// [HideInInspector]
	public List<TowerData> towerList = new List<TowerData>();

	public GameObject[] mapList = new GameObject[64];

	MessageScript message;

	public MapChipScript GetMapChipScript()
	{
		return mapChipScript;
	}

	void Awake()
	{
		mapChipScript = this;
	}

	// Use this for initialization
	void Start () {
		message = MessageScript.message.getMessageInstance();
		CsvReader csv = CsvReader.GetInstance();
		csv.loadFile("map1");
		map1 = csv.getArrayData();

		BuildMap(map1);
		StartCoroutine(getBuildList(map1));
		
		GetComponent<EnemySpawnerScript>().Init();
	}

	public void BuildMap(List<string[]> mapData)
	{
		for(int i = 0; i < mapData.Count; i++)
		{
			for(int j =0; j < mapData[i].Length; j++)
			{
				GameObject go = GameObject.Instantiate(mapChips[int.Parse(mapData[i][j])],new Vector3(i,0f,j),Quaternion.identity);
				go.transform.parent = this.transform.GetChild(0).transform;
			}
		}
	}

	IEnumerator getBuildList(List<string[]> mapData)
	{
		for(int i = 0;i<mapData.Count ;i++)
		{
			for(int j =0;j<mapData[0].Length;j++)
			{
				if(int.Parse(mapData[i][j]) == 0)
				{
					buildList.Add(new Vector3(i,0,j));
				}
			}
		}
		mapList = GameObject.FindGameObjectsWithTag("TowerBase");

		Debug.Log("地板:"+mapList.Length);
		yield return 0;
	}

	public void BuyTower(int level)
	{
		int money;
		if(level == 1)
		{
			money = 100;
		}
		else
		{
			money = 3*(level-1)*100;
		}

		if(buildList.Count <= 0)
		{
			if(Settings.Money - money >= 0)
			{
				Settings.Money -= money;
				buildList.Sort();
				int index = Random.Range(0,buildList.Count);
				int towerType = Random.Range(0,6);
				
				// Debug.Log(buildList[index]+","+towerType);

				Settings.Money += (int)(money*0.9f);

				message.AddMessage("购买失败"+level+"级");
				return;
			}
		}
		else
		{
			if(Settings.Money - money >= 0)
			{
				Settings.Money -= money;
				int index = Random.Range(0,buildList.Count);
				int towerType = Random.Range(0,6);
				// int towerType = 1;
				
				// Debug.Log(buildList[index]+","+towerType);

				int towerNum = towerList.Count;

				BuildTower(new Vector3(buildList[index].x,0.7f,buildList[index].z),towerType,level,towerNum);
				// towerList.Add(buildList[index]);
				
				towerList.Add(new TowerData(towerNum,towerType,level,new Vector3(buildList[index].x,0.7f,buildList[index].z),money,false,false));
				buildList.Remove(buildList[index]);
				message.AddMessage("购买塔"+level+"级");
			}
			else
			{
				//显示信息 金钱不足
				// Debug.Log("金钱不足:"+money);
				message.AddMessage("金钱不足"+(Settings.Money - money));
			}
		}
		
	}

	public void BuildTower(Vector3 position,int towerType,int level,int index)
	{
		TowerTest.GetComponent<MeshRenderer>().material = towerTypes[towerType];
		TowerTest.GetComponent<TowerTest>().TowerType = towerType;
		TowerTest.GetComponent<TowerTest>().level = level;
		TowerTest.GetComponent<TowerTest>().index = index;
		TowerTest.GetComponent<TowerTest>().attack = 60;
		// TowerTest.transform.position = new Vector3(buildList[index].x,1,buildList[index].y);
		GameObject go = GameObject.Instantiate(TowerTest,position,Quaternion.identity);
		go.transform.parent = this.transform.GetChild(1).transform;
	}

	public IEnumerator SetMapShader(int index,string colorName,Color color,Shader shader)
	{
		for(int i = 0;i < mapList.Length; i++)
		{
			if(towerList[index].TowerPostion.x == mapList[i].transform.position.x && towerList[index].TowerPostion.z == mapList[i].transform.position.z)
			{
				mapList[i].transform.gameObject.GetComponent<MeshRenderer>().material.shader=shader;
    			mapList[i].transform.gameObject.GetComponent<MeshRenderer>().material.SetColor(colorName,color); 
				break;
			}
		}
		yield return 0;
	}

	public void SoldTower()
	{		
		for(int i =0;i<towerList.Count;i++)
		{
			if(!towerList[i].isDelected && towerList[i].isSelected)
			{				
				StartCoroutine(RemoveSoldTower());	

				Settings.Money += (int)(towerList[i].price * 0.9f);
				towerList[i].isDelected = true;
			}
		}
			
	}

	IEnumerator RemoveSoldTower()
	{
		GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
		
		for(int i = 0; i < towers.Length; i++)
		{
			if(towers[i].GetComponent<TowerTest>().isSelected)
			{
				Destroy(towers[i].gameObject);
				Vector3 pos = new Vector3(towers[i].gameObject.transform.position.x,0,towers[i].gameObject.transform.position.z);
				buildList.Add(pos);
				break;
			}
		}
		this.gameObject.GetComponent<RayCheckScript>().CancelSelect();

		yield return 0;
	}

	public void LevelTower()
	{
		int count = 0;
		TowerData[] levelData = new TowerData[3];
		for(int i = 0; i < towerList.Count; i++)
		{
			if(!towerList[i].isDelected && towerList[i].isSelected)
			{
				if(towerList[i].TowerLevel == 6)
				{
					message.AddMessage("最高等级");
					return;
				}

				levelData[count] = towerList[i];
				count ++;
				break;
			}
		}

		for(int i = 0; i < towerList.Count; i++)
		{
			if(!towerList[i].isDelected && !towerList[i].isSelected && towerList[i].TowerType == levelData[0].TowerType && towerList[i].TowerLevel == levelData[0].TowerLevel && count < 3)
			{
				levelData[count] = towerList[i];
				count ++;
			}
		}

		if(count <= 2)
		{
			message.AddMessage("升级失败");
			return;
		}
		else
		{
			message.AddMessage("升级成功");
		}

		int lvlPrice = levelData[0].price*3;

		int index = towerList.Count;

		TowerData newTower = new TowerData(towerList.Count,levelData[0].TowerType,levelData[0].TowerLevel+1,levelData[0].TowerPostion,lvlPrice,false,false);
		towerList.Add(newTower);

		BuildTower(new Vector3(levelData[0].TowerPostion.x,0.7f,levelData[0].TowerPostion.z),towerList[index].TowerType,towerList[index].TowerLevel,index);

		for(int i = 0 ;i < towerList.Count; i++)
		{
			for(int j = 0; j< levelData.Length; j++)
			{
				if(levelData[j].index == towerList[i].index)
				{
					towerList[i].isDelected = true;
					continue;
				}
			}
		}

		StartCoroutine(RemoveDeleteTower(levelData));

	}

	IEnumerator RemoveDeleteTower(TowerData[] levelData)
	{
		for(int i = 1; i < levelData.Length; i++)
		{
			Vector3 pos = new Vector3(levelData[i].TowerPostion.x,0,levelData[i].TowerPostion.z);
			buildList.Add(pos);
		}

		this.gameObject.GetComponent<RayCheckScript>().CancelSelect();

		GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
		GameObject[] temp = towers;
		for(int i = 0; i < temp.Length; i++)
		{
			for(int j =0; j< towerList.Count; j ++)
			{
				if(towerList[j].isDelected && temp[i].GetComponent<TowerTest>().index == towerList[j].index)
				{
					Destroy(towers[i].gameObject);
				}			
			}
		}

		yield return 0;
	}

	public void SetTowerSelected(int index,bool isSelected)
	{
		towerList[index].isSelected = isSelected;
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log("buildList.Count:"+buildList.Count);
		// Debug.Log("towerList.Count:"+towerList.Count);
	}


}
