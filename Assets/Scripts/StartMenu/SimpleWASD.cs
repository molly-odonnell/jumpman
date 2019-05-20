using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleWASD : MonoBehaviour {

	public float speed;
	public GameObject camera;
	public Text relevantText;
	public GameObject JTCbutton;
	public GameObject muggingButton;
	public Material defaultMaterial;
	public Material selectMaterial;
	public GameObject room;
	public Object JTCScene;
	public Object muggingScene;

	private float stageStretcher;
	private int jumpToMiniGame;

	// Use this for initialization
	void Start () {
		jumpToMiniGame = 0;
		stageStretcher = 1;
	}

	void checkLookAt(){
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, out hit)) {
			if (hit.transform.tag == "JumpingButton") {
				relevantText.text = "Press Space to Play\nJumping to Conclusions";
				JTCbutton.GetComponent<Renderer> ().material = selectMaterial;
				muggingButton.GetComponent<Renderer> ().material = defaultMaterial;
				if (Input.GetKey(KeyCode.Space))
				{
					jumpToMiniGame = 2;
				}
			} else if (hit.transform.tag == "MuggingButton") {
				relevantText.text = "Press Space to Play\nMugging";
				muggingButton.GetComponent<Renderer> ().material = selectMaterial;
				JTCbutton.GetComponent<Renderer> ().material = defaultMaterial;
				if (Input.GetKey(KeyCode.Space))
				{
					jumpToMiniGame = 1;
				}
			} else {
				relevantText.text = "Jumpman";
				JTCbutton.GetComponent<Renderer> ().material = defaultMaterial;
				muggingButton.GetComponent<Renderer> ().material = defaultMaterial;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		int leftRight = 0;
		int forwardBack = 0;
		if (Input.GetKey(KeyCode.W))
		{
			forwardBack += 1;
		}
		if (Input.GetKey(KeyCode.A))
		{
			leftRight-=1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			forwardBack -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			leftRight += 1;
		}
		Vector3 position = this.transform.position;
		float angle = Mathf.Deg2Rad * camera.transform.eulerAngles.y;

		float zComponentOfForwardBackward = forwardBack * Mathf.Cos (angle);
		float xComponentOfForwardBackward = forwardBack * Mathf.Sin (angle);

		float zComponentOfLeftRight = leftRight * Mathf.Cos (angle + (Mathf.Deg2Rad * 90.0f));
		float xComponentOfLeftRight = leftRight * Mathf.Sin (angle + (Mathf.Deg2Rad * 90.0f));

		float xComponent = xComponentOfForwardBackward + xComponentOfLeftRight;
		float zComponent = zComponentOfForwardBackward + zComponentOfLeftRight;

		float distance = Mathf.Sqrt (Mathf.Pow (position.x, 2) + Mathf.Pow (position.z, 2));
		float dotProductOfPosAndDir = position.x * xComponent + position.z * zComponent;

		float adjustedSpeed = speed;
		if (dotProductOfPosAndDir > 0) {
			adjustedSpeed = speed * Mathf.Pow(1.7f, -1.0f * distance);
		}

		position.x += adjustedSpeed * xComponent;
		position.z += adjustedSpeed * zComponent;

		this.transform.position = position;

		checkLookAt ();

		if (jumpToMiniGame > 0) {
			stageStretcher += 5.0f;
			if (stageStretcher > 50.0f) {
				switch (jumpToMiniGame) {
				case 1:
					SceneManager.LoadScene (1);
					break;
				case 2:
					SceneManager.LoadScene(2);
					break;
				default:
					break;
				}
			} else {
				room.transform.localScale = new Vector3 (stageStretcher, 0.0f, stageStretcher);
			}
		}
	}
}
