using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))] // Anything we attach this script to will require a BoxCollider
public class BoxGizmos : MonoBehaviour {

	public Color gizmoColor;


	void OnDrawGizmos(){
		Gizmos.color = gizmoColor;
		Gizmos.DrawCube(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
