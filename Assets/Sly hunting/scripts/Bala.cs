using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Bala : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	private Jugador jugador;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		Debug.Log (transform.eulerAngles.z);

		foreach (Jugador j in FindObjectsOfType<Jugador> ()) {
			if (j.playerControl) {
				jugador = j;
				break;
			}
		}
		float inclinacion = 0f;
		if (transform.eulerAngles.z <= 90.0f) {
			inclinacion = transform.eulerAngles.z / 91;
		} else if (transform.eulerAngles.z >= 260.0f && transform.eulerAngles.z <= 360.0f) {
			inclinacion = ((transform.eulerAngles.z - 260) / 110) - 1;
		}
		Debug.Log (inclinacion);
		if (jugador.izquierda) 
				rb.AddForce (new Vector2 (-1, inclinacion) * speed, ForceMode2D.Impulse);
			else rb.AddForce (new Vector2 (1, inclinacion) * speed, ForceMode2D.Impulse);
	}

	void Update() {
//		rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collider2D other) {
		Destroy (gameObject);
	}
}