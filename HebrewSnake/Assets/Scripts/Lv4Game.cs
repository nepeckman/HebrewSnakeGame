using UnityEngine;
using System.Collections;

public class Lv4Game : GameImpl {

	void Start () {
		StartCoroutine(startCoroutine(GameImpl.NORMAL_BOUNDARY, GameImpl.NORMAL_LIVES, GameImpl.HARD_SPEED));
	}
}
