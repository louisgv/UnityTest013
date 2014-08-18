using UnityEngine;
using System.Collections;

public class PlanetGradient : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player"){
			col.gameObject.rigidbody2D.gravityScale = 1.8f;
			//Debug.Log(col.transform.rigidbody2D.gravityScale);
			//Debug.Log(Physics2D.gravity);
			transform.renderer.material.color = Color.Lerp(transform.renderer.material.color,
			                                               Camera.main.backgroundColor,
			                                               Time.deltaTime);
		}
	}
}
