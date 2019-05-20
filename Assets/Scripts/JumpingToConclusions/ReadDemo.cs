using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadDemo : MonoBehaviour {

	public TextAsset csv; 

	public GameObject paperContainer;
	public GameObject suspectProfilePrefab;
	public GameObject CrimeDescPrefab;
	public GameObject privacyScreen;
	public AudioSource pageFlip;
	public AudioSource golfClap;
	public AudioSource incorrecNoise;

	private int recentInput = 0;
	private bool waitingForInput = false;

	private string[,] outputGrid;
	private int NumberOfCases;
	private int i;

	private bool moveTheOtherSheets;

	void Start () {
		CSVReader.DebugOutputGrid( CSVReader.SplitCsvGrid(csv.text) ); 
		outputGrid = CSVReader.SplitCsvGrid (csv.text);
		NumberOfCases = int.Parse(outputGrid[1,0]);

		i = 0;
		summonPapers ();
		waitingForInput = true;
		moveTheOtherSheets = false;
	}

	void Update () {
		if(waitingForInput){
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				recentInput = 3;
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				recentInput = 1;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				recentInput = 2;
			}
			if(recentInput != 0){
				waitingForInput = false;
				StartCoroutine (BlackScreen());
			}
		}
		if (moveTheOtherSheets) {
			foreach (Transform child in paperContainer.transform) {
				if (!(child.tag == recentInput.ToString ())) {
					child.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
				}
			}
		}
	}

	void summonPapers (){
		GameObject myCPaper = Instantiate (CrimeDescPrefab);
		myCPaper.GetComponent<PaperSupplier>().loadCrimeDesc(outputGrid, i);
		myCPaper.transform.position = new Vector3 (18, 20, 0);
		myCPaper.transform.SetParent (paperContainer.transform);
		for (int j = 1; j<=3; j++) {
			GameObject myPaper = Instantiate (suspectProfilePrefab);
			myPaper.GetComponent<PaperSupplier>().loadSuspectProfile(outputGrid, i, j);
			myPaper.transform.position = new Vector3 ((j-1)*18, 0, 0);
			myPaper.transform.SetParent (paperContainer.transform);
			myPaper.tag = j.ToString ();
		}
		if (i == NumberOfCases - 1) {
			i = 0;
		} else {
			i++;
		}
		waitingForInput = true;
	}

	IEnumerator BlackScreen()
	{
		pageFlip.Play (1000);

		moveTheOtherSheets = true;

		foreach (Transform child in paperContainer.transform) {
			if (child.tag == recentInput.ToString ()) {
				if (child.GetComponent<PaperSupplier> ().guilty) {
					golfClap.Play ();
				} else {
					incorrecNoise.Play ();
				}
			}
		}

		yield return new WaitForSeconds (0.25f);

		foreach (Transform child in paperContainer.transform) {
			if (!(child.tag == recentInput.ToString ())) {
				GameObject.Destroy (child.gameObject);
			}
		}

		foreach (Transform child in paperContainer.transform) {
			GameObject.Destroy (child.gameObject);
		}

		Renderer rend = privacyScreen.GetComponent<Renderer>();
		rend.enabled = true;

		yield return new WaitForSeconds (0.25f);
		rend.enabled = false;
		recentInput = 0;
		summonPapers();
		moveTheOtherSheets = false;
	}

}