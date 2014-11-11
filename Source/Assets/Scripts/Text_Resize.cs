using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_Resize : MonoBehaviour {

	//How big you want the font
	public int font_size = 14;
	//Relative size (Approximate size of my game view at 1080p)
	private float screen_factor = 960f;
	//what's the current screen size (width)?
	private int screen_size;
	
	// Use this for initialization
	void Start () {
		//Get the screen size (width)
		screen_size = Screen.width;
		//Change the font size based on how big the screen size is compared to the screen_factor
		this.guiText.fontSize = (int) (font_size * (screen_size / screen_factor));
	}
	
	// Update is called once per frame
	void Update () {
		//Did the screen size change?
		if(Screen.width != screen_size){
			//Update screen_size var nd recalculate font size
			screen_size = Screen.width;
			this.guiText.fontSize = (int) (font_size * (screen_size / screen_factor));
		}
	}
}
