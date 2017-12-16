using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Suelo : MonoBehaviour {
    public AudioClip destroyWall;
    private Animator animator;
    private Transform Posicion;
	public float volumen = 1.0f;


    private void Start()
    {
		
		
		animator = GetComponent<Animator>();
        Posicion = transform;

    }

	void Awake () {
		
	}

	// Update is called once per frame
	void Update () {

		
	}

	public float getVol () {
		return this.volumen;
	}

	public void setVol(float vol) {
		this.volumen = vol;
	}

    public void animDestroy() {
        animator.StopPlayback();
        animator.Play("destroy");
        Posicion = transform;
		AudioSource.PlayClipAtPoint(destroyWall, Posicion.position, getVol());
    }

    private IEnumerator destroyW()
    {
        yield return new WaitForSeconds(2.0f);     
        
        Destroy(this.gameObject);
    }

    public void destroy(){
        
        animDestroy();
        StartCoroutine(destroyW());
        
	}

		


}