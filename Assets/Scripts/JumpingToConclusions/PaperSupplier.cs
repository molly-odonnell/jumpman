using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PaperSupplier : MonoBehaviour {

	public GameObject thePlane;

	public TextMesh CrimeKey;
	public TextMesh Desc;

	public TextMesh Name;
	public TextMesh Alias;
	public TextMesh Age;
	public TextMesh Workplace;
	public TextMesh Height;
	public TextMesh Sex;
	public TextMesh Race;
	public TextMesh Alibi;
	public bool guilty;

	public static List<int> getIndices(string s){
		var foundIndexes = new List<int>();
		for (int i = s.IndexOf (' '); i > -1; i = s.IndexOf (' ', i + 1)) {
			// for loop end when i=-1 ('a' not found)
			foundIndexes.Add (i);
		}
		return foundIndexes;
	}

	public static string SpliceText(string text, int lineLength) {
		var foundIndexes = getIndices (text);
		int lastSpaceIndex = -1;
		int lastNewlineIndex = -1;
		int j = 0;
		while(j < text.Length){
			if (foundIndexes.Contains (j)) {
				lastSpaceIndex = j;
			}
			if (text [j] == '\n') {
				lastNewlineIndex = j;
			}
			if ((j - lastNewlineIndex > lineLength) && lastSpaceIndex > 0) {
				text = text.Remove(lastSpaceIndex, 1).Insert (lastSpaceIndex, "\n");
				return SpliceText (text, lineLength);
			}
			j++;
		}
		return text;
	}

	public string reSizeText(string x){
		float paperWidth = thePlane.GetComponent<MeshRenderer> ().bounds.extents [0];
		return SpliceText (x, 2 * Mathf.RoundToInt(paperWidth*2));
	}

	public void autoMove(TextMesh top, TextMesh bottom){
		float topGuy = top.GetComponent<MeshRenderer> ().bounds.center [1];
		float topGuy2 = top.GetComponent<MeshRenderer> ().bounds.extents [1];
		float bottomGuy = bottom.GetComponent<MeshRenderer> ().bounds.center [1];
		float bottomGuy2 = bottom.GetComponent<MeshRenderer> ().bounds.extents [1];
		float bottomOfTop = topGuy - topGuy2;
		float topOfBottom = bottomGuy + bottomGuy2;
		bottom.transform.Translate (new Vector3 (0, bottomOfTop - topOfBottom));
	}

	public void loadSuspectProfile (string[,] input, int caseNum, int suspectNum) {
		int Row = 2 + caseNum * 4 + suspectNum;
		Name.text = SpliceText (input [2, Row], 13);
		Age.text = SpliceText (input[4,Row], 12);
		//autoMove (Name, Age);
		Alias.text = SpliceText ("        " + input[3,Row], 16);
		//autoMove (Age, Alias);
		Workplace.text = SpliceText (input[5,Row], 20);
		//autoMove (Alias, Workplace);
		Height.text = SpliceText (input[6,Row], 10);
		//autoMove (Workplace, Height);
		Sex.text = SpliceText (input[7,Row], 7);
		//autoMove (Workplace, Sex);
		Race.text = SpliceText (input[8,Row], 13);
		//autoMove (Sex, Race);
		Alibi.text = SpliceText ("         " + input[9,Row], 33);
		//autoMove (Race, Alibi);
		if (input [10, Row] == "1") {
			guilty = true;
		} else {
			guilty = false;
		}
	}

	public void loadCrimeDesc (string[,] input, int caseNum) {
		int Row = 2 + caseNum * 4;
		CrimeKey.text = reSizeText ("Crime: " + input[0,Row]);
		Desc.text = reSizeText ("Description: " + input[1,Row]);
		autoMove (CrimeKey, Desc);
	}
}
