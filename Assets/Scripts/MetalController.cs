using UnityEngine;
using System.Collections;

public class MetalController : MonoBehaviour {

	public float metalRadius = 10f;
	private Transform player;		// Reference to the player.
	Animator anim;
	
	void Awake ()
	{
		// Setting up the reference.
		FindPlayer();


	}

	void FindPlayer(){
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		if(playerGO){
			player = playerGO.transform;
		}
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		//maxMetalTouch = playerGO.maxMetalTouch;
	}
	
	// Update is called once per frame
	void Update () {
		if(player){
			float distanceToPlayer = (player.position - transform.position).magnitude;
			anim.SetFloat("DistanceToPlayer", distanceToPlayer);
		}else{
			//Debug.Log ("FInding Player");
			FindPlayer();
		}


	}

	void OnMouseDown () {
		Debug.Log("HEY Im CLICKED");

	}
}
