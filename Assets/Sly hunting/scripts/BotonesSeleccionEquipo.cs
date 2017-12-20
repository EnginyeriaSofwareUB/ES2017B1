using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesSeleccionEquipo : MonoBehaviour {

	GameObject obj;
	GameObject slider;
	/*Image monoI;
	Image cazadorD;
	Image monoD;
	Image cazadorI;
	Text txt;*/

	void Awake() {
	
		/*txt = GameObject.Find ("txtNumPlayers").GetComponent<Text> ();
		txt.text = "3 Jugadores";

		monoI = GameObject.Find ("MonoI").GetComponent<Image> ();
		cazadorD = GameObject.Find ("CazadorD").GetComponent<Image> ();
		monoD = GameObject.Find ("MonoD").GetComponent<Image> ();
		cazadorI = GameObject.Find ("CazadorI").GetComponent<Image> ();
		monoI.enabled = true;
		monoD.enabled = false;
		cazadorI.enabled = false;
		cazadorD.enabled = true;

		slider = GameObject.Find ("numPlayers");
		slider.GetComponent<Slider> ().minValue = 5;
		slider.GetComponent<Slider> ().maxValue = 15;

		if (GameObject.FindGameObjectWithTag ("controlOptions") != null) {
			obj = GameObject.FindGameObjectWithTag ("controlOptions");
			Destroy (obj);
		} else {
			PlayerPrefs.SetInt ("Musica", 1);
			PlayerPrefs.SetInt("Sonido", 1);
		}*/

	}

	public void MapaScene(){
		SceneManager.LoadScene ("scenary");

	}
	public void Help(){
		SceneManager.LoadScene ("controls");
	}


	public void changeNumPlayers()
	{
		/*txt.text = ((int)slider.GetComponent<Slider>().value).ToString();
		txt.text += " Jugadores";
		PlayerPrefs.SetInt("nPlayers", (int)slider.GetComponent<Slider>().value);*/
	}

	public void changePlayer()
	{
		/*monoI.enabled = !monoI.enabled;
		monoD.enabled = !monoD.enabled;
		cazadorI.enabled = !cazadorI.enabled;
		cazadorD.enabled = !cazadorD.enabled;*/
	}
}
