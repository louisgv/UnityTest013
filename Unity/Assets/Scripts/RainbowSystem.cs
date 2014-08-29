using UnityEngine;
using System.Collections;

public class RainbowSystem : MonoBehaviour {

	// Use this for initialization
	public bool success = false;
	public bool failure = false;
	public bool playing = true; // check if the game has been concluded.
	
	void Update(){
		if (playing){
			if (success){
				transform.parent.parent.GetComponent<GameManagerS2>().success = true;
				playing = false;
			}
			if (failure){
				transform.parent.parent.GetComponent<GameManagerS2>().failure = true;
				playing = false;
			}
		}
	}
}
