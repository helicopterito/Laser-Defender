using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;

	private Text myText;

	void Start(){
		myText = GetComponent<Text> ();
		Reset ();
	}
	public void Score(int points){
		score += points;
		myText.text = score.ToString ();
		if (score == 50000) {
			SceneManager.LoadScene ("Win");
		}
	}

	public static void Reset(){
		score = 0;
	}
}
