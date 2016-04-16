using System;
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

	#endregion

	void Start() {
		currForm = prevForm = startingForm;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Player");
		formText.text = currForm.ToString();
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

		Move(velocity);

		//shooting
		Shoot();
	}

	public void Shoot() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			timeShooting = 0;
		} else if (Input.GetKey(KeyCode.Space)) {
			timeShooting++;
			if (timeShooting % fireRate == 0) {
				GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
				temp.GetComponent<Projectile>().velocity = new Vector2(0f, 1f);
				temp.GetComponent<Projectile>().type = ProjectileType.PlayerShot;
			}
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

	public void Move(Vector2 velocity) {
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