using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: RadiusSpawner 
/// </summary>
public class RadiusSpawner : MonoBehaviour {

	#region Fields

	public float spawnRate = 2;
	public float spawnRadius = 5;
	public GameObject toSpawn; 

	#endregion
	
	void Start () {
		//toSpawn = Resources.Load<GameObject>("Prefabs/Enemy");
		InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
	}
	
	void Update () {
	
	}

	void SpawnEnemy() {
		Vector2 spawnPos = new Vector2( transform.position.x, transform.position.y );
		Instantiate( toSpawn, ( Random.insideUnitCircle * spawnRadius ) + spawnPos, Quaternion.identity );
	}
}
