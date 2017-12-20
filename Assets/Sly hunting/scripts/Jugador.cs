using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jugador : MonoBehaviour {

	public Rigidbody2D rb;
	float speed = 8.0f; //10 se quito public
	float FPS = 4.0f; //60 se quito public
	float estaminaRate = 5.0f;
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
	float forceJump = 50.0f;
	public GameObject healthBar;
	public GameObject staminaBar;
	public GameObject arrowIndicator;
	private float timeArrow;
	private float totalTimeArrow;
	private bool arrowActive;

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
	public float volumen;

	public static GameObject create(string type, Equipo equipo){
		GameObject player = (GameObject)Resources.Load(type, typeof(GameObject));
		GameObject pl = Instantiate (player);
		Jugador jd = pl.GetComponent<Jugador>();
		jd.tipo = type;
		//jd.setVol (volumen);
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

		death = false;
		//GameObject arrowIndicator = transform.Find ("infoIcons").gameObject.transform.Find("ArrowIndicator").gameObject;
		arrowActive = false;
		arrowIndicator.SetActive (arrowActive);
		timeArrow = 3.0f;

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


		if (playerControl && currentStamina > 0 && !death) {
			if ((totalTimeArrow < Time.time) && arrowActive) {
				//GameObject arrowIndicator = transform.Find ("infoIcons").gameObject.transform.Find("ArrowIndicator").gameObject;
				arrowIndicator.SetActive (false);
				arrowActive = false;
			}
				
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft (); 

			} else if (Input.GetKey (KeyCode.RightArrow) && !death) {
				moveRight ();

				/*} else if (Input.GetKey (KeyCode.Space) && !jumping) {
				jump ();*/      

			} else if (Input.GetKey (KeyCode.Space) && rb.velocity.y <= 0 && !death) {
				jump ();

			} else if (Input.GetKey(KeyCode.UpArrow)) {
				lookUp();

			} else if (Input.GetKey(KeyCode.DownArrow)) {
				lookDown();

			} else if (Input.GetKey (KeyCode.Return) && !death) {
				fire ();

			} else {

				idle ();

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
		GameObject infoIcon = transform.Find ("infoIcons").gameObject;
		switch(dir) {
		case "left":
			if (infoIcon.transform.localPosition.z < 0) {
				Vector3 pos = infoIcon.transform.localPosition;
				pos.z *= -1;
				infoIcon.transform.localPosition = pos;
			}
			break;
		case "right":
			if (infoIcon.transform.localPosition.z > 0) {
				Vector3 pos = infoIcon.transform.localPosition;
				pos.z *= -1;
				infoIcon.transform.localPosition = pos;
			}
			break;
		}
	}
	private void jump(){
		if (!jumping) {
			jumping = true;
			rb.velocity = new Vector2 (rb.velocity.x, forceJump);
		}
		jumping = true;
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
		if (currentW.getBullets() > 0) {
			// Animación correcta
			/*if (!toRight) animator.Play("rightShoot");
			else animator.Play("leftShoot");*/

			// Michael
			animator.Play("rightShoot");
			Debug.Log (getVol ());
			currentW.fire (toRight, puntoFuego,this, getVol());
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

	public float getVol () {
		return this.volumen;
	}

	public void setVol(float vol) {
		this.volumen = vol;
	}

	public void quitLife(float demage){
		
		currentHealth -= demage;


		Posicion = transform;
		if (tipo == "Cazador") {
			AudioSource.PlayClipAtPoint(hurtHunter, Posicion.position, getVol());
		}
		if (tipo == "Mono") {
			AudioSource.PlayClipAtPoint(hurtMonkey, Posicion.position, getVol());
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
		showArrowIndicator (what);
	}

	public void showArrowIndicator(bool show) {
		Debug.Log ("we started this with "+ show);
		//GameObject arrowIndicator = transform.Find ("infoIcons").gameObject.transform.Find("ArrowIndicator").gameObject;
		arrowIndicator.SetActive (show);
		totalTimeArrow = Time.time + timeArrow;
		arrowActive = show;

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

	public int getBullets() {
		Arma currentW = weapons [currentWeapon];
		return currentW.getBullets ();
	}

    public void animDeath() {
        animator.StopPlayback();
        animator.Play("death");
        Posicion = transform;
		AudioSource.PlayClipAtPoint(deathSound, Posicion.position, getVol());
    }

    private IEnumerator morir()
    {
        yield return new WaitForSeconds(2.0f);
        
        equipo.removePlayer(this.gameObject);
        Destroy(this.gameObject);
		death = false;
    }

    public void destroy(){
        death = true;
        animDeath();
        StartCoroutine(morir());
        
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "floor" || other.gameObject.tag == "tree" || other.gameObject.tag == "stone") {
			jumping = false;
		}
	}	


}