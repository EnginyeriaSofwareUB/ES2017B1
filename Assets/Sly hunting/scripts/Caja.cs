using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	//float fallSpeed = 8.0f;
	private float valEstamina = 20.0f;
	private float valVida = 20.0f;

	public ControlladorPartida control;

	public AudioClip goodItem;
	public AudioClip badItem;
	Transform posicion;
	private int controlSonido;
	private Animator animator;
	public float volumen;

	public static void create(string type, ControlladorPartida controla, float volAnim) {
		GameObject obj = (GameObject)Resources.Load(type, typeof(GameObject));
		GameObject box = Instantiate(obj);
		Caja caja = obj.GetComponent<Caja>();
		caja.setControl (controla);
		caja.setVolumenAnim (volAnim);
		caja.spawn();
	}
		
	public void Start() {
		controlSonido = PlayerPrefs.GetInt("Sonido");
        animator = GetComponent<Animator>();
    }

	public ControlladorPartida getControl() {
        
        return control;
	}

	public void setControl(ControlladorPartida ctrl) {
		control = ctrl;
	}

	public void setVolumenAnim(float valor) {
		this.volumen = valor;
	}

	void spawn() {
			Vector3 pos = new Vector3 (UnityEngine.Random.Range (0, 702), 160, 0);
			this.transform.position = pos;
			Debug.Log (pos);
	}

    void animx(string name)
    {
        animator.StopPlayback();
        //Debug.Log("dentro de animwinEnergy antes");
        animator.Play(name);
        //Debug.Log("dentro de animwinEnergy despues");
    }

    private IEnumerator timeAnimTurn()
    {
        Debug.Log("dentro de tiempo perdida de turno");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("dentro de tiempo perdida de turno");
        Destroy(this.gameObject);
    }
    private IEnumerator timeAnim()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            posicion = transform;
            Jugador player = other.gameObject.GetComponent<Jugador>();
            
            float rndCajas = UnityEngine.Random.Range(0, 10);
            //Debug.Log (rndCajas);
            if (rndCajas >= 3.0f)
            { // Cajas buenas				
              //Debug.Log ("Cajas Buenas");
                float rndCajasBuenas = UnityEngine.Random.Range(0, 10);
                //Debug.Log (rndCajasBuenas);
				AudioSource.PlayClipAtPoint(goodItem, posicion.position, this.volumen);
 
                if (rndCajasBuenas >= 3.0f)
                {
                    animx("winEnergy");
                    StartCoroutine(timeAnim());
                    if (player.getEstamina() <= 80.0f)
                    {
                        player.addEstamina(valEstamina);
                    }
                }
                else
                {
                    animx("winLife");
                    StartCoroutine(timeAnim());
                    if (player.getVida() <= 80.0f)
                    {
                        player.addVida(valVida);
                    }

                }

            }
            else
            { // Cajas malas

                //Debug.Log ("Cajas Malas");
                float rndCajasMalas = UnityEngine.Random.Range(0, 10);
                //Debug.Log (rndCajasMalas);
                if (rndCajasMalas >= 5.0f)
                {
					AudioSource.PlayClipAtPoint(badItem, posicion.position, this.volumen);
                    animx("loseEnergy");
                    StartCoroutine(timeAnim());
                    if (player.getEstamina() >= 20.0f)
                    {

                        player.subEstamina(valEstamina);
                    }
                }
                else if (rndCajasMalas < 5.0f && rndCajasMalas >= 2.0f)
                {
					AudioSource.PlayClipAtPoint(badItem, posicion.position, this.volumen);
                    Debug.Log("cambiar turno");
                    animx("loseTurn");
                    StartCoroutine(timeAnimTurn());
                    this.control.changeTurn();
                }
                else
                {
                    animx("loseLife");
                    StartCoroutine(timeAnim());
                    player.quitLife(valVida);
                }

            }

			this.control.subNumCajas ();
        }
    }
}
