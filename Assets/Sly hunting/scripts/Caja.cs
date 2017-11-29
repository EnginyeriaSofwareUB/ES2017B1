using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	//float fallSpeed = 8.0f;
	private float valEstamina = 10.0f;
	private float valVida = 10.0f;
	private GameObject box;
	private string nPrefab;

	private ControlladorPartida control;

	public Caja(string nom, ControlladorPartida controla) {
		nPrefab = nom;
		this.control = controla;
		Debug.Log (control);
		this.spawn ();
	}

	void spawn() {
		GameObject obj = (GameObject)Resources.Load (nPrefab, typeof(GameObject));
		GameObject box = Instantiate (obj);
		Vector3 pos = new Vector3(UnityEngine.Random.Range(0,702),160,0);
		box.transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log (this.control);
		if (other.gameObject.tag == "Player") {
			Jugador player = other.gameObject.GetComponent<Jugador> ();
			float rnd = UnityEngine.Random.value;
			Debug.Log (rnd);
			if (rnd >= 0.5) {
				//Debug.Log (player.getVida ());
				player.addVida(valVida);
				//Debug.Log (player.getVida ());
			} else if (rnd < 0.5 && rnd >= 0.2) {
				//Debug.Log (player.getEstamina ());
				player.addEstamina(valEstamina);
				//Debug.Log (player.getEstamina ());
			} else {
				//this.control.changeTurn ();
			}
			Destroy (gameObject);
		}
	}

}
