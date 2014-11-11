using UnityEngine;
using System.Collections;

public class Game_Timer : MonoBehaviour {

	public static Game_Timer time;

	public float game_time = 0;
	public bool lost = false;

	void Awake(){
		time = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!lost){
			game_time += Time.deltaTime;

			//Make a var to hold the game time to only 2 decimal places
			float truncated_time = ((int) (game_time * 100)) / 100f;
			//Update the game time on-screen
			this.guiText.text = "Time Survived: " + truncated_time;
		}
	}
}
