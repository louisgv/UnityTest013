using UnityEngine;
using System.Collections;

public class CaneraHintS2 : MonoBehaviour {

	
	public float timer = 9.0f;
	private int hint = 0;
	
	public int objectiveCount = 0;
	public int goal = 9;
	//private float orthographPreSize;
	// Update is called once per frame
	void Update () {
		timer -= Time.fixedDeltaTime;
		if (timer < 0 ){
			if (hint==0){
				hint = Random.Range(1,transform.Find("Hints").childCount + 1);
				//Debug.Log(transform.Find("Hint" + hint.ToString()).gameObject);
				transform.Find("Hints").
					Find("Hint" + hint.ToString()).gameObject.SetActive(true);
				timer = 9.0f;
			}
			else if (hint != 0) {
				transform.Find("Hints").
					Find("Hint" + hint.ToString()).gameObject.SetActive(false);
				hint = 0;
				timer = 9.0f;
			}
		}
		if (objectiveCount == goal){
			transform.Find("Done").gameObject.SetActive(true);
		}
	}
}
