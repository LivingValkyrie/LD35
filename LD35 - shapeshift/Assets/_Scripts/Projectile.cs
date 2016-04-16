using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: Projectile 
/// </summary>
public class Projectile : MonoBehaviour {

	#region Fields

	public ProjectileType type;
	public float speed;
	public float timeToDie = 5;

	#endregion
	
	void Start () {
		Destroy(gameObject, timeToDie);
	}
	
	void Update () {
		transform.Translate(new Vector2(0f, speed * Time.deltaTime ) );
	}
}

public enum ProjectileType {
	Solid, Energy
}