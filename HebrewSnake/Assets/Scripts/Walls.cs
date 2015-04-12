using UnityEngine;
using System.Collections;

public class Walls : MonoBehaviour {

	// gets gamecontroller for gameover method
	public GameController gamecontroller;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Lead Letter")){
			gamecontroller.GameOver(false);
		}
	}
}
