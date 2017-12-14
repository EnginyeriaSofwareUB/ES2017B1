using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Suelo : MonoBehaviour {
    public AudioClip destroyWall;
    private Animator animator;
    private Transform Posicion;


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

	
    public void animDestroy() {
        animator.StopPlayback();
        animator.Play("destroy");
        Posicion = transform;
        AudioSource.PlayClipAtPoint(destroyWall, Posicion.position, 1.0f);
        

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