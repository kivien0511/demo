using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : BaseManager<AudioManager> 
{
	//唯一的背景音乐
	private AudioSource bkMusic = null;
	//背景音乐音量大小
	private float bkValue = 1;
	//音效依附对象
	private GameObject soundObj = null;
	//音效列表
	private List<AudioSource> soundList = new List<AudioSource>();
	//音效音量大小
	private float soundValue = 1;

	public AudioManager()
	{
		MonoManager.GetInstance().AddUpdateLinster(Update);
	}

	private void Update()
	{
		soundList.ForEach((x)=>{
			if(!x.isPlaying)
			{
				GameObject.Destroy(x);
				soundList.Remove(x);
			}
		});
	}


	/// <summary>
	/// 播放背景音乐
	/// </summary>
	/// <param name="name"></param>
	public void PlayBKMusic(string name)
	{
		if(bkMusic == null)
		{
			GameObject obj = new GameObject();
			obj.name = "BkMusic";
			bkMusic = obj.AddComponent<AudioSource>();
		}

		//异步加载背景音乐 加载完成后 播放
		ResourcesManager.GetInstance().LoadAsync<AudioClip>("Audio/BGM/"+name,(clip)=>{
			bkMusic.clip = clip;
			bkMusic.volume = bkValue;
			bkMusic.Play();
		});
	}

	/// <summary>
	/// 改变背景音乐音量大小
	/// </summary>
	/// <param name="v"></param>
	public void ChangeBKVolume(float v)
	{
		bkValue = v;
		if(bkMusic == null)return;
		bkMusic.volume = bkValue;
	}

	/// <summary>
	/// 暂停播放背景音乐
	/// </summary>
	public void PauseBKMusic()
	{
		if(bkMusic == null)return;
		bkMusic.Pause();
	}

	/// <summary>
	/// 停止播放背景音乐
	/// </summary>
	public void StopBKMusic()
	{
		if(bkMusic == null)return;
		bkMusic.Stop();
	}

	/// <summary>
	/// 播放音效
	/// </summary>
	/// <param name="name"></param>
	public void PlaySound(string name, bool isLoop,UnityAction<AudioSource> callback = null)
	{
		if(soundObj == null)
		{
			soundObj = new GameObject();
			soundObj.name = "Sound";
		}

		//当音效资源异步加载结束后 再添加一个音效
		ResourcesManager.GetInstance().LoadAsync<AudioClip>("Audio/Sound/"+name,(clip)=>{
			AudioSource source = soundObj.AddComponent<AudioSource>();
			source.clip = clip;
			source.loop = isLoop;
			source.volume = soundValue;
			source.Play();
			soundList.Add(source);
			
			if(callback != null)
			callback(source);
		});
	}


	/// <summary>
	/// 改变音效声音大小
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSoundValue(float value)
	{
		soundValue = value;
		soundList.ForEach((x)=>{x.volume = soundValue;});
	}

	/// <summary>
	/// 停止播放音效
	/// </summary>
	/// <param name="source"></param>
	public void StopSound(AudioSource source)
	{
		if(soundList.Contains(source))
		{
			source.Stop();
			soundList.Remove(source);
			GameObject.Destroy(source);
		}
	}
}
