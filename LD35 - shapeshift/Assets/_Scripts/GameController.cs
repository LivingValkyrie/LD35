using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController music;
	public int score;
	public int lives;

	void Awake() {
		DontDestroyOnLoad(this);
		if (music == null) {
			music = this;
			score = 0;
		} else {
			Destroy(gameObject);
		}
	}

	public void Respawn() {
		Invoke("SpawnPlayer", 3);
	}

	void SpawnPlayer() {
		foreach ( GameObject go in GameObject.FindGameObjectsWithTag( "Enemy" ) ) {
			Destroy( go );
		}

		GameObject temp = Resources.Load<GameObject>("Prefabs/Player");
		Instantiate(temp, Vector3.zero, Quaternion.identity);
		
	}

}