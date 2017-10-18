using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {

    public Rigidbody rb;
	public float speed = 1.0f;
	public float FPS = 60f;
	private SpriteRenderer mySpriteRenderer;
	private bool toRight = true;
    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {
		Vector3 pos = transform.position;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (toRight) {
				mySpriteRenderer.flipX = true;

				toRight = false;
			}
			pos.x -=  speed * Time.deltaTime * FPS;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			if (!toRight) {
				mySpriteRenderer.flipX = false;
				toRight = true;
			}
			pos.x +=  speed * Time.deltaTime * FPS;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			pos.y += speed * Time.deltaTime * FPS;
		}
		transform.position = pos;
    }
}
