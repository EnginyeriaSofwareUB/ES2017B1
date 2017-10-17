using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {
	public bool pl1;
	public bool pl2;
	public float timer = 0.0f;
	public float startTime = 10.0f;
	public bool estatTimer;
	//public Button button;
	public Text txt;
	public Text txt2;

	// Use this for initialization
	void Awake () {
		//Button btn = button.GetComponent<Button> ();
		//btn.onClick.AddListener (buttonReset);
		pl1 = true;
		pl2 = false;
		timer = startTime;
		estatTimer = true;
		setText ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (estatTimer) {
			//displayTimer ();
			timer -= Time.deltaTime;

			if (timer <= 0) {
				
				changeTurn ();
				setText ();
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

	void buttonStop(){
		stop ();
	}

	void setText()
	{
		if (pl1) {
			txt2.text = "";
			txt.text = "Turn del jugador 1";

		}
		else{
			txt.text = "";
			txt2.text = "Turn del jugador 2";

		}
	}

	/*void displayTimer(){
		int displaySeconds;
		displaySeconds = (int) timer % 60;
		string text = string.Format ("{1:00}", displaySeconds);
		txt.text = text;


	}*/

		
}
