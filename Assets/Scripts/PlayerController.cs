﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Settings
	public float maxSpeed = 10f;
	public float runSpeed = 30f;
	public float walkSpeed = 10f;
	private float currSpeed = 0f; // Speed character is moving, changes with walk/run actions
	public float metalRadius = 3;
	private float jumpForce = 700;
	private bool facingRight = true;
	public float magneticForce = 30;

	// States
	public bool dead = false;
	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	bool doubleJump = false;

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("Dead", false);
		dead = false;

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(dead){
			return;
		}
		// Check if we're on the ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); // Returns true if we hit a collider
		anim.SetBool("Grounded", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		if(grounded){
			doubleJump = false;
		}

		float move = Input.GetAxis("Horizontal");
		float moveTotal = move * currSpeed;
		anim.SetFloat("Speed", Mathf.Abs(moveTotal)); // move of 10 or -10 is passed as 10.
		rigidbody2D.velocity = new Vector2(moveTotal, rigidbody2D.velocity.y);
		if(move > 0 && !facingRight || move < 0 && facingRight){ // If were moving right and not facing right
			Flip (); // Flip Sprite
		}

	}



	void Update (){
		if(dead){
			return;
		}
		DrawDebug();
		currSpeed = (Input.GetButton("Run"))?runSpeed:walkSpeed;

		//******** JUMPING ***********/
		if( (grounded || !doubleJump)&& Input.GetButtonDown("Jump")){
			anim.SetBool("Grounded", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));

			if(!doubleJump && !grounded){
				doubleJump = true;
			}
		}
		/*
		//########## METAL PUSHING ############/
		// Find all the colliders on the Metals layer within the bombRadius.
		if(Input.GetButton("Fire1") || Input.GetButton("Fire2")){
			float pushOrPull = (Input.GetButton("Fire1")) ? -1 : 1;
			Collider2D[] metals = Physics2D.OverlapCircleAll(transform.position, metalRadius, 1 << LayerMask.NameToLayer("Metal"));
			Vector2 force;
			Collider2D metal;
			for (int i = 0; i<metals.Length; i ++) {
				metal = metals[i]; 
				//  Take the difference in the two positions (magnitude of them subtracted) and Clamp it between 0 and 1
				// Magnitude of (-2.1, -0.3, 0.0) = 2.135368 but divided by 10 is 0.213
				// So 1 - 0.213 = 0.786 which is our distance multiplier
				float distanceMultiplier = Mathf.Clamp(1f - ((metal.transform.position - transform.position).magnitude / 10), 0f, 1f); 
				//Debug.Log (distanceMultiplier);
				//Debug.Log ((metal.transform.position - transform.position));
				force = (metal.transform.position - transform.position) * distanceMultiplier * magneticForce;
				//rigidbody2D.AddForce(force * magneticForce);
				rigidbody2D.AddForce(pushOrPull * force); 
			}
		}
		*/
	}

	void DrawDebug(){
		/*
		Vector3 forward = transform.TransformDirection(Vector2.right * metalRadius);
		Debug.DrawRay (transform.position, forward, Color.green);
		forward = transform.TransformDirection(new Vector2(1 * -metalRadius, 0));
		Debug.DrawRay (transform.position, forward, Color.green);
		forward = transform.TransformDirection(new Vector2(0, 1 * metalRadius));
		Debug.DrawRay (transform.position, forward, Color.green);
		forward = transform.TransformDirection(new Vector2(0, 1 * -metalRadius));
		Debug.DrawRay (transform.position, forward, Color.green);
		*/
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color= Color.red;
		Gizmos.DrawWireSphere (transform.position, metalRadius);
	}

	void Flip () {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Die(){
		if(!dead){
			Debug.Log ("DIED22");
			dead = true;
			anim.SetBool("Dead", true);
			GetComponent<Entity>().Die();
		}

	}

	public void Revive(){
		anim.SetBool("Dead", false);
		dead = false;
	}
}
