using UnityEngine;
using System.Collections;

public class GameManagerS2 : MonoBehaviour {

	public GameObject[] level;
	public string resultMsg; // receive from rainbow wether red/black/or white hole
	
	
	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
