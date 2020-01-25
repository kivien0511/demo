using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class player : MonoBehaviour {

	public GameObject targetObj;
	public GameObject bulletObj;

	GameObject energy;

	SpringJoint2D sj;

	Vector2 move;

	[HideInInspector]
	public Vector2 target;

	[HideInInspector]
	public bool isTrapped = false;

	[HideInInspector]
	public bool isSheild = false;

	[HideInInspector]
	public bool isSlowDrop = false;

	[HideInInspector]
	public bool isOtherLife = false;

	bool isTouch = false;
	float t = 0;

	public LineRenderer line;

	// Use this for initialization
	void Start () {
		sj = this.GetComponent<SpringJoint2D>();
		energy = GameObject.FindWithTag("Energy");
		line.material = new Material(Shader.Find("Particles/Additive"));

	}
	
	// Update is called once per frame
	void Update () {

		// Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity);


		// Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity);

		

		move = TCKInput.GetAxis("Joystick");
		
		if(bulletObj.activeInHierarchy)
		{
			line.gameObject.SetActive(true);

			line.SetPositions(
					new Vector3[]
					{
						this.transform.position,
						bulletObj.transform.position
					}
				);

		}
		else
		{
			line.gameObject.SetActive(false);
		}

		
		if(isTrapped)
		{
			sj.connectedAnchor = bulletObj.transform.position;
			
			isTrapped = false;
		}


			
		
		if(!this.gameObject.GetComponent<SpringJoint2D>().enabled)
		{
			// line.gameObject.SetActive(false);
			if(!isTouch && this.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
			{
				if(isSlowDrop)
				{
					Debug.Log("缓慢下落");//SlowFall

					this.gameObject.GetComponent<Rigidbody2D>().velocity /= 1.2f;

					t += Time.deltaTime;
					if(t > 3)
					{
						isSlowDrop = false;
					}
				}
				else
				{
					Debug.Log("下落");//Fall
				}
			}
		}

	}

	public void TouchUp()
	{		
		if((!bulletObj.activeInHierarchy|| bulletObj.GetComponent<bullet>().isStop))
		{
			// line.gameObject.SetActive(true);			

			isTouch = false;

			targetObj.SetActive(false);

			bulletObj.SetActive(true);
			bulletObj.transform.position = this.transform.position;
			bulletObj.GetComponent<bullet>().isBack = false;

			// targetObj.transform.position = target;
			// sj.connectedAnchor = target;
			GetComponent<SpringJoint2D>().enabled = false;

			if(energy.GetComponent<Energy>().energy >= 20)
			{
				energy.GetComponent<Energy>().energy -= 20;
				Time.timeScale = 1f;
			}
			else
			{
				energy.GetComponent<Energy>().energy = 0;
				Time.timeScale = 1f;
			}					
		}
	}

	public void TouchDown()
	{
		if((!bulletObj.activeInHierarchy|| bulletObj.GetComponent<bullet>().isStop))
		{	float l = Mathf.Sqrt(move.x * move.x + move.y * move.y);

			if(l >= 0.3){
			bulletObj.GetComponent<bullet>().isBack = false;
			bulletObj.GetComponent<bullet>().isStop = false;
			bulletObj.SetActive(false);

			if(energy.GetComponent<Energy>().energy >=0)
			{
				energy.GetComponent<Energy>().energy -= 10 * Time.deltaTime;
				Time.timeScale = 0.3f;
			}
			else 
			{
				energy.GetComponent<Energy>().energy = 0;
				Time.timeScale = 1f;
			}
			

			isTouch = true;

	
			
				
				bulletObj.transform.position = this.transform.position;
				

				float len = 5 * l;

				if(len <= 3)
				{
					len = 3;
				}

				target = move.normalized * -1 * len + (Vector2)this.transform.position;

				//发射目标点 ShootPointShow
				targetObj.SetActive(true);
				targetObj.transform.position = target;

				//发射物设定目标点 ShootPointSet
				bulletObj.GetComponent<bullet>().target = target;
		
			}		
		
		}
		
	}

}
