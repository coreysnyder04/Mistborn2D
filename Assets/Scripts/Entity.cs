using UnityEngine;
using System.Collections;


public class Entity : MonoBehaviour {
	
	public float health;
	Animator anim;


	void Start () {
		anim = GetComponent<Animator>();

	}
	
	public void TakeDamage(float dmg) {
		health -= dmg;
		
		if (health <= 0) {
			Die();	
		}
	}
	
	public void Die() {
		GetComponent<PlayerController>().Die();
	}
}
