using UnityEngine;
using System.Collections;

public class PlanetDeath : MonoBehaviour {

	//Need to modify colors and so on.
	//Need to make gravity center point or something like that.
	//Need to make for a good ... something/
	// On the player, we need a script to change color, and we need the 
	// little Orb!
	
	public float gravityStrength = 100.0f;
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("In!");
			Physics2D.gravity = (Vector2)
				(transform.position - col.transform.position).normalized * 
					gravityStrength;
			//Debug.Log(Physics2D.gravity);
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		Camera.main.GetComponent<BackGroundGradient>().randomColor();
		if (col.gameObject.tag == "Player"){
			
			//Debug.Log("In!");
			//col.gameObject.GetComponent<PlayerControl>().spawnPosition = 
				//col.transform.position;
			//Debug.Log(Physics2D.gravity);
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			
			col.gameObject.rigidbody2D.gravityScale = 0.1f;
			col.gameObject.rigidbody2D.velocity = col.gameObject.rigidbody2D.velocity.normalized * 0.1f;
			//col.gameObject.rigidbody2D.angularVelocity = 0.1f;
			Physics2D.gravity = (Vector2)Vector3.down;
		}
	}
}
