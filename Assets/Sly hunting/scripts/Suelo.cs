using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Suelo : MonoBehaviour {
    public AudioClip destroyWall1;
    public AudioClip destroyWall2;
    private Animator animator;
    private Transform Posicion;
	public float volumen = 1.0f;
    private float lifeGround;


    private void Start()
    {
		
		
		animator = GetComponent<Animator>();
        Posicion = transform;
        lifeGround = 40.0f;

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
        if (lifeGround == 0.0)
        {
            animator.Play("destroy");
            Posicion = transform;
            AudioSource.PlayClipAtPoint(destroyWall2, Posicion.position, getVol());

        }
        else {
            animator.Play("destroy1");
            Posicion = transform;
            AudioSource.PlayClipAtPoint(destroyWall1, Posicion.position, getVol());

        }
    }


    private IEnumerator timeDestroy1()
    {
        yield return new WaitForSeconds(1.0f);       
    }

    private IEnumerator timeDestroy2()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    public void destroy(float damage){

        this.lifeGround -= damage;
        animDestroy();
        if (this.lifeGround == 0.0) StartCoroutine(timeDestroy2());
        else StartCoroutine(timeDestroy1());


    }

		


}