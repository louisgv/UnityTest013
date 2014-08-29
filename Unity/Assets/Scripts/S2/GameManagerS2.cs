using UnityEngine;
using System.Collections;

public class GameManagerS2 : MonoBehaviour {

	public GameObject[] levels;
	public string resultMsg; // receive from rainbow wether red/black/or white hole
	private GameObject level;
	
	private Transform player;
	
	public bool success = false;
	public bool failure = false;
	public GUIStyle timer;
	public GUIStyle result;
	public GUIStyle levelChooser;
	private int levelNo = -1;
	//private string levelName;
	
	public bool playing = true;
	
	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		//Time.timeScale = 0;
	
	}
	
	void OnGUI(){
		
		//We need 2 arrows for Level up and Down here! They should be on the side. 
		//< x > where x = levelNo.
		// [<] a button
		// x a Label
		// [>] a Button
		Color bg = Camera.main.backgroundColor;
		if (GUI.Button(new Rect(Screen.width/2-45, 0, 30, 30), "<")){
			if (levelNo > 1){
				transform.GetComponent<IndieQuiltCommunicator>().difficulty -= 1;
			}
		}
		GUI.Button(new Rect(Screen.width/2-15,0,30,30), levelNo.ToString());
		if (GUI.Button(new Rect(Screen.width/2 +15,0,30,30), ">")){
			if (levelNo < 10){
				transform.GetComponent<IndieQuiltCommunicator>().difficulty += 1;
			}
		}
		timer.normal.textColor = new Color (1.0f - bg.r, 1.0f - bg.g, 1.0f - bg.b);
		GUI.Label(new Rect(0,Screen.height/9, Screen.width, 30), ((int)timeLeft).ToString(), timer);
		if (success){
			result.normal.textColor = new Color (1.0f - bg.r, 1.0f - bg.g, 1.0f - bg.b);
			GUI.Label(new Rect(9, Screen.height - 90, 0, 45), "Success!", result);
			if (levelNo < 10){
				if (GUI.Button(new Rect (9, Screen.height - 45, Screen.width, 45), "Next", result)){
					transform.GetComponent<IndieQuiltCommunicator>().difficulty += 1;
				}
			}
			else {
				if (GUI.Button(new Rect (9, Screen.height - 45, Screen.width, 45), "Click to Finish", result)){
					transform.GetComponent<IndieQuiltCommunicator>().finished = true;
				}
			}
		}
		if (failure){
			result.normal.textColor = new Color (1.0f - bg.r, 1.0f - bg.g, 1.0f - bg.b);
			GUI.Label(new Rect(0, Screen.height - 90 , Screen.width, 45), "Failed...", result);
			if (GUI.Button(new Rect (0, Screen.height - 45, Screen.width, 45), "Click to Retry", result)){
				StartCoroutine(LoadLevel(0.0f));
			}
		}
	}
	
	void FixedUpdate(){
		//Check Win/lose state, then either instantiate the win prefabs, or the lose prefabs.
		if (playing){
			if (success){			
				Debug.Log("Success");
				transform.GetComponent<IndieQuiltCommunicator>().success = true;
				playing = false;
			}
			else if (failure){
				Debug.Log("Failed");
				failure = true;
				transform.GetComponent<IndieQuiltCommunicator>().success = false;
				playing = false;
			}
			if (timeLeft > 1 && !success && !failure) 
				timeLeft -= Time.deltaTime;
			else if (timeLeft < 1){
				failure = true;
				transform.GetComponent<IndieQuiltCommunicator>().success = false;
				playing = false;
				Time.timeScale = 0;
			}
		}
		if (transform.GetComponent<IndieQuiltCommunicator>().difficulty != levelNo){
			Debug.Log("LevelChanged, Before = " + levelNo.ToString());
			levelNo = transform.GetComponent<IndieQuiltCommunicator>().difficulty;
			Debug.Log("After = " + levelNo.ToString());
			StartCoroutine(LoadLevel (0.0f));
		}
	}
	public float timeLeft = 30;
	IEnumerator LoadLevel(float waitTime)
	{			
		// ... pause briefly
		yield return new WaitForSeconds(waitTime);
		// ... and then reload the level.
		timeLeft = 30.0f; // Reset the timeLeft variable.
		//levelName = "lv" + levelNo.ToString();
		if (level != null) // check if there are any level prefab loaded. 
			Destroy(level); // Destroy the level Prefab.
		level = Instantiate(levels[levelNo-1]) as GameObject;
		level.transform.parent = transform;
		success = false;
		failure = false;
		playing = true;
		player.position = Vector2.zero;
		player.GetComponent<TouchMoveS2>().enabled = true;
		Time.timeScale = 1;
		Camera.main.orthographicSize = 12.0f;
		Camera.main.backgroundColor = Color.black;
	}
}
