using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeRobbed : MonoBehaviour {

	private float maxSize = .3333f;
	private Collider2D targetCol;

	public bool isRobbed = false;

	public Collider2D playerCol;
	public float timeRequired = .01f;

	// Use this for initialization
	void Start () {
		playerCol = GameObject.Find("Player").GetComponent<Collider2D>();
		targetCol = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update () {

		if (!isRobbed) {
			// if colliding with player, increase size of progress bar
			if (targetCol.IsTouching (playerCol)) {
				if (transform.GetChild (1).localScale.x < maxSize) {
					transform.GetChild (1).localScale += new Vector3 (timeRequired, 0);
				} else { // have robbed them successfully
					MoneyManager.moneyInPocket++;
					isRobbed = true;
				}
			} else { // if not, decrease size of progress bar
				if (transform.GetChild (1).localScale.x > 0) {
					transform.GetChild (1).localScale -= new Vector3 (timeRequired, 0);
				}
			}
		}

	}
		
}
