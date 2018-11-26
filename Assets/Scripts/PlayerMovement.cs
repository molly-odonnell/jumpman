using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Animator animator;
	public float speed =  .1f;

	private Rigidbody2D playerRigidbody2D;
	private int state = 5;

	// Use this for initialization
	void Start () {
		playerRigidbody2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetInteger("state", state);
		if (Input.GetKey ("a")) {
			playerRigidbody2D.MovePosition (playerRigidbody2D.position + Vector2.left * speed);
			//transform.Translate (Vector2.left * speed);
			state = 0;
		} if (Input.GetKey("d")) {
			playerRigidbody2D.MovePosition (playerRigidbody2D.position + Vector2.right * speed);
			state = 1;
		} if (Input.GetKey ("w")) {
			playerRigidbody2D.MovePosition (playerRigidbody2D.position + Vector2.up * speed);
			state = 2;
		} if (Input.GetKey ("s")) {
			playerRigidbody2D.MovePosition (playerRigidbody2D.position + Vector2.down * speed);
			state = 3;
		}

	}
}
