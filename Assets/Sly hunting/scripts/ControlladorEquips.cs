using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlladorEquips : MonoBehaviour
{
	Equipo hunters;
	Equipo animals;

	public float timer = 0.0f;
	public float startTime = 5.0f;
	public bool estatTimer;
	public Text txtTimer;

	private Equipo actualEquipo;

	void Awake ()
	{
		hunters = new Equipo ("Hunter");
		animals = new Equipo ("Animal");
		actualEquipo = hunters;
		actualEquipo.pickPlayerToPlay ();
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
	}

	public void changeTurn(){
		actualEquipo.dismissPlayer ();
		if (actualEquipo.Equals (animals)) {
			actualEquipo = hunters;
		} else {
			actualEquipo = animals;
		}
		actualEquipo.pickPlayerToPlay ();
		timer = startTime;
	}

}

