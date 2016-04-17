using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: SceneTransistions 
/// </summary>
public class SceneTransistions : MonoBehaviour {

	#region Fields

	#endregion

	public void LoadScene(string levelToLoad) {
		SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
	}

	public void Quit() {
		Application.Quit();
	}
}
