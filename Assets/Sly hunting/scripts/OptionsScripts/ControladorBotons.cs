using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorBotons : MonoBehaviour {
	
	GameObject obj;

	void Start(){
		obj = GameObject.FindGameObjectWithTag("Music");
	}

	public void MenuScene() {
		SceneManager.LoadScene ("menu");
	}

	public void FullScreen() {
		Screen.fullScreen = !Screen.fullScreen;
	}
		
	public void StopAudio() {
		if (obj.GetComponent<AudioSource> ().isPlaying) {
			obj.GetComponent<AudioSource> ().Stop ();
		} else {
			obj.GetComponent<AudioSource> ().Play ();
		}
	}
}
