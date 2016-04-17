using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController music;
	public int score;
	public int lives;

	void Awake() {
		DontDestroyOnLoad (this);
		if ( music == null) {
			music = this;
			score = 0;
		} else {
			Destroy (gameObject);
		}
	}
}
