using UnityEngine;
using System.Collections;

public class RedWall : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//StartCoroutine("ReloadGame");
			col.transform.position = 
				col.gameObject.GetComponent<PlayerControl>().spawnPosition;
		}
	}
	// Update is called once per frame
	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
