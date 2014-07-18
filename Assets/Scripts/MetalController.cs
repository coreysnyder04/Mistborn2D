using UnityEngine;
using System.Collections;

public class MetalController : MonoBehaviour {

	public float metalRadius = 10f;
	private Transform player;		// Reference to the player.
	Animator anim;
	
	void Awake ()
	{
		// Setting up the reference.
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		player = playerGO.transform;
		Rigidbody2D playerRigidbody = playerGO.rigidbody2D;

	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		//maxMetalTouch = playerGO.maxMetalTouch;
	}
	
	// Update is called once per frame
	void Update () {
		float distanceToPlayer = (player.position - transform.position).magnitude;
		anim.SetFloat("DistanceToPlayer", distanceToPlayer);

	}

	void OnMouseDown () {
		Debug.Log("HEY Im CLICKED");

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
				playerRigidbody.AddForce(pushOrPull * force); 
			}
		}

		*/
	}
}
