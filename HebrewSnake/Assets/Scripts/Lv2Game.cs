using UnityEngine;
using System.Collections;

public class Lv2Game : GameImpl {

	// Use this for initialization

	void Start () {
		StartCoroutine(startCoroutine (GameImpl.NORMAL_BOUNDARY, GameImpl.EASY_LIVES, GameImpl.NORMAL_SPEED));
	}
	

}
