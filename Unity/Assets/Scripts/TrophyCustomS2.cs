using UnityEngine;
using System.Collections;

public class TrophyCustomS2 : MonoBehaviour {

	public int trophyID;
	public bool isZoomTrophy;
	public bool isInsideTrophy;

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			if (GJAPI.User != null){
				if (trophyID > 0){
					GJAPI.Trophies.Add((uint)trophyID);
					GJAPI.Trophies.Get((uint)trophyID);
					GJAPIHelper.Trophies.ShowTrophyUnlockNotification ((uint)trophyID);
					Debug.Log ("You Got " + trophyID.ToString());
				}
			}
		}
	}
	void Update(){
		if (Camera.main.orthographicSize>90.0f){
			if (isZoomTrophy){
				Debug.Log ("You got W.W trophy");
			}
		}
	}
}
