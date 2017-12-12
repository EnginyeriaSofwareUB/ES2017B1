using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour {
	
	public void MapaScene(){
		SceneManager.LoadScene ("scenary");

	}

	public void OptionsScene(){
		SceneManager.LoadScene ("options");
	}

    public void MenuScene()
    {
        SceneManager.LoadScene("menu");
    }

    public void QuitGame()
    {
		Application.Quit();
        Debug.Log("dentro de salir");
	}


}
