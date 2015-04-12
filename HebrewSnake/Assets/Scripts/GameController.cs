using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Boundary
{
	public float[,] spawnZones = new float[4, 4] {{-7.5f, -1.0f, 1.0f, 3.5f}, {1.0f, 7.5f, 1.0f, 3.5f}, 
		{-7.5f, -1.0f, -4.5f, -1.0f}, {1.0f, 7.5f, -4.5f, -1.0f}};
}

public class GameController : MonoBehaviour {

	// leader is used to deal with the game objects in the tail
	// boundary sets the bounds for food spawning
	// textboxes display the text
	private Leader leader;
	public Boundary boundary;
	public GUIText textbox;
	public GUIText lowerbox;
	public GUIText alertbox;

	// food is kept in a list for easy deletion and management
	public List<GameObject> food = new List<GameObject>();

	// these variables are used in multiple methods and are declared before
	// to avoid problems
	private Letter letterscript;
	private float x;
	private float y;
	private GameObject newletter;
	Object clone;
	GameObject letterclone;

	// counter needed for a while loop in an area that wouldn't accept a for loop
	public int counter;

	// bools used to start, restart, and end the game, int used for lives
	private bool gameover = false;
	private bool gamestart = false;
	private bool needToRestart = false;
	private int lives;
	List<GameObject> checkpoint = new List<GameObject> ();

	public GameObject leadletter;
	public GameObject bet;
	public GameObject vet;
	public GameObject gimel;
	public GameObject dalet;
	public GameObject hay;
	public GameObject vav;
	public GameObject zayin;
	public GameObject chet;
	public GameObject tet;
	public GameObject yod;
	public GameObject caf;
	public GameObject chaf;
	public GameObject lamad;
	public GameObject mem;
	public GameObject nun;
	public GameObject samach;
	public GameObject ayin;
	public GameObject pey;
	public GameObject fey;
	public GameObject tzadik;
	public GameObject koof;
	public GameObject resh;
	public GameObject shin;
	public GameObject sin;
	public GameObject tav;

	// gets a leader and displays start text
	void Start(){
		leadletter = GameObject.FindWithTag ("Lead Letter");
		leader = (Leader) leadletter.GetComponent(typeof(Leader));
		textbox.text = "Press any key to start!";
		lowerbox.text = "";
		alertbox.text = "";
		lives = 3;
	}

	// checks to restart and start the game
	void Update(){
		if (!gamestart && Input.anyKey) {
			leader.Begin ();
			Begin ();
			gamestart = true;
		}
		if (needToRestart && Input.GetKeyDown("return")) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	// begins the game after update detects a start command
	void Begin(){
		SpawnFood ();
	}



	// called by the letter script if the letter is the correct food
	public void SpawnTail(){
		newletter = WhichLetter (leader.tail.Count);
		// newletter is a prefab object, clone is an object, I needed to get it into a game object
		// to manage it correctly (add it to gameobject lists and get its script component)
		clone = Instantiate (newletter, leader.tail.Last ().transform.position, Quaternion.identity);
		letterclone = clone as GameObject;
		leader.tail.Add (letterclone);
		letterscript = (Letter) letterclone.GetComponent(typeof(Letter));
		// needs to start as not Tail so beginning letters (that spawn on the leader)
		// dont end the game, changed to true after movement is applied
		letterscript.isTail = false;
		letterscript.isFood = false;
		int size = food.Count;
		// deletes the food, because I'm deleting I can't use foreach and I need to set 
		// a constant to loop until
		for (int i=0; i<size; i++) {
			GameObject letter = food[size - i - 1];
			food.Remove (letter);
			Destroy(letter.gameObject);
		}
		if (leader.tail.Count > 25) {
			GameOver(true);
		}
		if (!gameover) {
			SpawnFood ();
		}
		if ((float) (leader.tail.Count/5) == (float)(leader.tail.Count)/5.0f){
			SaveCheckpoints();
		}
	}

	// called by the spawn tail method to replace food after SpawnTail deletes it
	void SpawnFood(){
		// Clears the Wrong Letter text when its no longer relevant
		alertbox.text = "";
		// randomOffset picks one of the 4 boudary arrays
		int randomOffset = (int)(Random.Range (0, 3));
		while (counter < 3) {
			if (counter == 0){
				newletter = WhichLetter (leader.tail.Count);
				textbox.text = newletter.tag;
			} else {
				int letterIdx = (int)(Random.Range (1, 25));
				newletter = WhichLetter (letterIdx);
			}
			do {
			x = (int)(Random.Range (boundary.spawnZones[randomOffset, 0], boundary.spawnZones[randomOffset, 1]));
			y = (int)(Random.Range (boundary.spawnZones[randomOffset, 2], boundary.spawnZones[randomOffset, 3]));
			} while (Mathf.Abs(leader.transform.position.x - x) < 1 && Mathf.Abs(leader.transform.position.y - y) < 1);
			clone = Instantiate (newletter, new Vector2(x, y), Quaternion.identity);
			letterclone = clone as GameObject;
			food.Add(letterclone);
			letterscript = (Letter) letterclone.GetComponent(typeof(Letter));
			letterscript.isTail = false;
			letterscript.isFood = true;
			counter++;
			randomOffset = (randomOffset + 1) % 4;
		}
		counter = 0;
	}

	public void SaveCheckpoints(){
		checkpoint = leader.tail;
		Debug.Log (checkpoint[0].transform.position);
	}

	public void GetCheckpoints(){
		int size = checkpoint.Count;
		for (int i = 0; i < size; i++) {
			if (i == 0){
				Debug.Log (checkpoint[0].transform.position);
				Instantiate(leadletter, checkpoint[0].transform.position, Quaternion.identity);
				leader.tail.Add(leadletter);
			} else {
				newletter = WhichLetter (i);
				// newletter is a prefab object, clone is an object, I needed to get it into a game object
				// to manage it correctly (add it to gameobject lists and get its script component)
				clone = Instantiate (newletter, checkpoint[i].transform.position, Quaternion.identity);
				letterclone = clone as GameObject;
				leader.tail.Add (letterclone);
				letterscript = (Letter) letterclone.GetComponent(typeof(Letter));
				// needs to start as not Tail so beginning letters (that spawn on the leader)
				// dont end the game, changed to true after movement is applied
				letterscript.isTail = false;
				letterscript.isFood = false;
			}
		}
		Debug.Log("Hi from getcheckpoints");
	}

	// called by walls and letters, if player hits wall or hits tail
	public void GameOver(bool winner){
		int size = leader.tail.Count;
		for (int i=0; i<size; i++) {
			GameObject letter = leader.tail[size - i - 1];
			food.Remove (letter);
			Destroy(letter.gameObject);

		}
		if (lives > 0) {
			lives--;
			GetCheckpoints();
			return;
		}
		Debug.Log("Hi from Game over");
		if (winner) {
			textbox.text = "Mazel Tov!";
		} else {
			textbox.text = "Oi Vey!";
		}
		lowerbox.text = "Press enter to restart!";
		alertbox.text = "";
		gameover = true;
		needToRestart = true;
	}

	// gets the prefab object corrosponding to an index
	public GameObject WhichLetter(int index){
		GameObject letter;
		if (index == 1) {
			letter = bet;
		} else if (index == 2) {
			letter = vet;
		} else if (index == 3) {
			letter = gimel;
		} else if (index == 4) {
			letter = dalet;
		} else if (index == 5) {
			letter = hay;
		} else if (index == 6) {
			letter = vav;
		} else if (index == 7) {
			letter = zayin;
		} else if (index == 8) {
			letter = chet;
		} else if (index == 9) {
			letter = tet;
		} else if (index == 10) {
			letter = yod;
		} else if (index == 11) {
			letter = caf;
		} else if (index == 12) {
			letter = chaf;
		} else if (index == 13) {
			letter = lamad;
		} else if (index == 14) {
			letter = mem;
		} else if (index == 15) {
			letter = nun;
		} else if (index == 16) {
			letter = samach;
		} else if (index == 17) {
			letter = ayin;
		} else if (index == 18) {
			letter = pey;
		} else if (index == 19) {
			letter = fey;
		} else if (index == 20) {
			letter = tzadik;
		} else if (index == 21) {
			letter = koof;
		} else if (index == 22) {
			letter = resh;
		} else if (index == 23) {
			letter = shin;
		} else if (index == 24) {
			letter = sin;
		} else if (index == 25) {
			letter = tav;
		} else {
			letter = null;
		}
		
		return letter;
	}

}
