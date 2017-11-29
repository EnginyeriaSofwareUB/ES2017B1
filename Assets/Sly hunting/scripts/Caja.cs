using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	//float fallSpeed = 8.0f;
	private float valEstamina = 20.0f;
	private float valVida = 20.0f;

	public ControlladorPartida control;

	public static void create(string type, ControlladorPartida controla) {
		GameObject obj = (GameObject)Resources.Load(type, typeof(GameObject));
		GameObject box = Instantiate(obj);
		Caja caja = obj.GetComponent<Caja>();
		caja.setControl (controla);
		caja.spawn();

	}

	public ControlladorPartida getControl() {
		return control;
	}

	public void setControl(ControlladorPartida ctrl) {
		control = ctrl;
	}

	void spawn() {
		Vector3 pos = new Vector3(UnityEngine.Random.Range(0,702),160,0);
		this.transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Jugador player = other.gameObject.GetComponent<Jugador> ();
			float rndCajas = UnityEngine.Random.Range (0, 10);
			Debug.Log (rndCajas);
			if (rndCajas >= 3.0f) { // Cajas buenas
				Debug.Log ("Cajas Buenas");
				float rndCajasBuenas = UnityEngine.Random.Range (0, 10);
				Debug.Log (rndCajasBuenas);
				if (rndCajasBuenas >= 3.0f) {
					if (player.getEstamina () <= 80.0f)
						player.addEstamina (valEstamina);
				} else {
					if (player.getVida () <= 80.0f)
						player.addVida (valVida);
				}
			} else { // Cajas malas
				Debug.Log ("Cajas Malas");
				float rndCajasMalas = UnityEngine.Random.Range (0, 10);
				Debug.Log (rndCajasMalas);
				if (rndCajasMalas >= 5.0f) {
					if (player.getEstamina () >= 20.0f)
						player.subEstamina (valEstamina);
				} else if (rndCajasMalas < 5.0f && rndCajasMalas >=2.0f)
					this.control.changeTurn ();
				else {
					player.quitLife (valVida);
				}
			}
			Destroy (gameObject);
		}
	}
}
