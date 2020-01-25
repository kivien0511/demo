using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvReader {

	static CsvReader csv;

	public List<string[]> arrayData;  

    private CsvReader()   //单例，构造方法为私有
    {
        arrayData = new List<string[]>();
    }

    public static CsvReader GetInstance()   //单例方法获取对象
    {
        if(csv == null)
        {
            csv = new CsvReader();
        }
        return csv;
    } 
	
	public void loadFile(string fileName)
    {
        arrayData.Clear();        

        var binAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;  

        string[] lineArray = binAsset.text.Split ('\n');  

        for(int i = 0 ;i< lineArray.Length; i++)
        {
            arrayData.Add(lineArray[i].Split(','));
        } 

    }

	public List<string[]> getArrayData()
	{
		return arrayData;
	}

    public string getString(int row,int col)
    {
        return arrayData[row][col];
    }

    public int getInt(int row,int col)
    {
        return int.Parse(arrayData[row][col]);
    }
}
