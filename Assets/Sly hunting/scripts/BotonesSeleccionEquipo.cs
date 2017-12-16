using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesSeleccionEquipo : MonoBehaviour {

	GameObject obj;

	void Awake() {
		obj = GameObject.FindGameObjectWithTag ("controlOptions");
		if (obj != null) {
			Destroy (obj);
		} else {
			PlayerPrefs.SetInt ("Musica", 1);
			PlayerPrefs.SetInt("Sonido", 1);
		}

	}

	public void MapaScene(){
		SceneManager.LoadScene ("scenary");

	}
	//public void ChangePlayer1(){
		//GameObject.Find("PlayerJ1").GetComponent<Button>().image.overrideSprite = "completeMonkey.psd";




	//}
}
