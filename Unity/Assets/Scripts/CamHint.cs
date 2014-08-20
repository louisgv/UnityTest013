using UnityEngine;
using System.Collections;

public class CamHint : MonoBehaviour {

	public float zoomSpeed = 9.0f;
	public int objectiveCount = 0;
	public float timer = 9.0f;
	private int hint = 0;
	//private float orthographPreSize;
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			float orthographPreSize = Camera.main.orthographicSize;
			if (Camera.main.orthographicSize < 540){
					Camera.main.orthographicSize += zoomSpeed * Time.deltaTime ;			
				transform.localScale *= Camera.main.orthographicSize/orthographPreSize;
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			float orthographPreSize = Camera.main.orthographicSize;			
			if (Camera.main.orthographicSize > 9){
				Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime;
			}
			transform.localScale *= Camera.main.orthographicSize/orthographPreSize;
		}
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
