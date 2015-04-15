using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameImpl Lv1Game;
	public GameImpl Lv2Game;
	public GameImpl Lv3Game;
	public GameImpl Lv4Game;
	public GameImpl Lv5Game;
	public GameObject Canvas;

	public Walls northwall;
	public Walls southwall;
	public Walls eastwall;
	public Walls westwall;

	public void startLv1Game(){
		Instantiate (Lv1Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		Canvas.SetActive (false);
	}

	public void startLv2Game(){
		Instantiate (Lv2Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		Canvas.SetActive (false);
	}

	public void startLv3Game(){
		Instantiate (Lv3Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		Canvas.SetActive (false);
	}

	public void startLv4Game(){
		Instantiate (Lv4Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		Canvas.SetActive (false);
	}

	public void startLv5Game(){
		Instantiate (Lv5Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		Canvas.SetActive (false);
	}

	public void endGame(GameObject game){
		Destroy (game);
		Canvas.SetActive (true);
	}
}
