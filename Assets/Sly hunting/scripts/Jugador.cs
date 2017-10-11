using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {
    public float Velocidad;
    public Rigidbody rb;
    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        float MoverHorizontal = Input.GetAxis("Horizontal");
        float MoverVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(MoverHorizontal,0.0f,MoverVertical);
        
        rb.AddForce(movimiento * Velocidad * Time.deltaTime);
        
    }
}
