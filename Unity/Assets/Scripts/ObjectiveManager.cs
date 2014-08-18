using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {

	
	private bool check = false;

	void OnTriggerEnter2D(Collider2D col){
		if (!check && col.gameObject.tag == "Player"){
		
			renderer.material.color =  Color.Lerp(renderer.material.color, Color.green, 1.0f);
			Camera.main.GetComponent<CamHint>().objectiveCount ++;
			transform.Find("Check").gameObject.SetActive(true);
			check = true;
		}
	}
}