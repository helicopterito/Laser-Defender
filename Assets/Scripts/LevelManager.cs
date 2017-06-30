using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("Solicitud para cargar el nivel: " + name);
		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quiero salir!");
		Application.Quit ();
	}

	public void LoadNextLevel(){
		//Application.LoadLevel(Application.loadedLevel +1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	
	}
		
}
