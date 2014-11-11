using UnityEngine;
using System.Collections;

public class Enemy_Generator : MonoBehaviour {

	public GameObject player;

	//How quickly the game gets harder
	public float difficulty_step;

	//How many enemies spawn a second
	public float initial_spawn_rate;
	public float spawn_rate;
	//everytime this = spawn_rate, spawn an enemy
	private float spawn_timer = 0;

	//Movement
	//Sine
	public float initial_move_speed;
	public float initial_sine_speed;
	public float initial_sine_frequency;
	//Pathfinding
	public float initial_tracking_speed;
	public float initial_life;

	//Enemy Variables
	public GameObject[] enemies;
	//initial chance based from 0 to rand_val_max
	public float[] spawn_chance = {3, 8};
	public float rand_val_max = 10f;


	//Spawn Locations (Where enemies can spawn outside of the map)
	private float side_bound = 9.5f;
	private float top_bound = 5.5f;

	// Use this for initialization
	void Start () {
		//Find the player in the scene
		player = GameObject.FindGameObjectWithTag("Player");
		//Set the spawn rate
		spawn_rate = initial_spawn_rate;
	}
	
	// Update is called once per frame
	void Update () {
		//Up the spawn rate based on the difficulty_step
		spawn_rate = initial_spawn_rate + (.1f * difficulty_step * Game_Timer.time.game_time);
		Spawn_Enemies();
	}

	public void Spawn_Enemies(){
		//Up the spawn chances of enemies based on difficulty_step
		//Hardcoded to left sine and pathfinding enemies spawn more late game
		spawn_chance[0] += .1f * difficulty_step;
		spawn_chance[1] += .15f * difficulty_step;
		//Max value in random range
		rand_val_max += .18f * difficulty_step;


		spawn_timer += Time.deltaTime * spawn_rate;
		//spawn an enemy
		if(spawn_timer >= 1){
			//reset the spawn_timer
			spawn_timer = 0;

			//Enemy Types: Simple, Sine, Tracking
			//Choose enemy_type based on a random value
			//Relates to spawn_chance array
			float spawn_value = Random.Range(0, rand_val_max);
			int enemy_index = 0;
			if(spawn_value > spawn_chance[1]){
				enemy_index = 2;
			}
			else if(spawn_value > spawn_chance[0]){
				enemy_index = 1;
			}
			//Actually create (Instantiate the enemy)
			GameObject enemy = Instantiate(enemies[enemy_index], transform.position, Quaternion.identity) as GameObject;
			Enemy e_script = enemy.GetComponent<Enemy>();


			//Set the variables of the enemy
			//If its basic or sine
			if(enemy_index == 0 || enemy_index == 1){
				//Set the movement speed
				e_script.movement_speed = initial_move_speed + (.1f * difficulty_step * Time.time);
				//If it's sine, set the sine specific vars
				if(enemy_index == 1){
					e_script.sine_speed = initial_sine_speed + (.1f * difficulty_step * Time.time);
					e_script.sine_frequency = initial_sine_frequency + (.1f * difficulty_step * Time.time);
				}
			}
			//If its Pathfinding, set Pathfinding vars
			else{
				e_script.movement_speed = initial_tracking_speed + (.1f * difficulty_step * Time.time);
				e_script.life = initial_life + (.1f * difficulty_step * Time.time);
				e_script.player = player;
			}

			//Where will the enemy spawn? (Top, Bottom, Left, Right)
			int loc = (int) Random.Range(1,4);
			//Top
			if(loc == 1){
				enemy.transform.position = new Vector2(Random.Range(-side_bound, side_bound), top_bound);

				//If it is a basic or sine enemy (Only two with a need for a direction)
				if(enemy_index == 0 || enemy_index == 1){
					//Add its movement direction
					e_script.direction = new Vector2(0, -1);
					//If it is a sine enemy (Needs a direction for sinusoudal movement)
					if(enemy_index == 1){
						//Set the axis for sinusoudal movement
						e_script.direction_2 = new Vector2(1, 0);
					}
				}
			}
			//Same as loc 1, but for the bottom
			//Bottom
			else if(loc == 2){
				enemy.transform.position = new Vector2(Random.Range(-side_bound, side_bound), -top_bound);
				if(enemy_index == 0 || enemy_index == 1){
					e_script.direction = new Vector2(0, 1);
					if(enemy_index == 1){
						e_script.direction_2 = new Vector2(-1, 0);
					}
				}
				
			}
			//Left
			else if(loc == 3){
				enemy.transform.position = new Vector2(-side_bound, Random.Range(-top_bound, top_bound));
				if(enemy_index == 0 || enemy_index == 1){
					e_script.direction = new Vector2(1, 0);
					if(enemy_index == 1){
						e_script.direction_2 = new Vector2(0, 1);
					}
				}
				
			}
			//Right
			else{
				enemy.transform.position = new Vector2(side_bound, Random.Range(-top_bound, top_bound));
				if(enemy_index == 0 || enemy_index == 1){
					e_script.direction = new Vector2(-1, 0);
					if(enemy_index == 1){
						e_script.direction_2 = new Vector2(0, -1);
					}
				}
			}
		}
	}
}
