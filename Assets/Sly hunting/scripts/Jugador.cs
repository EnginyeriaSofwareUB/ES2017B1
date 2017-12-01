using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	float speed = 8.0f; //10 se quito public
	float FPS = 4.0f; //60 se quito public
	float estaminaRate = 2.0f;
	public bool toRight = true;
	public bool playerControl;
	public bool jumping = false;
	public float currentStamina = 0.0f;
	private float maxStamina = 100.0f;
	public float currentHealth = 0.0f;
	private float maxHealth = 100.0f;

	private Animator animator;
	private List<Arma> weapons;
	private int currentWeapon;
	private Equipo equipo;
	public Transform puntoFuego;
	float forceJump = 20.0f;
	public GameObject healthBar;
	public GameObject staminaBar;

	//Variable Antoni
	public Vector2 ju;

	//#xC
	//sonidos:

	private AudioSource source;
	public AudioClip jumpSound;
	//public AudioClip stepSound;
	public AudioClip hurtMonkey;
	public AudioClip hurtHunter;
	public AudioClip deathSound;
	private Transform Posicion;
	private String tipo;

	public static GameObject create(string type, Equipo equipo){
		GameObject player = (GameObject)Resources.Load(type, typeof(GameObject));
		GameObject pl = Instantiate (player);
		Jugador jd = pl.GetComponent<Jugador>();
		jd.tipo = type;
		jd.setEquipo (equipo);
		return pl;
	
	}

	// Hasta aqui correcto

	private void Start()
	{
		
		currentWeapon = 0;
		currentHealth = maxHealth;
		currentStamina = maxStamina;
		toRight = true;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		rb.mass = 15000f;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		//Posicion = transform;
		//Antoni
		ju  = new Vector3(0.0f, 10.0f);

	}

	void Awake () {
		playerControl = false;
		//animator.StopPlayback();
		//idle();
	}

	// Update is called once per frame
	void Update () {

		if (playerControl && currentStamina > 0) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft (); 

			} else if (Input.GetKey (KeyCode.RightArrow)) {
				moveRight ();

				/*} else if (Input.GetKey (KeyCode.Space) && !jumping) {
				jump ();*/      

			} else if (Input.GetKey (KeyCode.Space) && rb.velocity.y <= 0) {
				jump ();

			} else if (Input.GetKey(KeyCode.UpArrow)) {
				lookUp();

			} else if (Input.GetKey(KeyCode.DownArrow)) {
				lookDown();

			} else if (Input.GetKey (KeyCode.Return)) {
				fire ();

			} else {
				idle ();
			}

		}

		setHealthBar ();
		setStaminaBar ();

		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			if (Camera.main.orthographicSize <= 50)
				Camera.main.orthographicSize += 0.5f;
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (Camera.main.orthographicSize >= 1)
				Camera.main.orthographicSize -= 0.5f;
		}
	}

	private void moveRight(){
		toRight = true;
		animator.StopPlayback();
		animator.Play("rightRun");
		Quaternion aux = this.transform.rotation;
		aux.y = 0;
		this.transform.rotation = aux;
		this.transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
		currentStamina -= Time.deltaTime * FPS *estaminaRate;
	}

	private void moveLeft(){
		toRight = false;
		animator.StopPlayback();		
		animator.Play("rightRun");
		Quaternion aux = this.transform.rotation;
		aux.y = 180;
		this.transform.rotation = aux;
		this.transform.Translate(Vector3.right * speed * Time.deltaTime * FPS);
		currentStamina -= Time.deltaTime * FPS *estaminaRate;
	}

	private void jump(){
		if (!jumping) {
			jumping = true;
			animator.StopPlayback();
			animator.Play("rightJump");

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

			// Antoni
			rb.AddForce(ju, ForceMode2D.Impulse);

			jumping = false;
			currentStamina -= Time.deltaTime * FPS *estaminaRate;
		}
	}

	/*void jump(){
		animator.StopPlayback();
		animator.Play("rightJump");
		rb.AddForce(ju, ForceMode2D.Impulse);
        jumping = true;
    }*/

	private void lookUp() {
		float angle  = puntoFuego.transform.localEulerAngles.z + 1;
		if ((angle  <= 90.0f && angle >= 0.0f) || (angle <= 361.0f && angle >= 270.0f)) {
			puntoFuego.transform.Rotate (0, 0, 1);	
		}
	}

	private void lookDown() {
		float angle = puntoFuego.transform.localEulerAngles.z;
		if ((angle  <= 90.0f && angle >= 0.0f) || (angle <= 361.0f && angle >= 270.0f)) {
			puntoFuego.transform.Rotate (0, 0, -1);	
		}

	}

	private void fire() {
		//xC
		animator.StopPlayback();
		Arma currentW = weapons [currentWeapon];
		if (currentW.getBullets () > 0) {
			// Animación correcta
			/*if (!toRight) animator.Play("rightShoot");
			else animator.Play("leftShoot");*/

			// Michael
				animator.Play("rightShoot");
			currentW.fire (toRight, puntoFuego,this);
		}
	}

	private void idle(){
		animator.StopPlayback();
		animator.Play("rightIdle");
	}

	//****************** GETTERS & SETTERS **********************/

	public void setWeapons(List<Arma> armas) {
		weapons = armas;
	}

	//We call this method when we find a box with a new weapon and we have already added to the list
	public void updateWeaponsTeam(Arma weapon) {
		equipo.addWeapon (weapon);
	}

	public float getEstamina(){
		return currentStamina;
	}

	public void addEstamina(float val){
		currentStamina += val;
	}

	public void subEstamina(float val){
		
		currentStamina -= val;
	}

	public float getVida(){
		return currentHealth;
	}

	public void addVida(float val) {
		currentHealth += val;
	}

	public void quitLife(float demage){
		
		currentHealth -= demage;


		Posicion = transform;
		if (tipo == "Cazador") {
			AudioSource.PlayClipAtPoint(hurtHunter, Posicion.position, 1.0f);
		}
		if (tipo == "Mono") {
			AudioSource.PlayClipAtPoint(hurtMonkey, Posicion.position, 1.0f);
		}
		if (currentHealth <= 0) {
			destroy ();
		}
	}

	public void setPlayerControl (bool what){
		if (what == false) {
			idle ();
		} else {
			//restauramos estamina en el turno
			currentStamina = 100;
		}
		playerControl = what;
	}

	public void setEquipo(Equipo eq){
		equipo = eq;
	}

	private void setHealthBar() {
		healthBar.transform.localScale = new Vector3 (Mathf.Clamp(currentHealth / maxHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	private void setStaminaBar() {
		staminaBar.transform.localScale = new Vector3 (Mathf.Clamp(currentStamina / maxStamina,0f ,1f), staminaBar.transform.localScale.y, staminaBar.transform.localScale.z);
	}

	public void destroy(){
		equipo.removePlayer (this.gameObject);
		Destroy (this.gameObject);
	}
		
	/*private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "muerteSegura"){
			destroy ();
		}

	}*/

	/*void OnCollisionStay2D(Collision2D col)
	{
		//print("Collision detected with a trigger object");
		jumping = false;
	}*/
}