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
	bool sonido = false;

	void Awake(){
		obj = GameObject.FindGameObjectWithTag("Music");
		buttonMusicaOff = GameObject.Find ("ButtonMusicaOff");
		buttonMusicaOn = GameObject.Find ("ButtonMusicaOn");
		buttonSonidoOff = GameObject.Find ("ButtonSonidoFXOff");
		buttonSonidoOn = GameObject.Find ("ButtonSonidoFXOn");
		buttonSonidoOn.SetActive (false);
		buttonMusicaOn.SetActive (false);
	}

	public void MenuScene() {
		SceneManager.LoadScene ("menu");
	}

	public void FullScreen() {
		Screen.fullScreen = !Screen.fullScreen;
	}
		
	public void ControlAudio() {
		if (obj.GetComponent<AudioSource> ().isPlaying) {
			obj.GetComponent<AudioSource> ().Stop ();
			buttonMusicaOff.SetActive (true);
			buttonMusicaOn.SetActive (false);
		} else {
			obj.GetComponent<AudioSource> ().Play ();
			buttonMusicaOn.SetActive (true);
			buttonMusicaOff.SetActive (false);
		}
	}

	public void ControlSonidoOff() {
		buttonSonidoOff.SetActive (false);
		buttonSonidoOn.SetActive (true);
	}

	public void ControlSonidoOn() {
		buttonSonidoOff.SetActive (true);
		buttonSonidoOn.SetActive (false);
	}
}
