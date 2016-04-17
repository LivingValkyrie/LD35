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
	int scoreValue;

	#endregion

	void Start() {
		Debug.LogWarning("finish ai - movement, shooting ranges");
		target = GameObject.FindWithTag("Player");

		//print(target.name);
		//velocity = transform.position;
		isShooting = false;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Enemy");

		switch (type) {
				case EnemyType.Large:
				scoreValue = 500;
				break;
				case EnemyType.Medium:
				scoreValue = 300;
				break;
				case EnemyType.Small:
				scoreValue = 100;
				break;
			default:
				scoreValue = 0;
				break;
		}
	}

	void Update() {
		if (moveBySetVelocity) {
			transform.Translate(velocity * moveSpeed * Time.deltaTime);
		} else if (target) {
			velocity = target.transform.position - transform.position;

			if (velocity.magnitude >= minRange) {
				transform.Translate(velocity * moveSpeed * Time.deltaTime);
			}

			//transform.LookAt(target.transform);
			//transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
			// pasted code. doesnt quite fit what i want, but gives a cool AI effect
			if (type != EnemyType.Large) {
				Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position,
				                                              transform.TransformDirection(Vector3.up));
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			} //
			if (velocity.magnitude <= maxRange) {
				//within ranges
				isShooting = true;
				Shoot();
			} else {
				isShooting = false;
				timeShooting = 0;
			}
		}else if (!target) {
			transform.Translate( Vector3.down * moveSpeed * 5 * Time.deltaTime );
		}
	}

	public void TakeDamage() {
		health--;
		if (health <= 0) {
			//add sound, sound delay and animation
			GameController.music.score += scoreValue;
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