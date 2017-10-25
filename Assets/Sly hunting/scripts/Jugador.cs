using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	public float speed = 1.0f;
	public float FPS = 60f;
	private SpriteRenderer mySpriteRenderer;
	public bool toRight = true;

	public bool playerControl;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	void Awake () {
		playerControl = false;
	}

	public void setPlayerControl (bool what){
		playerControl = what;
	}
    // Update is called once per frame
    void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (playerControl) {
				if (toRight) {
					mySpriteRenderer.flipX = true;
					toRight = false;
				}
				rb.transform.Translate (Vector3.left * speed * Time.deltaTime * FPS);
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (playerControl) {
				if (!toRight) {
					mySpriteRenderer.flipX = false;
					toRight = true;
				}
				rb.transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
			} 
		}
		if (Input.GetKey (KeyCode.Space)) {
			if (playerControl)
				rb.transform.Translate (Vector3.up * speed * Time.deltaTime * FPS);
			
		}
			
	}
}
