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
	private String typ;
	private List<Arma> weapons; 
	private readonly int inventory = 6;

	public Equipo (string type,int number,ControlladorPartida controPartida){
		for (int i = 0; i < number; i++) { // aqui se cambió hasta < porque coge 1 jugador más
			GameObject player = (GameObject)Resources.Load(type, typeof(GameObject));
			GameObject pl = Instantiate (player);
			players.Add (pl);
			typ = type;
		}
		weapons = new List<Arma> ();
		weapons.Add (new Arma (100000));//arma inicial balas "infinitas"
		controPartida.setRandomPositions (players);
	}

	public Jugador pickPlayerToPlay(){
		GameObject playerGo = players[rnd.Next(players.Count)];
		actualplayer = playerGo.GetComponent<Jugador> ();
		actualplayer.setPlayerControl(true);
		actualplayer.setWeapons (weapons);
		return actualplayer;
	}
		
	public void dismissPlayer(){
		actualplayer.setPlayerControl(false);
	}

	public void addWeapon(List<Arma> playerWeapons, Arma weapon) {
		if (playerWeapons.Count < 6) {
			playerWeapons.Add (weapon);			
		}//else full inventory
		updateWeapons(playerWeapons);
	}

	//********* GETTERS & SETTERS **************/

	public void updateWeapons(List<Arma> weapons) {
		this.weapons = weapons;
	}

	public string getTyo(){
		return typ;
	}

	public Jugador getActualPlayer() {
		return actualplayer;
	}
}

