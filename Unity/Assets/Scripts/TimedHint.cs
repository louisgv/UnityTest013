using UnityEngine;
using System.Collections;

public class TimedHint : MonoBehaviour {

	// Use this for initialization
	private static int touchedCount = 0;
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			++touchedCount;
		}
		if (touchedCount >= 9){
			transform.Find("Hint1").gameObject.SetActive(true);
		}
		if (touchedCount >= 18){
			transform.Find("Hint1").gameObject.SetActive(false);
			transform.Find("Hint2").gameObject.SetActive(true);
		}
	}
	
	
}
