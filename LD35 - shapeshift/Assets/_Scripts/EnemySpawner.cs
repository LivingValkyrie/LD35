using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: EnemySpawner 
/// </summary>
public class EnemySpawner : MonoBehaviour {

	#region Fields

	public float spawnRate = 2;
	public float spawnRadius = 5;
	GameObject enemy; 

	#endregion
	
	void Start () {
		enemy = Resources.Load<GameObject>("Prefabs/Enemy");
		InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
	}
	
	void Update () {
	
	}

	void SpawnEnemy() {
		Instantiate(enemy, Random.insideUnitCircle * spawnRadius, Quaternion.identity);
	}
}
