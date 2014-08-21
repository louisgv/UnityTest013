using UnityEngine;
using System.Collections;

public class CamHint : MonoBehaviour {

	public int objectiveCount = 0;
	public float timer = 9.0f;
	private int hint = 0;
	//private float orthographPreSize;
	// Update is called once per frame
	void Update () {
		timer -= Time.fixedDeltaTime;
		if (timer < 0 ){
			if (hint==0){
				hint = Random.Range(1,14);
				//Debug.Log(transform.Find("Hint" + hint.ToString()).gameObject);
				transform.Find("Hint" + hint.ToString()).gameObject.SetActive(true);
				timer = 9.0f;
			}
		else if (hint != 0) {
				transform.Find("Hint" + hint.ToString()).gameObject.SetActive(false);
				hint = 0;
				timer = 9.0f;
			}
		}
		if (objectiveCount == 18){
			transform.Find("Done").gameObject.SetActive(true);
		}
	}
}
