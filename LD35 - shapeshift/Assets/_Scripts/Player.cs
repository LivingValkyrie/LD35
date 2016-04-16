﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: Player 
/// </summary>
public class Player : MonoBehaviour {
	#region Fields

	public PlayerForm startingForm;
	PlayerForm currForm, prevForm;

	Vector2 velocity;
	public float speed = 10;

	GameObject projectile;
	public int fireRate = 5;
	int timeShooting;

	public int health = 10;
	public int lives = 3;

	public Text formText;

	[Header("Form Specials")]
	public int attackSpecialAmmo = 15;
	public int attackSpecialFireRate;
	int timeShootingAttackSpecial;
	public Vector2 attackSpecialOffset;

	#endregion

	void Start() {
		Debug.LogWarning("finish player states - special abilities, sprite changing");

		currForm = prevForm = startingForm;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Player");
		formText.text = currForm.ToString();
		timeShooting = 0;
	}

	void Update() {
		//change form?
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeForm(PlayerForm.Attack);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ChangeForm(PlayerForm.Defense);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			ChangeForm(PlayerForm.Speed);
		}

		//move and shoot
		Move();
		Shoot();
		UseSpecial();
	}

	public void UseSpecial() {
		switch (currForm) {
			case PlayerForm.Attack:
				if (Input.GetKeyDown(KeyCode.Space)) {
					timeShootingAttackSpecial = 0;
				} else if (Input.GetKey(KeyCode.Space)) {
					timeShootingAttackSpecial++;
					if (timeShootingAttackSpecial % attackSpecialFireRate == 0) {
						if (attackSpecialAmmo >= 1) {
							attackSpecialAmmo--;
							Vector3 spawnPos1 = transform.position + new Vector3(attackSpecialOffset.x, attackSpecialOffset.y);
							GameObject temp1 = Instantiate(projectile, spawnPos1, Quaternion.identity) as GameObject;
							temp1.GetComponent<Projectile>().velocity = new Vector2(0f, 1f);
							temp1.GetComponent<Projectile>().type = ProjectileType.PlayerShot;

							Vector3 spawnPos2 = transform.position + new Vector3(-attackSpecialOffset.x, attackSpecialOffset.y);
							GameObject temp2 = Instantiate(projectile, spawnPos2, Quaternion.identity) as GameObject;
							temp2.GetComponent<Projectile>().velocity = new Vector2(0f, 1f);
							temp2.GetComponent<Projectile>().type = ProjectileType.PlayerShot;
						}
					}
				}
				break;
			case PlayerForm.Defense:
				break;
			case PlayerForm.Speed:
				break;
		}
	}

	public void Shoot() {
		timeShooting++;
		if (timeShooting % fireRate == 0) {
			GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			temp.GetComponent<Projectile>().velocity = new Vector2(0f, 1f);
			temp.GetComponent<Projectile>().type = ProjectileType.PlayerShot;
		}
	}

	public void TakeDamage() {
		switch (currForm) {
			case PlayerForm.Attack:
				health -= 2;
				break;
			case PlayerForm.Defense:
				health--;
				break;
			case PlayerForm.Speed:
				health -= 4;
				break;
		}

		if (health <= 0) {
			//add animation, sound, sound delay and gameover/life removal
			lives--;
			if (lives <= 0) {
				SceneManager.LoadScene("Gameover");
			}
			Destroy(gameObject);
		}
	}

	public void Move() {
		float moveSpeed;

		switch (currForm) {
			case PlayerForm.Attack:
				moveSpeed = speed;
				break;
			case PlayerForm.Defense:
				moveSpeed = speed * 0.5f;
				break;
			case PlayerForm.Speed:
				moveSpeed = speed * 2;
				break;
			default:
				moveSpeed = speed;
				break;
		}

		//movement
		velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime,
		                       Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime);
		transform.Translate(velocity);
	}

	public void ChangeForm(PlayerForm form) {
		if (prevForm != form) {
			prevForm = currForm;
			currForm = form;
			formText.text = currForm.ToString();
		}
	}

	PlayerForm PreviousForm() {
		if ((int) currForm <= 0) {
			//first statee
			return currForm;
		} else {
			return currForm - 1;
		}
	}

	PlayerForm NextForm() {
		if ((int) currForm >= Enum.GetValues(typeof (PlayerForm)).Length) {
			//on final state
			return currForm;
		} else {
			return currForm + 1;
		}
	}
}

public enum PlayerForm {
	Attack,
	Defense,
	Speed
}