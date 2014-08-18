using UnityEngine;
using System.Collections;

public class BackGroundGradient : MonoBehaviour {

	public Color previousColor;
	
	void Awake(){
	
		previousColor = camera.backgroundColor;
		
	}
	
	public float speed = 0.9f;
	void Update(){
		if (camera.backgroundColor != rndColor){
			//float t = Mathf.PingPong(Time.time, duration) / duration;
			camera.backgroundColor = Color.Lerp(camera.backgroundColor, 
			                                    //new Color(Random.Range(0f,255.0f), Random.Range(0f,255.0f), Random.Range(0f,255.0f)), 
			                                    rndColor,
			                                    Time.deltaTime*speed);
		}
		//else{
		//	randomColor();
		//}
	}
	
	private Color rndColor;
	public void randomColor(){
		previousColor = camera.backgroundColor;
		rndColor = new Color(Random.Range(0f,1.0f), 
		                     Random.Range(0f,1.0f), 
		                     Random.Range(0f,1.0f),
		                     Random.Range(0f,1.0f));
		//Debug.Log(rndColor);
	}
}
