
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorBotons : MonoBehaviour {
	
	GameObject obj;
	GameObject buttonMusicaOff;
	GameObject buttonMusicaOn;
	GameObject buttonSonidoOff;
	GameObject buttonSonidoOn;
	GameObject buttonPantallaOff;
	GameObject buttonPantallaOn;
	bool sonido = true;
	bool pantalla = true;

	void Awake(){
		obj = GameObject.FindGameObjectWithTag("Music");
		buttonMusicaOff = GameObject.Find ("ButtonMusicaOff");
		buttonMusicaOn = GameObject.Find ("ButtonMusicaOn");
		buttonSonidoOff = GameObject.Find ("ButtonSonidoFXOff");
		buttonSonidoOn = GameObject.Find ("ButtonSonidoFXOn");
		buttonPantallaOff = GameObject.Find("ButtonPantallaCompletaOff");
		buttonPantallaOn = GameObject.Find("ButtonPantallaCompletaOn");
		buttonSonidoOff.SetActive (false);
		buttonMusicaOff.SetActive (false);
		buttonPantallaOff.SetActive (false);
		PlayerPrefs.SetInt("Sonido", 1);
	}

	public void MenuScene() {
		SceneManager.LoadScene ("menu");
	}

	public void FullScreen() {
		Screen.fullScreen = !Screen.fullScreen;
		if (pantalla)
		{
			buttonPantallaOff.SetActive(true);
			buttonPantallaOn.SetActive(false);
			pantalla = false;
		}
		else
		{
			buttonPantallaOff.SetActive(false);
			buttonPantallaOn.SetActive(true);
			pantalla = true;
		}
	}
		
	public void ControlAudio() {
		if (obj.GetComponent<AudioSource> ().isPlaying) {
			obj.GetComponent<AudioSource> ().Stop ();
			buttonMusicaOff.SetActive (true);
			buttonMusicaOn.SetActive (false);
		} else {
			obj.GetComponent<AudioSource> ().Play ();
			obj.GetComponent<AudioSource> ().volume = 0.1f;
			buttonMusicaOn.SetActive (true);
			buttonMusicaOff.SetActive (false);
		}
	}

	public void ControlSonidoFX() {
		if (sonido) {
			buttonSonidoOn.SetActive (true);
			buttonSonidoOff.SetActive (false);
			PlayerPrefs.SetInt("Sonido", 1);
			sonido = false;
		} else {
			buttonSonidoOn.SetActive (false);
			buttonSonidoOff.SetActive (true);
			PlayerPrefs.SetInt("Sonido", 0);
			sonido = true;
		}
	}
}
