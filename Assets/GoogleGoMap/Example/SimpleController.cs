﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleController : MonoBehaviour
{
	public float speed = 6.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public Animator Player;


	void Start(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		

		// Move Character Controller
		if (moveDirection.magnitude > 5) {
			controller.Move (moveDirection * Time.deltaTime);
			Player.SetBool ("Run",true);
		}
		else {
			Player.SetBool ("Run",false);

		}
			


	}
}