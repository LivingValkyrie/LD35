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

	public Dictionary<KeyCode, PlayerForm> formKeys;

	#endregion

	void Start() {}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeForm(PlayerForm.Attack);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ChangeForm(PlayerForm.Defense);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			ChangeForm(PlayerForm.Speed);
		}

		print(currForm);
	}

	public void ChangeForm(PlayerForm form) {
		currForm = form;
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