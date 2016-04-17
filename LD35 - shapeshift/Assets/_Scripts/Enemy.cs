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
	public bool moveBySetVelocity;
	public Vector2 velocity;
	public float moveSpeed = 3;
	GameObject target;
	public float minRange, maxRange;
	bool isShooting;
	int timeShooting;
	public int fireRate;
	GameObject projectile;
	public int health;

	#endregion

	void Start() {
		Debug.LogWarning("finish ai - movement, shooting ranges");
		target = GameObject.FindWithTag("Player");

		//print(target.name);
		//velocity = transform.position;
		isShooting = false;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Enemy");
	}

	void Update() {
		if (moveBySetVelocity) {
			transform.Translate(velocity * moveSpeed * Time.deltaTime);
		} else if (target) {
			velocity = target.transform.position - transform.position;

			//print(velocity);
			transform.Translate(velocity * moveSpeed * Time.deltaTime );

			//transform.LookAt(target.transform);
			//transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
			// pasted code. doesnt quite fit what i want, but gives a cool AI effect
			if (type != EnemyType.Large) {
				Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position,
				                                              transform.TransformDirection(Vector3.up));
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			} //
			if (velocity.magnitude >= minRange && velocity.magnitude <= maxRange) {
				//within ranges
				isShooting = true;
				Shoot();
			} else {
				isShooting = false;
				timeShooting = 0;
			}
		}
	}

	public void TakeDamage() {
		health--;
		if (health <= 0) {
			//add sound, sound delay and animation
			Destroy(gameObject);
		}
	}

	void Shoot() {
		if (isShooting) {
			timeShooting++;
			if (timeShooting % fireRate == 0) {
				GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
				temp.GetComponent<Projectile>().velocity = target.transform.position - transform.position;
			}
		}
	}
}

public enum EnemyType {
	Small,
	Medium,
	Large
}