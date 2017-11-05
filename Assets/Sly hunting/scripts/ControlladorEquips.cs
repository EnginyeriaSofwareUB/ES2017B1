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
    private GameObject hunter;
    private GameObject animal;
    private GameObject actual;
    private Camera camera;

    private Equipo actualEquipo;
    void Start ()
    {
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        animal = GameObject.FindGameObjectWithTag("Animal");
        camera = Camera.main;
        actualEquipo = hunters;
        actual = hunter;
        actualEquipo.pickPlayerToPlay();
    }
    void Awake ()
	{
        
        hunters = new Equipo ("Hunter");
		animals = new Equipo ("Animal");
		
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
		if (actualEquipo.Equals (animals)) {
			actualEquipo = hunters;
            actual = hunter;

        }else{
			actualEquipo = animals;
            actual = animal;
        }
		actualEquipo.pickPlayerToPlay ();
		timer = startTime;
	}

}

