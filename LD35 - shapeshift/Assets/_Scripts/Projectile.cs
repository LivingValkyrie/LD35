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

	#endregion
	
	void Start () {
	
	}
	
	void Update () {
	
	}
}

public enum ProjectileType {
	Solid, Energy
}