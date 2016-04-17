using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
	private static DontDestroyOnLoad Music;
	void Awake() {
		DontDestroyOnLoad (this);
		if (Music == null) {
			Music = this;
		} else {
			Destroy (gameObject);
		}
	}
}
