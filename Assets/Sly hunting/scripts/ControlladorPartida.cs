using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlladorPartida : MonoBehaviour
{
	Equipo hunters;
	Equipo animals;
	public float timer = 0.0f;
	public float startTime = 255.0f;
	public bool estatTimer;
	public Text txtTimer;
	private GameObject hunter;
	private GameObject animal;
	private GameObject actual;
	private Camera camera;
	public int numero_jugadores = 3;
	private Equipo actualEquipo;

	void Start ()
	{
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
		estatTimer = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}

		if (estatTimer) {
			timer -= Time.deltaTime;
			txtTimer.text = "Time left: " + Mathf.Round (timer) + " s";

			if (timer <= 0) {
				changeTurn ();
			}
		}
			camera.transform.position = new Vector3(actual.transform.position.x, 0, -10);
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

	public bool checkIfPosEmpty(Vector3 targetPos) {
		GameObject[] allTerrain = GameObject.FindGameObjectsWithTag ("floor");
		foreach (GameObject current in allTerrain) {
			if (current.transform.position == targetPos)
				return false;
		}
		return true;
	}



}