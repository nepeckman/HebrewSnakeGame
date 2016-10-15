using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameImpl Lv1Game;
	public GameImpl Lv2Game;
	public GameImpl Lv3Game;
	public GameImpl Lv4Game;
	public GameImpl Lv5Game;

	public GameObject PlayCanvas;
	public GameObject StartCanvas;
	public GameObject InstructionCanvas;
	public GameObject CreditCanvas;

	public GUIText textbox;
	public GUIText alertbox;
	public GUIText lowerbox;
	public GUIText livesbox;

	public Walls northwall;
	public Walls southwall;
	public Walls eastwall;
	public Walls westwall;

	public AudioSource IntroMusic;

	public void Start(){
		clearGUItext ();
		IntroMusic.Play ();
	}

	public void clearGUItext(){
		textbox.text = "";
		alertbox.text = "";
		lowerbox.text = "";
		livesbox.text = "";
	}

	public void playCanvas(bool set){
		PlayCanvas.SetActive (set);
	}

	public void instructionsCanvas(bool set){
		InstructionCanvas.SetActive (set);
	}

	public void startCanvas(bool set){
		StartCanvas.SetActive (set);
	}

	public void creditCanvas(bool set){
		CreditCanvas.SetActive (set);
	}

	public void startLv1Game(){
		IntroMusic.Stop ();
		Instantiate (Lv1Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		PlayCanvas.SetActive (false);
	}

	public void startLv2Game(){
		IntroMusic.Stop ();
		Instantiate (Lv2Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		PlayCanvas.SetActive (false);
	}

	public void startLv3Game(){
		IntroMusic.Stop ();
		Instantiate (Lv3Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		PlayCanvas.SetActive (false);
	}

	public void startLv4Game(){
		IntroMusic.Stop ();
		Instantiate (Lv4Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		PlayCanvas.SetActive (false);
	}

	public void startLv5Game(){
		IntroMusic.Stop ();
		Instantiate (Lv5Game, new Vector2 (0.0f, 0.0f), Quaternion.identity);
		northwall.Begin ();
		southwall.Begin ();
		eastwall.Begin ();
		westwall.Begin ();
		PlayCanvas.SetActive (false);
	}



	public void endGame(GameObject game){
		Destroy (game);
		clearGUItext ();
		startCanvas(true);
		IntroMusic.Play();
	}
}
