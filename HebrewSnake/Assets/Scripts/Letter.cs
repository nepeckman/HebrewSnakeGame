using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour {
	// bools to identify is letter is part of the tail or food
	public bool isTail;
	public bool isFood;

	// gamecontroller to activate spawntail (for right food) gameover (for tail or wrong food)
	// leader to get the tail count size
	private Game gamecontroller;
	private Leader leader;

	void Start(){
		GameObject controller = GameObject.FindWithTag ("GameController");
		gamecontroller = (Game) controller.GetComponent(typeof(Game));
		GameObject leadletter = GameObject.FindWithTag ("Lead Letter");
		leader = (Leader)leadletter.GetComponent (typeof(Leader));
	}

	void OnTriggerEnter2D(Collider2D other){
		if (isFood && other.gameObject.CompareTag ("Lead Letter") && gameObject.tag == gamecontroller.nextLetter().tag) {
			gamecontroller.DestroyFood();
			gamecontroller.SpawnTail ();

			if (gamecontroller.checkVictory()){
				gamecontroller.GameOver(true);
			} else {
				GameObject newletter = gamecontroller.nextLetter();
				gamecontroller.SpawnFood(newletter);
			}

		} else if (isTail && other.gameObject.CompareTag ("Lead Letter")) {
			gamecontroller.Death();
		} else if (isFood && other.gameObject.CompareTag ("Lead Letter")) {
			// Text is cleared when the right letter is called
			// and it Game controller spawns tail and then food
			gamecontroller.setText("Wrong letter!");
		}
	}
}
