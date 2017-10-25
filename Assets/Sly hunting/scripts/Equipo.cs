using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipo {
	List<Jugador> players = new List<Jugador>();

	public int puntos;
	private static System.Random rnd = new System.Random ();
	private Jugador actualplayer;

	public Equipo (string type){
		foreach (GameObject player in GameObject.FindObjectsOfType(typeof(GameObject))) {
			if (player.tag == type) {
				player.AddComponent<Jugador>();
				players.Add (player.GetComponent<Jugador>());
			}
		}
	}

	public void pickPlayerToPlay(){
		actualplayer = players[rnd.Next(players.Count)];
		actualplayer.setPlayerControl(true);
	}

	public void dismissPlayer(){
		actualplayer.setPlayerControl(false);
	}
}

