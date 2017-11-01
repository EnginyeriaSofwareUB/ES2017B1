using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {
   
    private GameObject actual;

	// Use this for initialization
	// Update is called once per frame
	void Update () {
        actual = GameObject.FindGameObjectWithTag("Hunter");


        transform.position = new Vector3(actual.transform.position.x, 0, 0);
        
		
	}
}
