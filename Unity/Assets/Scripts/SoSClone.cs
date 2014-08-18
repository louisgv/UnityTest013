using UnityEngine;
using System.Collections;

public class SoSClone : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			transform.Find("Congrat").gameObject.SetActive(true);
			Time.timeScale = 0;
		}
	}
}
