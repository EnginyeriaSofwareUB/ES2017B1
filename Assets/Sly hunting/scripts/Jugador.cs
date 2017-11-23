using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	float speed = 3.0f; //10 se quito public
	float FPS = 4.0f; //60 se quito public
	public bool toRight = true;
	public bool playerControl;
	private bool jumping = false;
	private float estamina = 100;
	private float vida = 100;
	private Animator animator;
    bool izquierda = true;
    bool derecha = false;


	float forceJump = 20.0f;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();

		rb.mass = 15000f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

	}
	void Awake () {
		playerControl = false;
	}

	public float getEstamina(){
		return estamina;
	}

	public void addEstamina(float val){
		estamina += val;
	}

	public void subEstamina(float val){
		estamina -= val;
	}

	public void setPlayerControl (bool what){
		playerControl = what;
	}

    // Update is called once per frame
    void Update () {

		float stam = estamina - (estamina/5); //cada acción resta un 1/5 = 20%
		if (playerControl && stam > 0) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft ();
                
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				moveRight ();
                

			} else if (Input.GetKey (KeyCode.Space) && rb.velocity.y <= 0) {
				jump ();
			
			} else {
				idle ();
			}
			
		}
	}

	void moveRight(){
        /*
        if (toRight) {
			Vector3 newScale = this.transform.localScale;
			newScale.x *= -1;
			this.transform.localScale = newScale;
			toRight = false;
		}*/
        animator.StopPlayback();
        animator.Play("rightRun");
        //rb.velocity = new Vector2( speed * Time.deltaTime * FPS, rb.velocity.y);
        this.transform.Translate(Vector3.right * speed * Time.deltaTime * FPS);
        derecha = true;
        izquierda = false;

    }

	void moveLeft(){
        /*
		if (!toRight) {
			Vector3 newScale = this.transform.localScale;
			newScale.x *= -1;
			this.transform.localScale = newScale;					
			toRight = true;
		}*/
		animator.StopPlayback();		
		animator.Play("leftRun");
        //rb.velocity = new Vector2(speed * Time.deltaTime * FPS, rb.velocity.y);
        //rb.velocity = new Vector2(-1 * speed * Time.deltaTime * FPS, rb.velocity.y);
        this.transform.Translate(Vector3.right * (-1) * speed * Time.deltaTime * FPS);
        izquierda = true;
        derecha = false;
    }


	void jump(){
		if (!jumping) {
			jumping = true;
			animator.StopPlayback();
			animator.Play("saltar");

			float startGravity = rb.gravityScale;
			rb.gravityScale = 0;
			//rb.velocity = new Vector2 (rb.velocity.x, speed * Time.deltaTime * FPS);
			this.transform.Translate(Vector2.up * forceJump * Time.deltaTime * FPS);
			float timer = 0f;
			while (timer < 2f) {
				timer += Time.deltaTime;
			}
			//Set gravity back to normal at the end of the jump
			rb.gravityScale = startGravity;
			jumping = false;
		}

	}
	void idle(){
		animator.StopPlayback();
		if (derecha) animator.Play("rightIdle");
        else animator.Play("leftIdle");
    }
}
