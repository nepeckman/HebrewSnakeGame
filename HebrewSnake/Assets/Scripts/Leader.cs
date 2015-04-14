using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Leader : MonoBehaviour {

	// used to move the leader in a direction, by moveDistance, with frequency speed
	Vector2 direction; 
	public float moveDistance;
	public float speed;

	// keeps track of the tail objects
	public List<GameObject> tail = new List<GameObject>();

	// moves each letter to the next spot
	private Vector2 positionForNext;
	private Vector2 position;
	private Letter letterscript;
	
	public void Begin (float speed) {
		// starts it in a direction, adds itself to the gameobject list, makes move repeating
		this.speed = speed;
		direction = Vector2.right * moveDistance;
		tail.Insert(0, gameObject);
		InvokeRepeating ("Move", 0.1f * speed, 0.1f * speed);
	}

	void Update () {
		if (Input.GetKey("right") && direction != -Vector2.right * moveDistance){
			direction = Vector2.right * moveDistance;
		}
		else if (Input.GetKey("left") && direction != Vector2.right * moveDistance){
			direction = -Vector2.right * moveDistance;
		}
		else if (Input.GetKey("up") && direction != -Vector2.up * moveDistance){
			direction = Vector2.up * moveDistance;
		}
		else if (Input.GetKey("down") && direction != Vector2.up * moveDistance){
			direction = -Vector2.up * moveDistance;
		}
	}

	void Move(){
		foreach (GameObject letter in tail) {
			positionForNext = letter.transform.position;
			if (!letter.CompareTag("Lead Letter")){
				letter.transform.position = position;
				letterscript = (Letter) letter.GetComponent(typeof(Letter));
				if (!letterscript.isTail){
					letterscript.isTail = true;
				}
			}
			else {
				letter.transform.Translate(direction);
			}
			position = positionForNext;
		}
	}
}
