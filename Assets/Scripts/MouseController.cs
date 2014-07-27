using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	private Transform player;		// Reference to the player.
	private GameObject playerGO; 
	private LineRenderer lineRenderer;
	public float metalRadius = 6;
	public float magneticForce = 80;

	public Color c1 = Color.white;
	public Color c2 = new Color(1, 1, 1, 0);
  	int lengthOfLineRenderer = 20;


	public string SortingLayerName = "Foreground";
	public int SortingOrder = 2;

	// Use this for initialization
	void Start () {

		//setupLineRenderer();
	
	}

	void Awake ()
	{
		// Setting up the reference.
		FindPlayer();

	}

	// Update is called once per frame
	void Update () {



		if(player){
			//bool isDead = ;
			//Debug.Log(isDead);
			if(playerGO.GetComponent<PlayerController>().dead == true){
				lineRenderer.SetPosition(0, player.position);
				lineRenderer.SetPosition(1, player.position);
				return;
			}

			lineRenderer.SetPosition(0, player.position);
			lineRenderer.SetPosition(1, player.position);
			
			//########## METAL PUSHING ############/
			// Find all the colliders on the Metals layer within the bombRadius.
			Collider2D[] metals = Physics2D.OverlapCircleAll(player.position, metalRadius, 1 << LayerMask.NameToLayer("Metal"));
			Vector2 force;
			Collider2D metal;
			float closest = 0; // Holds distance value of the closest metal
			Collider2D closestMetal = null;
			float distanceMultiplier;
			for (int i = 0; i<metals.Length; i ++) {
				distanceMultiplier = Mathf.Clamp(1f - ((metals[i].transform.position - player.position).magnitude / 10), 0f, 1f); 
				if(distanceMultiplier > closest){ // If this metal is closer than 0.01 it's the new closest and we'll use it as our closest metal
					//metal = ; 
					closestMetal = metals[i];
				}
			}
			
			if(closestMetal){
				
				Vector3 pos = player.position;
				pos.z = -20;
				lineRenderer.SetPosition(0, pos);
				//lineRenderer.renderer.sortingLayerName = "Foreground";
				Vector3 posTwo = closestMetal.transform.position;
				posTwo.z = -20;
				lineRenderer.SetPosition(1, posTwo);
				
				if(Input.GetButton("Fire1") || Input.GetButton("Fire2")){
					float pushOrPull = (Input.GetButton("Fire1")) ? -1 : 1;
					//  Take the difference in the two positions (magnitude of them subtracted) and Clamp it between 0 and 1
					// Magnitude of (-2.1, -0.3, 0.0) = 2.135368 but divided by 10 is 0.213
					// So 1 - 0.213 = 0.786 which is our distance multiplier
					distanceMultiplier = Mathf.Clamp(1f - ((closestMetal.transform.position - player.position).magnitude / 10), 0f, 1f); 
					//Debug.Log (distanceMultiplier);
					//Debug.Log ((metal.transform.position - player.position));
					force = (closestMetal.transform.position - player.position) * distanceMultiplier * magneticForce;
					//rigidbody2D.AddForce(force * magneticForce);
					playerGO.rigidbody2D.AddForce(pushOrPull * force); 
				}
			}
		}else{
			FindPlayer();
		}


	}

	void FindPlayer(){
		playerGO = GameObject.FindGameObjectWithTag("Player");
		if(playerGO){
			player = playerGO.transform;
			Debug.Log (player);
			setupLineRenderer();
		}


	}

	void setupLineRenderer(){
		lineRenderer = playerGO.AddComponent("LineRenderer") as LineRenderer;
		Debug.Log(lineRenderer.renderer.sortingLayerName);
		lineRenderer.renderer.sortingLayerName = "Foreground";
		lineRenderer.renderer.sortingOrder = 0;

		Material material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.material = material;
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.05f, 0.05f);
		lineRenderer.SetVertexCount(2);
	}
}
