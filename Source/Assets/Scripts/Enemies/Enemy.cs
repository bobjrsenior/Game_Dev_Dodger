using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//Basic Sine, Pathfinding
	public int enemy_type;

	public float movement_speed;
	//Which way will the object go (x,y)
	//(1,0) = Left-Right (-1,0) = Right-Left
	//(0,1) = Bottom-Top (0,-1) = Top-Bottom
	public Vector2 direction;

	//If Sine
	public Vector2 direction_2;
	public float sine_speed;
	public float sine_frequency;

	//If Pathfinding
	public float life;
	public float life_timer = 0;
	//Who to follow
	public GameObject player;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Basic Enemy
		if(enemy_type == 0){
			//Set the velocity in the right direction
			this.rigidbody2D.velocity = direction * movement_speed;
			
			//If the object flies offscreen, destroy it
			if(Mathf.Abs(this.transform.position.x) > 11 || Mathf.Abs(this.transform.position.y) > 7){
				Destroy(this.gameObject);
			}
		}
		//Sine Enemy
		else if(enemy_type == 1){
			//Set the velocity in the right direction
			//First part is the same as the basic enemy.
			//Second part add velocity in direction 2 (perpendicular to normal direction)
			this.rigidbody2D.velocity = (direction * movement_speed) + (direction_2 * sine_speed * Mathf.Sin(Time.time * sine_frequency * 3.14159f));
			
			//If the object flies offscreen, destroy it
			if(Mathf.Abs(this.transform.position.x) > 11 || Mathf.Abs(this.transform.position.y) > 7){
				Destroy(this.gameObject);
			}
		}
		//Pathfinding Enemy
		else if(enemy_type == 2){

			life_timer += Time.fixedDeltaTime;
			//If its life runs out, destroy the enemy
			if(life_timer >= life){
				Destroy(this.gameObject);
			}
			//Find which way the player is
			Vector2 dir = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
			//make the direction magnitude 1 (keeps movement speed the same)
			dir.Normalize();
			//Set velocity towards the player
			rigidbody2D.velocity = dir * movement_speed;
		}
	}
}
