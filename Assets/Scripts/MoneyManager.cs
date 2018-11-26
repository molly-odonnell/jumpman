using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

	public static int moneyInPocket = 0;
	public static int moneyInBank = 0;
	public Text moneyText;

	void Update(){
		moneyText.text = "$$$ in pocket: " + moneyInPocket;
	}
}