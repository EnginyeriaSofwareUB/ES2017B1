using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Equipo : MonoBehaviour  {
	List<GameObject> players = new List<GameObject>();

	public int puntos;
	private static System.Random rnd = new System.Random ();
	private Jugador actualplayer;

	public Equipo (string type,int number,ControlladorPartida controPartida){
		for (int i = 0; i <= number; i++) {
			GameObject player = (GameObject)Resources.Load(type, typeof(GameObject));
			GameObject pl = Instantiate (player);
			players.Add (pl);
		}
		controPartida.setRandomPositions (players);
	}

	public Jugador pickPlayerToPlay(){
		GameObject playerGo = players[rnd.Next(players.Count)];
		actualplayer = playerGo.GetComponent<Jugador> ();
		actualplayer.setPlayerControl(true);
		return actualplayer;
	}

	public void dismissPlayer(){
		actualplayer.setPlayerControl(false);
	}


}

