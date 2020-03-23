using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour {

	GUIStyle style;

	AudioSource source;

	float v = 0;
	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,100,100),"Play BGM"))
		{			
			v = 0;
			AudioManager.GetInstance().ChangeBKVolume(v);
			AudioManager.GetInstance().PlayBKMusic("PlayBGM");
		}

		if(GUI.Button(new Rect(0,100,100,100),"Pause BGM"))
			AudioManager.GetInstance().PauseBKMusic();

		if(GUI.Button(new Rect(0,200,100,100),"Stop BGM"))
			AudioManager.GetInstance().StopBKMusic();

		v += Time.deltaTime/100;
		AudioManager.GetInstance().ChangeBKVolume(v);
	
		if(GUI.Button(new Rect(100,0,100,100),"Play SE"))
			AudioManager.GetInstance().PlaySound("AttackSE",false,(s)=>{
				source = s;
			});

		if(GUI.Button(new Rect(100,100,100,100),"Play SE"))
		{
			AudioManager.GetInstance().StopSound(source);
			source = null;
		}	
	}
}
