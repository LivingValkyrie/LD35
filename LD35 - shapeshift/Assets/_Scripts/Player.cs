using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	#endregion

	void Start() {
		currForm = prevForm = startingForm;
		projectile = Resources.Load<GameObject>("Prefabs/Projectile - Player");
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

		//movement
		velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime,
		                       Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
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
			}
		}
	}

	public void Move(Vector2 velocity) {
		transform.Translate(velocity);
	}

	public void ChangeForm(PlayerForm form) {
		prevForm = currForm;
		currForm = form;
		print("changd form from " + prevForm + " to " + currForm);
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