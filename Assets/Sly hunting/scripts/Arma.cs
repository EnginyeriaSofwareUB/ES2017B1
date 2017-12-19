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


	public Arma(int bullets)  {
		this.bullets = bullets;
	}

	public void fire(Boolean toRight,Transform puntoFuego,Jugador actual, float volumen){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Debug.Log ("Arma------------------");
			Debug.Log (volumen);
			Bala.create(puntoFuego, damage, actual, volumen);
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

