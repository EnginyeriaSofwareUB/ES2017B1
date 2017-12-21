using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Bala : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	private Jugador jugador;
	private float damage;
	private float gastoEstamina;
    



    //xC
    public AudioClip shootSound;
    private Transform Posicion;

	// Modificación SonidoFX
	public float vol;

	public static void create (Transform puntoFuego,float damage, Jugador actual, float volumen, float estaminaArma){
		GameObject bulletPrefab =(GameObject)Resources.Load("Bala", typeof(GameObject));
		GameObject b = Instantiate (bulletPrefab, puntoFuego.position, puntoFuego.transform.rotation);
		Bala bala = b.GetComponent<Bala>();
		Debug.Log ("Bala------------------");
		Debug.Log (volumen);
		bala.setVolAnim (volumen);
		bala.jugador = actual;
		bala.damage = damage;
		bala.gastoEstamina = estaminaArma;
        
    }

	void Start() {
		jugador.GetComponent<Jugador>().subEstamina(gastoEstamina);
        rb = GetComponent<Rigidbody2D> ();
		float inclinacion = 0f;
        
        
        if (transform.eulerAngles.z <= 90.0f) {
			inclinacion = transform.eulerAngles.z / 91;
		} else if (transform.eulerAngles.z >= 260.0f && transform.eulerAngles.z <= 360.0f) {
			inclinacion = ((transform.eulerAngles.z - 260) / 110) - 1;
		}
		Debug.Log (inclinacion);
		if (!jugador.toRight) 
				rb.AddForce (new Vector2 (-1, inclinacion) * speed, ForceMode2D.Impulse);
			else rb.AddForce (new Vector2 (1, inclinacion) * speed, ForceMode2D.Impulse);
        //xC
        Posicion = transform;

		// Modificación SonidoFX
		AudioSource.PlayClipAtPoint(shootSound, Posicion.position, this.vol);
    }

	void Update() {
//		rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	/*void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player") {
			Jugador jugadoratacado = other.gameObject.GetComponent<Jugador> ();
			jugadoratacado.quitLife (damage);
		} else if (other.gameObject.tag == "floor" || other.gameObject.tag == "stone" || other.gameObject.tag == "tree") {
            Debug.Log("Obj" + other.gameObject);
            Terreno destruir = other.gameObject.GetComponent<Terreno>();
            destruir.decLife();
           // Destroy(other.gameObject);
              
		}
		Destroy (this.gameObject);
	}*/

	public void setVolAnim(float valor) {
		this.vol = valor;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Jugador jugadoratacado = other.gameObject.GetComponent<Jugador>();
            jugadoratacado.quitLife(damage);
        }
        else if (other.gameObject.tag == "floor")
        {
            Suelo ground = other.gameObject.GetComponent<Suelo>();
            Debug.Log("daño de bala:" + damage);
            ground.destroy(damage);
            

        }
        else if (other.gameObject.tag == "stone" || other.gameObject.tag == "tree" || other.gameObject.tag == "bridge")
        {
            Destroy(other.gameObject);

        }

        Destroy(this.gameObject);
    }


}