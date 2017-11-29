using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	//float fallSpeed = 8.0f;
	private float valEstamina = 10.0f;
	private float valVida = 10.0f;

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
			float rnd = UnityEngine.Random.value;
			Debug.Log (rnd);
			if (rnd >= 0.5) {
				player.addVida(valVida);
				//Debug.Log (player.getVida ());
			} else if (rnd < 0.5 && rnd >= 0.2) {
				//Debug.Log (player.getEstamina ());
				player.addEstamina(valEstamina);
			} else {
				this.control.changeTurn();	
			}
			Destroy (gameObject);
		}
	}

}
