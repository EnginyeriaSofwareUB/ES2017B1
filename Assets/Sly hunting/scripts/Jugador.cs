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
	private float estamina = 100;
	private float vida = 100;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.mass = 15000f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		List<GameObject> characters = new List<GameObject> ();

		characters.AddRange(GameObject.FindGameObjectsWithTag ("Hunter"));
		characters.AddRange (GameObject.FindGameObjectsWithTag("Animal"));
		setRandomPositions (characters);

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

	private bool checkIfPosEmpty(Vector3 targetPos) {
		GameObject[] allTerrain = GameObject.FindGameObjectsWithTag ("floor");
		foreach (GameObject current in allTerrain) {
			if (current.transform.position == targetPos)
				return false;
		}
		return true;
	}

	private void setRandomPositions (List<GameObject> objects) {
		GameObject[] terrain;
		terrain = GameObject.FindGameObjectsWithTag ("floor");
		objects.ForEach (obj => {
			Vector3 newPos = terrain[UnityEngine.Random.Range(0,terrain.Length)].transform.position;
			newPos.y += 5;
			while (!checkIfPosEmpty (newPos)) {
				newPos.y += 5;
			}
			obj.transform.position = newPos;
		});
	}

    // Update is called once per frame
    void Update () {
		float stam = estamina - (estamina/5); //cada acción resta un 1/5 = 20%
		if (playerControl && stam > 0) {
			if (Input.GetKey (KeyCode.LeftArrow) ) {
				if (playerControl) {
					if (toRight) {
						Vector3 newScale = this.transform.localScale;
						newScale.x *= -1;
						this.transform.localScale = newScale;
						toRight = false;
					}
					rb.velocity = new Vector2 ((-1) * speed * Time.deltaTime * FPS, rb.velocity.y);
				}
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				if (playerControl) {
					if (!toRight) {
						Vector3 newScale = this.transform.localScale;
						newScale.x *= -1;
						this.transform.localScale = newScale;					
						toRight = true;
					}
					rb.velocity = new Vector2 (speed * Time.deltaTime * FPS, rb.velocity.y);
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
}
