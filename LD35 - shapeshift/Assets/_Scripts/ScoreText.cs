using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: ScoreText 
/// </summary>
public class ScoreText : MonoBehaviour {

	#region Fields

	Text myText;

	#endregion
	
	void Start () {
		myText = GetComponent<Text>();
	}
	
	void Update () {
		myText.text = GameController.music.score.ToString();
	}
}
