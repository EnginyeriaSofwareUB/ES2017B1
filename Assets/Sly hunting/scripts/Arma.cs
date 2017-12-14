using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Arma  {

	public int bullets;

	float nextFire = 0f;
	float fireRate = 0.5f;
	float damage = 20.0f; 
	String type;

	public Arma(int bullets,String type="regular")  {
		this.bullets = bullets;
		this.type = type;
		if (type == "Bazoca") {
			damage *= 2f; //incrementem un 50% si es Bazoca
		}
	}

	public String getTipus(){
		return type;
	}

	public void fire(Boolean toRight,Transform puntoFuego,Jugador actual){
		if (Time.time > nextFire && getBullets() > 0) {
			nextFire = Time.time + fireRate;
			if (actual.isDestructor ()) {
				Bala.create (puntoFuego, damage*1.5f, actual);//incrementem un 50% si dispara un destructor
			} else {
				Bala.create (puntoFuego, damage, actual);
			}
			decreaseBullet ();
		}
	}

	public void decreaseBullet() {
		bullets -= 1;
	}

	public void addBullets(int number) {
		bullets += number;
	}

	public int getBullets() {
		return bullets;
	}
		
}

