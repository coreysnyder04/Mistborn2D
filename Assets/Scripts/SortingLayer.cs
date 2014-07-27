using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {

	public string SortingLayerName;
	public int SortingOrder = 2;

	// Use this for initialization
	void Start(){
		MeshRenderer Mesh = gameObject.GetComponent<MeshRenderer>();

		Mesh.sortingLayerName = SortingLayerName;
		Mesh.sortingOrder = SortingOrder;
	}

}
