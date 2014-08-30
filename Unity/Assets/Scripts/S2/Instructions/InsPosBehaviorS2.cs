using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class InsPosBehaviorS2 : MonoBehaviour {

	public AudioClip touched;
	public string msg;
	public bool noChangeColor = false;
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("In!");
			if (!noChangeColor){
				Camera.main.GetComponent<BackGroundGradient>().randomColor();
			}
			transform.GetComponent<TextMesh>().text = msg;
			audio.PlayOneShot(touched, 0.7f);
		}
	}
	
	// Update is called once per frame
	public float dir = -1;
	public float speed = 9.0f;
	public bool noDirection = false;
	
	void Update () {
		if (noDirection){
			transform.Find("ArrowS2").Rotate(Vector3.forward * dir, Time.deltaTime*speed);
		}
	}
	
	public float angle = 0; // The Angle of the arrow
	void Awake(){
		if (angle!= 0){
			transform.Find("ArrowS2").eulerAngles = new Vector3(0,0,angle);
		}
	}
}
