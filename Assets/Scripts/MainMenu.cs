using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text hS;
	// Use this for initialization
	void Start () {
		
		HSFunction ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play () {
			
		SceneManager.LoadScene (1);


	
	}

	public void HSFunction ()
	{
	
		hS.text = PlayerPrefs.GetInt ("HighScore").ToString();


	}
}
