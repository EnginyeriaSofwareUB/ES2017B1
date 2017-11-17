using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador2 : MonoBehaviour {

	public Rigidbody2D rb;
	public float speed = 3.0f;
	public float FPS = 6.0f;
	private SpriteRenderer mySpriteRenderer;
	public bool toRight = true;
	public bool playerControl;
	private bool jumping = false;   
    private Transform[] obj;
    private GameObject personaje;
    private Animator animator;
    private AudioClip jumpsound;
    private bool derecha = false;
    private bool izquierda = true;

    private void Start()
	{

        obj = GetComponentsInChildren<Transform>(true);

        personaje = obj[1].gameObject;//mono
        //personaje = obj[9].gameObject;//cazador
        Debug.Log("numero" + personaje.name);
        animator = personaje.GetComponent<Animator>();
        rb = personaje.GetComponent<Rigidbody2D> ();
		rb.mass = 15000f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            animator.StopPlayback();
            animator.Play("rightRun");
            personaje.transform.Translate(Vector3.right * (-1) * speed * Time.deltaTime * FPS);
            izquierda = true;
            derecha = false;
            if (playerControl)
            {
                if (toRight)
                {
                    Vector3 newScale = this.transform.localScale;
                    newScale.x *= -1;
                    this.transform.localScale = newScale;
                    toRight = false;
                }
                rb.velocity = new Vector2((-1) * speed * Time.deltaTime * FPS, rb.velocity.y);
                //rb.transform.Translate (Vector3.left * (-1)speed * Time.deltaTime * FPS);
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("antes de mover1");

            animator.StopPlayback();
            animator.Play("leftRun");
            //Debug.Log("antes de mover1");
            personaje.transform.Translate(Vector3.right *  speed * Time.deltaTime * FPS);
            Debug.Log("vector:" + Vector3.right);
            derecha = true;
            izquierda = false;
            if (playerControl)
            {
                if (!toRight)
                {
                    Vector3 newScale = this.transform.localScale;
                    newScale.x *= -1;
                    this.transform.localScale = newScale;
                    toRight = true;
                }
                //rb.velocity = new Vector2 (speed * Time.deltaTime * FPS, rb.velocity.y);
                //rb.transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);


            }


        }

        else if (Input.GetKey(KeyCode.Space) && rb.velocity.y <= 0)
        {

            animator.StopPlayback();
            animator.Play("saltar");
            personaje.transform.Translate(Vector3.up * 6.0f * speed * Time.deltaTime * FPS);
            GetComponent<AudioSource>().PlayOneShot(jumpsound, 1.0f);

            if (playerControl && !jumping)
            {
                jumping = true;
                float startGravity = rb.gravityScale;
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime * FPS);
                float timer = 0f;
                while (timer < 2f)
                {
                    timer += Time.deltaTime;
                }
                //Set gravity back to normal at the end of the jump
                rb.gravityScale = startGravity;
                jumping = false;
            }
        }

        else {

            Debug.Log("detenido");
            animator.StopPlayback();
            if (personaje.name == "Cazador")
            {
                animator.Play("leftIdle");
            }
            else
            {
                Debug.Log("mono");
                if(derecha) animator.Play("leftIdle");
                else animator.Play("rightIdle");
            }
        }
			
	}
}
