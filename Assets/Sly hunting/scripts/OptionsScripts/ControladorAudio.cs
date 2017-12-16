using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorAudio : MonoBehaviour {
	
	GameObject[] objs;

	void Awake(){
		objs = GameObject.FindGameObjectsWithTag ("Music");
		this.gameObject.GetComponent<AudioSource> ().volume = 0.1f;
		if (objs.Length > 1)
			Destroy (this.gameObject);
		DontDestroyOnLoad (this.gameObject);
	}
}

