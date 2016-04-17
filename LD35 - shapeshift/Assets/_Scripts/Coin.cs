using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: Coin 
/// </summary>
public class Coin : MonoBehaviour {

	#region Fields

	public int scoreValue;
	public float speed;

	#endregion
	
	void Start () {
	
	}
	
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime) ;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			GameController.music.score += scoreValue;
		}
	}
}
