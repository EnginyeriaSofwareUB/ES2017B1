using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Arma : MonoBehaviour {

	public int bullets;
	private GameObject bulletPrefab;

	public Arma(int bullets)  {
		this.bullets = bullets;
		bulletPrefab =(GameObject)Resources.Load("Bala", typeof(GameObject));
	}

	public GameObject getPrefab () {
		return bulletPrefab;
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

