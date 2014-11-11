using UnityEngine;
using System.Collections;

public class Enemy_3 : MonoBehaviour {

	public float movement_speed;
	public float life;
	public float life_timer = 0;

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		life_timer += Time.fixedDeltaTime;
		if(life_timer >= life){
			Destroy(this.gameObject);
		}
		Vector2 dir = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
		dir.Normalize();
		rigidbody2D.velocity = dir * movement_speed;
	}
}
