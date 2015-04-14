using UnityEngine;
using System.Collections;

public interface Game {

	void SpawnTail();
	void SpawnFood(GameObject newletter);
	void DestroyTail();
	void DestroyFood();
	void Death();
	void GameOver(bool winner);
	void Retry(int number_of_letters);
	bool checkVictory();
	GameObject WhichLetter(int index);
	GameObject nextLetter();
	GameObject nextLetter(int index);
	void setText(GUIText textbox, string message);
	void setText(string message);
}
