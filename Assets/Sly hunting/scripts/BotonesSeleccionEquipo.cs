using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesSeleccionEquipo : MonoBehaviour {

	GameObject obj;
	GameObject slider;
	Text txt;

	void Awake() {
		slider = GameObject.Find ("numPlayers");
		txt = GameObject.Find ("txtNumPlayers").GetComponent<Text> ();
		txt.text = "3 Jugadores";
		slider.GetComponent<Slider> ().minValue = 5;
		slider.GetComponent<Slider> ().maxValue = 15;

		if (GameObject.FindGameObjectWithTag ("controlOptions") != null) {
			obj = GameObject.FindGameObjectWithTag ("controlOptions");
			Destroy (obj);
		} else {
			PlayerPrefs.SetInt ("Musica", 1);
			PlayerPrefs.SetInt("Sonido", 1);
		}

	}

	public void MapaScene(){
		SceneManager.LoadScene ("scenary");

	}


	public void ChangePlayer() {
		txt.text = ((int)slider.GetComponent<Slider> ().value).ToString ();
		txt.text += " Jugadores";
		PlayerPrefs.SetInt ("nPlayers", (int)slider.GetComponent<Slider> ().value);
	}
}
