using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

    public Rigidbody rb;
	public float speed = 1.0f;
	public float FPS = 60f;
	private SpriteRenderer mySpriteRenderer;
	public bool toRight = true;
	public bool playerControl;
	List<GameObject> players = new List<GameObject>();

	public float timer = 0.0f;
	public float startTime = 5.0f;
	public bool estatTimer;
	//public Button button;
	public Text txtTimer;
	//public Text txt2;

	void Awake () {
		//Button btn = button.GetComponent<Button> ();
		//btn.onClick.AddListener (buttonReset);
		playerControl = true;
		timer = startTime;
		estatTimer = true;
		//setText ();
	}

    // Update is called once per frame
    void Update () {
		if (Input.GetKey ("escape"))
			Application.Quit ();
		
		foreach (GameObject player in GameObject.FindObjectsOfType(typeof(GameObject))) {
			if (player.tag == "Player") {
				players.Add (player);
			}
		}

		if (estatTimer) {
			timer -= Time.deltaTime;
			txtTimer.text = "Time left: " + Mathf.Round (timer) + " s";

			if (timer <= 0) {
				changeTurn ();
				//setText ();
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (playerControl) {
				if (toRight) {
					players [0].GetComponent<SpriteRenderer> ().flipX = true;
					toRight = false;
				}
				players [0].transform.Translate (Vector3.left * speed * Time.deltaTime * FPS);
			} else {
				if (toRight) {
					players [1].GetComponent<SpriteRenderer> ().flipX = true;
					toRight = false;
				}
				players [1].transform.Translate (Vector3.left * speed * Time.deltaTime * FPS);
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (playerControl) {
				if (!toRight) {
					players [0].GetComponent<SpriteRenderer> ().flipX = false;
					toRight = true;
				}
				players [0].transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
			} else {
				if (!toRight) {
					players [1].GetComponent<SpriteRenderer> ().flipX = false;
					toRight = true;
				}
				players [1].transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
			}
		}
		if (Input.GetKey (KeyCode.Space)) {
			if (playerControl)
				players [0].transform.Translate (Vector3.up * speed * Time.deltaTime * FPS);
			else
				players[1].transform.Translate (Vector3.up * speed * Time.deltaTime * FPS);
		}
			
	}

	public void onClick(){
		changeTurn ();
	}

	void changeTurn(){
		playerControl = !playerControl;
		timer = startTime;
	}
		
}
