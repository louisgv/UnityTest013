using UnityEngine;
using System.Collections;

public class Special30sWaitGoal : MonoBehaviour {

	// Use this for initialization
	public bool isInside = false;
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("In!");
			isInside = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Player"){
			isInside = false;
		}
	}	
	void Start () {
		StartCoroutine(special30sWait());
	}
	
	public bool done = false;
	void FixedUpdate(){
		if (done){
			transform.parent.Find("RainbowSurf").transform.localScale = Vector3.Lerp(
				transform.parent.Find("RainbowSurf").transform.localScale,
				new Vector3 (0.18f,0.18f,0),
				Time.deltaTime * 0.9f);
		}
	}
	
	IEnumerator special30sWait(){
		yield return new WaitForSeconds(27.0f);
		if (isInside){
			transform.parent.Find("RainbowSurf").gameObject.SetActive (true);
			done = true;
			if (GJAPI.User != null){
				GJAPI.Trophies.Add(10825) ;
				GJAPI.Trophies.Get(10825);
				GJAPIHelper.Trophies.ShowTrophyUnlockNotification (10825);
			}
		}
	}
}
