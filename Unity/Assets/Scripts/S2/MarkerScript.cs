using UnityEngine;
using System.Collections;

public class MarkerScript : MonoBehaviour {
	
	public bool isNameConvention = false;
	// Use this for initialization
	void Start () {
		if (isNameConvention){
			transform.name = transform.parent.name;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
