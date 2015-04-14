using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class GameImpl : MonoBehaviour, Game {

	// leader is used to deal with the game objects in the tail
	// boundary sets the bounds for food spawning
	// textboxes display the text
	private Leader leader;
	public Boundary boundary;
	public GUIText textbox;
	public GUIText lowerbox;
	public GUIText alertbox;
	public GUIText livesbox;

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

	private int lives;
	
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

	// Use this for initialization
	void Start () {
		clone = Instantiate (leadletter, new Vector2(0.0f, 0.0f), Quaternion.identity);
		letterclone = clone as GameObject;
		leader = (Leader) letterclone.GetComponent(typeof(Leader));
		textbox.text = "Press any key to start!";
		lowerbox.text = "";
		alertbox.text = "";
		livesbox.text = "Lives: " + lives;
		boundary = new Boundary (new SpawnZone (-7.5f, -1.0f, 1.0f, 3.5f), new SpawnZone (1.0f, 7.5f, 1.0f, 3.5f), new SpawnZone (-7.5f, -1.0f, -4.5f, -1.0f), new SpawnZone (1.0f, 7.5f, -4.5f, -1.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnTail(){
		newletter = nextLetter ();
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
	}

	public void SpawnFood(GameObject newletter){
		// Clears the Wrong Letter text when its no longer relevant
		
		//TODO better method of handing GUI texts
		alertbox.text = "";
		textbox.text = newletter.tag;
		// randomOffset picks one of the 4 boudary arrays
		int randomOffset = (int)(Random.Range (0, 3));
		while (counter < 3) {
			if (counter != 0){
				int letterIdx = (int)(Random.Range (1, 25));
				newletter = nextLetter (letterIdx);
			}
			Vector2 spawnpoint;
			do {
				spawnpoint = boundary.getSpawnPoint((BoundaryEnum) randomOffset);
			} while (Mathf.Abs(leader.transform.position.x - spawnpoint.x) < 1 && Mathf.Abs(leader.transform.position.y - spawnpoint.y) < 1);
			clone = Instantiate (newletter, spawnpoint, Quaternion.identity);
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

	public 	void DestroyTail(){
		int size = leader.tail.Count;
		for (int i=0; i<size; i++) {
			GameObject letter = leader.tail[size - i - 1];
			leader.tail.Remove(letter);
			Destroy(letter.gameObject);
		}
	}

	public 	void DestroyFood(){
		int size = food.Count;
		for (int i=0; i<size; i++) {
			GameObject letter = food[size - i - 1];
			food.Remove (letter);
			Destroy(letter.gameObject);
		}
	}

	public void Death(){
		if (lives > 0) {
			lives--;
			Retry (leader.tail.Count);
		} else {
			GameOver(false);
		}
	}

	public void GameOver(bool winner){
		DestroyFood ();
		DestroyTail ();
		if (winner) {
			textbox.text = "Mazel Tov!";
		} else {
			textbox.text = "Oi Vey!";
		}
		lowerbox.text = "Press enter to restart!";
		alertbox.text = "";
	}

	public void Retry(int number_of_letters){
		for (int idx = 0; idx < number_of_letters; idx++) {
			if (idx == 0){
				clone = Instantiate (leadletter, new Vector2(0.0f, 0.0f), Quaternion.identity);
				letterclone = clone as GameObject;
				leader.tail.Add (letterclone);
				leader = (Leader) letterclone.GetComponent(typeof(Leader));
			} else {
				newletter = nextLetter(idx);
				// newletter is a prefab object, clone is an object, I needed to get it into a game object
				// to manage it correctly (add it to gameobject lists and get its script component)
				float x_placement = (float) idx;
				clone = Instantiate (newletter, new Vector2(-x_placement/2, 0.0f), Quaternion.identity);
				letterclone = clone as GameObject;
				leader.tail.Add (letterclone);
				letterscript = (Letter) letterclone.GetComponent(typeof(Letter));
				// needs to start as not Tail so beginning letters (that spawn on the leader)
				// dont end the game, changed to true after movement is applied
				letterscript.isTail = false;
				letterscript.isFood = false;
			}
			alertbox.text = "";
			textbox.text = "Press any key to retry";
			livesbox.text = "Lives: " + lives;
		}
	}

	public bool checkVictory(){
		if (leader.tail.Count > 25) {
			return true;
		} else {
			return false;
		}
	}

	public void setText(GUIText GUI_textbox, string message){
		GUI_textbox.text = message;
	}

	public void setText(string message){
		this.alertbox.text = message;
	}

	public GameObject nextLetter(){
		return WhichLetter (leader.tail.Count);
	}

	public GameObject nextLetter(int index){
		return WhichLetter (index);
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
