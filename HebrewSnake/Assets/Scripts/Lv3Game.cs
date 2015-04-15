using UnityEngine;
using System.Collections;

public class Lv3Game : GameImpl{

	void Start () {
		StartCoroutine(startCoroutine(GameImpl.NORMAL_BOUNDARY, GameImpl.NORMAL_LIVES, GameImpl.NORMAL_SPEED));
	}

}
