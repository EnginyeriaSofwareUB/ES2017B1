using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSegura : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Jugador player = other.gameObject.GetComponent<Jugador> ();
			player.destroy ();
		}
	}

}
