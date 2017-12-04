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
	// ... aqui

	Equipo hunters;
	Equipo animals;
	Caja cajas;

	public float timer = 0.0f;
	//public float startTime = 255.0f;
	float startTime = 15.0f; //se cambia a 15 para probar movimientos, inicial 255
	public bool estatTimer;
	public Text txtTimer;


	public  GameObject actual;
	private Camera camera;
	private int numero_jugadores = 3; //son 3 
	private Equipo actualEquipo;

	private float timeBox = 0.0f;
	private float startTimeBox = 10.0f;


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
		timer = startTime;
		timeBox = startTimeBox;

		estatTimer = true;

		// Rango de limites de la camara
		minCameraPos.x = 87.0f;
		maxCameraPos.x = 636.0f;
		minCameraPos.y = 53.5f;
		maxCameraPos.y = 140.0f;
		minCameraPos.z = -10.0f;
		maxCameraPos.z = -10.0f;

	}

	// Update is called once per frame
	void Update ()
	{
		if (!actual || actual.GetComponent<Jugador>().getEstamina() <= 0) {
			changeTurn ();
		}

		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}

		if (estatTimer) {
			timer -= Time.deltaTime;
			timeBox -= Time.deltaTime;
			txtTimer.text = "Time left: " + Mathf.Round (timer) + " s";

			if (timer <= 0) {
				changeTurn ();
			}

			if (timeBox <= 0) {
				spawnBoxes ();
			}
		}

		float posX = Mathf.SmoothDamp (camera.transform.position.x, actual.transform.position.x, ref velocity.x, smoothTimeX);
		float posy = Mathf.SmoothDamp (camera.transform.position.y, actual.transform.position.y, ref velocity.y, smoothTimeY);

		camera.transform.position = new Vector3 (posX, posy, camera.transform.position.z);
		if (bounds) {
			camera.transform.position = new Vector3 (Mathf.Clamp (camera.transform.position.x, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp (camera.transform.position.y, minCameraPos.y, maxCameraPos.y),
				Mathf.Clamp (camera.transform.position.z, minCameraPos.z, maxCameraPos.z));
		}


		//camera.transform.position = new Vector3(actual.transform.position.x + 6, actual.transform.position.y + 6, -10);

		/*if (actual.transform.position.x > MIN_X){
			if (actual.transform.position.y > MIN_Y){
				if (actual.transform.position.x < MAX_X) {
					if (actual.transform.position.y < MAX_Y){
						
					}
				}
			}
		}*/
	}

	public void changeTurn(){
		actualEquipo.dismissPlayer ();
		if (actualEquipo.getTyo() == "Mono") {
			actualEquipo = hunters;
		}else{
			actualEquipo = animals;
		}
		actual = actualEquipo.pickPlayerToPlay ().gameObject;
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
		Caja.create ("Caja", this);			
		timeBox = startTimeBox;
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
		Application.Quit ();
	}
}