using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Equipo   {
	List<GameObject> players = new List<GameObject>();

	public int puntos;
	private static System.Random rnd = new System.Random ();
	private Jugador actualplayer;
	private String typ;
	private List<Arma> weapons; 
	private readonly int inventory = 6;
	ControlladorPartida controlPartida;

	public Equipo (string type,int number,ControlladorPartida controlPartida){
		for (int i = 0; i < number; i++) { // aqui se cambió hasta < porque coge 1 jugador más
			players.Add (Jugador.create(type,this));
			typ = type;
		}
		weapons = new List<Arma> ();
		weapons.Add (new Arma (100000));//arma inicial balas "infinitas"
		this.controlPartida = controlPartida;
		controlPartida.setRandomPositions (players);
	}

	public Jugador pickPlayerToPlay(){
		GameObject playerGo = players[rnd.Next(players.Count)];
		actualplayer = playerGo.GetComponent<Jugador> ();
		actualplayer.setPlayerControl(true);
		actualplayer.setWeapons (weapons);
		return actualplayer;
	}
		
	public void dismissPlayer(){
		if (actualplayer) {
			actualplayer.setPlayerControl (false);
		}
	}

	public void addWeapon(Arma weapon) {
		if (weapons.Count < 6) {
			weapons.Add (weapon);			
		}
	}

	//********* GETTERS & SETTERS **************/
	public void removePlayer(GameObject player){
		if (player == actualplayer) {
			actualplayer = null;
		}
		players.Remove (player);
		if (players.Count == 0) {
			controlPartida.finish (this);
		}
	}

	public int getSizeEquipo(){
		return  players.Count;
	}
	public string getTyo(){
		return typ;
	}

}

