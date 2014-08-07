﻿using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform target;
	private float trackSpeed = 75;


	void Awake ()
	{
		// Setting up the reference.
		//SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
	}

	public void SetTarget(Transform t){
		target = t;
		//transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);
	}

	void LateUpdate(){ // Late Update runs after all updates have run per frame
		// We want to move the camera AFTER we move the target
		if(target){
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
			
		}
		
	}

	void OnGUI()
	{
		GUI.Box (new Rect (5,5,500,30), "My Var Is: Corey");
	}

	
	// Increase n towards target by space
	private float IncrementTowards(float n, float target, float a){ // ( Current Position, Target Position, Acceleration)
		if(n == target){
			return n;
		}else{
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n)) ? n : target; // if n has now passed target then return target, otherwise reutrn n
		}
	}
}
