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
	public float timeToDie = 3;
	public Vector2 velocity;

	#endregion
	
	void Start () {
		Destroy(gameObject, timeToDie);
	}
	
	void Update () {
		transform.Translate( velocity * speed * Time.deltaTime );
	}

	void OnTriggerEnter2D(Collider2D other) {
		//print(other.tag);
		if (type == ProjectileType.PlayerShot) {
			if (other.tag == "Enemy") {
				other.GetComponent<Enemy>().TakeDamage();
				Destroy( gameObject );
			}
		} else {
			if (other.tag == "Player") {
				other.GetComponent<Player>().TakeDamage();
				Destroy(gameObject);
			}else if ( other.tag == "AbsorptionField" ) {
				other.transform.parent.GetComponent<Player>().attackSpecialAmmo++;
				Destroy( gameObject );
			}
		}
	}
}

public enum ProjectileType {
	PlayerShot, Energy, Solid
}