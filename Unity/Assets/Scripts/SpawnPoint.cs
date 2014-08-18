using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	//public transform 
	// Might need to check 
	private static int spawnTime = 0;
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerControl>().spawnPosition = 
				transform.position;
			transform.Find("Check").gameObject.SetActive(true);
			++spawnTime;
		}
	}
}
