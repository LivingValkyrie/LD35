using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: SplashScreen 
/// </summary>
public class SplashScreen : MonoBehaviour {

	#region Fields

	public float count = 3;

	#endregion
	
	void Start () {
	
		Invoke("ChangeScene", count);

	}
	
	void ChangeScene () {
		SceneManager.LoadScene("Main Menu");
	}
}
