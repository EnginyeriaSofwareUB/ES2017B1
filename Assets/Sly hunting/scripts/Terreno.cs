using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terreno : MonoBehaviour {
    public int life;

	// Use this for initialization
	void Start () {
        life = 3;
		
	}
	public void decLife()
    {
        life--;
    }
	// Update is called once per frame
	void Update () {
        if(life == 0)
        {
            Destroy(this.gameObject);
        }
		
	}
}
