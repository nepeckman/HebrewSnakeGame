using UnityEngine;
using System.Collections;

public interface Game {

	void SpawnTail();
	void SpawnFood();
	void DestroyTail();
	void DestroyFood();
	void Death();
	void GameOver(bool winner);
	void Retry(int number_of_letters);

}
