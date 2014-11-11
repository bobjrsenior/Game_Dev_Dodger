using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	//Use Mouse or keyboard
	public bool mouse_controls;

	//Lives
	public int lives;
	private GUIText life_text;

	//Movement Variables
	public float max_speed;
	//Generally acceleration should be high (less sliding)
	public float acceleration;

	// Use this for initialization
	void Start () {
		life_text = GameObject.FindGameObjectWithTag("Lives_UI").GetComponent<GUIText>();
		life_text.text = "Lives Left: " + lives;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If you want to control the player with a mouse
		if(mouse_controls){
			this.transform.position = Mouse_Position();
		}
		//If you want keyboard controls (mouse_controls = false)
		else{
			//With keyboardcontrols, force is used because just changing postion
			//is like teleporting an objects and can break physics

			//Holds force
			Vector2 force = new Vector2();
			//Positive if pressing D or Right Arrow Key, negative if pressing A or Left Arrow Key
			force.x = Input.GetAxis("Horizontal") * acceleration;
			//Positive if pressing W or Up Arrow Key, negative if pressing S or Down Arrow Key
			force.y = Input.GetAxis("Vertical") * acceleration;
			if(force.sqrMagnitude == 0){
				this.rigidbody2D.velocity = Vector2.zero;
			}
			//Apply the force
			this.rigidbody2D.AddForce(force);

			//Keep the x and y velocity within the max velocity
			//Mathf.Clamp(Varible, Min_Value, Vax_Value)
			this.rigidbody2D.velocity = new Vector2(Mathf.Clamp(this.rigidbody2D.velocity.x, -max_speed, max_speed), Mathf.Clamp(this.rigidbody2D.velocity.y, -max_speed, max_speed));
		}

		//Keep the player from leaving the screen
		this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x, -8.7f, 8.7f), Mathf.Clamp(this.transform.position.y, -4.8f, 4.8f));
	}

	//By default the mouse position is given in pixels, not Unity Units
	//This converts it
	public Vector2 Mouse_Position(){
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Did you run into an enemy?
		if(other.CompareTag("Enemy")){
			//Destory the enemy
			Destroy(other.gameObject);
			//Decrement your lives and update the gui
			lives --;
			life_text.text = "Lives Left: " + lives;
			//If you run out of lives, tell Game_Timer you lost
			//and destroy the player script/diable the gameobject
			if(lives <= 0){
				Game_Timer.time.lost = true;
				this.enabled = false;
				Destroy(this.GetComponent<Player_Controller>());
			}
		}
	}
}
