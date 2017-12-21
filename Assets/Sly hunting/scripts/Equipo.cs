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
	private int order;
	private readonly List<String> roles = new List<String>{"agile","destructor"};

	public Equipo (string type,int number,ControlladorPartida controlPartida){
		int roleSize = roles.Count;
		for (int i = 0; i < number; i++) { // aqui se cambió hasta < porque coge 1 jugador más
			String rol = roles[i%roleSize];
			players.Add (Jugador.create(type,this,rol));
			typ = type;
		}

		order = 0;
		weapons = new List<Arma> ();
		weapons.Add (new Arma (100000));//arma inicial balas "infinitas"
		weapons.Add (new Arma(3,"Bazoca"));
		this.controlPartida = controlPartida;
		controlPartida.setRandomPositions (players);
	}

	public Jugador pickPlayerToPlay(){
		if (order >= players.Count) {
			order = 0;
		}else if (players.Count == 0){
			controlPartida.finish (this);
			return null;
		}
		GameObject playerGo = players[order];
		actualplayer = playerGo.GetComponent<Jugador> ();
		actualplayer.setPlayerControl(true);
		actualplayer.setWeapons (weapons);
		order++;
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

	public Jugador getActualPlayer() {
		return actualplayer;
	}
}

