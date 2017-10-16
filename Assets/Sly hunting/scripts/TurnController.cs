using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {
	public bool pl1;
	public bool pl2;
	public float timer = 0.0;
	public float startTime = 30.0;
	public bool estatTimer = true;
	public Button button;

	// Use this for initialization
	void init () {
		Button btn = button.GetComponent<Button> ();
		btn.onClick.AddListener (buttonReset);
		pl1 = true;
		pl2 = false;
		timer = startTime;	
	}
	
	// Update is called once per frame
	void Update () {
		if (estatTimer) {
			displayTimer ();
			timer -= Time.deltaTime;

			if (timer <= 0) {
				changeTurn ();	
			}
		}
	}

	void stop(){
		this.estatTimer = false;
	}

	void changeTurn(){
		pl1 = !pl1;
		pl2 = !pl2;
		timer = startTime;
	}

	void buttonReset(){
		changeTurn ();
	}

	void displayTimer(){
		int displaySeconds;
		displaySeconds = (int) timer % 60;
		string text = string.Format ("{1:00}", displaySeconds);
		GUI.Label (Rect (400, 25, 100, 30), text);


	}

		
}
