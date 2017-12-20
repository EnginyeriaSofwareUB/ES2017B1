using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ControlladorPartida : MonoBehaviour
{
	// Añadido hasta ...
	private Vector2 velocity;
	private float smoothTimeY = 0.2f;
	private float smoothTimeX = 0.2f;
	private bool bounds = true;
	private Vector3 minCameraPos;
	private Vector3 maxCameraPos;
	private float size = 60.0f;
    //pantalla pausa
    private bool pausado;
    private GameObject pauseScreen;
    private GameObject buttonContinue;
	private GameObject buttonQuit;
	private GameObject sliderMusica;
	private GameObject sliderFX;
    //private GameObject textFX;
    //private GameObject textMusica;
    private GameObject imgVolumen;
    private GameObject imgFx;
    private GameObject imgMas;
    private GameObject imgMas1;
    private GameObject imgMenos;
    private GameObject imgMenos1;

    private GameObject obj;
	private int music; 
	private int so;
	public float valorSlider;
    // ... aqui

    Equipo hunters;
	Equipo animals;
	Caja cajas;

	public float timer = 0.0f;
	//public float startTime = 255.0f;
	float startTime = 25.0f; //se cambia a 15 para probar movimientos, inicial 255
	public bool estatTimer;
	public Text txtTimer;
	public Text txtBullets;

	public  GameObject actual;
	private Camera camera;
	private int numero_jugadores = 3; //son 3 
	private Equipo actualEquipo;

	// Tiempo caida caja
	private float timeBox = 0.0f;
	private float startTimeBox = 25.0f;
	private int numCajas = 0;
	private int maxCajas = 20;


	void Start ()
	{
		camera = Camera.main;
		// Añadido 
		camera.orthographicSize = size;

		actualEquipo = (UnityEngine.Random.value > 0.5f) ? hunters : animals;
		actual = actualEquipo.pickPlayerToPlay().gameObject;

    }

	void Awake ()
	{

		hunters = new Equipo ("Cazador",numero_jugadores,this);
		animals = new Equipo ("Mono",numero_jugadores,this);

		txtTimer =  GameObject.Find("textTimer").GetComponent<Text>();
		txtBullets =  GameObject.Find("textBullets").GetComponent<Text>();
		timer = startTime;
		timeBox = startTimeBox;

		estatTimer = true;

		// Rango de limites de la camara
		minCameraPos.x = 106.0f;
		maxCameraPos.x = 615.0f;
		minCameraPos.y = 53.5f;
		maxCameraPos.y = 140.0f;
		minCameraPos.z = -10.0f;
		maxCameraPos.z = -10.0f;

		// Menu pausa
		obj = GameObject.Find("AudioController");
		pauseScreen = GameObject.Find("Image");
		buttonContinue = GameObject.Find("ButtonContinue");
		buttonQuit = GameObject.Find ("ButtonQuit");
		sliderMusica = GameObject.Find ("VolumenMusica");
		sliderFX = GameObject.Find ("VolumenFX");
        imgVolumen = GameObject.Find("imgVolumen");
        imgMas = GameObject.Find("imgMas");
        imgMas1 = GameObject.Find("imgMas1");
        imgMenos = GameObject.Find("imgMenos");
        imgMenos1 = GameObject.Find("imgMenos1");

        imgVolumen = GameObject.Find("imgVolumen");
        imgFx = GameObject.Find("imgFx");
        pauseScreen.SetActive(false);
		buttonContinue.SetActive(false);
		buttonQuit.SetActive(false);
		sliderMusica.SetActive(false);
		sliderFX.SetActive(false);
        imgVolumen.SetActive(false);
        imgFx.SetActive(false);
        imgMenos.SetActive(false);
        imgMenos1.SetActive(false);
        imgMas.SetActive(false);
        imgMas1.SetActive(false);


		// SonidoFX
		music = PlayerPrefs.GetInt ("Musica");

		so = PlayerPrefs.GetInt ("Sonido");
		if (music == 0) {
			sliderMusica.GetComponent<Slider> ().value = 0.0f;
		} else {
			sliderMusica.GetComponent<Slider> ().value = 0.1f;
		}
		if (so == 0) {
			sliderFX.GetComponent<Slider> ().value = 0.0f;
			setValorSlider (0.0f);
		} else {
			setValorSlider (1.0f);
			sliderFX.GetComponent<Slider> ().value = 1.0f;
		}
		pausado = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!actual || actual.GetComponent<Jugador>().getEstamina() <= 0) {
			changeTurn ();
		}

		if (Input.GetKey ("escape")) {
            // PauseMenu
			controlPauseMenu();
        }

		if (estatTimer) {
			timer -= Time.deltaTime;
			timeBox -= Time.deltaTime;
			txtTimer.text = "Time left: " + Mathf.Round (timer) + " s";

			if (timer <= 0) {
				changeTurn ();
			}

			if (timeBox <= 0 && this.getNumCajas() < this.maxCajas) {
				spawnBoxes ();
			}
		}

		updateBulletsText ();

		float posX = Mathf.SmoothDamp (camera.transform.position.x, actual.transform.position.x, ref velocity.x, smoothTimeX);
		float posy = Mathf.SmoothDamp (camera.transform.position.y, actual.transform.position.y, ref velocity.y, smoothTimeY);

		camera.transform.position = new Vector3 (posX, posy, camera.transform.position.z);
		if (bounds) {
			camera.transform.position = new Vector3 (Mathf.Clamp (camera.transform.position.x, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp (camera.transform.position.y, minCameraPos.y, maxCameraPos.y),
				Mathf.Clamp (camera.transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}

	private void updateBulletsText() {
		int bullets = actualEquipo.getActualPlayer ().getBullets ();
		if (bullets > 10000)
			txtBullets.text = "Bullets left: unlimited";
		else 
			txtBullets.text = "Bullets left: " + bullets;
	}

	public void changeTurn(){
		actualEquipo.dismissPlayer ();
		if (actualEquipo.getTyo() == "Mono") {
			actualEquipo = hunters;
		}else{
			actualEquipo = animals;
		}
		actual = actualEquipo.pickPlayerToPlay().gameObject;
		timer = startTime;
	}

	public void setRandomPositions (List<GameObject> objects) {
		GameObject[] terrain;
		terrain = GameObject.FindGameObjectsWithTag ("floor");
		objects.ForEach (obj => {
			Vector3 newPos = terrain[UnityEngine.Random.Range(0,terrain.Length)].transform.position;
			newPos.y += 9;
			while (!checkIfPosEmpty (newPos)) {
				newPos.y += 9;
			}
			obj.transform.position = newPos;
		});
	}

	public void spawnBoxes () {
		addNumCajas ();
		Caja.create ("Caja", this, getValorSlider());	
		timeBox = startTimeBox;
	}

	public int getNumCajas() {
		return numCajas;
	}

	public void addNumCajas() {
		Debug.Log ("entroadd");
		numCajas += 1;
	}

	public void subNumCajas() {
		Debug.Log ("entrosub");
		if (numCajas == maxCajas) {
			timeBox = startTimeBox;
		}
		numCajas -= 1;	
	}

	public bool checkIfPosEmpty(Vector3 targetPos) {
		GameObject[] allTerrain = GameObject.FindGameObjectsWithTag ("floor");
		foreach (GameObject current in allTerrain) {
			if (current.transform.position == targetPos)
				return false;
		}
		return true;
	}

	public Equipo getCurrentTeam() {
		return actualEquipo;
	}

	public void finish(Equipo equipoPerdedor){
		//Habría que hacer la resolución del fin del juego

		Debug.Log ("finishh");
		Application.Quit ();
	}

	public void controlPauseMenu() {
		if (!pausado) {
			pausado = true;
			pauseScreen.SetActive(true);
			buttonContinue.SetActive(true);
			buttonQuit.SetActive(true);
			sliderMusica.SetActive(true);
			sliderFX.SetActive(true);
            //textMusica.SetActive(true);
            //textFX.SetActive(true);
            imgVolumen.SetActive(true);
            imgFx.SetActive(true);
            imgMenos.SetActive(true);
            imgMenos1.SetActive(true);
            imgMas.SetActive(true);
            imgMas1.SetActive(true);
            Time.timeScale = 0;

			if (music == 0) {
				obj.GetComponent<AudioSource>().Play ();
				obj.GetComponent<AudioSource>().volume = 0.0f;
			}
		}
	}

	public void continueGame() {
		pausado = false;
		pauseScreen.SetActive(false);
		buttonContinue.SetActive(false);
		buttonQuit.SetActive(false);
		sliderMusica.SetActive(false);
		sliderFX.SetActive(false);
		//textMusica.SetActive(false);
		//textFX.SetActive(false);
        imgVolumen.SetActive(false);
        imgFx.SetActive(false);
        imgMenos.SetActive(false);
        imgMenos1.SetActive(false);
        imgMas.SetActive(false);
        imgMas1.SetActive(false);
        Time.timeScale = 1;
	}

	public void changeMusicaSlider() {
		if (music == 1) {
			obj.GetComponent<AudioSource> ().volume = sliderMusica.GetComponent<Slider> ().value;
		} else {
			obj.GetComponent<AudioSource> ().volume = sliderMusica.GetComponent<Slider> ().value;
		}

	}

	public void searchPlayers() {
		setValorSlider(sliderFX.GetComponent<Slider> ().value);
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.GetComponent <Jugador> ().setVol(sliderFX.GetComponent<Slider> ().value);
		}

		// Si falla a lo mejor es porque esta mal puesto los tags en los suelos
		GameObject[] suelo = GameObject.FindGameObjectsWithTag ("floor");
		foreach (GameObject floor in suelo) {
			floor.GetComponent <Suelo> ().setVol(sliderFX.GetComponent<Slider> ().value);
		}

		GameObject[] caja = GameObject.FindGameObjectsWithTag ("caja");
		foreach (GameObject box in caja) {
			box.GetComponent <Caja> ().setVolumenAnim(sliderFX.GetComponent<Slider> ().value);
		}
	}

	public float getValorSlider() {
		return this.valorSlider;
	}

	public void setValorSlider(float valor) {
		this.valorSlider = valor;
	}
}