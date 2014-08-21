using UnityEngine;
using System.Collections;

public class RainbowSurf : MonoBehaviour {

	// Use this for initialization
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("In!");
			
			//Debug.Log(Physics2D.gravity);
		}
	}
}
