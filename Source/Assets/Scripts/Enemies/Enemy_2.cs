using UnityEngine;
using System.Collections;

public class Enemy_2 : MonoBehaviour {

	public float movement_speed;
	public float sine_speed;
	public float sine_frequency;

	//Which way will the object go (x,y)
	//(1,0) = Left-Right (-1,0) = Right-Left
	//(0,1) = Bottom-Top (0,-1) = Top-Bottom
	public Vector2 direction;
	public Vector2 direction_2;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Set the velocity in the right direction
		this.rigidbody2D.velocity = (direction * movement_speed) + (direction_2 * sine_speed * Mathf.Sin(Time.time * sine_frequency * 3.14159f));

		//If the object flies offscreen, destroy it
		if(Mathf.Abs(this.transform.position.x) > 11 || Mathf.Abs(this.transform.position.y) > 7){
			Destroy(this.gameObject);
		}
	}
}
