using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	public bool playerControl;
	private bool agile = false;
	private bool destructor = false;

	float speed = 8.0f; 
	float FPS = 4.0f; 
	float estaminaRate = 2.0f;
	public bool toRight = true;
	public bool jumping = false;
	float forceJump = 50.0f;

	public float currentStamina = 0.0f;
	private float maxStamina = 100.0f;
	public float currentHealth = 0.0f;
	private float maxHealth = 100.0f;

	private Animator animator;
	private List<Arma> weapons;
	private int currentWeapon;
	public Transform puntoFuego;
	private Equipo equipo;

	public GameObject healthBar;
	public GameObject staminaBar;

	//Variable Antoni
	public Vector2 ju;
    //#xC
    //sonidos:
    private bool death;
	private AudioSource source;
	public AudioClip jumpSound;
	//public AudioClip stepSound;
	public AudioClip hurtMonkey;
	public AudioClip hurtHunter;
	public AudioClip deathSound;
	private Transform Posicion;
	private String tipo;

	//Modificación SonidoFX
	private int controlSonido;

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
        death = true;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		ju  = new Vector3(0.0f, 10.0f);
		rb.mass = 1f;
		//Modificación SonidoFX
		controlSonido = PlayerPrefs.GetInt("Sonido");

	}

	void Awake () {
		playerControl = false;
		//animator.StopPlayback();
		//idle();
	}

	// Update is called once per frame
	void Update () {

		if (playerControl && currentStamina > 0 && death) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft (); 

			} else if (Input.GetKey (KeyCode.RightArrow)) {
				moveRight ();

				/*} else if (Input.GetKey (KeyCode.Space) && !jumping) {
				jump ();*/      

			} else if (Input.GetKey (KeyCode.Space) && rb.velocity.y <= 0) {
				jump ();

			} else if (Input.GetKey (KeyCode.UpArrow)) {
				lookUp ();

			} else if (Input.GetKey (KeyCode.DownArrow)) {
				lookDown ();

			} else if (Input.GetKey (KeyCode.Return)) {
				fire ();

			} else if(Input.GetKey(KeyCode.C)){
				changeWeapon ();

			} else {
				if (death) idle ();
			}

		}

		setHealthBar ();
		setStaminaBar ();

	}



	private void moveRight(){
		toRight = true;
		animator.StopPlayback();
		if(!jumping) animator.Play("rightRun");
		Quaternion aux = this.transform.rotation;
		aux.y = 0;
		this.transform.rotation = aux;
		this.transform.Translate (Vector3.right * speed * Time.deltaTime * FPS);
		barPosition ("right");
		currentStamina -= Time.deltaTime * FPS *estaminaRate;
	}

	private void moveLeft(){
		toRight = false;
		animator.StopPlayback();		
		if(!jumping) animator.Play("rightRun");
		Quaternion aux = this.transform.rotation;
		aux.y = 180;
		this.transform.rotation = aux;
		this.transform.Translate(Vector3.right * speed * Time.deltaTime * FPS);
		barPosition ("left");
		currentStamina -= Time.deltaTime * FPS *estaminaRate;
	}

	private void barPosition(String dir) {
		GameObject healthBar = transform.Find("HealthBar").gameObject;
		GameObject staminaBar = transform.Find("StaminaBar").gameObject;
		switch(dir) {
		case "left":
			if (healthBar.transform.localPosition.z < 0) {
				Vector3 pos = healthBar.transform.localPosition;
				pos.z *= -1;
				healthBar.transform.localPosition = pos;
				pos = staminaBar.transform.localPosition;
				pos.z *= -1;
				staminaBar.transform.localPosition = pos;
			}
			break;
		case "right":
			if (healthBar.transform.localPosition.z > 0) {
				Vector3 pos = healthBar.transform.localPosition;
				pos.z *= -1;
				healthBar.transform.localPosition = pos;
				pos = staminaBar.transform.localPosition;
				pos.z *= -1;
				staminaBar.transform.localPosition = pos;
			}
			break;
		}
	}
	private void jump(){
		Debug.Log (jumping);
		if (!jumping) {
			jumping = true;
			Debug.Log ("we are in "+jumping);
			rb.velocity = new Vector2 (rb.velocity.x, forceJump);
		}
		jumping = true;
	}
		

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
	public void makeAgile(){
		
		if (agile == false) {
			speed *= 2;
			estaminaRate /= 2;
			//TODO icon ágil
		}
		agile = true;
	}

	public void makeDestructor(){
		if (destructor == false) {
			//TODO icon destructor
		}
		destructor = true;
	}

	public bool isDestructor(){
		return destructor;
	}

	public void setWeapons(List<Arma> armas) {
		weapons = armas;
	}
	public void changeWeapon(){
		if (currentWeapon == (weapons.Count - 1)) {
			currentWeapon = 0;
		} else {
			currentWeapon += 1;
		}
		Debug.Log ("TIPOOO: "+weapons[currentWeapon].getTipus());
		//TODO Aquí caldría cambia el visual del arma
		//weapons[currentWeapon].getSprite()
	}
	//We call this method when we find a box with a new weapon and we have already added to the list
	public void updateWeaponsTeam(Arma weapon) {
		weapons.Add(weapon);
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
		if (tipo == "Cazador" && controlSonido != 0) {
			AudioSource.PlayClipAtPoint(hurtHunter, Posicion.position, 1.0f);
		}
		if (tipo == "Mono" && controlSonido != 0) {
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

	public Equipo getEquipo(){
		return equipo;	
	}

	private void setHealthBar() {
		healthBar.transform.localScale = new Vector3 (Mathf.Clamp(currentHealth / maxHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	private void setStaminaBar() {
		staminaBar.transform.localScale = new Vector3 (Mathf.Clamp(currentStamina / maxStamina,0f ,1f), staminaBar.transform.localScale.y, staminaBar.transform.localScale.z);
	}

    public void animDeath() {
        animator.StopPlayback();
        animator.Play("death");
        Posicion = transform;
        if (controlSonido != 0) AudioSource.PlayClipAtPoint(deathSound, Posicion.position, 1.0f);

    }

    private IEnumerator morir()
    {
        yield return new WaitForSeconds(2.5f);
        
        equipo.removePlayer(this.gameObject);
        Destroy(this.gameObject);
    }

    public void destroy(){
        death = false;
        animDeath();
        StartCoroutine(morir());
        
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "floor" || other.gameObject.tag == "tree" || other.gameObject.tag == "stone") {
			jumping = false;
		}
	}	


}