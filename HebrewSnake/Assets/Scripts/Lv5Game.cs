using UnityEngine;
using System.Collections;

public class Lv5Game : GameImpl {

	void Start () {
		StartCoroutine(startCoroutine(GameImpl.NORMAL_BOUNDARY, GameImpl.HARD_LIVES, GameImpl.HARD_SPEED));
	}
}
