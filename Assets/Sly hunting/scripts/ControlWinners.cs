using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlWinners : MonoBehaviour {

	Image monoWin;
	Image cazaWin;
	Button buttonMenu;
	Button buttonQuit;
	string winner;

	void Awake() {
		monoWin = GameObject.Find ("MonoWinner").GetComponent<Image> ();
		cazaWin = GameObject.Find ("CazaWinner").GetComponent<Image> ();
		buttonMenu = GameObject.Find ("ButtonPrincipal").GetComponent<Button> ();
		buttonQuit= GameObject.Find ("ButtonQuit").GetComponent<Button> ();
		winner = PlayerPrefs.GetString ("Winner");
		Debug.Log (winner);
		monoWin.enabled = true;
		cazaWin.enabled = true;
		imageWinner ();
	}

	public void imageWinner() {
		Debug.Log (winner);
		if (winner == "Mono") {
			Debug.Log ("entro mono");
			cazaWin.enabled = !cazaWin.enabled;

		} else if (winner == "Cazador"){
			Debug.Log ("entro caza");
			monoWin.enabled = !monoWin.enabled;
		}
	}

	public void sceneMenu() {
		SceneManager.LoadScene ("menu");
	}

	public void sceneQuit() {
		Application.Quit ();
	}
}