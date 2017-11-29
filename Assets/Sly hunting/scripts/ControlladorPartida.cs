using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlladorPartida : MonoBehaviour
{
	Equipo hunters;
	Equipo animals;
	Caja cajas;

	public float timer = 0.0f;
	//public float startTime = 255.0f;
    float startTime = 15.0f; //se cambia a 15 para probar movimientos, inicial 255
    public bool estatTimer;
	public Text txtTimer;
	public int MIN_X, MIN_Y, MAX_X, MAX_Y;
	public  GameObject actual;
	private Camera camera;
	private int numero_jugadores = 3; //son 3 
	private Equipo actualEquipo;

	private float timeBox = 0.0f;
	private float startTimeBox = 5.0f;


	void Start ()
	{
		MIN_X = 56;
		MIN_Y = 18;
		MAX_X = 660;
		MAX_Y = 131;
		camera = Camera.main;
		actualEquipo = hunters;
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
	}

	// Update is called once per frame
	void Update ()
	{
		if (!actual || actual.GetComponent<Jugador>().getEstamina() < 0) {
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

        camera.transform.position = new Vector3(actual.transform.position.x - 10.0f, actual.transform.position.y + 14.0f, -10);
        /*if (actual.transform.position.x > MIN_X){
			if (actual.transform.position.y > MIN_Y){
				if (actual.transform.position.x < MAX_X) {
					if (actual.transform.position.y < MAX_Y){
						camera.transform.position = new Vector3(actual.transform.position.x, actual.transform.position.y, -10);
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
			newPos.y += 5;
			while (!checkIfPosEmpty (newPos)) {
				newPos.y += 5;
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