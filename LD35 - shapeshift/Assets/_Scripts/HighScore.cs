using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: HighScore 
/// </summary>
public class HighScore : MonoBehaviour {
	#region Fields

	public bool showScores;
	public bool resetScores;
	public Text[] scoreTexts;
	static bool needsUpdated = false;

	#endregion

	void Start() {
		if (!PlayerPrefs.HasKey("FirstRun") || resetScores) {
			PlayerPrefs.SetString("FirstRun", "yup");
			for (int i = 0; i < 10; i++) {
				PlayerPrefs.SetInt("Score " + i, 0);
			}
		}
		
		if (showScores) {
			for (int i = 0; i < scoreTexts.Length; i++) {
				scoreTexts[i].text = PlayerPrefs.GetInt( "Score " + i ).ToString();
			}
			needsUpdated = false;
		}
	}

	void Update() {
		if (needsUpdated) {
			for ( int i = 0; i < scoreTexts.Length; i++ ) {
				scoreTexts[i].text = PlayerPrefs.GetInt( "Score " + i ).ToString();
			}
			needsUpdated = false;
		}
	}

	public void CallScoreUpdateWithRandom() {
		UpdateScores(Random.Range(0, 100));
	}

	public static void UpdateScores(int score) {
		int[] scores = GetArrayAndSort();

		for (int index = 0; index < scores.Length; index++) {
			if (score >= scores[index]) {
				int scoreToMove = scores[index];
				scores[index] = score;
				score = scoreToMove;
			}
		}

		for ( int i = 0; i < scores.Length; i++ ) {
			PlayerPrefs.SetInt( "Score " + i, scores[i] );
		}
		needsUpdated = true;
	}

	static int[] GetArrayAndSort() {
		int[] scores = new int[10];
		for (int i = 0; i < scores.Length; i++) {
			scores[i] = PlayerPrefs.GetInt("Score " + i);
		}

		Array.Sort(scores);
		Array.Reverse( scores );

		return scores;
	}
}