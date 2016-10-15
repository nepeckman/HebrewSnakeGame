using UnityEngine;
using System.Collections;

public interface Game {

	IEnumerator startCoroutine(Boundary boundary, int lives, float speed);
	void SpawnTail();
	void SpawnFood(GameObject newletter);
	void DestroyTail();
	void DestroyFood();
	void ScoreSound();
	void FailSound();
	void Death();
	void startGameOver(bool winner);
	IEnumerator GameOver(bool winner);
	IEnumerator Retry(int number_of_letters, float speed);
	bool checkVictory();
	GameObject WhichLetter(int index);
	GameObject nextLetter();
	GameObject nextLetter(int index);
	void setText(GUIText textbox, string message);
	void setText(string message);
}
