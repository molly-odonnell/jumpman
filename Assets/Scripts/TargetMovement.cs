using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour {


	public float speed =  .05f;
	private bool move = true;
	private float interval = 1f;
	// Use this for initialization
	void Start () {
		
		InvokeRepeating("ToggleMove", 0.0f , interval);
	}
	
	// Update is called once per frame
	void Update () {

		if (move) {
			transform.Translate (Vector2.down * speed);
		}

	}

	void ToggleMove() {
		move = !move;
		interval = Random.Range (2.0f, 6.0f);
	}

	void OnBecameInvisible () {
		Destroy(gameObject);
	}
}
