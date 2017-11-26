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
    public float jumpForce = 20.0f;
    public Vector2 ju;


    float forceJump = 20.0f;

	 void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
        ju  = new Vector3(0.0f, 10.0f);
       // rb.mass = 1f;
		//rb.constraints = RigidbodyConstraints2D.FreezeRotation;

	}
	void Awake () {
		playerControl = false;
        //animator.StopPlayback();
        //idle();
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
                

			} else if (Input.GetKey (KeyCode.Space) && !jumping) {
				jump ();
                jumping = true;
                
			
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
   
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col);
        print("Collision detected with a trigger object");
        if (col.gameObject.name == "Suelo")
        {
            Debug.Log(col);
            jumping = false;
        }
        
        
    }
    void jump(){
			animator.StopPlayback();
			animator.Play("saltar");
            //float startGravity = rb.gravityScale;
            //rb.gravityScale = 0;
            //rb.velocity = new Vector2 (rb.velocity.x, speed * Time.deltaTime * FPS);
            rb.AddForce(ju, ForceMode2D.Impulse);
            
            //this.transform.Translate(Vector2.up * forceJump * Time.deltaTime * FPS);
			
			//Set gravity back to normal at the end of the jump
			//rb.gravityScale = startGravity;
            
			

	}
	void idle(){
		animator.StopPlayback();
		if (derecha) animator.Play("rightIdle");
        else animator.Play("leftIdle");
    }
}
