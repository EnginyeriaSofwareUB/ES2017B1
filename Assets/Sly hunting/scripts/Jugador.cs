using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	public float speed = 10.0f;
	public float FPS = 60f;
	private SpriteRenderer mySpriteRenderer;
	public bool toRight = true;
	public bool playerControl;
	private bool jumping = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.mass = 500f;
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
				float move = Input.GetAxis ("Horizontal");
				rb.velocity = new Vector2 ((-1)*speed * Time.deltaTime * FPS, rb.velocity.y);
				//rb.transform.Translate (Vector3.left * speed * Time.deltaTime * FPS);
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (playerControl) {
				if (!toRight) {
					mySpriteRenderer.flipX = false;
					toRight = true;
				}
				float move = Input.GetAxis ("Horizontal");
				rb.velocity = new Vector2 (speed * Time.deltaTime * FPS, rb.velocity.y);
				//rb.transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
			} 
		}
		if (Input.GetKey (KeyCode.Space) && rb.velocity.y <= 0) {
			
			if (playerControl && !jumping) {
				jumping = true;
				float startGravity = rb.gravityScale;
				rb.gravityScale = 0;
				rb.velocity = new Vector2 (rb.velocity.x, speed * Time.deltaTime * FPS);
				float timer = 0f;
				while (timer < 2f) {
					timer += Time.deltaTime;
				}
				//Set gravity back to normal at the end of the jump
				rb.gravityScale = startGravity;
				jumping = false;
			}
		}
			
	}
}
