using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour {
	
	public float speed = 300;

	void Update () {
		transform.Rotate(Vector3.forward * speed * Time.deltaTime,Space.World);
	}
	
	void OnTriggerEnter2D(Collider2D c){
		Debug.Log ("TRIGGERED");
		Debug.Log (c.tag);
		if (c.tag == "Player") {
			c.GetComponent<Entity>().TakeDamage(10);
		}
	}


	void OnTriggerEnter(Collider c) {
			Debug.Log ("TRIGGERED");
			Debug.Log (c.tag);
			if (c.tag == "Player") {
				c.GetComponent<Entity>().TakeDamage(10);
			}
	}
}
