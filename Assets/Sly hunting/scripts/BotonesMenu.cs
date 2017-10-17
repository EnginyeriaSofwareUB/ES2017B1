using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour {
    public bool Jugar = false;
    public bool Opciones = false;
    public bool Salir = false;
    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        
		
	}

    // Update is called once per frame
    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        
    }
    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;

    }
    public void OnMouseUpAsButton()
    {
        if (Jugar)
            SceneManager.LoadScene(1);
        if (Opciones)
            SceneManager.LoadScene(2);
        if (Salir)
            Application.Quit();
    }
}
