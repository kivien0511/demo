using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InputManager.GetInstance().StartOrEndCheck(true);

		InputManager.GetInstance().SetKeyCode(KeyCode.W);
		InputManager.GetInstance().SetKeyCode(KeyCode.A);
		InputManager.GetInstance().SetKeyCode(KeyCode.S);
		InputManager.GetInstance().SetKeyCode(KeyCode.D);

		EventCenter.GetInstance().AddEventLinster<KeyCode>("KeyDown",CheckInputDown);
		EventCenter.GetInstance().AddEventLinster<KeyCode>("KeyUp",CheckInputUp);
	}

	private void CheckInputDown(KeyCode key)
	{
		switch(key)
		{
			case KeyCode.W:
			case KeyCode.S:
			case KeyCode.A:
			case KeyCode.D:
				Debug.Log(key+" Down");
				break;
		}
	}
	
	private void CheckInputUp(KeyCode key)
	{
		switch(key)
		{
			case KeyCode.W:
			case KeyCode.S:
			case KeyCode.A:
			case KeyCode.D:
				Debug.Log(key+" Up");
				break;
		}
	}
}
