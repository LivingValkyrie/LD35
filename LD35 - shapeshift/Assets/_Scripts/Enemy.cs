using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: Enemy 
/// </summary>
public class Enemy : MonoBehaviour {

	#region Fields

	public EnemyType type;
	GameObject target;
	Vector2 velocity;
	public float minRange, maxRange;
	bool isShooting;
	int timeShooting;
	public int fireRate;
	GameObject projectile;

	#endregion
	
	void Start () {
		target = GameObject.FindWithTag("Player");
		//print(target.name);
		velocity = transform.position;
		isShooting = false;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Enemy");
	}
	
	void Update () {
		velocity = transform.position - target.transform.position;
		transform.Translate(velocity * Time.deltaTime);
		if (velocity.magnitude >= minRange && velocity.magnitude <= maxRange) {
			//within ranges
			Shoot();
		} else {
			isShooting = false;
			timeShooting = 0;
		}
	}

	void Shoot() {
		if (isShooting) {
			timeShooting++;
			if (timeShooting % fireRate == 0) {
				GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity ) as GameObject;
			}
		}
	}
}

public enum EnemyType {
	
}