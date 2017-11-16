using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorOpcions : MonoBehaviour {

	public AudioSource audio;

	Toggle t = null;
	bool estat;
	int i;
	static bool created = false;

	void Start() {
		if (!created) {
			DontDestroyOnLoad (audio);
			created = true;
		}else{
			Destroy(audio);
		}
			
		i = PlayerPrefs.GetInt ("isOn");
		if(audio == null)
			audio = gameObject.AddComponent<AudioSource>();
		if (i == 1) {
			estat = true;
			t = GameObject.Find ("Toggle").GetComponent<Toggle>();
			t.isOn = false;
		}


	}

	public void ToggleChanged() {
		if (audio.isPlaying) {
			audio.Stop ();

		} else {
			audio.Play ();	
		}



	}

	public void MenuScene() {
		PlayerPrefs.SetInt ("isOn",1);
		SceneManager.LoadScene ("menu");



	}
		
}

