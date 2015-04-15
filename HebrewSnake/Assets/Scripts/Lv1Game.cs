using UnityEngine;
using System.Collections;

public class Lv1Game : GameImpl {
	
	void Start () {
		StartCoroutine(startCoroutine (GameImpl.NORMAL_BOUNDARY, GameImpl.EASY_LIVES, GameImpl.EASY_SPEED));
	}

}
